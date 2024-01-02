using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace Wordle
{
    // Game Stats for StatsPage output
    public class GameStats
    {
        public DateTime Timestamp
        {
            get;
            set;
        }
        public string CorrectWord
        {
            get;
            set;
        }
        public int NumberOfTries
        {
            get;
            set;
        }
        public string GameResult
        {
            get;
            set;
        }
        public List<string> Colors
        {
            get;
            set;
        }
    }
}