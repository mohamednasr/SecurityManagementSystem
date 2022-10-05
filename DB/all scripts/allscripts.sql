IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BlackListEntity] (
    [Id] bigint NOT NULL IDENTITY,
    [Ser] bigint NOT NULL,
    [Name] nvarchar(max) NULL,
    [Company] nvarchar(max) NULL,
    [Job] nvarchar(max) NULL,
    [Nat_Id] nvarchar(max) NULL,
    [Reason] nvarchar(max) NULL,
    CONSTRAINT [PK_BlackListEntity] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210219195316_init', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CustomerTypeLookup] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_CustomerTypeLookup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Government] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Government] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CustomerTypeId] bigint NOT NULL,
    [ParentCustomerId] bigint NULL,
    [CommercialNumber] nvarchar(max) NULL,
    [TaxId] nvarchar(max) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_CustomerTypeLookup_CustomerTypeId] FOREIGN KEY ([CustomerTypeId]) REFERENCES [CustomerTypeLookup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customers_Customers_ParentCustomerId] FOREIGN KEY ([ParentCustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Zones] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [GovernmentId] bigint NOT NULL,
    CONSTRAINT [PK_Zones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Zones_Government_GovernmentId] FOREIGN KEY ([GovernmentId]) REFERENCES [Government] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Contracts] (
    [Id] bigint NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CustomerId] bigint NOT NULL,
    CONSTRAINT [PK_Contracts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contracts_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerContacts] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Job] nvarchar(max) NULL,
    [CustomerId] bigint NOT NULL,
    [Email] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    CONSTRAINT [PK_CustomerContacts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerContacts_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Sites] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [ZoneId] bigint NOT NULL,
    [ContractsId] bigint NULL,
    CONSTRAINT [PK_Sites] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sites_Contracts_ContractsId] FOREIGN KEY ([ContractsId]) REFERENCES [Contracts] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Sites_Zones_ZoneId] FOREIGN KEY ([ZoneId]) REFERENCES [Zones] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Contracts_CustomerId] ON [Contracts] ([CustomerId]);
GO

CREATE INDEX [IX_CustomerContacts_CustomerId] ON [CustomerContacts] ([CustomerId]);
GO

CREATE INDEX [IX_Customers_CustomerTypeId] ON [Customers] ([CustomerTypeId]);
GO

CREATE INDEX [IX_Customers_ParentCustomerId] ON [Customers] ([ParentCustomerId]);
GO

CREATE INDEX [IX_Sites_ContractsId] ON [Sites] ([ContractsId]);
GO

CREATE INDEX [IX_Sites_ZoneId] ON [Sites] ([ZoneId]);
GO

