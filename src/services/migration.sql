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

CREATE TABLE [Subjects] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [IsDisabled] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Teachers] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [Position] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [IsDisabled] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Weeks] (
    [Id] uniqueidentifier NOT NULL,
    [Type] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Weeks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Faculties] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Adress] nvarchar(max) NOT NULL,
    [DeanId] uniqueidentifier NULL,
    [TimeTableId] uniqueidentifier NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Faculties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Faculties_Teachers_DeanId] FOREIGN KEY ([DeanId]) REFERENCES [Teachers] ([Id])
);
GO

CREATE TABLE [Schedules] (
    [Id] uniqueidentifier NOT NULL,
    [EvenWeekId] uniqueidentifier NOT NULL,
    [OddWeekId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Schedules] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Schedules_Weeks_EvenWeekId] FOREIGN KEY ([EvenWeekId]) REFERENCES [Weeks] ([Id]),
    CONSTRAINT [FK_Schedules_Weeks_OddWeekId] FOREIGN KEY ([OddWeekId]) REFERENCES [Weeks] ([Id])
);
GO

CREATE TABLE [Rooms] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [FacultyId] uniqueidentifier NOT NULL,
    [IsDisabled] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rooms_Faculties_FacultyId] FOREIGN KEY ([FacultyId]) REFERENCES [Faculties] ([Id])
);
GO

CREATE TABLE [Specialities] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [FacultyId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Specialities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Specialities_Faculties_FacultyId] FOREIGN KEY ([FacultyId]) REFERENCES [Faculties] ([Id])
);
GO

CREATE TABLE [TeacherFaculties] (
    [Id] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    [FacultyId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TeacherFaculties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TeacherFaculties_Faculties_FacultyId] FOREIGN KEY ([FacultyId]) REFERENCES [Faculties] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TeacherFaculties_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TimeTables] (
    [Id] uniqueidentifier NOT NULL,
    [FacultyId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_TimeTables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TimeTables_Faculties_FacultyId] FOREIGN KEY ([FacultyId]) REFERENCES [Faculties] ([Id])
);
GO

CREATE TABLE [Semesters] (
    [Id] uniqueidentifier NOT NULL,
    [ScheduleId] uniqueidentifier NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Semesters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Semesters_Schedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [Schedules] ([Id])
);
GO

CREATE TABLE [LessonTimes] (
    [Id] uniqueidentifier NOT NULL,
    [LessonNumber] int NOT NULL,
    [StartAt] datetime2 NOT NULL,
    [EndAt] datetime2 NOT NULL,
    [TimeTableId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_LessonTimes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LessonTimes_TimeTables_TimeTableId] FOREIGN KEY ([TimeTableId]) REFERENCES [TimeTables] ([Id])
);
GO

CREATE TABLE [Groups] (
    [Id] uniqueidentifier NOT NULL,
    [Course] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [FacultyId] uniqueidentifier NOT NULL,
    [SpecialityId] uniqueidentifier NOT NULL,
    [HeadStudentId] uniqueidentifier NULL,
    [CuratorId] uniqueidentifier NULL,
    [IsDisabled] bit NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Groups_Faculties_FacultyId] FOREIGN KEY ([FacultyId]) REFERENCES [Faculties] ([Id]),
    CONSTRAINT [FK_Groups_Specialities_SpecialityId] FOREIGN KEY ([SpecialityId]) REFERENCES [Specialities] ([Id]),
    CONSTRAINT [FK_Groups_Teachers_CuratorId] FOREIGN KEY ([CuratorId]) REFERENCES [Teachers] ([Id])
);
GO

CREATE TABLE [Lessons] (
    [Id] uniqueidentifier NOT NULL,
    [LessonNumber] int NOT NULL,
    [IsOnline] bit NOT NULL,
    [Url] nvarchar(max) NULL,
    [Type] int NOT NULL,
    [WeekId] uniqueidentifier NOT NULL,
    [LessonTimeId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    [RoomId] uniqueidentifier NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lessons_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]),
    CONSTRAINT [FK_Lessons_LessonTimes_LessonTimeId] FOREIGN KEY ([LessonTimeId]) REFERENCES [LessonTimes] ([Id]),
    CONSTRAINT [FK_Lessons_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]),
    CONSTRAINT [FK_Lessons_Subjects_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subjects] ([Id]),
    CONSTRAINT [FK_Lessons_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([Id]),
    CONSTRAINT [FK_Lessons_Weeks_WeekId] FOREIGN KEY ([WeekId]) REFERENCES [Weeks] ([Id])
);
GO

CREATE TABLE [Students] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Faculties_DeanId] ON [Faculties] ([DeanId]);
GO

CREATE INDEX [IX_Groups_CuratorId] ON [Groups] ([CuratorId]);
GO

CREATE INDEX [IX_Groups_FacultyId] ON [Groups] ([FacultyId]);
GO

CREATE INDEX [IX_Groups_HeadStudentId] ON [Groups] ([HeadStudentId]);
GO

CREATE INDEX [IX_Groups_SpecialityId] ON [Groups] ([SpecialityId]);
GO

CREATE INDEX [IX_Lessons_GroupId] ON [Lessons] ([GroupId]);
GO

CREATE INDEX [IX_Lessons_LessonTimeId] ON [Lessons] ([LessonTimeId]);
GO

CREATE INDEX [IX_Lessons_RoomId] ON [Lessons] ([RoomId]);
GO

CREATE INDEX [IX_Lessons_SubjectId] ON [Lessons] ([SubjectId]);
GO

CREATE INDEX [IX_Lessons_TeacherId] ON [Lessons] ([TeacherId]);
GO

CREATE INDEX [IX_Lessons_WeekId] ON [Lessons] ([WeekId]);
GO

CREATE INDEX [IX_LessonTimes_TimeTableId] ON [LessonTimes] ([TimeTableId]);
GO

CREATE INDEX [IX_Rooms_FacultyId] ON [Rooms] ([FacultyId]);
GO

CREATE INDEX [IX_Schedules_EvenWeekId] ON [Schedules] ([EvenWeekId]);
GO

CREATE INDEX [IX_Schedules_OddWeekId] ON [Schedules] ([OddWeekId]);
GO

CREATE INDEX [IX_Semesters_ScheduleId] ON [Semesters] ([ScheduleId]);
GO

CREATE INDEX [IX_Specialities_FacultyId] ON [Specialities] ([FacultyId]);
GO

CREATE INDEX [IX_Students_GroupId] ON [Students] ([GroupId]);
GO

CREATE INDEX [IX_TeacherFaculties_FacultyId] ON [TeacherFaculties] ([FacultyId]);
GO

CREATE INDEX [IX_TeacherFaculties_TeacherId] ON [TeacherFaculties] ([TeacherId]);
GO

CREATE UNIQUE INDEX [IX_TimeTables_FacultyId] ON [TimeTables] ([FacultyId]);
GO

ALTER TABLE [Groups] ADD CONSTRAINT [FK_Groups_Students_HeadStudentId] FOREIGN KEY ([HeadStudentId]) REFERENCES [Students] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609054001_SetupDatabaseStructure', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LessonTimes]') AND [c].[name] = N'StartAt');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [LessonTimes] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [LessonTimes] ALTER COLUMN [StartAt] time NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[LessonTimes]') AND [c].[name] = N'EndAt');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [LessonTimes] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [LessonTimes] ALTER COLUMN [EndAt] time NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240609062335_UseTimeOnlyForTimeTable', N'8.0.6');
GO

COMMIT;
GO

