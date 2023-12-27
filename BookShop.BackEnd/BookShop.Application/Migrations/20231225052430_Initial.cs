using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Application.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "Admin", new byte[] { 5, 124, 226, 144, 73, 5, 156, 247, 105, 109, 153, 77, 54, 97, 60, 189, 190, 196, 238, 146, 149, 50, 60, 78, 45, 210, 252, 118, 211, 24, 153, 138, 177, 109, 127, 249, 195, 33, 64, 6, 44, 6, 36, 40, 241, 170, 71, 229, 152, 157, 19, 64, 58, 90, 125, 217, 246, 49, 82, 23, 133, 111, 206, 118 }, new byte[] { 98, 253, 206, 213, 196, 231, 26, 18, 170, 180, 165, 254, 113, 107, 85, 224, 180, 231, 213, 249, 74, 102, 37, 11, 173, 40, 184, 146, 88, 246, 253, 57, 19, 109, 106, 235, 139, 193, 161, 96, 34, 14, 220, 168, 14, 60, 7, 156, 191, 196, 102, 104, 0, 138, 98, 196, 140, 7, 63, 166, 77, 177, 78, 98, 206, 144, 144, 21, 64, 211, 41, 254, 9, 139, 46, 243, 43, 230, 39, 202, 156, 121, 202, 71, 219, 7, 231, 48, 247, 40, 15, 151, 23, 131, 140, 66, 33, 221, 223, 180, 151, 236, 249, 236, 142, 228, 201, 43, 204, 93, 6, 209, 66, 163, 32, 225, 215, 111, 118, 76, 100, 210, 247, 118, 35, 207, 187, 59 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