CREATE INDEX [IX_Zones_GovernmentId] ON [Zones] ([GovernmentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312131727_contract', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Departments] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ShiftTypesLookup] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_ShiftTypesLookup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Jobs] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [DepartmentId] bigint NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Jobs_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [EmployeeCode] nvarchar(max) NULL,
    [NationalId] nvarchar(14) NULL,
    [Phone] nvarchar(max) NOT NULL,
    [Phone2] nvarchar(max) NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NULL,
    [InsuranceNumber] nvarchar(max) NULL,
    [JobId] bigint NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SiteEmployees] (
    [Id] bigint NOT NULL IDENTITY,
    [SiteId] bigint NOT NULL,
    [JobId] bigint NOT NULL,
    [ShiftTypeId] bigint NOT NULL,
    [ShiftValue] decimal(18,2) NOT NULL,
    [EmployeesPerShift] int NOT NULL,
    CONSTRAINT [PK_SiteEmployees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SiteEmployees_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SiteEmployees_ShiftTypesLookup_ShiftTypeId] FOREIGN KEY ([ShiftTypeId]) REFERENCES [ShiftTypesLookup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SiteEmployees_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Employees_JobId] ON [Employees] ([JobId]);
GO

CREATE INDEX [IX_Jobs_DepartmentId] ON [Jobs] ([DepartmentId]);
GO

CREATE INDEX [IX_SiteEmployees_JobId] ON [SiteEmployees] ([JobId]);
GO

CREATE INDEX [IX_SiteEmployees_ShiftTypeId] ON [SiteEmployees] ([ShiftTypeId]);
GO

CREATE INDEX [IX_SiteEmployees_SiteId] ON [SiteEmployees] ([SiteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312140201_siteEmployees', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [EquipmentTypesLookup] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_EquipmentTypesLookup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SiteEquipments] (
    [Id] bigint NOT NULL IDENTITY,
    [SiteId] bigint NOT NULL,
    [EquipmentTypeId] bigint NOT NULL,
    [EquipmentValue] decimal(18,2) NOT NULL,
    [EquipmentCount] int NOT NULL,
    CONSTRAINT [PK_SiteEquipments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SiteEquipments_EquipmentTypesLookup_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypesLookup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SiteEquipments_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SiteEquipments_EquipmentTypeId] ON [SiteEquipments] ([EquipmentTypeId]);
GO

CREATE INDEX [IX_SiteEquipments_SiteId] ON [SiteEquipments] ([SiteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312141616_siteEquipment', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SiteEquipments] DROP CONSTRAINT [FK_SiteEquipments_EquipmentTypesLookup_EquipmentTypeId];
GO

DROP INDEX [IX_SiteEquipments_EquipmentTypeId] ON [SiteEquipments];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEquipments]') AND [c].[name] = N'EquipmentTypeId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SiteEquipments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SiteEquipments] DROP COLUMN [EquipmentTypeId];
GO

ALTER TABLE [SiteEquipments] ADD [EquipmentId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE TABLE [CountriesLookup] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_CountriesLookup] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SiteEmployeesAssign] (
    [Id] bigint NOT NULL IDENTITY,
    [SiteEmployeeId] bigint NOT NULL,
    [EmployeeId] bigint NOT NULL,
    [EmployeeShiftSalary] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_SiteEmployeesAssign] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SiteEmployeesAssign_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_SiteEmployeesAssign_SiteEmployees_SiteEmployeeId] FOREIGN KEY ([SiteEmployeeId]) REFERENCES [SiteEmployees] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Equipments] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [ManufactureId] bigint NOT NULL,
    [EquipmentTypeId] bigint NOT NULL,
    [EquipmentPrice] decimal(18,2) NOT NULL,
    [EquipmentTotalCount] bigint NOT NULL,
    CONSTRAINT [PK_Equipments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Equipments_EquipmentTypesLookup_EquipmentTypeId] FOREIGN KEY ([EquipmentTypeId]) REFERENCES [EquipmentTypesLookup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Equipments_CountriesLookup_ManufactureId] FOREIGN KEY ([ManufactureId]) REFERENCES [CountriesLookup] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SiteEquipments_EquipmentId] ON [SiteEquipments] ([EquipmentId]);
GO

CREATE INDEX [IX_Equipments_EquipmentTypeId] ON [Equipments] ([EquipmentTypeId]);
GO

CREATE INDEX [IX_Equipments_ManufactureId] ON [Equipments] ([ManufactureId]);
GO

CREATE INDEX [IX_SiteEmployeesAssign_EmployeeId] ON [SiteEmployeesAssign] ([EmployeeId]);
GO

CREATE INDEX [IX_SiteEmployeesAssign_SiteEmployeeId] ON [SiteEmployeesAssign] ([SiteEmployeeId]);
GO

ALTER TABLE [SiteEquipments] ADD CONSTRAINT [FK_SiteEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312150319_siteEmployeeAssign', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SiteEquipments] DROP CONSTRAINT [FK_SiteEquipments_Equipments_EquipmentId];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Equipments]') AND [c].[name] = N'EquipmentPrice');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Equipments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Equipments] DROP COLUMN [EquipmentPrice];
GO

ALTER TABLE [SiteEmployeesAssign] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [EquipmentDetails] (
    [Id] bigint NOT NULL IDENTITY,
    [EquipmentId] bigint NOT NULL,
    [Serial] nvarchar(max) NULL,
    [ProductionDate] datetime2 NOT NULL,
    [EquipmentPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_EquipmentDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EquipmentDetails_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([Id])
);
GO

CREATE TABLE [SiteEquipmentsAssign] (
    [Id] bigint NOT NULL IDENTITY,
    [SiteEquipmenteId] bigint NOT NULL,
    [EquipmentId] bigint NOT NULL,
    [EquipmentValue] decimal(18,2) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_SiteEquipmentsAssign] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SiteEquipmentsAssign_EquipmentDetails_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [EquipmentDetails] ([Id]),
    CONSTRAINT [FK_SiteEquipmentsAssign_SiteEquipments_SiteEquipmenteId] FOREIGN KEY ([SiteEquipmenteId]) REFERENCES [SiteEquipments] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_EquipmentDetails_EquipmentId] ON [EquipmentDetails] ([EquipmentId]);
GO

CREATE INDEX [IX_SiteEquipmentsAssign_EquipmentId] ON [SiteEquipmentsAssign] ([EquipmentId]);
GO

CREATE INDEX [IX_SiteEquipmentsAssign_SiteEquipmenteId] ON [SiteEquipmentsAssign] ([SiteEquipmenteId]);
GO

ALTER TABLE [SiteEquipments] ADD CONSTRAINT [FK_SiteEquipments_Equipments_EquipmentId] FOREIGN KEY ([EquipmentId]) REFERENCES [Equipments] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312153419_equipment', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Contracts] ADD [ContractContactPersonId] bigint NULL;
GO

