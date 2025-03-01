using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ICaNet.Infrastructure.Infrastructure.Core
{
    /// <inheritdoc />
    public partial class updateProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplier_SuppLierdId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SuppLierdId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SuppLierdId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplier_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Supplier_SupplierId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "SuppLierdId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SuppLierdId",
                table: "Products",
                column: "SuppLierdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Supplier_SuppLierdId",
                table: "Products",
                column: "SuppLierdId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
