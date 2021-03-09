using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorCondominios.DAL.Migrations
{
    public partial class AtualizacaodoBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartamentos_Usuarios_codigoMorador",
                table: "Apartamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartamentos_Usuarios_codigoProprietario",
                table: "Apartamentos");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "29b55b64-24d9-4b48-b9da-0f2ca1689374");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "2d676129-0e93-4eb3-bf4e-e7e36c2498fd");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "84fc1437-9e10-42c9-87da-0803728f3e46");

            migrationBuilder.RenameColumn(
                name: "codigoProprietario",
                table: "Apartamentos",
                newName: "CodigoProprietario");

            migrationBuilder.RenameColumn(
                name: "codigoMorador",
                table: "Apartamentos",
                newName: "CodigoMorador");

            migrationBuilder.RenameIndex(
                name: "IX_Apartamentos_codigoProprietario",
                table: "Apartamentos",
                newName: "IX_Apartamentos_CodigoProprietario");

            migrationBuilder.RenameIndex(
                name: "IX_Apartamentos_codigoMorador",
                table: "Apartamentos",
                newName: "IX_Apartamentos_CodigoMorador");

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "f57ae114-e365-4ce8-8158-b6d69c7428ff", "e736cd9d-12df-4ce3-b7b5-958aae2053bc", "Morador do Prédio", "Morador", "MORADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "c366f83c-b973-4f04-87c9-5ae7ffe1ffe3", "296ee513-42dc-4b0b-a1d3-907b2e3e8ce6", "Síndico do Prédio", "Sindico", "SINDICO" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "40acd91b-5239-4287-a63d-94abbab6fe11", "c73ce9cf-9cf7-4809-be70-9ebedeab290e", "Administrador do Prédio", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartamentos_Usuarios_CodigoMorador",
                table: "Apartamentos",
                column: "CodigoMorador",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartamentos_Usuarios_CodigoProprietario",
                table: "Apartamentos",
                column: "CodigoProprietario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartamentos_Usuarios_CodigoMorador",
                table: "Apartamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartamentos_Usuarios_CodigoProprietario",
                table: "Apartamentos");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "40acd91b-5239-4287-a63d-94abbab6fe11");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "c366f83c-b973-4f04-87c9-5ae7ffe1ffe3");

            migrationBuilder.DeleteData(
                table: "Funcoes",
                keyColumn: "Id",
                keyValue: "f57ae114-e365-4ce8-8158-b6d69c7428ff");

            migrationBuilder.RenameColumn(
                name: "CodigoProprietario",
                table: "Apartamentos",
                newName: "codigoProprietario");

            migrationBuilder.RenameColumn(
                name: "CodigoMorador",
                table: "Apartamentos",
                newName: "codigoMorador");

            migrationBuilder.RenameIndex(
                name: "IX_Apartamentos_CodigoProprietario",
                table: "Apartamentos",
                newName: "IX_Apartamentos_codigoProprietario");

            migrationBuilder.RenameIndex(
                name: "IX_Apartamentos_CodigoMorador",
                table: "Apartamentos",
                newName: "IX_Apartamentos_codigoMorador");

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "29b55b64-24d9-4b48-b9da-0f2ca1689374", "fda1d158-83fe-4202-9f7c-995921305e12", "Morador do Prédio", "Morador", "MORADOR" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "84fc1437-9e10-42c9-87da-0803728f3e46", "302894d6-c008-4345-b1ef-aaca9bc94401", "Síndico do Prédio", "Sindico", "SINDICO" });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Id", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[] { "2d676129-0e93-4eb3-bf4e-e7e36c2498fd", "03d51910-3333-49c6-b778-550e917a5965", "Administrador do Prédio", "Administrador", "ADMINISTRADOR" });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartamentos_Usuarios_codigoMorador",
                table: "Apartamentos",
                column: "codigoMorador",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartamentos_Usuarios_codigoProprietario",
                table: "Apartamentos",
                column: "codigoProprietario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
