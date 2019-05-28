using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodBank.Migrations
{
    public partial class ConversationModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondUserName",
                table: "Conversations",
                newName: "PostOwnerName");

            migrationBuilder.RenameColumn(
                name: "FirstUserName",
                table: "Conversations",
                newName: "ApplierName");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Message",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_OwnerId",
                table: "Message",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_OwnerId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_OwnerId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "PostOwnerName",
                table: "Conversations",
                newName: "SecondUserName");

            migrationBuilder.RenameColumn(
                name: "ApplierName",
                table: "Conversations",
                newName: "FirstUserName");
        }
    }
}
