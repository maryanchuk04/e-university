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

CREATE TABLE [Permissions] (
    [Id] int NOT NULL IDENTITY,
    [Type] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Picture] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserPermissions] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PermissionId] int NOT NULL,
    CONSTRAINT [PK_UserPermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPermissions_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserPermissions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTokens] (
    [Id] uniqueidentifier NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [TokenType] int NOT NULL,
    [CreatedOn] datetime2 NOT NULL,
    [ExpiredOn] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [UserId] uniqueidentifier NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Type') AND [object_id] = OBJECT_ID(N'[Permissions]'))
    SET IDENTITY_INSERT [Permissions] ON;
INSERT INTO [Permissions] ([Id], [Name], [Type])
VALUES (1, N'FullAccess', 8),
(2, N'ScheduleEditor', 2),
(3, N'ScheduleViewer', 1),
(4, N'NoAccess', 0),
(5, N'FacultyFullAccess', 4);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Type') AND [object_id] = OBJECT_ID(N'[Permissions]'))
    SET IDENTITY_INSERT [Permissions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [Description], [Name])
VALUES (0, N'User', N'User'),
(1, N'Student role', N'Student'),
(4, N'Schedule administrator', N'Schedule Admin'),
(8, N'Faculty administrator. Has admin role on own faculty.', N'Faculty Admin'),
(16, N'Administrator role. Full Access', N'Admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

CREATE INDEX [IX_UserPermissions_PermissionId] ON [UserPermissions] ([PermissionId]);
GO

CREATE INDEX [IX_UserPermissions_UserId] ON [UserPermissions] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE UNIQUE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

CREATE INDEX [IX_UserTokens_UserId] ON [UserTokens] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240512182355_SetupDataBase', N'8.0.4');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [FullName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609153445_AddFullNameForEachUser', N'8.0.4');
GO

COMMIT;
GO

