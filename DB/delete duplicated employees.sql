
 delete from [securityMS].[dbo].SiteEmployeesAssign
  WHERE EmployeeId NOT IN
    (
        SELECT MIN(ID) AS MinRecordID
        FROM [securityMS].[dbo].[Employees]
        GROUP BY [Name], 
                 [EmployeeCode], 
                 [NationalId]
    );

	 delete from [securityMS].[dbo].SiteEmployeesAttendance
  WHERE EmployeeId NOT IN
    (
        SELECT MIN(ID) AS MinRecordID
        FROM [securityMS].[dbo].[Employees]
        GROUP BY [Name], 
                 [EmployeeCode], 
                 [NationalId]
    );

  delete from [securityMS].[dbo].[Employees]
    WHERE ID NOT IN
    (
        SELECT MIN(ID) AS MinRecordID
        FROM [securityMS].[dbo].[Employees]
        GROUP BY [Name], 
                 [EmployeeCode], 
                 [NationalId]
    );