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

        public string GameLog { get; set; }
        public bool RequirePlayerInput { get; set; }

        public void AppendToGameLog(string textToAppend)
        {
            GameLog += textToAppend + Environment.NewLine;
        }
    }
}