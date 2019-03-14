using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class GameStateTracker
    {
        public Player HumanPlayer { get; set; }
        public AIPlayer AIPlayer { get; set; }
        public CardCollection Deck { get; set; }
        public GameStates CurrentGameState { get; set; }
        public int CurrentRound { get; set; }
        public CardCollection DiscardPile { get; set; }

        public string GameLog { get; set; }
        public bool RequirePlayerInput { get; set; }

        public Player PlayerWithMostMadnessThisRound {get; set;}

        public void AppendToGameLog(string textToAppend)
        {
            GameLog += textToAppend + Environment.NewLine;
        }

        private string CodeDuplicationTest(int param1, int param2)
        {
            int var1;
            int var2;

            if (param1 == param2)
            {
                var1 = 5;
                var2 = 3;
            }
            else if (param1 * 2 == param2)
            {
                var1 = 6;
                var2 = 3;
            }
            else
            {
                var1 = 4;
                var2 = var1 + 7;
            }

            string var3 = Convert.ToString(var1 + var2);

            return var3;
        }
    }
}