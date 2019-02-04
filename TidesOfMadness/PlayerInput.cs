using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class PlayerInput
    {
        public List<Card> SelectedCards;
        public SuitOption SelectedSuit;
        public MadnessBonus SelectedBonus;

        public PlayerInput()
        {
            SelectedCards = new List<Card>();
        }
    }
}
