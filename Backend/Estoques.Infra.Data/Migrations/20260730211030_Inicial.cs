using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Estoques.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NMUsuario = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NMLogin = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CDSenha = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Adquirente",
                columns: table => new
                {
                    IDAdquirente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMAdquirente = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TXEndereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TXAnotacao = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquirente", x => x.IDAdquirente);
                    table.ForeignKey(
                        name: "FK_Adquirente_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    IDFornecedor = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMFornecedor = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TXEndereco = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TXAnotacao = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.IDFornecedor);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFabricante",
                columns: table => new
                {
                    IDProdutoFabricante = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMProdutoFabricante = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFabricante", x => x.IDProdutoFabricante);
                    table.ForeignKey(
                        name: "FK_ProdutoFabricante_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoSituacao",
                columns: table => new
                {
                    IDProdutoSituacao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMProdutoSituacao = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoSituacao", x => x.IDProdutoSituacao);
                    table.ForeignKey(
                        name: "FK_ProdutoSituacao_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoTipo",
                columns: table => new
                {
                    IDProdutoTipo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMProdutoTipo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoTipo", x => x.IDProdutoTipo);
                    table.ForeignKey(
                        name: "FK_ProdutoTipo_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    IDProduto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDProdutoTipo = table.Column<int>(type: "integer", nullable: false),
                    IDProdutoSituacao = table.Column<int>(type: "integer", nullable: false),
                    IDProdutoFabricante = table.Column<int>(type: "integer", nullable: false),
                    IDUsuario = table.Column<int>(type: "integer", nullable: false),
                    NMProduto = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DSProduto = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    INProdutoCor = table.Column<byte>(type: "smallint", nullable: false),
                    QTProduto = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    INProdutoMedida = table.Column<byte>(type: "smallint", nullable: true),
                    LKProdutoImagem = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.IDProduto);
                    table.ForeignKey(
                        name: "FK_Produto_ProdutoFabricante_IDProdutoFabricante",
                        column: x => x.IDProdutoFabricante,
                        principalTable: "ProdutoFabricante",
                        principalColumn: "IDProdutoFabricante",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produto_ProdutoSituacao_IDProdutoSituacao",
                        column: x => x.IDProdutoSituacao,
                        principalTable: "ProdutoSituacao",
                        principalColumn: "IDProdutoSituacao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produto_ProdutoTipo_IDProdutoTipo",
                        column: x => x.IDProdutoTipo,
                        principalTable: "ProdutoTipo",
                        principalColumn: "IDProdutoTipo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produto_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoHistorico",
                columns: table => new
                {
                    IDProdutoHistorico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDProduto = table.Column<int>(type: "integer", nullable: false),
                    IDFornecedor = table.Column<int>(type: "integer", nullable: false),
                    IDAdquirente = table.Column<int>(type: "integer", nullable: true),
                    INProdutoHistoricoTipo = table.Column<byte>(type: "smallint", nullable: false),
                    DTProdutoHistorico = table.Column<DateTime>(type: "date", nullable: false),
                    QTProdutoHistorico = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    VLProdutoHistorico = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoHistorico", x => x.IDProdutoHistorico);
                    table.ForeignKey(
                        name: "FK_ProdutoHistorico_Adquirente_IDAdquirente",
                        column: x => x.IDAdquirente,
                        principalTable: "Adquirente",
                        principalColumn: "IDAdquirente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoHistorico_Fornecedor_IDFornecedor",
                        column: x => x.IDFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "IDFornecedor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoHistorico_Produto_IDProduto",
                        column: x => x.IDProduto,
                        principalTable: "Produto",
                        principalColumn: "IDProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adquirente_IDUsuario",
                table: "Adquirente",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_IDUsuario",
                table: "Fornecedor",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IDProdutoFabricante",
                table: "Produto",
                column: "IDProdutoFabricante");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IDProdutoSituacao",
                table: "Produto",
                column: "IDProdutoSituacao");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IDProdutoTipo",
                table: "Produto",
                column: "IDProdutoTipo");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IDUsuario",
                table: "Produto",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFabricante_IDUsuario",
                table: "ProdutoFabricante",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoHistorico_IDAdquirente",
                table: "ProdutoHistorico",
                column: "IDAdquirente");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoHistorico_IDFornecedor",
                table: "ProdutoHistorico",
                column: "IDFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoHistorico_IDProduto",
                table: "ProdutoHistorico",
                column: "IDProduto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoSituacao_IDUsuario",
                table: "ProdutoSituacao",
                column: "IDUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoTipo_IDUsuario",
                table: "ProdutoTipo",
                column: "IDUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoHistorico");

            migrationBuilder.DropTable(
                name: "Adquirente");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "ProdutoFabricante");

            migrationBuilder.DropTable(
                name: "ProdutoSituacao");

            migrationBuilder.DropTable(
                name: "ProdutoTipo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