CREATE INDEX [IX_Contracts_ContractContactPersonId] ON [Contracts] ([ContractContactPersonId]);
GO

ALTER TABLE [Contracts] ADD CONSTRAINT [FK_Contracts_CustomerContacts_ContractContactPersonId] FOREIGN KEY ([ContractContactPersonId]) REFERENCES [CustomerContacts] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312154134_updateContractContacts', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEquipmentsAssign]') AND [c].[name] = N'EquipmentValue');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SiteEquipmentsAssign] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [SiteEquipmentsAssign] DROP COLUMN [EquipmentValue];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312154322_removeEquipmentValueFromSite', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] ADD [Address] nvarchar(max) NULL;
GO

ALTER TABLE [Customers] ADD [ZoneId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

CREATE INDEX [IX_Customers_ZoneId] ON [Customers] ([ZoneId]);
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Zones_ZoneId] FOREIGN KEY ([ZoneId]) REFERENCES [Zones] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210326133256_customerAddress', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'ZoneId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Customers] ALTER COLUMN [ZoneId] bigint NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BlackListEntity]') AND [c].[name] = N'Ser');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [BlackListEntity] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [BlackListEntity] ALTER COLUMN [Ser] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210326185606_makeZoneOptional', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210326213002_fixParentCustomerRelation', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Jobs]') AND [c].[name] = N'Name');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Jobs] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Jobs] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Departments]') AND [c].[name] = N'Name');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Departments] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Departments] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210326231851_requiredEntities', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SiteEmployeeAttendanceEntities] (
    [Id] bigint NOT NULL IDENTITY,
    [EmployeeId] bigint NOT NULL,
    [SiteId] bigint NOT NULL,
    [AttendanceDate] datetime2 NOT NULL,
    [ShiftId] bigint NOT NULL,
    CONSTRAINT [PK_SiteEmployeeAttendanceEntities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SiteEmployeeAttendanceEntities_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SiteEmployeeAttendanceEntities_ShiftTypesLookup_ShiftId] FOREIGN KEY ([ShiftId]) REFERENCES [ShiftTypesLookup] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SiteEmployeeAttendanceEntities_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SiteEmployeeAttendanceEntities_EmployeeId] ON [SiteEmployeeAttendanceEntities] ([EmployeeId]);
GO

CREATE INDEX [IX_SiteEmployeeAttendanceEntities_ShiftId] ON [SiteEmployeeAttendanceEntities] ([ShiftId]);
GO

CREATE INDEX [IX_SiteEmployeeAttendanceEntities_SiteId] ON [SiteEmployeeAttendanceEntities] ([SiteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210329165003_attendance', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SiteEmployeeAttendanceEntities] DROP CONSTRAINT [FK_SiteEmployeeAttendanceEntities_Employees_EmployeeId];
GO

ALTER TABLE [SiteEmployeeAttendanceEntities] DROP CONSTRAINT [FK_SiteEmployeeAttendanceEntities_ShiftTypesLookup_ShiftId];
GO

ALTER TABLE [SiteEmployeeAttendanceEntities] DROP CONSTRAINT [FK_SiteEmployeeAttendanceEntities_Sites_SiteId];
GO

ALTER TABLE [SiteEmployeeAttendanceEntities] DROP CONSTRAINT [PK_SiteEmployeeAttendanceEntities];
GO

EXEC sp_rename N'[SiteEmployeeAttendanceEntities]', N'SiteEmployeesAttendance';
GO

EXEC sp_rename N'[SiteEmployeesAttendance].[IX_SiteEmployeeAttendanceEntities_SiteId]', N'IX_SiteEmployeesAttendance_SiteId', N'INDEX';
GO

EXEC sp_rename N'[SiteEmployeesAttendance].[IX_SiteEmployeeAttendanceEntities_ShiftId]', N'IX_SiteEmployeesAttendance_ShiftId', N'INDEX';
GO

EXEC sp_rename N'[SiteEmployeesAttendance].[IX_SiteEmployeeAttendanceEntities_EmployeeId]', N'IX_SiteEmployeesAttendance_EmployeeId', N'INDEX';
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [AttendanceStatusId] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [SiteEmployeesAttendance] ADD CONSTRAINT [PK_SiteEmployeesAttendance] PRIMARY KEY ([Id]);
GO

CREATE TABLE [AttendanceStatusLookup] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_AttendanceStatusLookup] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_SiteEmployeesAttendance_AttendanceStatusId] ON [SiteEmployeesAttendance] ([AttendanceStatusId]);
GO

