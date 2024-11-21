using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.Migrations
{
    /// <inheritdoc />
   /* public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89d2fb4f-7372-411c-8df5-5bb948f23bbb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d08ba48f-d291-4de3-baab-91778e05b026");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bc5a015-71fd-4289-9be5-f51dc9841331", null, "Administrator", "ADMINISTRATOR" },
                    { "e1bacb73-66e9-4cdf-9405-31056f91efca", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc5a015-71fd-4289-9be5-f51dc9841331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1bacb73-66e9-4cdf-9405-31056f91efca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "89d2fb4f-7372-411c-8df5-5bb948f23bbb", null, "User", "USER" },
                    { "d08ba48f-d291-4de3-baab-91778e05b026", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }*/


    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if roles with the given IDs already exist
            migrationBuilder.Sql(@"
            IF EXISTS (SELECT 1 FROM AspNetRoles WHERE Id = '89d2fb4f-7372-411c-8df5-5bb948f23bbb')
            BEGIN
                DELETE FROM AspNetRoles WHERE Id = '89d2fb4f-7372-411c-8df5-5bb948f23bbb'
            END

            IF EXISTS (SELECT 1 FROM AspNetRoles WHERE Id = 'd08ba48f-d291-4de3-baab-91778e05b026')
            BEGIN
                DELETE FROM AspNetRoles WHERE Id = 'd08ba48f-d291-4de3-baab-91778e05b026'
            END
        ");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                { "5bc5a015-71fd-4289-9be5-f51dc9841331", null, "Administrator", "ADMINISTRATOR" },
                { "e1bacb73-66e9-4cdf-9405-31056f91efca", null, "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert roles back to previous state (just insert them back)
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc5a015-71fd-4289-9be5-f51dc9841331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1bacb73-66e9-4cdf-9405-31056f91efca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                { "89d2fb4f-7372-411c-8df5-5bb948f23bbb", null, "User", "USER" },
                { "d08ba48f-d291-4de3-baab-91778e05b026", null, "Administrator", "ADMINISTRATOR" }
                });
        }   
    }

}
