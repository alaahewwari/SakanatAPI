using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteRentPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentPeriod",
                table: "Apartments");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0f77feac-28e6-4741-96a7-f954ab70d80b"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("22dd888c-ae72-451e-9c58-1ac2f0547c2f"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2aaff023-58b8-4c89-a898-171573a15739"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2bfe8b81-9c44-4c0c-aa1e-b9aa68d15ce8"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("57ed738b-9a40-4b4c-a23c-ec24870d7f58"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("701ae20f-76ea-4316-854f-6616aca7c6a7"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8d90e7a2-198e-426b-abbb-7b53b751ec2c"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a4225617-0aeb-4faf-a748-ef6a6a31d94e"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d49a1339-07e3-4ea2-94e1-237bc37bc511"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f0b8f861-af7e-48be-a91a-8f4f8cbc6c62"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f2e3bd05-84fb-42bd-a744-a4050316a90b"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fb460870-c643-4cbc-92fc-28d86bbf6bde"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 26));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RentPeriod",
                table: "Apartments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0f77feac-28e6-4741-96a7-f954ab70d80b"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("22dd888c-ae72-451e-9c58-1ac2f0547c2f"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2aaff023-58b8-4c89-a898-171573a15739"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2bfe8b81-9c44-4c0c-aa1e-b9aa68d15ce8"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("57ed738b-9a40-4b4c-a23c-ec24870d7f58"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("701ae20f-76ea-4316-854f-6616aca7c6a7"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8d90e7a2-198e-426b-abbb-7b53b751ec2c"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a4225617-0aeb-4faf-a748-ef6a6a31d94e"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d49a1339-07e3-4ea2-94e1-237bc37bc511"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f0b8f861-af7e-48be-a91a-8f4f8cbc6c62"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f2e3bd05-84fb-42bd-a744-a4050316a90b"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fb460870-c643-4cbc-92fc-28d86bbf6bde"),
                column: "CreationDate",
                value: new DateOnly(2024, 6, 25));
        }
    }
}
