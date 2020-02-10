using Microsoft.EntityFrameworkCore.Migrations;

namespace YoutubeSearch.Web.Data.Migrations.ApplicationDatabase
{
    public partial class NewCollumns1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ulong>(
                name: "CommentCount",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "DislikeCount",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "LikeCount",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "ViewCount",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "CommentCount",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "SubscriberCount",
                table: "Channels",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "VideoCount",
                table: "Channels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "SubscriberCount",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "VideoCount",
                table: "Channels");
        }
    }
}
