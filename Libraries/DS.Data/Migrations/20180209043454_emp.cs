using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DS.Data.Migrations
{
    public partial class emp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "ID_User",
                table: "Employee",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee",
                column: "ID_User",
                principalTable: "User",
                principalColumn: "ID_User",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "ID_User",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_User_ID_User",
                table: "Employee",
                column: "ID_User",
                principalTable: "User",
                principalColumn: "ID_User",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
