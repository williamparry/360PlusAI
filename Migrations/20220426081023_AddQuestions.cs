using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreeSixtyPlusAI.Migrations
{
    public partial class AddQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "main",
                table: "Questions",
                columns: new[] { "Id", "QuestionCategoryId", "QuestionText" },
                values: new object[,]
                {
                    { new Guid("5f57df7d-7210-445e-a2bc-60608f189b91"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "What does this employee need to improve on?" },
                    { new Guid("9cb16d51-bc92-49a3-91cb-d4c966089c03"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "What does this employee do well?" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "main",
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("5f57df7d-7210-445e-a2bc-60608f189b91"));

            migrationBuilder.DeleteData(
                schema: "main",
                table: "Questions",
                keyColumn: "Id",
                keyValue: new Guid("9cb16d51-bc92-49a3-91cb-d4c966089c03"));
        }
    }
}
