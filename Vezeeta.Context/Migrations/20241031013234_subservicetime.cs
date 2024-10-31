using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Context.Migrations
{
    /// <inheritdoc />
    public partial class subservicetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "SubServicesTimeSlots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "SubServicesTimeSlots");
        }
    }
}
