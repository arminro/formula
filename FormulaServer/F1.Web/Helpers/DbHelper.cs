using F1.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using F1Web.DataAccess.Interfaces;
using F1Web.Security;
using System;
using System.Threading.Tasks;

namespace F1Web.Helpers
{
    /// <summary>
    /// Class serving as a helper in generating the test user
    /// </summary>
    public class DbHelper
    {
        public static async Task SetupTestDb(IServiceProvider serviceProvider, AppSettings appSettings)
        {         
            await SetupTestUser(appSettings, serviceProvider);
            await SetupTestTeams(serviceProvider);
        }

        private static async Task SetupTestUser(AppSettings appSettings, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var jwtProvider = serviceProvider.GetRequiredService<ITokenService>();
            var testUserExist = await userManager.FindByNameAsync(appSettings.TestUserName);

            //var passHash = jwtProvider.GenerateToken(appSettings.TestPassword);

            if (testUserExist == null)
            {
                await userManager.CreateAsync(new User()
                {
                    UserName = appSettings.TestUserName,
                }, appSettings.TestPassword);
            }
        }

        private static async Task SetupTestTeams(IServiceProvider serviceProvider)
        {
            var playerService = serviceProvider.GetRequiredService<IRepository<FormulaTeam>>();
            Random rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                FormulaTeam team = new FormulaTeam()
                {
                    EntryfeePaid = rand.Next(0, 2) == 1,
                    Name = $"FormulaTeam{i}",
                    NumberOfChampionshipsWon = rand.Next(0, 11),
                    YearOfFoundation = rand.Next(1990, 2021)
                };

                await playerService.CreateAsync(team);
            }
        }
    }
}
