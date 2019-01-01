using System;
using System.Collections.Generic;
using System.IO;

namespace TidesOfMadness
{
    public static class DeckGenerator
    {
        private static List<Suits> AllSuits = new List<Suits> { Suits.GreaterOldOnes, Suits.Locations, Suits.Manuscripts, Suits.OuterGods, Suits.Races };

        static string FileFolder = $"{Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName}\\CardImages";

        public static CardCollection GenerateDeck()
        {
            CardCollection deck = new CardCollection();

            deck.AddCardToCollection(
                new Card(
                    CardNames.Azathoth,
                    Suits.OuterGods,
                    false,
                    ScoreConditions.ScoreBySet,
                    3,
                    new List<Suits> { Suits.GreaterOldOnes },
                    GetImage("Azathoth")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Cthulhu,
                    Suits.GreaterOldOnes,
                    false,
                    ScoreConditions.ScoreBySet,
                    9,
                    new List<Suits> { Suits.OuterGods, Suits.Races, Suits.Locations },
                    GetImage("Cthulhu")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Dagon,
                    Suits.GreaterOldOnes,
                    true,
                    ScoreConditions.ScoreOneMajority,
                    7,
                    new List<Suits> { Suits.Races },
                    GetImage("Dagon")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Deep_Ones,
                    Suits.Races,
                    true,
                    ScoreConditions.ScoreBySet,
                    6,
                    new List<Suits> { Suits.GreaterOldOnes, Suits.Manuscripts },
                    GetImage("DeepOnes")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Dreamlands,
                    Suits.None,
                    false,
                    ScoreConditions.NoScore,
                    0,
                    null,
                    GetImage("Dreamlands")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Elder_Things,
                    Suits.Races,
                    false,
                    ScoreConditions.ScoreBySet,
                    3,
                    new List<Suits> { Suits.OuterGods },
                    GetImage("ElderThings")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Great_Race_Of_Yith,
                    Suits.Races,
                    true,
                    ScoreConditions.ScoreOneMajority,
                    7,
                    new List<Suits> { Suits.Manuscripts },
                    GetImage("GreatRaceOfYith")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Hastur,
                    Suits.GreaterOldOnes,
                    false,
                    ScoreConditions.ScoreBySet,
                    3,
                    new List<Suits> { Suits.Manuscripts },
                    GetImage("Hastur")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Innsmouth,
                    Suits.Locations,
                    false,
                    ScoreConditions.ScoreMissingSuits,
                    3,
                    AllSuits,
                    GetImage("Innsmouth")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Miskatonic_University,
                    Suits.None,
                    true,
                    ScoreConditions.ScoreEachMajority,
                    4,
                    AllSuits,
                    GetImage("MiskatonicUniversity")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Mountains_Of_Madness,
                    Suits.Locations,
                    false,
                    ScoreConditions.ScoreBySet,
                    3,
                    new List<Suits> { Suits.Races },
                    GetImage("MountainsOfMadness")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Necronomicon,
                    Suits.Manuscripts,
                    true,
                    ScoreConditions.ScoreByMadness,
                    1,
                    null,
                    GetImage("Necronomicon")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Nyarlathotep,
                    Suits.OuterGods,
                    false,
                    ScoreConditions.ScoreBySet,
                    13,
                    AllSuits,
                    GetImage("Nyarlathotep")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Pnakotic_Manuscripts,
                    Suits.Manuscripts,
                    true,
                    ScoreConditions.ScoreOneMajority,
                    7,
                    new List<Suits> { Suits.OuterGods },
                    GetImage("PnakoticManuscripts")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Rlyeh,
                    Suits.Locations,
                    true,
                    ScoreConditions.ScoreOneMajority,
                    7,
                    new List<Suits> { Suits.GreaterOldOnes },
                    GetImage("Rlyeh")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Shub_Niggurath,
                    Suits.None,
                    false,
                    ScoreConditions.NoScore,
                    0,
                    null,
                    GetImage("Shub_Niggurath")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Unaussprechlichen_Kulten,
                    Suits.Manuscripts,
                    false,
                    ScoreConditions.ScoreBySet,
                    3,
                    new List<Suits> { Suits.Locations },
                    GetImage("Unaussprechlichen_Kulten")
                )
            );

            deck.AddCardToCollection(
                new Card(
                    CardNames.Yog_Sothoth,
                    Suits.OuterGods,
                    true,
                    ScoreConditions.ScoreOneMajority,
                    7,
                    new List<Suits> { Suits.Locations },
                    GetImage("Yog_Sothoth")
                )
            );

            return deck;
        }

        private static string GetImage(string fileName)
        {
            return $"{FileFolder}\\{fileName}.jpg";
        }
    }
}
