using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class ResolveMadnessOption
    {
        public MadnessBonus Bonus { get; set; }
        public string Text { get; set; }

        public ResolveMadnessOption(MadnessBonus bonus, string text)
        {
            Bonus = bonus;
            Text = text;
        }
    }
}
