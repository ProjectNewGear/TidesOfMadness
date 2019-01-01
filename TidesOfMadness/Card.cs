using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class Card
    {
        public string Art { get; }

        public CardNames CardNameEnum { get; set; }

        public string CardNameDisplay { get; set; }

        public Suits Suit { get; set; }

        public bool HasMadness { get; set; }

        public ScoreConditions ScoreCondition { get; set; }

        public int ScoreValue { get; set; }

        public List<Suits> SuitsToScore { get; set; }

        public bool DoubleScore { get; set; }

        public string GetDisplayName()
        {
            if (CardNameEnum == CardNames.Rlyeh)
            {
                return "R'lyeh";
            }
            return CardNameEnum.ToString().Replace("_", " ");
        }

        public Card(CardNames name, Suits suit, bool madness, ScoreConditions scoreCondition, int scoreValue, List<Suits> suitsToScore, string fullImagePath)
        {
            CardNameEnum = name;
            CardNameDisplay = GetDisplayName();
            Suit = suit;
            HasMadness = madness;
            ScoreCondition = scoreCondition;
            ScoreValue = scoreValue;
            SuitsToScore = suitsToScore;
            DoubleScore = false;
            Art = fullImagePath;
        }
    }
}
