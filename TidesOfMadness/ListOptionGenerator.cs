using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public static class ListOptionGenerator
    {
        public static List<ResolveMadnessOption> GenerateMadnessListOptions(Player player)
        {
            List<ResolveMadnessOption> options = new List<ResolveMadnessOption>();

            options.Add
                (
                    new ResolveMadnessOption(MadnessBonus.GainPoints, "Gain 4 Points")
                );

            if (player.MadnessTotal > 0)
            {
                options.Add
                    (
                        new ResolveMadnessOption(MadnessBonus.RemoveMadness, "Remove 1 Madness Token")
                    );
            }

            return options;
        }

        public static List<SuitOption> GenerateSuitOptions()
        {
            List<SuitOption> options = new List<SuitOption>()
            {
                    new SuitOption(Suits.Races, "Races (Pink)"),
                    new SuitOption(Suits.Locations, "Locations (Red)"),
                    new SuitOption(Suits.OuterGods, "Outer Gods (Yellow)"),
                    new SuitOption(Suits.GreaterOldOnes, "Greater Old Ones (Blue)"),
                    new SuitOption(Suits.Manuscripts, "Manuscripts (Green)")
            };

            return options;
        }
    }
}
