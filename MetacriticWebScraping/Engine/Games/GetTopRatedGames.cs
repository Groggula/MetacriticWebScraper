using System;
using MetacriticWebScraping.Helpers;
using MetacriticWebScraping.Models;
using PuppeteerSharp;

namespace MetacriticWebScraping.Engine.Games;

public class GetTopRatedGames

{
    public static async Task<List<GameInfo>> FilterGameData(string url, Action<int, int> progressCallback, int count, bool displayPlatform)
    {
        var options = Launcher.GetLaunchOptions();

        var browser = await Puppeteer.LaunchAsync(options);
        var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        var gameList = await page.QuerySelectorAllAsync(".clamp-summary-wrap");

        var topGames = new List<GameInfo>();

        for (int i = 0; i < count; i++)
        {
            var gameElement = gameList[i];

            var titleHandle = await page.EvaluateFunctionHandleAsync(
                "el => el.querySelector('.title h3').innerText", gameElement);

            var metascoreHandle = await page.EvaluateFunctionHandleAsync(
                "el => el.querySelector('.clamp-metascore .metascore_w').innerText", gameElement);

            var userscoreHandle = await page.EvaluateFunctionHandleAsync(
                "el => el.querySelector('.clamp-userscore .metascore_w').innerText", gameElement);

            var releaseDateHandle = await page.EvaluateFunctionHandleAsync(
                "el => el.querySelector('.clamp-details > span').innerText", gameElement);


            string releaseDate = await releaseDateHandle.JsonValueAsync<string>();
            string title = await titleHandle.JsonValueAsync<string>();
            string metascore = await metascoreHandle.JsonValueAsync<string>();
            string userscore = await userscoreHandle.JsonValueAsync<string>();

            string platform = string.Empty;

            if (displayPlatform)
            {
                var platformHandle = await page.EvaluateFunctionHandleAsync(
                    "el => el.querySelector('.clamp-details .platform .data').innerText", gameElement);
                platform = await platformHandle.JsonValueAsync<string>();

            }

            progressCallback?.Invoke(i + 1, count);

            topGames.Add(new GameInfo
            {
                Title = title,
                ReleaseDate = releaseDate,
                Metascore = metascore,
                Userscore = userscore,
                Platform = platform
            });

        }

        await browser.CloseAsync();

        return topGames;
    }


    public static void DisplayTopGames(List<GameInfo> games, bool displayPlatform)
    {
        Console.Clear();

        if (displayPlatform)
        {
            Console.WriteLine("\tTitle\t\t\t\t\t\tMetascore\tUserscore\tRelease date\t\tPlatform");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("\tTitle\t\t\t\t\t\tMetascore\tUserscore\tRelease date");
            Console.WriteLine("--------------------------------------------------------------------------------------------------------");
        }

        for (int i = 0; i < games.Count; i++)
        {
            GameInfo game = games[i];
            var rowNumber = (i + 1) < 10 ? $" {i + 1}" : (i + 1).ToString();

            string title = game.Title!.Length > 50 ? string.Concat(game.Title.AsSpan(0, 47), "...") : game.Title;

            if (displayPlatform)
            {
                Console.WriteLine($"{rowNumber}: {title,-55} {game.Metascore,-14} {game.Userscore, -12} {game.ReleaseDate, -23} {game.Platform}");
            }
            else
            {
                Console.WriteLine($"{rowNumber}: {title,-55} {game.Metascore,-14} {game.Userscore,-12} {game.ReleaseDate}");
            }
        }
    }

    public static async Task PrintToConsole(string url, bool displayPlatform, int count = 10)
    {        
        Console.Write("Loading... ");
        var topGames = await FilterGameData(url, ProgressHelper.UpdateProgress, count, displayPlatform);

        DisplayTopGames(topGames, displayPlatform);

        Console.WriteLine();
        Console.WriteLine("Press any key to return to the menu...");
        Console.ReadKey();
    }
}


