using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TidesOfMadness
{
    public class PlayerInput
    {
        private BindingList<Card> SelectedCards;
        public BindingList<Card> SelectedCardsTestProperty
        {
            get { return SelectedCards; }
            set { SelectedCards = value; }
        }

        public SuitOption SelectedSuit;
        public MadnessBonus SelectedBonus;

        public PlayerInput()
        {
            SelectedCards = new BindingList<Card>();
        }
    }
}