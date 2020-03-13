using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CMS.Data.Migrations
{
    public partial class nahida05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Percels_BranchId",
                table: "Percels",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Percels_Branches_BranchId",
                table: "Percels",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Percels_Branches_BranchId",
                table: "Percels");

            migrationBuilder.DropIndex(
                name: "IX_Percels_BranchId",
                table: "Percels");
        }
    }
}
