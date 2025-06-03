using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Insert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Inserting default user roles
            migrationBuilder.Sql("INSERT INTO [ACCESS_LEVEL]([NAME]) VALUES ('Administrator'), ('User');");

            //Inserting default admin user
            migrationBuilder.Sql("INSERT INTO [USER]([NAME], [EMAIL], [PASSWORD], [ID_ACCESS_LEVEL]) VALUES ('Admin', 'admin@cloudgames.com.br', 'Mudar@123', 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
