using System.Net;
using static Wordle.DownloadWords;

namespace Wordle
{
    //C:\Users\galin\AppData\Local\Packages\cf61b0af-98d7-4a63-9711-ea3b84acd9b2_9zz4h110yvjzm\LocalState
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            // Word file path
            var path = FileSystem.Current.AppDataDirectory;
            var fullPath = Path.Combine(path, "words.txt");

            static string GetRandomWord(string filePath)
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Use a random number generator to select a word
                Random random = new Random();
                int randomIndex = random.Next(0, lines.Length);
                string randomLine = lines[randomIndex];

                // Split the "line" into words
                string[] word = randomLine.Split(' ');
                return word[0];
            }

            InitializeComponent();
            DownloadWords download = new DownloadWords();
            test.Text += GetRandomWord(fullPath);
            // Select a random word from the saved file


        }
    }
}