using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnesCenter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lockers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lockers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    LockerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Lockers_LockerId",
                        column: x => x.LockerId,
                        principalTable: "Lockers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Clients_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ClientServices",
                columns: table => new
                {
                    ClientsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientServices", x => new { x.ClientsId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_ClientServices_Clients_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientServices_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Birthday", "Email", "IsActive", "LockerId", "Name", "Patronymic", "Phone", "Surname", "TrainerId" },
                values: new object[] { new Guid("33333333-3333-3333-3333-333333333333"), new DateOnly(1985, 8, 20), "petr@example.com", true, null, "Петр", "Иванович", "+7-999-333-33-33", "Сидоров", null });

            migrationBuilder.InsertData(
                table: "Lockers",
                columns: new[] { "Id", "ClientId", "Number" },
                values: new object[,]
                {
                    { new Guid("05592c6c-fbdc-4062-98b8-f684d76466cc"), null, 19 },
                    { new Guid("115feead-2df3-42f8-932a-d3ec78436457"), null, 17 },
                    { new Guid("176e5c8b-f3c8-44aa-9b14-5994eb29361c"), null, 12 },
                    { new Guid("2fadd572-18ae-4595-b275-0aa65dac5d6a"), null, 13 },
                    { new Guid("3f2bd26d-6429-45af-84d2-17e6135f5c08"), null, 1 },
                    { new Guid("41cd3c34-fbbb-4602-b4a1-842f8a12f857"), null, 9 },
                    { new Guid("4c5ee02b-f560-433b-8e05-c02040f793d2"), null, 6 },
                    { new Guid("4ffeb940-cf64-433d-a263-efe39daf41aa"), null, 2 },
                    { new Guid("546e7559-4f61-4d63-b848-a21f7d4f0abb"), null, 4 },
                    { new Guid("6e5b62cc-2633-429c-96bd-fbfd1759ac97"), null, 8 },
                    { new Guid("8607c335-37ac-4e37-b342-caf081c3206c"), null, 5 },
                    { new Guid("8db55bea-fb46-4de9-80cf-4146db28e34b"), null, 10 },
                    { new Guid("a127544d-0a72-44be-adbf-328faa6c09b8"), null, 15 },
                    { new Guid("a2141563-297c-44a0-807b-1e20cb16014e"), null, 11 },
                    { new Guid("adab007e-71ce-4030-8dc8-62b1e1674820"), null, 20 },
                    { new Guid("cbc55f31-c410-497c-9a43-fdf4f92f6b5a"), null, 7 },
                    { new Guid("df5501e0-6396-4e11-87d7-ccf62e8ece0c"), null, 14 },
                    { new Guid("f18c859b-4b0b-4119-8d52-27ff79bcec08"), null, 3 },
                    { new Guid("f5a374b9-222f-4a62-8c54-e459bf339758"), null, 16 },
                    { new Guid("f713318b-2db5-455e-b379-aba3e639abf5"), null, 18 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { "CROSSFIT", "Кроссфит", 500 },
                    { "CRYOSAUNA", "Криосауна", 1000 },
                    { "POOL", "Бассейн", 200 },
                    { "SAUNA", "Сауна", 0 },
                    { "SOLARIUM", "Солярий", 400 }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "Name", "Patronymic", "Phone", "Status", "Surname" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Алексей", "Сергеевич", "+7-999-111-11-11", 0, "Иванов" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Мария", "Ивановна", "+7-999-444-44-44", 0, "Петрова" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Birthday", "Email", "IsActive", "LockerId", "Name", "Patronymic", "Phone", "Surname", "TrainerId" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), new DateOnly(1990, 5, 15), "ivan@example.com", true, null, "Иван", "Алексеевич", "+7-999-222-22-22", "Петров", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LockerId",
                table: "Clients",
                column: "LockerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TrainerId",
                table: "Clients",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ServicesId",
                table: "ClientServices",
                column: "ServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Lockers_Number",
                table: "Lockers",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientServices");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Lockers");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
