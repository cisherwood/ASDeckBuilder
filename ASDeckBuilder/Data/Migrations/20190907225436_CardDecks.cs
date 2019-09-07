using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASDeckBuilder.Data.Migrations
{
    public partial class CardDecks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_Cards_CardId",
                table: "Decks");

            migrationBuilder.DropIndex(
                name: "IX_Decks_CardId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Decks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Decks",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardDecks",
                columns: table => new
                {
                    CardDeckId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<int>(nullable: false),
                    DeckId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDecks", x => x.CardDeckId);
                    table.ForeignKey(
                        name: "FK_CardDecks_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDecks_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_CardId",
                table: "CardDecks",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDecks_DeckId",
                table: "CardDecks",
                column: "DeckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardDecks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Decks");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Decks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "Quantity",
                table: "Decks",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Decks_CardId",
                table: "Decks",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_Cards_CardId",
                table: "Decks",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
