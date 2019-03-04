using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TidesOfMadness
{
    public class PlayerInput
    {
        public BindingList<Card> SelectedCards;
        public SuitOption SelectedSuit;
        public MadnessBonus SelectedBonus;

        public PlayerInput()
        {
            SelectedCards = new BindingList<Card>();
        }
    }
}
