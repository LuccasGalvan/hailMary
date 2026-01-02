using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoLoja.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "TipoCategorias",
                columns: new[] { "TipoCategoriaId", "Nome" },
                values: new object[,]
                {
                    { 1, "Tipo" },
                    { 2, "País" }
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nome", "TipoCategoriaId" },
                values: new object[,]
                {
                    { 1, "Coins", 1 },
                    { 2, "Match boxes", 1 },
                    { 3, "Stamps", 1 },
                    { 4, "Playing Cards", 1 },
                    { 5, "Brazil", 2 },
                    { 6, "UK", 2 },
                    { 7, "Japan", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoCategorias",
                keyColumn: "TipoCategoriaId",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "TipoCategorias",
                columns: new[] { "TipoCategoriaId", "Nome" },
                values: new object[,]
                {
                    { 1, "Tipo" },
                    { 2, "Editora" },
                    { 3, "Grupo" },
                    { 4, "Género" },
                    { 5, "Plataforma" }
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nome", "TipoCategoriaId" },
                values: new object[,]
                {
                    { 1, "Filme", 1 },
                    { 2, "Música", 1 },
                    { 3, "Jogo", 1 },
                    { 4, "Série", 1 },
                    { 5, "Sony", 2 },
                    { 6, "Warner", 2 },
                    { 7, "Universal", 2 },
                    { 8, "EA Games", 2 },
                    { 9, "Metallica", 3 },
                    { 10, "Queen", 3 },
                    { 11, "Eminem", 3 },
                    { 12, "Coldplay", 3 },
                    { 13, "Ação", 4 },
                    { 14, "Drama", 4 },
                    { 15, "Rock", 4 },
                    { 16, "Pop", 4 },
                    { 17, "RAP", 4 },
                    { 18, "PS5", 5 },
                    { 19, "Xbox", 5 },
                    { 20, "PC", 5 },
                    { 21, "Nintendo Switch", 5 }
                });
        }
    }
}
