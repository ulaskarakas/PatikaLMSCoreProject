using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatikaLMSCoreProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixSettingEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SettingEntity",
                table: "SettingEntity");

            migrationBuilder.RenameTable(
                name: "SettingEntity",
                newName: "Settings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Settings",
                table: "Settings",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Settings",
                table: "Settings");

            migrationBuilder.RenameTable(
                name: "Settings",
                newName: "SettingEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SettingEntity",
                table: "SettingEntity",
                column: "Id");
        }
    }
}
