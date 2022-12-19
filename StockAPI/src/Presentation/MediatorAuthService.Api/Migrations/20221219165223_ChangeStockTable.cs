using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockAPI.Api.Migrations
{
    public partial class ChangeStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VariantCode",
                table: "Stocks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Stocks",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0bfa391-a604-4049-a868-359091461e46"),
                columns: new[] { "CreatedDate", "Password", "RefreshToken" },
                values: new object[] { new DateTime(2022, 12, 19, 16, 52, 23, 255, DateTimeKind.Utc).AddTicks(501), "AI+0B5yUP+y2vTj/FpVyJabLkyaKNDn8ItOHpMp6LmnqiHcmwGLMpxu+QaPOS20W2A==", "AA/UJq5n2zAI1jM1BYLtBofYNMqwqzo5kZTL3p5PS3zT2fGKUxbYpBDz17/pZNlohA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VariantCode",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "ProductCode",
                table: "Stocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0bfa391-a604-4049-a868-359091461e46"),
                columns: new[] { "CreatedDate", "Password", "RefreshToken" },
                values: new object[] { new DateTime(2022, 12, 19, 16, 49, 58, 990, DateTimeKind.Utc).AddTicks(1897), "AApzeXsF964+qc/0G3G6AFf1Sg0POqR5abdyTjpjzBWIwvf1hNMOcTnMvrOqCieUEw==", "AJhCHzqyX0qdj3iu7NkpUe4xw5QnZxqi8onkU3WlkETnLGWscaBf3EmF9gyjooRHOQ==" });
        }
    }
}
