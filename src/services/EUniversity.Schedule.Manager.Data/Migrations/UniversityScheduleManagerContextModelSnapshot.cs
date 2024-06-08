﻿// <auto-generated />
using System;
using EUniversity.Schedule.Manager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EUniversity.Schedule.Manager.Data.Migrations
{
    [DbContext(typeof(UniversityScheduleManagerContext))]
    partial class UniversityScheduleManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Faculty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DeanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TimeTableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeanId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Course")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CuratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HeadStudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SpecialityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("FacultyId");

                    b.HasIndex("HeadStudentId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<int>("LessonNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("LessonTimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WeekId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LessonTimeId");

                    b.HasIndex("RoomId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("WeekId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.LessonTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("LessonNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TimeTableId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TimeTableId");

                    b.ToTable("LessonTimes");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EvenWeekId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OddWeekId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EvenWeekId");

                    b.HasIndex("OddWeekId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.TimeTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FacultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId")
                        .IsUnique();

                    b.ToTable("TimeTables");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Week", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Faculty", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Teacher", "Dean")
                        .WithMany()
                        .HasForeignKey("DeanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dean");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Group", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Teacher", "Curator")
                        .WithMany()
                        .HasForeignKey("CuratorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Student", "HeadStudent")
                        .WithMany()
                        .HasForeignKey("HeadStudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Curator");

                    b.Navigation("Faculty");

                    b.Navigation("HeadStudent");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Lesson", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.LessonTime", "LessonTime")
                        .WithMany("Lessons")
                        .HasForeignKey("LessonTimeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Room", "Room")
                        .WithMany("Lessons")
                        .HasForeignKey("RoomId");

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Teacher", "Teacher")
                        .WithMany("Lessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Week", "Week")
                        .WithMany("Lessons")
                        .HasForeignKey("WeekId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("LessonTime");

                    b.Navigation("Room");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.LessonTime", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.TimeTable", "TimeTable")
                        .WithMany("LessonTimes")
                        .HasForeignKey("TimeTableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TimeTable");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Room", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Faculty", "Faculty")
                        .WithMany("Rooms")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Schedule", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Week", "EvenWeek")
                        .WithMany()
                        .HasForeignKey("EvenWeekId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Week", "OddWeek")
                        .WithMany()
                        .HasForeignKey("OddWeekId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EvenWeek");

                    b.Navigation("OddWeek");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Semester", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Speciality", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Faculty", "Faculty")
                        .WithMany("Specialities")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Student", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.TimeTable", b =>
                {
                    b.HasOne("EUniversity.Schedule.Manager.Data.Models.Faculty", "Faculty")
                        .WithOne("TimeTable")
                        .HasForeignKey("EUniversity.Schedule.Manager.Data.Models.TimeTable", "FacultyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Faculty", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("Specialities");

                    b.Navigation("TimeTable")
                        .IsRequired();
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Group", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.LessonTime", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Room", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Subject", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Teacher", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.TimeTable", b =>
                {
                    b.Navigation("LessonTimes");
                });

            modelBuilder.Entity("EUniversity.Schedule.Manager.Data.Models.Week", b =>
                {
                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}
