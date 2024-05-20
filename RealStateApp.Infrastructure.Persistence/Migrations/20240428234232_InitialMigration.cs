using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealStateApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Improvements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImprovementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Improvements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    SaleCategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<float>(type: "real", nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_PropertyTypes_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "PropertyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_SaleCategories_SaleCategoryId",
                        column: x => x.SaleCategoryId,
                        principalTable: "SaleCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientFavoriteProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientFavoriteProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientFavoriteProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImprovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImprovementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImprovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImprovements_Improvements_ImprovementId",
                        column: x => x.ImprovementId,
                        principalTable: "Improvements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyImprovements_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientFavoriteProperties_PropertyId",
                table: "ClientFavoriteProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_SaleCategoryId",
                table: "Properties",
                column: "SaleCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImages",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImprovements_ImprovementId",
                table: "PropertyImprovements",
                column: "ImprovementId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImprovements_PropertyId",
                table: "PropertyImprovements",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientFavoriteProperties");

            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "PropertyImprovements");

            migrationBuilder.DropTable(
                name: "Improvements");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropTable(
                name: "SaleCategories");
        }
    }
}
