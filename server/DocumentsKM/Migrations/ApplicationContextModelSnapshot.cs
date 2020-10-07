﻿// <auto-generated />
using System;
using DocumentsKM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DocumentsKM.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-rc.1.20451.13");

            modelBuilder.Entity("DocumentsKM.Models.Department", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("number")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("code");

                    b.Property<int?>("DepartmentHeadId")
                        .HasColumnType("integer")
                        .HasColumnName("department_head_id");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsIndustrial")
                        .HasColumnType("boolean")
                        .HasColumnName("is_industrial");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("character varying(140)")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("short_name");

                    b.HasKey("Number")
                        .HasName("pk_departments");

                    b.HasIndex("DepartmentHeadId")
                        .HasDatabaseName("ix_departments_department_head_id");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("DocumentsKM.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("begin_date");

                    b.Property<int>("DepartmentNumber")
                        .HasColumnType("integer")
                        .HasColumnName("department_number");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("end_date");

                    b.Property<DateTime>("FiredDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("fired_date");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("full_name");

                    b.Property<bool>("HasCanteen")
                        .HasColumnType("boolean")
                        .HasColumnName("has_canteen");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("phone_number");

                    b.Property<int?>("PositionCode")
                        .HasColumnType("integer")
                        .HasColumnName("position_code");

                    b.Property<DateTime>("RecruitedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("recruited_date");

                    b.Property<int>("VacationType")
                        .HasColumnType("integer")
                        .HasColumnName("vacation_type");

                    b.HasKey("Id")
                        .HasName("pk_employees");

                    b.HasIndex("DepartmentNumber")
                        .HasDatabaseName("ix_employees_department_number");

                    b.HasIndex("PositionCode")
                        .HasDatabaseName("ix_employees_position_code");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("DocumentsKM.Models.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AdditionalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("additional_code");

                    b.Property<int?>("ChiefSpecialistId")
                        .HasColumnType("integer")
                        .HasColumnName("chief_specialist_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("code");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("department_id");

                    b.Property<int?>("GroupLeaderId")
                        .HasColumnType("integer")
                        .HasColumnName("group_leader_id");

                    b.Property<int>("MainBuilderId")
                        .HasColumnType("integer")
                        .HasColumnName("main_builder_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("SubnodeId")
                        .HasColumnType("integer")
                        .HasColumnName("subnode_id");

                    b.HasKey("Id")
                        .HasName("pk_marks");

                    b.HasIndex("ChiefSpecialistId")
                        .HasDatabaseName("ix_marks_chief_specialist_id");

                    b.HasIndex("DepartmentId")
                        .HasDatabaseName("ix_marks_department_id");

                    b.HasIndex("GroupLeaderId")
                        .HasDatabaseName("ix_marks_group_leader_id");

                    b.HasIndex("MainBuilderId")
                        .HasDatabaseName("ix_marks_main_builder_id");

                    b.HasIndex("SubnodeId")
                        .HasDatabaseName("ix_marks_subnode_id");

                    b.ToTable("marks");
                });

            modelBuilder.Entity("DocumentsKM.Models.MarksApprovals", b =>
                {
                    b.Property<int>("MarkId")
                        .HasColumnType("integer")
                        .HasColumnName("mark_id");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.HasKey("MarkId", "EmployeeId")
                        .HasName("pk_marks_approvals");

                    b.HasIndex("EmployeeId")
                        .HasDatabaseName("ix_marks_approvals_employee_id");

                    b.ToTable("marks_approvals");
                });

            modelBuilder.Entity("DocumentsKM.Models.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ActiveNode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("active_node");

                    b.Property<string>("AdditionalName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("additional_name");

                    b.Property<int>("ChiefEngineerId")
                        .HasColumnType("integer")
                        .HasColumnName("chief_engineer_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("code");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("project_id");

                    b.HasKey("Id")
                        .HasName("pk_nodes");

                    b.HasIndex("ChiefEngineerId")
                        .HasDatabaseName("ix_nodes_chief_engineer_id");

                    b.HasIndex("ProjectId")
                        .HasDatabaseName("ix_nodes_project_id");

                    b.ToTable("nodes");
                });

            modelBuilder.Entity("DocumentsKM.Models.Position", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("code")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("short_name");

                    b.HasKey("Code")
                        .HasName("pk_positions");

                    b.ToTable("positions");
                });

            modelBuilder.Entity("DocumentsKM.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AdditionalName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("additional_name");

                    b.Property<int?>("Approved1Id")
                        .HasColumnType("integer")
                        .HasColumnName("approved1_id");

                    b.Property<int?>("Approved2Id")
                        .HasColumnType("integer")
                        .HasColumnName("approved2_id");

                    b.Property<string>("BaseSeries")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("base_series");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_projects");

                    b.HasIndex("Approved1Id")
                        .HasDatabaseName("ix_projects_approved1_id");

                    b.HasIndex("Approved2Id")
                        .HasDatabaseName("ix_projects_approved2_id");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("DocumentsKM.Models.Subnode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AdditionalName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("additional_name");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("code");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<int>("NodeId")
                        .HasColumnType("integer")
                        .HasColumnName("node_id");

                    b.HasKey("Id")
                        .HasName("pk_subnodes");

                    b.HasIndex("NodeId")
                        .HasDatabaseName("ix_subnodes_node_id");

                    b.ToTable("subnodes");
                });

            modelBuilder.Entity("DocumentsKM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer")
                        .HasColumnName("employee_id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("EmployeeId")
                        .HasDatabaseName("ix_users_employee_id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasDatabaseName("ix_users_login");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DocumentsKM.Models.Department", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "DepartmentHead")
                        .WithMany()
                        .HasForeignKey("DepartmentHeadId")
                        .HasConstraintName("fk_departments_employees_department_head_id");

                    b.Navigation("DepartmentHead");
                });

            modelBuilder.Entity("DocumentsKM.Models.Employee", b =>
                {
                    b.HasOne("DocumentsKM.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentNumber")
                        .HasConstraintName("fk_employees_departments_department_number")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentsKM.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionCode")
                        .HasConstraintName("fk_employees_positions_position_code");

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("DocumentsKM.Models.Mark", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "ChiefSpecialist")
                        .WithMany()
                        .HasForeignKey("ChiefSpecialistId")
                        .HasConstraintName("fk_marks_employees_chief_specialist_id");

                    b.HasOne("DocumentsKM.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("fk_marks_departments_department_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentsKM.Models.Employee", "GroupLeader")
                        .WithMany()
                        .HasForeignKey("GroupLeaderId")
                        .HasConstraintName("fk_marks_employees_group_leader_id");

                    b.HasOne("DocumentsKM.Models.Employee", "MainBuilder")
                        .WithMany()
                        .HasForeignKey("MainBuilderId")
                        .HasConstraintName("fk_marks_employees_main_builder_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentsKM.Models.Subnode", "Subnode")
                        .WithMany()
                        .HasForeignKey("SubnodeId")
                        .HasConstraintName("fk_marks_subnodes_subnode_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiefSpecialist");

                    b.Navigation("Department");

                    b.Navigation("GroupLeader");

                    b.Navigation("MainBuilder");

                    b.Navigation("Subnode");
                });

            modelBuilder.Entity("DocumentsKM.Models.MarksApprovals", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("fk_marks_approvals_employees_employee_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentsKM.Models.Mark", "Mark")
                        .WithMany()
                        .HasForeignKey("MarkId")
                        .HasConstraintName("fk_marks_approvals_marks_mark_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Mark");
                });

            modelBuilder.Entity("DocumentsKM.Models.Node", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "ChiefEngineer")
                        .WithMany()
                        .HasForeignKey("ChiefEngineerId")
                        .HasConstraintName("fk_nodes_employees_chief_engineer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocumentsKM.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("fk_nodes_projects_project_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiefEngineer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DocumentsKM.Models.Project", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "Approved1")
                        .WithMany()
                        .HasForeignKey("Approved1Id")
                        .HasConstraintName("fk_projects_employees_approved1_id");

                    b.HasOne("DocumentsKM.Models.Employee", "Approved2")
                        .WithMany()
                        .HasForeignKey("Approved2Id")
                        .HasConstraintName("fk_projects_employees_approved2_id");

                    b.Navigation("Approved1");

                    b.Navigation("Approved2");
                });

            modelBuilder.Entity("DocumentsKM.Models.Subnode", b =>
                {
                    b.HasOne("DocumentsKM.Models.Node", "Node")
                        .WithMany()
                        .HasForeignKey("NodeId")
                        .HasConstraintName("fk_subnodes_nodes_node_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Node");
                });

            modelBuilder.Entity("DocumentsKM.Models.User", b =>
                {
                    b.HasOne("DocumentsKM.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("fk_users_employees_employee_id");

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
