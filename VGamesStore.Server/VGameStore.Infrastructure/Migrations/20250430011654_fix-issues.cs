﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGameStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixissues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_GameId",
                table: "Genres",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Games_GameId",
                table: "Genres",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Games_GameId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_GameId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Genres");
        }
    }
}
