using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrigamiHelper.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complexities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Difficulty = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complexities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    IdentityUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ComplexityId = table.Column<int>(type: "integer", nullable: false),
                    SourceId = table.Column<int>(type: "integer", nullable: false),
                    StepCount = table.Column<int>(type: "integer", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModelImg = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Complexities_ComplexityId",
                        column: x => x.ComplexityId,
                        principalTable: "Complexities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Models_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelPapers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModelId = table.Column<int>(type: "integer", nullable: false),
                    PaperId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelPapers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelPapers_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelPapers_Papers_PaperId",
                        column: x => x.PaperId,
                        principalTable: "Papers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false),
                    StepNumber = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserProfileId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RequestId = table.Column<int>(type: "integer", nullable: false),
                    ResponderId = table.Column<int>(type: "integer", nullable: false),
                    Media = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responses_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", null, "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b2c3d4-5678-4eab-9fc1-100000000001", 0, "8f25155d-15e6-414c-998f-c20f3f37d846", "fan1@example.com", false, false, null, "FAN1@EXAMPLE.COM", "ORIGAMIFAN1", "AQAAAAIAAYagAAAAEOGSjpxhssO+k0Wc8lZ7+9sEh5FG+ssl4bJS67Vy18HTa4MNEZnEPzBLRXMD8WlkBQ==", null, false, "aa56d604-35e0-4c26-9468-d2045681f33c", false, "origamifan1" },
                    { "a1b2c3d4-5678-4eab-9fc1-100000000002", 0, "2ed14192-e23d-4768-94ae-2463bbe64179", "folder@example.com", false, false, null, "FOLDER@EXAMPLE.COM", "PAPERFOLDER", "AQAAAAIAAYagAAAAEEWWIt6MLs02qcAbeeOwXaC5++epvWfZUScQoqKUb7x6IX7oDX1HslyVNjWP4H891A==", null, false, "47b6e66d-09fb-4cf4-adfc-a8a2cbfc64ed", false, "paperfolder" },
                    { "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f", 0, "fb73066b-01ac-4782-96f1-8dcd88141c0a", "admina@strator.comx", false, false, null, null, null, "AQAAAAIAAYagAAAAEHwl1YfWVxwtLTSAnyCOMdVxk13cyqd1fDAzTEKQ+gef5b9pfeibbhv6/lxdI4CJSw==", null, false, "7622f53a-d009-46a7-8729-b985fa4b6a79", false, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Complexities",
                columns: new[] { "Id", "Difficulty" },
                values: new object[,]
                {
                    { 1, "Simple" },
                    { 2, "Low Intermediate" },
                    { 3, "Intermediate" },
                    { 4, "High Intermediate" },
                    { 5, "Complex" },
                    { 6, "Super Complex" }
                });

            migrationBuilder.InsertData(
                table: "Papers",
                columns: new[] { "Id", "Brand" },
                values: new object[,]
                {
                    { 1, "Kami" },
                    { 2, "Washi" },
                    { 3, "Foil" }
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Vietnamese Origami Group Vol. 1" },
                    { 2, "Works of Satoshi Kamiya 1995-2003" },
                    { 3, "Works of Katsuta Kyohei" },
                    { 4, "Origami Insects and Their Kin" },
                    { 5, "Origami Design Secrets" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f" });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Address", "FirstName", "IdentityUserId", "LastName" },
                values: new object[,]
                {
                    { 1, "101 Main Street", "Admina", "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f", "Strator" },
                    { 2, "22 Fold Street", "Olivia", "a1b2c3d4-5678-4eab-9fc1-100000000001", "Cranes" },
                    { 3, "88 Crease Lane", "Max", "a1b2c3d4-5678-4eab-9fc1-100000000002", "Valley" }
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Artist", "ComplexityId", "CreatedAt", "ModelImg", "SourceId", "StepCount", "Title", "UserProfileId" },
                values: new object[,]
                {
                    { 1, "Nguyen Hong Chuong", 4, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6242), "/Images/rat.png", 1, 121, "Cooking Rat", 1 },
                    { 2, "Satoshi Kamiya", 6, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6248), "/Images/dragon.jpg", 2, 274, "Ancient Dragon", 1 },
                    { 3, "Satoshi Kamiya", 6, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6249), "/Images/splash.jpg", 2, 49, "Splash", 1 },
                    { 4, "Robert Lang", 2, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6250), "/Images/leaf.jpg", 5, 32, "Maple Leaf", 2 },
                    { 5, "Katsuta Kyohei", 5, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6251), "/Images/octopus.png", 3, 125, "Octopus", 3 },
                    { 6, "John Montroll", 4, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6252), "/Images/owl.png", 4, 84, "Barn Owl", 2 }
                });

            migrationBuilder.InsertData(
                table: "ModelPapers",
                columns: new[] { "Id", "ModelId", "PaperId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 3 },
                    { 3, 2, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 3 },
                    { 6, 6, 1 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedAt", "Description", "ModelId", "StepNumber", "UserId", "UserProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 23, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6330), "Having trouble with the reverse fold. Not sure which layer to use.", 1, 5, 1, null },
                    { 2, new DateTime(2025, 6, 25, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6337), "Step 157's sink fold keeps tearing my paper. Is there a trick?", 2, 157, 1, null },
                    { 3, new DateTime(2025, 6, 26, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6338), "I'm not sure if the squash fold should go all the way through.", 2, 200, 1, null },
                    { 4, new DateTime(2025, 6, 24, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6338), "Step 12's squash fold keeps making my model asymmetrical.", 1, 12, 2, null },
                    { 5, new DateTime(2025, 6, 25, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6339), "This collapse is insane! Any tips on pre-creasing?", 2, 250, 3, null },
                    { 6, new DateTime(2025, 6, 22, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6340), "How do I sink the sides?", 3, 11, 2, null },
                    { 7, new DateTime(2025, 6, 23, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6341), "Can't figure out how the tentacles splay out. Help?", 5, 77, 3, null },
                    { 8, new DateTime(2025, 6, 25, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6467), "Squash fold here causes paper to rip. Paper too thick?", 6, 40, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Responses",
                columns: new[] { "Id", "CreatedAt", "Description", "Media", "RequestId", "ResponderId" },
                values: new object[] { 1, new DateTime(2025, 6, 24, 17, 57, 8, 361, DateTimeKind.Utc).AddTicks(6494), "Make sure you use an open sink instead of a closed sink.", "/Videos/splashvidstep11 (aud removed).mov", 6, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModelPapers_ModelId",
                table: "ModelPapers",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelPapers_PaperId",
                table: "ModelPapers",
                column: "PaperId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_ComplexityId",
                table: "Models",
                column: "ComplexityId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_SourceId",
                table: "Models",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_UserProfileId",
                table: "Models",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ModelId",
                table: "Requests",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserProfileId",
                table: "Requests",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestId",
                table: "Responses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IdentityUserId",
                table: "UserProfiles",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ModelPapers");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Papers");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Complexities");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
