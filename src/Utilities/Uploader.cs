using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
namespace Utilities
{
    public class Uploader
    {
        public Dictionary<string,string> uploadFile(HttpContext context, string storingPath)
        {
            try
            {
                var newFileName = string.Empty;
                var files = context.Request.Form.Files;

                if (files != null)
                {
                    var fileName = string.Empty;
                    string PathDB = string.Empty;
                    Dictionary<string, string> uploadedFiles = new Dictionary<string, string>();
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            //Getting FileName
                            fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                            checkFolder(".\\wwwroot" + storingPath);
                            //Assigning Unique Filename (Guid)
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                            //Getting file Extension
                            var FileExtension = Path.GetExtension(fileName);

                            // concating  FileName + FileExtension
                            newFileName = myUniqueFileName + FileExtension;

                            // Combines two strings into a path.
                            fileName = Path.Combine(".\\wwwroot" + storingPath + newFileName);

                            PathDB = Path.Combine(storingPath + newFileName);
                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                                uploadedFiles.Add(file.Name, PathDB);
                            }
                        }
                    }

                    return uploadedFiles;
                }
                throw new Exception("حدث خطأ اثناء الرفع");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteFile(string filePath)
        {
            try
            {
                filePath = Path.Combine(".\\wwwroot" + filePath);
                if (checkFile(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void checkFolder(string subPath)
        {
            bool exists = System.IO.Directory.Exists(subPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(subPath);
        }

        private bool checkFile(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
    }
}
