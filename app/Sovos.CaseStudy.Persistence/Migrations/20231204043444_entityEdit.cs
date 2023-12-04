using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sovos.CaseStudy.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class entityEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "InvoiceHeaders",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "InvoiceHeaders",
                newName: "SendDate");
        }
    }
}
