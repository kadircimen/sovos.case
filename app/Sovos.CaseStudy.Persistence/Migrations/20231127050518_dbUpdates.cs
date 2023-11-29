using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sovos.CaseStudy.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceId",
                table: "InvoiceHeaders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "InvoiceHeaders");
        }
    }
}
