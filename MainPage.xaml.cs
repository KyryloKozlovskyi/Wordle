using System.Net;
//using Android.Graphics.Fonts;
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

        static Dictionary<char, int> CountIdenticalLetters(string input)
        {
            Dictionary<char, int> letterCounts = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    if (letterCounts.ContainsKey(c))
                    {
                        letterCounts[c]++;
                    }
                    else
                    {
                        letterCounts[c] = 1;
                    }
                }
            }

            return letterCounts;
        }

        string CheckUserInput(string str)
        {
            str = str.ToLower();
            if (IsWordInFile(fullPath, str))
            {
                return str;
            }
            else
            {
                DisplayAlert("Check your input", "Not in word list!", "OK");
                return "";
            }
        }
        bool IsWordInFile(string filePath, string userInput)
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);
            //DisplayAlert("TEST", "TEST IS WORD IN FILE: " + lines, "OK");
            // Check if any line is equal to the specified word
            return lines.Any(line => line.Equals(userInput));
        }

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

        private void NewWordle_OnClicked(object sender, EventArgs e)
        {
            // Enable entry field 
            UserGuess.IsVisible = true;
            UserGuess.IsEnabled = true;

            // Enable enter button
            EnterButton.IsVisible = true;
            EnterButton.IsEnabled = true;

            // Disable new wordle button
            NewWordle.IsVisible = false;
            NewWordle.IsEnabled = false;

            // Enable reset button
            ResetButton.IsVisible = true;
            ResetButton.IsEnabled = true;

            // Get a random word from the file
            //word = GetRandomWord(fullPath).ToLower();
            word = "razor"; //GetRandomWord(fullPath);
            test.Text = word;
        }

        private void EnterButton_OnClicked(object sender, EventArgs e)
        {
            // Dictionaries to keep track of letters
            Dictionary<char, int> letterCounts = CountIdenticalLetters(word);
            Dictionary<char, int> userInputLetterCounts = new Dictionary<char, int>();

            // User input + validation
            if (UserGuess.Text != null)
            {
                string userInput = UserGuess.Text.ToLower();
                {
                    if (CheckUserInput(userInput) != "")
                    {
                        // Keeps track of remaining attempts
                        tries++;

                        // Loop through each row
                        for (int row = tries - 1; row < tries; row++)
                        {
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
                                        char lw = word[column]; // Get a letter from the word

                                        label.Text = lui.ToString().ToUpper();

                                        // Change background color based on the letter
                                        if (lui == lw)
                                        {
                                            frame.BackgroundColor = Color.FromRgb(83, 141, 78); // Green
                                            letterCounts[lw]--; // Decrement for the used letter
                                        }
                                        else if (word.Contains(lui.ToString()) && letterCounts[lui] > 0)
                                        {
                                            // Check if the letter is used in the user input
                                            if (userInputLetterCounts.ContainsKey(lui) && userInputLetterCounts[lui] > 0)
                                            {
                                                // If the letter is already used, color it gray
                                                frame.BackgroundColor = Color.FromRgb(58, 58, 60); // Gray
                                            }
                                            else
                                            {
                                                frame.BackgroundColor = Color.FromRgb(181, 159, 59); // Yellow
                                                // Increment the count for the used letter in user input
                                                if (userInputLetterCounts.ContainsKey(lui))
                                                    userInputLetterCounts[lui]++;
                                                else
                                                    userInputLetterCounts[lui] = 1;
                                            }
                                        }
                                        else
                                        {
                                            // Else color the background gray
                                            frame.BackgroundColor = Color.FromRgb(58, 58, 60); // Gray
                                        }
                                    }
                                }
                            }

                            // Break the loop if the word is correct. Reset the game
                            if (userInput == word)
                            {
                                DisplayAlert("You got it right!",
                                    "The word is: " + word.ToUpper() + "\nIt took you " + tries + " tries!", "OK");
                                // Disable entry field
                                UserGuess.IsVisible = false;
                                UserGuess.IsEnabled = false;
                                break;
                            }
                            // break the loop if user guessed 6 times
                            else if (tries == 6)
                            {
                                // Disable reset btn
                                ResetButton.IsEnabled = false;
                                ResetButton.IsVisible = false;
                                // Enable new wordle btn
                                NewWordle.IsVisible = true;
                                NewWordle.IsEnabled = true;

                                DisplayAlert("You didn't get it!",
                                    "The word is: " + word.ToUpper() + "\n", "OK");

                                break;
                            }
                        }
                    }
                }
            }
        }

        // The CountIdenticalLetters method remains the same as before

        private void ResetButton_OnClicked(object sender, EventArgs e)
        {
            // Reset the current game and get a new word
        }
    }
}