using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ThreeSixtyPlusAI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FriendlyName = table.Column<string>(type: "text", nullable: true),
                    Xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCategories",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionCategoryTitle = table.Column<string>(type: "text", nullable: false),
                    QuestionCategoryIcon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThreeSixtyReviews",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessCode = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreeSixtyReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionCategories_QuestionCategoryId",
                        column: x => x.QuestionCategoryId,
                        principalSchema: "main",
                        principalTable: "QuestionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreeSixtyReviewer",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreeSixtyReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessCode = table.Column<string>(type: "text", nullable: false),
                    HasFinished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreeSixtyReviewer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreeSixtyReviewer_ThreeSixtyReviews_ThreeSixtyReviewId",
                        column: x => x.ThreeSixtyReviewId,
                        principalSchema: "main",
                        principalTable: "ThreeSixtyReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreeSixtyReviewQuestion",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreeSixtyReviewId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreeSixtyReviewQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreeSixtyReviewQuestion_ThreeSixtyReviews_ThreeSixtyReview~",
                        column: x => x.ThreeSixtyReviewId,
                        principalSchema: "main",
                        principalTable: "ThreeSixtyReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreeSixtyReviewAnswer",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreeSixtyReviewerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreeSixtyReviewQuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreeSixtyReviewAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreeSixtyReviewAnswer_ThreeSixtyReviewer_ThreeSixtyReviewe~",
                        column: x => x.ThreeSixtyReviewerId,
                        principalSchema: "main",
                        principalTable: "ThreeSixtyReviewer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "QuestionCategories",
                columns: new[] { "Id", "QuestionCategoryIcon", "QuestionCategoryTitle" },
                values: new object[,]
                {
                    { new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "bi-person-badge", "Employee" },
                    { new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "bi-person-circle", "Manager" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "Questions",
                columns: new[] { "Id", "QuestionCategoryId", "QuestionText" },
                values: new object[,]
                {
                    { new Guid("069f8124-ae0f-49b5-800e-cc687c9067fb"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Does this employee use their time effectively?" },
                    { new Guid("08d16d66-080a-4a3d-8b7f-e84f1246e01d"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Does this employee act professionally?" },
                    { new Guid("0c74485e-ae23-4db9-af0b-1eafa4821e2b"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Is this manager available to provide help and feedback when you want it?" },
                    { new Guid("10e70c40-a226-42d6-8387-a66c2a021920"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Does this employee motivate others to reach goals?" },
                    { new Guid("21a16f98-155c-4e2b-acb9-09e887a001d6"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Does this manager always control emotions and behavior, even when faced with high-conflict or stressful situations?" },
                    { new Guid("24a957f0-261d-491b-b8b7-0929d0dd5adb"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Has this employee shown they apply feedback they receive to learn and grow?" },
                    { new Guid("27995e3e-c61a-400e-b5e6-02d7805d18e8"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "When making important decisions, does this manager consider the opinions of others?" },
                    { new Guid("3abbbbe4-9ec6-40f5-9332-d9c6b2b471ad"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Is this manager effective at solving problems?" },
                    { new Guid("3eb7d9f8-e9f4-469c-b292-1d6e53e633e0"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Do you believe this employee is honest, ethical and trustworthy?" },
                    { new Guid("65f77b25-b49b-4d8e-b8c1-00f02abc23b5"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Do you believe this employee knows and represents our company's goals and values?" },
                    { new Guid("6ec59213-067f-4bef-9a5a-f9258823cb25"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Does this manager treat others respectfully?" },
                    { new Guid("8074d239-5c43-4af6-960b-46ebd5d30cf1"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Do you feel this manager sets clear direction that aligns with the organization's strategy?" },
                    { new Guid("8c445394-cbf1-43c1-b33b-1e504f858fae"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Does this employee show initiative to solve problems?" },
                    { new Guid("938efc50-57f9-4166-b26c-c604f67ae344"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Does this employee prioritize the needs of the customer?" },
                    { new Guid("95ad7c70-84c1-4edd-bd6e-190769e02b08"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Do the actions of this manager inspire growth and development in others?" },
                    { new Guid("ac2ec8f3-9042-4fad-b918-5ebde5e7c765"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Do you receive constructive and helpful feedback from this manager?" },
                    { new Guid("aca2ecf3-1b76-4631-a49b-f2f823b1d8d9"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Does this manager consistently reward employees for good performance or behavior?" },
                    { new Guid("c2156adc-d47b-4d30-aff2-84c8fbebe963"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Can you rely on this employee to follow through with his/her promises and responsibilities?" },
                    { new Guid("e8b77d88-6720-48dd-8007-48221b777cf1"), new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"), "Has this employee shown initiative to take the lead on team projects or assignments?" },
                    { new Guid("f2acb881-da85-4b25-8081-936837a907fd"), new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"), "Is this manager able to resolve conflict appropriately?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionCategoryId",
                schema: "main",
                table: "Questions",
                column: "QuestionCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreeSixtyReviewAnswer_ThreeSixtyReviewerId",
                schema: "main",
                table: "ThreeSixtyReviewAnswer",
                column: "ThreeSixtyReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreeSixtyReviewer_ThreeSixtyReviewId",
                schema: "main",
                table: "ThreeSixtyReviewer",
                column: "ThreeSixtyReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreeSixtyReviewQuestion_ThreeSixtyReviewId",
                schema: "main",
                table: "ThreeSixtyReviewQuestion",
                column: "ThreeSixtyReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataProtectionKeys",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Questions",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ThreeSixtyReviewAnswer",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ThreeSixtyReviewQuestion",
                schema: "main");

            migrationBuilder.DropTable(
                name: "QuestionCategories",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ThreeSixtyReviewer",
                schema: "main");

            migrationBuilder.DropTable(
                name: "ThreeSixtyReviews",
                schema: "main");
        }
    }
}
