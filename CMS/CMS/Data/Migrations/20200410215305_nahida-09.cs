using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Data.Migrations
{
    public partial class nahida09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PercelLocation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PercelLocation_BranchId",
                table: "PercelLocation",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PercelLocation_Branches_BranchId",
                table: "PercelLocation",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PercelLocation_Branches_BranchId",
                table: "PercelLocation");

            migrationBuilder.DropIndex(
                name: "IX_PercelLocation_BranchId",
                table: "PercelLocation");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "PercelLocation");
        }
    }
}