ALTER TABLE [SiteEmployeesAttendance] ADD CONSTRAINT [FK_SiteEmployeesAttendance_AttendanceStatusLookup_AttendanceStatusId] FOREIGN KEY ([AttendanceStatusId]) REFERENCES [AttendanceStatusLookup] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD CONSTRAINT [FK_SiteEmployeesAttendance_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD CONSTRAINT [FK_SiteEmployeesAttendance_ShiftTypesLookup_ShiftId] FOREIGN KEY ([ShiftId]) REFERENCES [ShiftTypesLookup] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD CONSTRAINT [FK_SiteEmployeesAttendance_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210329173925_attendanceStatus', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Employees] ADD [FileNumber] bigint NULL;
GO

ALTER TABLE [Employees] ADD [IsIncludeBirthCertificate] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludeCriminalCertificate] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludeEducationCertificate] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludeIDCopy] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludeMilitaryCertificate] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludePersonalPhotos] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [IsIncludeWorkStub] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210416221036_employeeFields', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'FileNumber');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Employees] ALTER COLUMN [FileNumber] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210416222412_editEmployeeFields', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Contracts] DROP CONSTRAINT [FK_Contracts_CustomerContacts_ContractContactPersonId];
GO

DROP INDEX [IX_Contracts_ContractContactPersonId] ON [Contracts];
GO

ALTER TABLE [CustomerContacts] ADD [ContractsEntityId] bigint NULL;
GO

CREATE INDEX [IX_CustomerContacts_ContractsEntityId] ON [CustomerContacts] ([ContractsEntityId]);
GO

ALTER TABLE [CustomerContacts] ADD CONSTRAINT [FK_CustomerContacts_Contracts_ContractsEntityId] FOREIGN KEY ([ContractsEntityId]) REFERENCES [Contracts] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210604184341_fixContacts', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'IsIncludePersonalPhotos');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Employees] ALTER COLUMN [IsIncludePersonalPhotos] bit NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'IsIncludeMilitaryCertificate');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Employees] ALTER COLUMN [IsIncludeMilitaryCertificate] bit NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'IsIncludeEducationCertificate');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Employees] ALTER COLUMN [IsIncludeEducationCertificate] bit NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'IsIncludeBirthCertificate');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Employees] ALTER COLUMN [IsIncludeBirthCertificate] bit NULL;
GO

ALTER TABLE [Employees] ADD [BirthCertificateCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [CriminalCertificateSoftCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [EducationCertificateSoftCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [IDSoftCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [MilitaryCertificateCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [PersonalPhotoSoftCopy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [WorkStubSoftCopy] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210622183009_updateEmployeeFields', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEmployeesAssign]') AND [c].[name] = N'EmployeeShiftSalary');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [SiteEmployeesAssign] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [SiteEmployeesAssign] DROP COLUMN [EmployeeShiftSalary];
GO

ALTER TABLE [SiteEmployees] ADD [EmployeeShiftSalary] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210807130331_update-site-employee-salary', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Invoices] (
    [Id] bigint NOT NULL IDENTITY,
    [CompanyId] bigint NOT NULL,
    [CompanyName] nvarchar(max) NULL,
    [SitesCount] int NOT NULL,
    [FinalIncome] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [InvoicesDetails] (
    [Id] bigint NOT NULL IDENTITY,
    [Count] int NOT NULL,
    [Price] float NOT NULL,
    [Total] float NOT NULL,
    [ItemName] nvarchar(max) NULL,
    [InvoiceId] bigint NOT NULL,
    CONSTRAINT [PK_InvoicesDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InvoicesDetails_Invoices_InvoiceId] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoices] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_InvoicesDetails_InvoiceId] ON [InvoicesDetails] ([InvoiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210910191351_invoices', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Invoices]') AND [c].[name] = N'SitesCount');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Invoices] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Invoices] DROP COLUMN [SitesCount];
GO

ALTER TABLE [Invoices] ADD [InvoiceDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210910192842_invoiceDate', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InvoicesDetails]') AND [c].[name] = N'Total');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [InvoicesDetails] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [InvoicesDetails] ALTER COLUMN [Total] decimal(18,2) NOT NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InvoicesDetails]') AND [c].[name] = N'Price');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [InvoicesDetails] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [InvoicesDetails] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210911140430_dummy', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Employees] ADD [InsuranceAmount] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [InsuranceEndDate] datetime2 NULL;
GO

