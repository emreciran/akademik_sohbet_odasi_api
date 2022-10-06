using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class updatedmig13092022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Questions_Question_ID",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Question_ID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Question_ID",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "QuestionTags",
                columns: table => new
                {
                    Question_ID = table.Column<int>(type: "int", nullable: false),
                    Tag_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTags", x => new { x.Question_ID, x.Tag_ID });
                    table.ForeignKey(
                        name: "FK_QuestionTags_Questions_Question_ID",
                        column: x => x.Question_ID,
                        principalTable: "Questions",
                        principalColumn: "Question_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionTags_Tags_Tag_ID",
                        column: x => x.Tag_ID,
                        principalTable: "Tags",
                        principalColumn: "Tag_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_Tag_ID",
                table: "QuestionTags",
                column: "Tag_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionTags");

            migrationBuilder.AddColumn<int>(
                name: "Question_ID",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Question_ID",
                table: "Tags",
                column: "Question_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Questions_Question_ID",
                table: "Tags",
                column: "Question_ID",
                principalTable: "Questions",
                principalColumn: "Question_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
