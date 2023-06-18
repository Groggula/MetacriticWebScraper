using System;
using PuppeteerSharp;

namespace MetacriticWebScraping.Engine
{
	public class Launcher
	{
        public static LaunchOptions GetLaunchOptions()
        {
            return new LaunchOptions
            {
                Headless = true,

                // change this to wherever your browsers .exe is located.
                ExecutablePath = "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
            };
        }
    }
}