ALTER TABLE [Employees] ADD [InsurancePercentage] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [InsuranceStartDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210926191311_addInsuranceDetails', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AdvancedPayments] (
    [Id] bigint NOT NULL IDENTITY,
    [EmployeeId] bigint NOT NULL,
    [Amount] float NOT NULL,
    [installments] int NOT NULL,
    [IsAcceptable] bit NOT NULL,
    [PayedAt] datetime2 NOT NULL,
    [PayedBy] nvarchar(max) NULL,
    [CreatedBy] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [AcceptedBy] nvarchar(max) NULL,
    [AcceptedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_AdvancedPayments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdvancedPayments_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);
GO

CREATE TABLE [Penalties] (
    [Id] bigint NOT NULL IDENTITY,
    [EmployeeId] bigint NOT NULL,
    [PenaltyType] int NOT NULL,
    [Amount] float NOT NULL,
    [Reason] bigint NOT NULL,
    CONSTRAINT [PK_Penalties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Penalties_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);
GO

CREATE TABLE [Rewards] (
    [Id] bigint NOT NULL IDENTITY,
    [EmployeeId] bigint NOT NULL,
    [RewardType] int NOT NULL,
    [Amount] float NOT NULL,
    [Reason] bigint NOT NULL,
    CONSTRAINT [PK_Rewards] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rewards_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id])
);
GO

CREATE INDEX [IX_AdvancedPayments_EmployeeId] ON [AdvancedPayments] ([EmployeeId]);
GO

CREATE INDEX [IX_Penalties_EmployeeId] ON [Penalties] ([EmployeeId]);
GO

CREATE INDEX [IX_Rewards_EmployeeId] ON [Rewards] ([EmployeeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210927050808_RewardsPenaltiesAdvancedPayments', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Sites] ADD [Transportations] float NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210927051108_SiteTranspostations', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] ADD [TaxFileNumber] nvarchar(max) NULL;
GO

ALTER TABLE [Contracts] ADD [CommercialProfits] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [Contracts] ADD [ContractPDF] nvarchar(max) NULL;
GO

ALTER TABLE [Contracts] ADD [TaxPercentage] float NOT NULL DEFAULT 0.0E0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210927052852_contractCustomersUpdates', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rewards]') AND [c].[name] = N'Reason');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Rewards] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Rewards] ALTER COLUMN [Reason] nvarchar(max) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Penalties]') AND [c].[name] = N'Reason');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Penalties] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Penalties] ALTER COLUMN [Reason] nvarchar(max) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AdvancedPayments]') AND [c].[name] = N'PayedAt');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AdvancedPayments] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [AdvancedPayments] ALTER COLUMN [PayedAt] datetime2 NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AdvancedPayments]') AND [c].[name] = N'CreatedAt');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AdvancedPayments] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [AdvancedPayments] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AdvancedPayments]') AND [c].[name] = N'AcceptedAt');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [AdvancedPayments] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [AdvancedPayments] ALTER COLUMN [AcceptedAt] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210927153738_updateFields', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Employees] ADD [EndServiceReasonId] int NULL;
GO

ALTER TABLE [Employees] ADD [Notes] nvarchar(max) NULL;
GO

CREATE TABLE [EndServiceReasonLookup] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_EndServiceReasonLookup] PRIMARY KEY ([Id])
);
GO

INSERT into [dbo].[EndServiceReasonLookup] ([Name]) Values (N'إستقاله'), (N'إنهاء تعاقد'), (N'إقاله')
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211011183823_EndService', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Employees] ADD [IsActive] bit NOT NULL DEFAULT CAST(1 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211012134853_isActiveEmployee', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Employees] ADD [InsurancePrintCopy] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211024221057_insurancePrint', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Equipments]') AND [c].[name] = N'Description');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Equipments] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Equipments] DROP COLUMN [Description];
GO

ALTER TABLE [Equipments] ADD [AvailableTotalCount] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

ALTER TABLE [Equipments] ADD [Code] nvarchar(max) NULL;
GO

ALTER TABLE [Equipments] ADD [MinimumAlert] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [EquipmentDetails] ADD [ServiceOutDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [EquipmentDetails] ADD [StatusId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Items] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [TotalCount] int NOT NULL,
    [AvailableTotalCount] int NOT NULL,
    [MinimumAlert] int NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Uniform] (
    [Id] bigint NOT NULL IDENTITY,
    [Code] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Size] int NOT NULL,
    [TotalCount] int NOT NULL,
    [AvailableTotalCount] int NOT NULL,
    [MinimumAlert] int NOT NULL,
    CONSTRAINT [PK_Uniform] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ItemDetail] (
    [Id] bigint NOT NULL IDENTITY,
    [ItemId] bigint NOT NULL,
    [AssignedTo] bigint NULL,
    [AssignedToType] int NULL,
    [StatusId] int NOT NULL,
    [AddedDate] datetime2 NOT NULL,
    [OutDate] datetime2 NOT NULL,
    CONSTRAINT [PK_ItemDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemDetail_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id])
);
GO

