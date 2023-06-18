using System;
using MetacriticWebScraping.Engine.Games;

namespace MetacriticWebScraping.Engine;

public class Menu
{
    public static async Task MainMenu()
    {
        string url;

        Console.Clear();
        Console.WriteLine("--- METACRITIC WEB SCRAPER ---");
        Console.WriteLine("");
        Console.WriteLine("Choose an option from the menu below:");
        Console.WriteLine("1. Get current top rated games");
        Console.WriteLine("2. Get current top rated games by platform");
        Console.WriteLine("3. Get all time high rated games");
        Console.WriteLine("4. Exit");

        Console.Write("Enter your choice: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                url = "https://www.metacritic.com/game";
                await GetTopRatedGames.PrintToConsole(url, false, 10);
                break;
            case "2":
                await ChoosePlatformMenu();
                break;
            case "3":
                url = "https://www.metacritic.com/browse/games/score/metascore/all/all/filtered";
                await GetTopRatedGames.PrintToConsole(url, true, 50);
                break;
            case "4":
                Console.WriteLine("Exiting the program. Goodbye!");
                Environment.Exit(0);
                return; 
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
        await MainMenu();
    }

    public static async Task ChoosePlatformMenu()
    {
        Console.Clear();
        Console.WriteLine("Choose a platform:");
        Console.WriteLine("1. PC");
        Console.WriteLine("2. PlayStation");
        Console.WriteLine("3. Xbox");
        Console.WriteLine("4. Switch");
        Console.WriteLine("5. Go back to the main menu");

        Console.Write("Enter your choice: ");
        var platformChoice = Console.ReadLine();

        string platformUrl;
        switch (platformChoice)
        {
            case "1":
                platformUrl = "https://www.metacritic.com/game/pc";
                await GetTopRatedGames.PrintToConsole(platformUrl, false, 10);
                break;
            case "2":
                platformUrl = "https://www.metacritic.com/game/playstation-5";
                await GetTopRatedGames.PrintToConsole(platformUrl, false, 10);
                break;
            case "3":
                platformUrl = "https://www.metacritic.com/game/xbox-series-x";
                await GetTopRatedGames.PrintToConsole(platformUrl, false, 10);
                break;
            case "4":
                platformUrl = "https://www.metacritic.com/game/switch";
                await GetTopRatedGames.PrintToConsole(platformUrl, false, 10);
                break;
            case "5":
                Console.WriteLine("Returning to the main menu.");
                await MainMenu();
                return;
            default:
                Console.WriteLine("Invalid platform choice. Please try again.");
                return;
        }

        await ChoosePlatformMenu();
    }
}

