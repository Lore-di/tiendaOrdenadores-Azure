using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaA01.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    NombrePedido = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordenador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenador_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calor = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coste = table.Column<float>(type: "real", nullable: false),
                    Megas = table.Column<long>(type: "bigint", nullable: false),
                    Cores = table.Column<int>(type: "int", nullable: false),
                    Serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoComponente = table.Column<int>(type: "int", nullable: false),
                    OrdenadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componente_Ordenador_OrdenadorId",
                        column: x => x.OrdenadorId,
                        principalTable: "Ordenador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componente_OrdenadorId",
                table: "Componente",
                column: "OrdenadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenador_PedidoId",
                table: "Ordenador",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "Ordenador");

            migrationBuilder.DropTable(
                name: "Pedido");
        }
    }
}