CREATE TABLE [UniformDetails] (
    [Id] bigint NOT NULL IDENTITY,
    [UniformId] bigint NOT NULL,
    [AssignedTo] bigint NULL,
    [StatusId] int NOT NULL,
    [AddedDate] datetime2 NOT NULL,
    [OutDate] datetime2 NOT NULL,
    CONSTRAINT [PK_UniformDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UniformDetails_Employees_AssignedTo] FOREIGN KEY ([AssignedTo]) REFERENCES [Employees] ([Id]),
    CONSTRAINT [FK_UniformDetails_Uniform_UniformId] FOREIGN KEY ([UniformId]) REFERENCES [Uniform] ([Id])
);
GO

CREATE INDEX [IX_ItemDetail_ItemId] ON [ItemDetail] ([ItemId]);
GO

CREATE INDEX [IX_UniformDetails_AssignedTo] ON [UniformDetails] ([AssignedTo]);
GO

CREATE INDEX [IX_UniformDetails_UniformId] ON [UniformDetails] ([UniformId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211106114615_ItemsAndUniforms', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customers] DROP CONSTRAINT [FK_Customers_Zones_ZoneId];
GO

DROP INDEX [IX_Customers_ZoneId] ON [Customers];
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'ZoneId');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Customers] DROP COLUMN [ZoneId];
GO

ALTER TABLE [Customers] ADD [GovernmentId] bigint NULL;
GO

CREATE INDEX [IX_Customers_GovernmentId] ON [Customers] ([GovernmentId]);
GO

ALTER TABLE [Customers] ADD CONSTRAINT [FK_Customers_Government_GovernmentId] FOREIGN KEY ([GovernmentId]) REFERENCES [Government] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220902170317_updateCustomersEntity', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerContacts] DROP CONSTRAINT [FK_CustomerContacts_Contracts_ContractsEntityId];
GO

DROP INDEX [IX_CustomerContacts_ContractsEntityId] ON [CustomerContacts];
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerContacts]') AND [c].[name] = N'ContractsEntityId');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [CustomerContacts] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [CustomerContacts] DROP COLUMN [ContractsEntityId];
GO

CREATE INDEX [IX_Contracts_ContractContactPersonId] ON [Contracts] ([ContractContactPersonId]);
GO

ALTER TABLE [Contracts] ADD CONSTRAINT [FK_Contracts_CustomerContacts_ContractContactPersonId] FOREIGN KEY ([ContractContactPersonId]) REFERENCES [CustomerContacts] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220904214504_updateCustomerContact', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Zones] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Zones] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Zones] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Zones] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Zones] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [UniformDetails] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [UniformDetails] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [UniformDetails] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [UniformDetails] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [UniformDetails] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Uniform] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Uniform] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Uniform] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Uniform] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Uniform] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Sites] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Sites] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Sites] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Sites] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Sites] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEquipmentsAssign] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SiteEquipmentsAssign] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEquipmentsAssign] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SiteEquipmentsAssign] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [SiteEquipmentsAssign] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEquipments] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SiteEquipments] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEquipments] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SiteEquipments] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [SiteEquipments] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployeesAssign] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SiteEmployeesAssign] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployeesAssign] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SiteEmployeesAssign] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [SiteEmployeesAssign] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployees] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [SiteEmployees] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [SiteEmployees] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [SiteEmployees] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [SiteEmployees] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ShiftTypesLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ShiftTypesLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ShiftTypesLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ShiftTypesLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [ShiftTypesLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Rewards] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Rewards] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Rewards] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Rewards] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Rewards] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Penalties] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Penalties] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Penalties] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Penalties] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Penalties] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Jobs] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Jobs] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Jobs] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Jobs] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Jobs] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Items] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Items] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Items] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Items] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Items] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ItemDetail] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [ItemDetail] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [ItemDetail] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [ItemDetail] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [ItemDetail] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Government] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Government] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Government] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Government] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Government] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EquipmentTypesLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [EquipmentTypesLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EquipmentTypesLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [EquipmentTypesLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [EquipmentTypesLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Equipments] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Equipments] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Equipments] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Equipments] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Equipments] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EquipmentDetails] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [EquipmentDetails] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EquipmentDetails] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [EquipmentDetails] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [EquipmentDetails] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EndServiceReasonLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [EndServiceReasonLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [EndServiceReasonLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [EndServiceReasonLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [EndServiceReasonLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Employees] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Employees] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Employees] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Employees] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Departments] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Departments] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Departments] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Departments] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Departments] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerTypeLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [CustomerTypeLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerTypeLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [CustomerTypeLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [CustomerTypeLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Customers] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Customers] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Customers] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Customers] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Customers] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerContacts] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [CustomerContacts] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerContacts] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [CustomerContacts] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [CustomerContacts] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CountriesLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [CountriesLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [CountriesLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [CountriesLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [CountriesLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Contracts] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Contracts] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [Contracts] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Contracts] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [Contracts] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [BlackListEntity] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [BlackListEntity] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [BlackListEntity] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [BlackListEntity] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [BlackListEntity] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [AttendanceStatusLookup] ADD [CreatedAt] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [AttendanceStatusLookup] ADD [CreatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [AttendanceStatusLookup] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [AttendanceStatusLookup] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [AttendanceStatusLookup] ADD [UpdatedBy] nvarchar(max) NULL;
GO

