using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Limupa.Comment.Api.Migrations
{
    /// <inheritdoc />
    public partial class limupa_comment_mig_docker_one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    UserCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCommentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCommentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCommentRating = table.Column<int>(type: "int", nullable: false),
                    UserCommentCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserCommentStatus = table.Column<bool>(type: "bit", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.UserCommentID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserComments");
        }
    }
}
