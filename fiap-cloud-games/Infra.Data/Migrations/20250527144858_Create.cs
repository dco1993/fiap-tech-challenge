using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACCESS_LEVEL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DH_TIMESTAMP = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACCESS_LEVEL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GAME",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GENRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    METACRITIC = table.Column<decimal>(type: "DECIMAL(3,1)", nullable: false),
                    PUBLISHER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DEVELOPER = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RELEASE_DATE = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ABOUT = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DH_TIMESTAMP = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GAME", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PASSWORD = table.Column<string>(type: "VARCHAR(MAX)", nullable: false),
                    ID_ACCESS_LEVEL = table.Column<int>(type: "INT", nullable: false, defaultValueSql: "2"),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DH_TIMESTAMP = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_ACCESS_LEVEL_ID_ACCESS_LEVEL",
                        column: x => x.ID_ACCESS_LEVEL,
                        principalTable: "ACCESS_LEVEL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DISCOUNT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_GAME = table.Column<int>(type: "INT", nullable: false),
                    START_DISCOUNT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DISCOUNT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PERCENT_OFF = table.Column<float>(type: "real", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DH_TIMESTAMP = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISCOUNT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DISCOUNT_GAME_ID_GAME",
                        column: x => x.ID_GAME,
                        principalTable: "GAME",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "USER_GAME",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<int>(type: "INT", nullable: false),
                    ID_GAME = table.Column<int>(type: "INT", nullable: false),
                    STATUS = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    DH_TIMESTAMP = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_GAME", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_GAME_GAME_ID_GAME",
                        column: x => x.ID_GAME,
                        principalTable: "GAME",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_GAME_USER_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "USER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_IdGame",
                table: "DISCOUNT",
                column: "ID_GAME");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Genre",
                table: "GAME",
                column: "GENRE");

            migrationBuilder.CreateIndex(
                name: "IX_Game_Title",
                table: "GAME",
                column: "TITLE");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "USER",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdAccessLevel",
                table: "USER",
                column: "ID_ACCESS_LEVEL");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_IdGame",
                table: "USER_GAME",
                column: "ID_GAME");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_IdUser",
                table: "USER_GAME",
                column: "ID_USER");

            //Inserting default user roles
            migrationBuilder.Sql("INSERT INTO [ACCESS_LEVEL]([NAME]) VALUES ('Administrator'), ('User');");

            //Inserting default admin user
            migrationBuilder.Sql("INSERT INTO [USER]([NAME], [EMAIL], [PASSWORD], [ID_ACCESS_LEVEL]) VALUES ('Admin', 'admin@cloudgames.com.br', 'Mudar@123', 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DISCOUNT");

            migrationBuilder.DropTable(
                name: "USER_GAME");

            migrationBuilder.DropTable(
                name: "GAME");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "ACCESS_LEVEL");
        }
    }
}
