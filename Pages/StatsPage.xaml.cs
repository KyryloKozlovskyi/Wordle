using System.Text;
using System.Text.Json;
namespace Wordle;
public partial class StatsPage : ContentPage
{
    private String playerFilePath;
    private static string playerName;
    public StatsPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        playerFilePath = MainPage.playerFilePath;
        playerName = MainPage.playerName;
        base.OnAppearing();
        DisplayStats();
    }
    //   \ud83c\udf2b\ufe0f - Gray           \ud83d\udfe9 - Green             \ud83d\udfe8 - Yellow
    public async Task DisplayStats()
    {
        if (!string.IsNullOrWhiteSpace(playerName))
        {
            try
            {
                // Read the content of the player file
                string fileContent = await File.ReadAllTextAsync(playerFilePath);
                // Split the file content into lines
                string[] lines = fileContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                // Create a StringBuilder to build the final formatted string
                StringBuilder formattedStats = new StringBuilder();
                // Deserialize each line and append to the StringBuilder
                foreach (string line in lines)
                {
                    GameStats gameStats = JsonSerializer.Deserialize<GameStats>(line);
                    formattedStats.AppendLine($"Timestamp: {gameStats.Timestamp}\nCorrect Word: {gameStats.CorrectWord}\nNumber of Tries: {gameStats.NumberOfTries}\nGame Result: {gameStats.GameResult}");
                    formattedStats.AppendLine($"Emoji Grid:\n{gameStats.Colors[0]}{gameStats.Colors[1]}{gameStats.Colors[2]}{gameStats.Colors[3]}{gameStats.Colors[4]}");
                    formattedStats.AppendLine($"{gameStats.Colors[5]}{gameStats.Colors[6]}{gameStats.Colors[7]}{gameStats.Colors[8]}{gameStats.Colors[9]}");
                    formattedStats.AppendLine($"{gameStats.Colors[10]}{gameStats.Colors[11]}{gameStats.Colors[12]}{gameStats.Colors[13]}{gameStats.Colors[14]}");
                    formattedStats.AppendLine($"{gameStats.Colors[15]}{gameStats.Colors[16]}{gameStats.Colors[17]}{gameStats.Colors[18]}{gameStats.Colors[19]}");
                    formattedStats.AppendLine($"{gameStats.Colors[20]}{gameStats.Colors[21]}{gameStats.Colors[22]}{gameStats.Colors[23]}{gameStats.Colors[24]}");
                    formattedStats.AppendLine($"{gameStats.Colors[25]}{gameStats.Colors[26]}{gameStats.Colors[27]}{gameStats.Colors[29]}{gameStats.Colors[29]}");
                    //formattedStats.AppendLine($"\nEmoji Grid\n: {gameStats.Colors[5]}{gameStats.Colors[6]}{gameStats.Colors[7]}{gameStats.Colors[8]}{gameStats.Colors[9]}\n");
                    formattedStats.AppendLine();
                }
                // Display the formatted stats in a label (assuming you have a label named 'statsLabel')
                StatsLabel.Text = formattedStats.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading game stats: {ex.Message}");
            }
        }
        else
        {
            await DisplayAlert("Error", "Player name is required!", "OK");
        }
    }
}