ALTER TABLE [AdvancedPayments] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [AdvancedPayments] ADD [UpdatedAt] datetime2 NULL;
GO

ALTER TABLE [AdvancedPayments] ADD [UpdatedBy] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221001193159_updateBaseEntity', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Suppliers] (
    [Id] bigint NOT NULL IDENTITY,
    [SupplierNumber] nvarchar(max) NULL,
    [SupplierName] nvarchar(max) NULL,
    [Type] int NOT NULL,
    [CommercialNumber] nvarchar(max) NULL,
    [TaxId] nvarchar(max) NULL,
    [TaxFileNumber] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221001220810_addSuppliers', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Zones]') AND [c].[name] = N'CreatedAt');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Zones] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Zones] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UniformDetails]') AND [c].[name] = N'CreatedAt');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [UniformDetails] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [UniformDetails] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Uniform]') AND [c].[name] = N'CreatedAt');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Uniform] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Uniform] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'CreatedAt');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Sites]') AND [c].[name] = N'CreatedAt');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Sites] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Sites] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEquipmentsAssign]') AND [c].[name] = N'CreatedAt');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [SiteEquipmentsAssign] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [SiteEquipmentsAssign] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEquipments]') AND [c].[name] = N'CreatedAt');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [SiteEquipments] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [SiteEquipments] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEmployeesAttendance]') AND [c].[name] = N'CreatedAt');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [SiteEmployeesAttendance] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [SiteEmployeesAttendance] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEmployeesAssign]') AND [c].[name] = N'CreatedAt');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [SiteEmployeesAssign] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [SiteEmployeesAssign] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SiteEmployees]') AND [c].[name] = N'CreatedAt');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [SiteEmployees] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [SiteEmployees] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShiftTypesLookup]') AND [c].[name] = N'CreatedAt');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [ShiftTypesLookup] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [ShiftTypesLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rewards]') AND [c].[name] = N'CreatedAt');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Rewards] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Rewards] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Penalties]') AND [c].[name] = N'CreatedAt');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Penalties] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Penalties] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Jobs]') AND [c].[name] = N'CreatedAt');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Jobs] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Jobs] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Items]') AND [c].[name] = N'CreatedAt');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Items] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Items] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemDetail]') AND [c].[name] = N'CreatedAt');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [ItemDetail] DROP CONSTRAINT [' + @var39 + '];');
ALTER TABLE [ItemDetail] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var40 sysname;
SELECT @var40 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Government]') AND [c].[name] = N'CreatedAt');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Government] DROP CONSTRAINT [' + @var40 + '];');
ALTER TABLE [Government] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var41 sysname;
SELECT @var41 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EquipmentTypesLookup]') AND [c].[name] = N'CreatedAt');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [EquipmentTypesLookup] DROP CONSTRAINT [' + @var41 + '];');
ALTER TABLE [EquipmentTypesLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var42 sysname;
SELECT @var42 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Equipments]') AND [c].[name] = N'CreatedAt');
IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [Equipments] DROP CONSTRAINT [' + @var42 + '];');
ALTER TABLE [Equipments] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var43 sysname;
SELECT @var43 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EquipmentDetails]') AND [c].[name] = N'CreatedAt');
IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [EquipmentDetails] DROP CONSTRAINT [' + @var43 + '];');
ALTER TABLE [EquipmentDetails] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var44 sysname;
SELECT @var44 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EndServiceReasonLookup]') AND [c].[name] = N'CreatedAt');
IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [EndServiceReasonLookup] DROP CONSTRAINT [' + @var44 + '];');
ALTER TABLE [EndServiceReasonLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var45 sysname;
SELECT @var45 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'CreatedAt');
IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var45 + '];');
ALTER TABLE [Employees] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var46 sysname;
SELECT @var46 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Departments]') AND [c].[name] = N'CreatedAt');
IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [Departments] DROP CONSTRAINT [' + @var46 + '];');
ALTER TABLE [Departments] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var47 sysname;
SELECT @var47 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerTypeLookup]') AND [c].[name] = N'CreatedAt');
IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [CustomerTypeLookup] DROP CONSTRAINT [' + @var47 + '];');
ALTER TABLE [CustomerTypeLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var48 sysname;
SELECT @var48 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customers]') AND [c].[name] = N'CreatedAt');
IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [Customers] DROP CONSTRAINT [' + @var48 + '];');
ALTER TABLE [Customers] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var49 sysname;
SELECT @var49 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerContacts]') AND [c].[name] = N'CreatedAt');
IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [CustomerContacts] DROP CONSTRAINT [' + @var49 + '];');
ALTER TABLE [CustomerContacts] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var50 sysname;
SELECT @var50 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CountriesLookup]') AND [c].[name] = N'CreatedAt');
IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [CountriesLookup] DROP CONSTRAINT [' + @var50 + '];');
ALTER TABLE [CountriesLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var51 sysname;
SELECT @var51 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contracts]') AND [c].[name] = N'CreatedAt');
IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [Contracts] DROP CONSTRAINT [' + @var51 + '];');
ALTER TABLE [Contracts] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var52 sysname;
SELECT @var52 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[BlackListEntity]') AND [c].[name] = N'CreatedAt');
IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [BlackListEntity] DROP CONSTRAINT [' + @var52 + '];');
ALTER TABLE [BlackListEntity] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var53 sysname;
SELECT @var53 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AttendanceStatusLookup]') AND [c].[name] = N'CreatedAt');
IF @var53 IS NOT NULL EXEC(N'ALTER TABLE [AttendanceStatusLookup] DROP CONSTRAINT [' + @var53 + '];');
ALTER TABLE [AttendanceStatusLookup] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002182321_updateBaseEntityCreateAt', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SupplyTypes] (
    [Id] int NOT NULL IDENTITY,
    [SupplyName] nvarchar(max) NULL,
    [CreatedAt] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SupplyTypes] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002183510_supplyType', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var54 sysname;
