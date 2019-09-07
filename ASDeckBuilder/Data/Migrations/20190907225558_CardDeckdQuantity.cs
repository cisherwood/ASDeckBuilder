using Microsoft.EntityFrameworkCore.Migrations;

namespace ASDeckBuilder.Data.Migrations
{
    public partial class CardDeckdQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Quantity",
                table: "CardDecks",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CardDecks");
        }
    }
}
