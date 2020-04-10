using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Data.Migrations
{
    public partial class nahida08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PercelLocation");

            migrationBuilder.RenameColumn(
                name: "ReceivingDate",
                table: "PercelLocation",
                newName: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_PercelLocation_PercelId",
                table: "PercelLocation",
                column: "PercelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PercelLocation_Percels_PercelId",
                table: "PercelLocation",
                column: "PercelId",
                principalTable: "Percels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PercelLocation_Percels_PercelId",
                table: "PercelLocation");

            migrationBuilder.DropIndex(
                name: "IX_PercelLocation_PercelId",
                table: "PercelLocation");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "PercelLocation",
                newName: "ReceivingDate");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "PercelLocation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PercelLocation",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
