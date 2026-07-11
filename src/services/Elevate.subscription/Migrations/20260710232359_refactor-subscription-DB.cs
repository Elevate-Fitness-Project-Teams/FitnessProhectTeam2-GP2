using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elevate.subscription.Migrations
{
    /// <inheritdoc />
    public partial class refactorsubscriptionDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanId",
                table: "UserSubscriptions");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "UserSubscriptions",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "AutoRenew",
                table: "UserSubscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresAt",
                table: "UserSubscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tier",
                table: "UserSubscriptions",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserSubscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BillingLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PlanTier = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FailureReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingLogs_UserId",
                table: "BillingLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "AutoRenew",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "Tier",
                table: "UserSubscriptions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserSubscriptions");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserSubscriptions",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UserSubscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPlanId",
                table: "UserSubscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscriptions_UserId",
                table: "UserSubscriptions",
                column: "UserId");
        }
    }
}
