using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elevate.Auth.Migrations
{
    /// <inheritdoc />
    public partial class authenhancement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                table: "AspNetUsers",
                newName: "LockedUntil");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "OtpCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "LoginAttempts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLockedOut",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtpCodes_AppUserId",
                table: "OtpCodes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_AppUserId",
                table: "LoginAttempts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsLockedOut",
                table: "AspNetUsers",
                column: "IsLockedOut",
                filter: "[IsLockedOut] = 1");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginAttempts_AspNetUsers_AppUserId",
                table: "LoginAttempts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginAttempts_AspNetUsers_UserId",
                table: "LoginAttempts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtpCodes_AspNetUsers_AppUserId",
                table: "OtpCodes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_AspNetUsers_AppUserId",
                table: "LoginAttempts");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginAttempts_AspNetUsers_UserId",
                table: "LoginAttempts");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpCodes_AspNetUsers_AppUserId",
                table: "OtpCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtpCodes_AspNetUsers_UserId",
                table: "OtpCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_OtpCodes_AppUserId",
                table: "OtpCodes");

            migrationBuilder.DropIndex(
                name: "IX_LoginAttempts_AppUserId",
                table: "LoginAttempts");

            migrationBuilder.DropIndex(
                name: "IX_Users_IsLockedOut",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "OtpCodes");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "LoginAttempts");

            migrationBuilder.DropColumn(
                name: "IsLockedOut",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LockedUntil",
                table: "AspNetUsers",
                newName: "LastLoginAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
