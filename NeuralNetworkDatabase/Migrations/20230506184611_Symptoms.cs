using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeuralNetworkDatabase.Migrations
{
    /// <inheritdoc />
    public partial class Symptoms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symptoms",
                columns: table => new
                {
                    SymptomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdentity = table.Column<int>(type: "int", nullable: false),
                    NNType = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<double>(type: "float", nullable: false),
                    Sex = table.Column<double>(type: "float", nullable: false),
                    Cp = table.Column<double>(type: "float", nullable: false),
                    Trestbps = table.Column<double>(type: "float", nullable: false),
                    Chol = table.Column<double>(type: "float", nullable: false),
                    Fbs = table.Column<double>(type: "float", nullable: false),
                    Restecg = table.Column<double>(type: "float", nullable: false),
                    Thalach = table.Column<double>(type: "float", nullable: false),
                    Exang = table.Column<double>(type: "float", nullable: false),
                    Oldpeak = table.Column<double>(type: "float", nullable: false),
                    Slope = table.Column<double>(type: "float", nullable: false),
                    Ca = table.Column<double>(type: "float", nullable: false),
                    Thal = table.Column<double>(type: "float", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symptoms", x => x.SymptomId);
                    table.ForeignKey(
                        name: "FK_Symptoms_Users_UserIdentity",
                        column: x => x.UserIdentity,
                        principalTable: "Users",
                        principalColumn: "UserIdentity",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_UserIdentity",
                table: "Symptoms",
                column: "UserIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symptoms");
        }
    }
}
