using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public static class MadnessListOptionGenerator
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
    }
}
