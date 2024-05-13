﻿// <auto-generated />
using System;
using EUniversity.Authorization.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EUniversity.Authorization.Data.Migrations
{
    [DbContext(typeof(AuthorizationDbContext))]
    partial class AuthorizationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "FullAccess",
                            Type = 8
                        },
                        new
                        {
                            Id = 2,
                            Name = "ScheduleEditor",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Name = "ScheduleViewer",
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            Name = "NoAccess",
                            Type = 0
                        },
                        new
                        {
                            Id = 5,
                            Name = "FacultyFullAccess",
                            Type = 4
                        });
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 16,
                            Description = "Administrator role. Full Access",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 1,
                            Description = "Student role",
                            Name = "Student"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Faculty administrator. Has admin role on own faculty.",
                            Name = "Faculty Admin"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Schedule administrator",
                            Name = "Schedule Admin"
                        },
                        new
                        {
                            Id = 0,
                            Description = "User",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserPermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiredOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenType")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserPermission", b =>
                {
                    b.HasOne("EUniversity.Authorization.Data.Models.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EUniversity.Authorization.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserRole", b =>
                {
                    b.HasOne("EUniversity.Authorization.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EUniversity.Authorization.Data.Models.User", "User")
                        .WithOne("UserRole")
                        .HasForeignKey("EUniversity.Authorization.Data.Models.UserRole", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.UserToken", b =>
                {
                    b.HasOne("EUniversity.Authorization.Data.Models.User", null)
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EUniversity.Authorization.Data.Models.User", b =>
                {
                    b.Navigation("UserRole")
                        .IsRequired();

                    b.Navigation("UserTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
