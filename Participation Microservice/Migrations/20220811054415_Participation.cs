using Microsoft.EntityFrameworkCore.Migrations;

namespace Participation_Microservice.Migrations
{
    public partial class Participation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    Participation_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player_id = table.Column<int>(nullable: false),
                    Player_name = table.Column<string>(nullable: true),
                    Event_id = table.Column<int>(nullable: false),
                    Event_name = table.Column<string>(nullable: true),
                    Sports_id = table.Column<int>(nullable: false),
                    Sports_name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.Participation_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participation");
        }
    }
}
