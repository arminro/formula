using F1.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace F1.Data.Migrations
{
    public partial class Seed : Migration
    {
        static Random rand = new Random();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i < 3; i++)
            {
                FormulaTeam team = new FormulaTeam()
                {
                    EntryfeePaid = rand.Next(0, 2) == 1,
                    Name = $"FormulaTeam{i}",
                    NumberOfChampionshipsWon = rand.Next(0, 11),
                    YearOfFoundation = rand.Next(1990, 2021),
                    Id = Guid.NewGuid()
                };

                migrationBuilder.InsertData(
                   table: "Teams",
                   columns: new string[] { "EntryfeePaid", "Name", "NumberOfChampionshipsWon", "YearOfFoundation", "Id" },
                   values: new object[] { team.EntryfeePaid, team.Name, team.NumberOfChampionshipsWon, team.YearOfFoundation, team.Id });

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