SELECT @var54 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SupplyTypes]') AND [c].[name] = N'SupplyName');
IF @var54 IS NOT NULL EXEC(N'ALTER TABLE [SupplyTypes] DROP CONSTRAINT [' + @var54 + '];');
ALTER TABLE [SupplyTypes] ALTER COLUMN [SupplyName] nvarchar(max) NOT NULL;
ALTER TABLE [SupplyTypes] ADD DEFAULT N'' FOR [SupplyName];
GO

DECLARE @var55 sysname;
SELECT @var55 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'SupplierNumber');
IF @var55 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var55 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [SupplierNumber] nvarchar(max) NOT NULL;
ALTER TABLE [Suppliers] ADD DEFAULT N'' FOR [SupplierNumber];
GO

DECLARE @var56 sysname;
SELECT @var56 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Suppliers]') AND [c].[name] = N'SupplierName');
IF @var56 IS NOT NULL EXEC(N'ALTER TABLE [Suppliers] DROP CONSTRAINT [' + @var56 + '];');
ALTER TABLE [Suppliers] ALTER COLUMN [SupplierName] nvarchar(max) NOT NULL;
ALTER TABLE [Suppliers] ADD DEFAULT N'' FOR [SupplierName];
GO

CREATE INDEX [IX_Suppliers_Type] ON [Suppliers] ([Type]);
GO

ALTER TABLE [Suppliers] ADD CONSTRAINT [FK_Suppliers_SupplyTypes_Type] FOREIGN KEY ([Type]) REFERENCES [SupplyTypes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002191658_updateSupplierRelations', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var57 sysname;
SELECT @var57 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'InsurancePercentage');
IF @var57 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var57 + '];');
ALTER TABLE [Employees] ALTER COLUMN [InsurancePercentage] decimal(18,2) NULL;
GO

DECLARE @var58 sysname;
SELECT @var58 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'InsuranceAmount');
IF @var58 IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var58 + '];');
ALTER TABLE [Employees] ALTER COLUMN [InsuranceAmount] decimal(18,2) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002194457_updateInsuranceDataType', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchases] (
    [Id] bigint NOT NULL IDENTITY,
    [PurchaseDate] datetime2 NOT NULL,
    [SupplierId] bigint NOT NULL,
    [SupplyTypeId] int NOT NULL,
    [ItemId] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [CreatedAt] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [UpdatedAt] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Purchases_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]),
    CONSTRAINT [FK_Purchases_SupplyTypes_SupplyTypeId] FOREIGN KEY ([SupplyTypeId]) REFERENCES [SupplyTypes] ([Id])
);
GO

CREATE INDEX [IX_Purchases_SupplierId] ON [Purchases] ([SupplierId]);
GO

CREATE INDEX [IX_Purchases_SupplyTypeId] ON [Purchases] ([SupplyTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221002203856_AddPurchasesEntity', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221003203158_dummy22', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SiteEmployeesAttendance] ADD [Penality] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221005160232_abbsencePenality', N'6.0.9');
GO

COMMIT;
GO

