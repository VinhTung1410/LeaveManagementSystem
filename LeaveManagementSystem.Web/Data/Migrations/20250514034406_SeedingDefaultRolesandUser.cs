using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f963412-60af-4700-8067-5f219178ca30", null, "Administrator", "ADMINISTRATOR" },
                    { "729acc5a-5309-4253-81db-390da2618719", null, "Supervisor", "SUPERVISOR" },
                    { "94bbdce8-d8ea-433f-894c-aa5d9f472216", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "408aa945-3d84-4421-8342-7269ec64d949", 0, "d865cb02-6b25-4d44-8840-774eba3b0f43", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOiadyP6Mt6eSijURCv4CySeNulzf/lNUbungqjuDUbuuIOaxBwdQjHFfgLYsJf18A==", null, false, "eacac14f-02e3-4866-9104-48a1b9a5d06e", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
    table: "AspNetUserRoles",
    columns: new[] { "RoleId", "UserId" },
    values: new object[] { "0f963412-60af-4700-8067-5f219178ca30", "408aa945-3d84-4421-8342-7269ec64d949" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f963412-60af-4700-8067-5f219178ca30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "729acc5a-5309-4253-81db-390da2618719");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94bbdce8-d8ea-433f-894c-aa5d9f472216");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e9f639de-624f-4a4e-b8bf-2381725462f1", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");
        }
    }
}
