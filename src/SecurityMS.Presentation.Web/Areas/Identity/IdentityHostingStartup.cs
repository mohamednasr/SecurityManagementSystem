using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SecurityMS.Presentation.Web.Areas.Identity.IdentityHostingStartup))]
namespace SecurityMS.Presentation.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}