using System;
namespace MetacriticWebScraping.Helpers
{
	public static class ProgressHelper
	{
        public static void UpdateProgress(int current, int total)
        {
            if (current % 5 == 0 || current == total)
            {
                int progressPercentage = (int)((double)current / total * 100);
                Console.Write($"\rLoading... {progressPercentage}%    ");
            }
        }
    }
}

