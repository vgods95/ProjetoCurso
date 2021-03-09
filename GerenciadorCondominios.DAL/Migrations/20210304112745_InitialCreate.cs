using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorCondominios.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcoes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meses",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meses", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimeiroAcesso = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Funcoes_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alugueis",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    codigoMes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alugueis", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Alugueis_Meses_codigoMes",
                        column: x => x.codigoMes,
                        principalTable: "Meses",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historico_Recursos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    CodigoMes = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico_Recursos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Historico_Recursos_Meses_CodigoMes",
                        column: x => x.CodigoMes,
                        principalTable: "Meses",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Apartamentos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Andar = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigoMorador = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    codigoProprietario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartamentos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Apartamentos_Usuarios_codigoMorador",
                        column: x => x.codigoMorador,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Apartamentos_Usuarios_codigoProprietario",
                        column: x => x.codigoProprietario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Funcoes_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Funcoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CodigoUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Eventos_Usuarios_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CodigoUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Servicos_Usuarios_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CodigoUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Veiculos_Usuarios_CodigoUsuario",
                        column: x => x.CodigoUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CodigoAluguel = table.Column<int>(type: "int", nullable: false),
                    AluguelCodigo = table.Column<int>(type: "int", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Alugueis_AluguelCodigo",
                        column: x => x.AluguelCodigo,
                        principalTable: "Alugueis",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servico_Predios",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoServico = table.Column<int>(type: "int", nullable: false),
                    DataExecucao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servico_Predios", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Servico_Predios_Servicos_CodigoServico",
                        column: x => x.CodigoServico,
                        principalTable: "Servicos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29b55b64-24d9-4b48-b9da-0f2ca1689374", "fda1d158-83fe-4202-9f7c-995921305e12", "Morador do Prédio", "Morador", "MORADOR" },
                    { "84fc1437-9e10-42c9-87da-0803728f3e46", "302894d6-c008-4345-b1ef-aaca9bc94401", "Síndico do Prédio", "Sindico", "SINDICO" },
                    { "2d676129-0e93-4eb3-bf4e-e7e36c2498fd", "03d51910-3333-49c6-b778-550e917a5965", "Administrador do Prédio", "Administrador", "ADMINISTRADOR" }
                });

            migrationBuilder.InsertData(
                table: "Meses",
                columns: new[] { "Codigo", "Nome" },
                values: new object[,]
                {
                    { 1, "Janeiro" },
                    { 2, "Fevereiro" },
                    { 3, "Março" },
                    { 4, "Abril" },
                    { 5, "Maio" },
                    { 6, "Junho" },
                    { 7, "Julho" },
                    { 8, "Agosto" },
                    { 9, "Setembro" },
                    { 10, "Outubro" },
                    { 11, "Novembro" },
                    { 12, "Dezembro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alugueis_codigoMes",
                table: "Alugueis",
                column: "codigoMes");

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_codigoMorador",
                table: "Apartamentos",
                column: "codigoMorador");

            migrationBuilder.CreateIndex(
                name: "IX_Apartamentos_codigoProprietario",
                table: "Apartamentos",
                column: "codigoProprietario");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_CodigoUsuario",
                table: "Eventos",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Funcoes",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Historico_Recursos_CodigoMes",
                table: "Historico_Recursos",
                column: "CodigoMes");

            migrationBuilder.CreateIndex(
                name: "IX_Meses_Nome",
                table: "Meses",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_AluguelCodigo",
                table: "Pagamentos",
                column: "AluguelCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_UsuarioId",
                table: "Pagamentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Servico_Predios_CodigoServico",
                table: "Servico_Predios",
                column: "CodigoServico");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_CodigoUsuario",
                table: "Servicos",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuarios",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_CodigoUsuario",
                table: "Veiculos",
                column: "CodigoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_Placa",
                table: "Veiculos",
                column: "Placa",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartamentos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Historico_Recursos");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Servico_Predios");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Funcoes");

            migrationBuilder.DropTable(
                name: "Alugueis");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "Meses");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
