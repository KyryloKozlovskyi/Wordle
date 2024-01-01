﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public class DownloadWords
    {
        // Constructor used to run the code
        public DownloadWords()
        {
            Download();
        }

        // Run the DownloadData and SaveToFile methods
        public static async Task Download()
        {
            // Full path 
            var path = FileSystem.Current.AppDataDirectory;
            var fullPath = Path.Combine(path, "words.txt");

            // Url
            string url = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/words.txt";

            // Check if the file exists
            if (!File.Exists(fullPath))
            {
                // If not, download the words and save it to the file
                try
                {
                    string content = await DownloadData(url);
                    SaveToFile(fullPath, content);
                    //await Shell.Current.DisplayAlert("Saved!", "Words file has been saved!", "OK!");

                }
                catch (Exception ex)
                {
                    //await Shell.Current.DisplayAlert("Error occured!", "Words file hasn't been saved!", "ERROR!");
                }
            }
        }

        // Download data from the website
        static async Task<string> DownloadData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }

        // Save downloaded data to a file
        static void SaveToFile(string filePath, string content)
        {
            System.IO.File.WriteAllText(filePath, content);
        }
    }
}
