using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class SuitOption
    {
        public Suits Suit { get; set; }
        public string Text { get; set; }

        public SuitOption(Suits suit, string text)
        {
            Suit = suit;
            Text = text;
        }
    }
}
