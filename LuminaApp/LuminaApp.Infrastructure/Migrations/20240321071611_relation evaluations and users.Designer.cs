﻿// <auto-generated />
using System;
using LuminaApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LuminaApp.Infrastructure.Migrations
{
    [DbContext(typeof(LuminaAppContext))]
    [Migration("20240321071611_relation evaluations and users")]
    partial class relationevaluationsandusers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DocumentUser", b =>
                {
                    b.Property<int>("DocumentsDocumentId")
                        .HasColumnType("int");

                    b.Property<string>("studentsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DocumentsDocumentId", "studentsId");

                    b.HasIndex("studentsId");

                    b.ToTable("DocumentUser");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<int?>("SessionFK")
                        .HasColumnType("int");

                    b.Property<string>("StudentFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("attendanceType")
                        .HasColumnType("int");

                    b.HasKey("AttendanceId");

                    b.HasIndex("SessionFK")
                        .IsUnique()
                        .HasFilter("[SessionFK] IS NOT NULL");

                    b.HasIndex("StudentFK");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.ClassRoom", b =>
                {
                    b.Property<int>("ClassroomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassroomId"));

                    b.Property<string>("ClassroomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("employeeFk")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClassroomId");

                    b.HasIndex("employeeFk");

                    b.ToTable("ClassRooms");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<DateTime>("Creation_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("folderFK")
                        .HasColumnType("int");

                    b.HasKey("DocumentId");

                    b.HasIndex("folderFK");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Equipment", b =>
                {
                    b.Property<int>("EquipmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EquipmentId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("classRoomFK")
                        .HasColumnType("int");

                    b.HasKey("EquipmentId");

                    b.HasIndex("classRoomFK");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Evaluation", b =>
                {
                    b.Property<int>("EvaluationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EvaluationId"));

                    b.Property<DateTime>("EvaluationDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Mark")
                        .HasColumnType("real");

                    b.Property<string>("StudentFk")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherFk")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("coefficient")
                        .HasColumnType("real");

                    b.Property<int>("evaluationType")
                        .HasColumnType("int");

                    b.HasKey("EvaluationId");

                    b.HasIndex("StudentFk");

                    b.HasIndex("TeacherFk");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Folder", b =>
                {
                    b.Property<int>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolderId"));

                    b.Property<DateTime>("Creation_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modification_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParenFolderFK")
                        .HasColumnType("int");

                    b.Property<string>("TeacherFK")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FolderId");

                    b.HasIndex("ParenFolderFK");

                    b.HasIndex("TeacherFK");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoryId"));

                    b.Property<DateTime>("HistoryDate")
                        .HasColumnType("datetime2");

                    b.HasKey("HistoryId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"));

                    b.Property<int?>("SubjectFK")
                        .HasColumnType("int");

                    b.Property<string>("TeacherFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("end_hour")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("start_hour")
                        .HasColumnType("datetime2");

                    b.HasKey("SessionId");

                    b.HasIndex("SubjectFK");

                    b.HasIndex("TeacherFK");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Subject", b =>
                {
                    b.Property<int>("subjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("subjectId"));

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("coefficient")
                        .HasColumnType("real");

                    b.HasKey("subjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ParentFK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("historyFK")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ParentFK");

                    b.HasIndex("historyFK")
                        .IsUnique()
                        .HasFilter("[historyFK] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DocumentUser", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.Document", null)
                        .WithMany()
                        .HasForeignKey("DocumentsDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuminaApp.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("studentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Attendance", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.Session", "session")
                        .WithOne("attendance")
                        .HasForeignKey("LuminaApp.Domain.Entities.Attendance", "SessionFK");

                    b.HasOne("LuminaApp.Domain.Entities.User", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentFK")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Student");

                    b.Navigation("session");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.ClassRoom", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", "employee")
                        .WithMany("ClassRooms")
                        .HasForeignKey("employeeFk")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("employee");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Document", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.Folder", "folder")
                        .WithMany("Documents")
                        .HasForeignKey("folderFK")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("folder");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Equipment", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.ClassRoom", "classRoom")
                        .WithMany("equipments")
                        .HasForeignKey("classRoomFK");

                    b.Navigation("classRoom");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Evaluation", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", "Student")
                        .WithMany("StudentsEvaluations")
                        .HasForeignKey("StudentFk")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LuminaApp.Domain.Entities.User", "Teacher")
                        .WithMany("TeachersEvaluations")
                        .HasForeignKey("TeacherFk")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Folder", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.Folder", "ParentFolder")
                        .WithMany("Folders")
                        .HasForeignKey("ParenFolderFK")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LuminaApp.Domain.Entities.User", "Teacher")
                        .WithMany("Folders")
                        .HasForeignKey("TeacherFK")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ParentFolder");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Session", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.Subject", "subject")
                        .WithMany("sessions")
                        .HasForeignKey("SubjectFK");

                    b.HasOne("LuminaApp.Domain.Entities.User", "Teacher")
                        .WithMany("sessions")
                        .HasForeignKey("TeacherFK");

                    b.Navigation("Teacher");

                    b.Navigation("subject");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.User", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", "Parent")
                        .WithMany("Students")
                        .HasForeignKey("ParentFK")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("LuminaApp.Domain.Entities.History", "history")
                        .WithOne("user")
                        .HasForeignKey("LuminaApp.Domain.Entities.User", "historyFK");

                    b.Navigation("Parent");

                    b.Navigation("history");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuminaApp.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LuminaApp.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.ClassRoom", b =>
                {
                    b.Navigation("equipments");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Folder", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Folders");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.History", b =>
                {
                    b.Navigation("user");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Session", b =>
                {
                    b.Navigation("attendance");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.Subject", b =>
                {
                    b.Navigation("sessions");
                });

            modelBuilder.Entity("LuminaApp.Domain.Entities.User", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("ClassRooms");

                    b.Navigation("Folders");

                    b.Navigation("Students");

                    b.Navigation("StudentsEvaluations");

                    b.Navigation("TeachersEvaluations");

                    b.Navigation("sessions");
                });
#pragma warning restore 612, 618
        }
    }
}