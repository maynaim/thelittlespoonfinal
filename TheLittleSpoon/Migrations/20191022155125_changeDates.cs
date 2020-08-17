using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLittleSpoon.Migrations
{
    public partial class changeDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2019, 10, 13, 15, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2019, 5, 22, 8, 10, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2019, 5, 22, 10, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2019, 5, 22, 22, 40, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2019, 5, 22, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2019, 1, 28, 16, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2019, 3, 9, 18, 12, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2019, 5, 22, 8, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2019, 1, 28, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2019, 12, 15, 21, 30, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2019, 5, 2, 15, 50, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2019, 5, 2, 8, 10, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2019, 9, 21, 10, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2019, 1, 14, 22, 40, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2019, 4, 2, 3, 55, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2019, 9, 28, 16, 25, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 7,
                column: "DateCreated",
                value: new DateTime(2019, 9, 9, 18, 12, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 8,
                column: "DateCreated",
                value: new DateTime(2019, 8, 19, 8, 5, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 9,
                column: "DateCreated",
                value: new DateTime(2019, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 11,
                column: "DateCreated",
                value: new DateTime(2019, 10, 15, 21, 30, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
