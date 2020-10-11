﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_leave_management.Data.Migrations
{
    public partial class Addeddefaultdaysandperiod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveTypeVM");

            migrationBuilder.RenameColumn(
                name: "Defaultdays",
                table: "LeaveTypes",
                newName: "DefaultDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultDays",
                table: "LeaveTypes",
                newName: "Defaultdays");

            migrationBuilder.CreateTable(
                name: "LeaveTypeVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeVM", x => x.Id);
                });
        }
    }
}
