using System.Net;
//using Kotlin.Ranges;
using static Wordle.DownloadWords;

namespace Wordle
{
    //C:\Users\galin\AppData\Local\Packages\cf61b0af-98d7-4a63-9711-ea3b84acd9b2_9zz4h110yvjzm\LocalState
    public partial class MainPage : ContentPage
    {
        private static String path;
        private static String fullPath;
        private static String word;
        private int tries = 0;

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

        public MainPage()
        {
            // Word file path
            path = FileSystem.Current.AppDataDirectory;
            fullPath = Path.Combine(path, "words.txt");
            InitializeComponent();
            // Download word list
            DownloadWords download = new DownloadWords();
        }

        private void EnterButton_OnClicked(object sender, EventArgs e)
        {
            // Start the game
            if (word == null)
            {
                // Enable entry. Change button text to "Enter"
                UserGuess.IsVisible = true;
                EnterButton.Text = "Enter";

                // Get a random word from the file
                word = GetRandomWord(fullPath).ToLower();
                test.Text = word;
            }
            else
            {
                tries++;
                // User input
                string userInput = UserGuess.Text.ToLower();

                // Loop through each row
                for (int row = tries - 1; row < tries; row++)
                {
                    //test.Text += WordleGrid.RowDefinitions.Count;
                    // Loop through each column
                    for (int column = 0; column < WordleGrid.ColumnDefinitions.Count; column++)
                    {
                        // Get the Frame at the current row and column
                        Frame frame = WordleGrid.Children
                            .OfType<Frame>()
                            .FirstOrDefault(f => Grid.GetRow(f) == row && Grid.GetColumn(f) == column);

                        if (frame != null)
                        {
                            // Access the Label inside the Frame
                            Label label = frame.Content as Label;

                            if (label != null)
                            {
                                char lui = userInput[column]; // Get a letter from user input
                                char lw = word[column]; // Get a letter from the random word

                                //test.Text += "LUI = " + lui + " ************* lW = " + lw;
 
                                label.Text = lui.ToString().ToUpper(); 

                                // Change background color based
                                 if(lui == lw)
                                {
                                    frame.BackgroundColor = Color.FromRgb(83, 141, 78); // Green
                                }
                                else if (word.Contains(lui.ToString()))
                                {
                                    frame.BackgroundColor = Color.FromRgb(181, 159, 59); // Yellow
                                }
                                else
                                {
                                    frame.BackgroundColor = Color.FromRgb(58, 58, 60); // Gray
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}