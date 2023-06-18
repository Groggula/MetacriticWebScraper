using MetacriticWebScraping.Models;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetacriticWebScraping.Engine.Games;
using MetacriticWebScraping.Engine;

namespace MetacriticWebScraping;

class Program
{
    static async Task Main()
    {
        await Menu.MainMenu();
    }
}



