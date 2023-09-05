using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormBuilderDB.Migrations
{
    /// <inheritdoc />
    public partial class ComboBoxAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComboBoxFormData",
                columns: table => new
                {
                    Value = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormsDatumID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboBoxFormData", x => x.Value);
                    table.ForeignKey(
                        name: "FK_ComboBoxFormData_FormsData_FormsDatumID",
                        column: x => x.FormsDatumID,
                        principalTable: "FormsData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboBoxFormData_FormsDatumID",
                table: "ComboBoxFormData",
                column: "FormsDatumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboBoxFormData");
        }
    }
}
