using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace GcseChineseApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DetailA = table.Column<string>(nullable: true),
                    DetailB = table.Column<string>(nullable: true),
                    DetailC = table.Column<string>(nullable: true),
                    Level = table.Column<string>(nullable: false),
                    Marks = table.Column<byte>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false),
                    Timing = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentId);
                });
            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    ThemeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.ThemeId);
                });

            migrationBuilder.CreateTable(
                name: "Exampaper",
                columns: table => new
                {
                    ExampaperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Level = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exampaper", x => x.ExampaperId);
                    table.ForeignKey(
                        name: "FK_Exampaper_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ThemeThemeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topic_Theme_ThemeThemeId",
                        column: x => x.ThemeThemeId,
                        principalTable: "Theme",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Restrict);
                });
            
            migrationBuilder.CreateTable(
                name: "Resource",
                columns: table => new
                {
                    ResourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsOfficial = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    TopicTopicId = table.Column<int>(nullable: true),
                    Uri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource", x => x.ResourceId);
                    table.ForeignKey(
                        name: "FK_Resource_Topic_TopicTopicId",
                        column: x => x.TopicTopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Restrict);
                });
           }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Exampaper");
            migrationBuilder.DropTable("Resource");
            migrationBuilder.DropTable("Assessment");
            migrationBuilder.DropTable("Topic");
            migrationBuilder.DropTable("Theme");
        }
    }
}
