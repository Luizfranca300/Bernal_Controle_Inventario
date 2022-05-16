using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Bernal.ERP.ControleInventario.Infra.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventarios",
                columns: table => new
                {
                    inventario_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iniciado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    finalizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventarios", x => x.inventario_id);
                });

            migrationBuilder.CreateTable(
                name: "inventario_dispositivos",
                columns: table => new
                {
                    inventario_dispositivo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero_de_serie = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    iniciado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    finalizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    excluido_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    reponsavel_pela_contagem = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    inventario_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventario_dispositivos", x => x.inventario_dispositivo_id);
                    table.ForeignKey(
                        name: "FK_inventario_dispositivos_inventarios_inventario_id",
                        column: x => x.inventario_id,
                        principalTable: "inventarios",
                        principalColumn: "inventario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventario_itens",
                columns: table => new
                {
                    inventario_item_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    produto_codigo = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    produto_nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    embalagem = table.Column<int>(type: "integer", nullable: false),
                    quantidade_embalagem = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_custo_embalagem = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_final = table.Column<decimal>(type: "numeric", nullable: false),
                    icms = table.Column<decimal>(type: "numeric", nullable: false),
                    transporte = table.Column<decimal>(type: "numeric", nullable: false),
                    excluido_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inventario_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventario_itens", x => x.inventario_item_id);
                    table.ForeignKey(
                        name: "FK_inventario_itens_inventarios_inventario_id",
                        column: x => x.inventario_id,
                        principalTable: "inventarios",
                        principalColumn: "inventario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tipo_de_contagem",
                columns: table => new
                {
                    tipo_de_contagem_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sessao = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    equipe = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    inventario_item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo_de_contagem", x => x.tipo_de_contagem_id);
                    table.ForeignKey(
                        name: "FK_tipo_de_contagem_inventario_itens_inventario_item_id",
                        column: x => x.inventario_item_id,
                        principalTable: "inventario_itens",
                        principalColumn: "inventario_item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventario_dispositivos_inventario_id",
                table: "inventario_dispositivos",
                column: "inventario_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventario_itens_inventario_id",
                table: "inventario_itens",
                column: "inventario_id");

            migrationBuilder.CreateIndex(
                name: "IX_tipo_de_contagem_inventario_item_id",
                table: "tipo_de_contagem",
                column: "inventario_item_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventario_dispositivos");

            migrationBuilder.DropTable(
                name: "tipo_de_contagem");

            migrationBuilder.DropTable(
                name: "inventario_itens");

            migrationBuilder.DropTable(
                name: "inventarios");
        }
    }
}
