using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DS.Data.Migrations
{
    public partial class UserEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_User",
                table: "Employee",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ID_User",
                table: "Employee",
                column: "ID_User");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee",
                column: "ID_User",
                principalTable: "User",
                principalColumn: "ID_User",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_ID_User",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "ID_User",
                table: "Employee");
        }
    }
}
