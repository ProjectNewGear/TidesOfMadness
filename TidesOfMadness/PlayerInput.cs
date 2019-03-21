using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TidesOfMadness
{
    public class PlayerInput
    {
        public BindingList<Card> SelectedCards { get; set; }
        public SuitOption SelectedSuit { get; set; }
        public MadnessBonus SelectedBonus { get; set; }

        public PlayerInput()
        {
            SelectedCards = new BindingList<Card>();
        }
    }
}
