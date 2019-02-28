using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public static class ScoreCalculator
    {
        private static List<Suits> realSuits = new List<Suits> { Suits.GreaterOldOnes, Suits.Locations, Suits.Manuscripts, Suits.OuterGods, Suits.Races };

        public static int CalculateScore(Player me, Player you)
        {
            int score = 0;

            foreach (Card card in me.CardsInPlay.CardsInCollection)
            {
                switch (card.ScoreCondition)
                {
                    case ScoreConditions.ScoreOneMajority:
                        {
                            score += CalculateScoreByOneMajority(card.SuitsToScore[0], card.ScoreValue, me.CardsInPlay, you.CardsInPlay);
                        }
                        break;
                    case ScoreConditions.ScoreEachMajority:
                        {
                            score += CalculateScoreByAllMajorities(card.ScoreValue, me.CardsInPlay, you.CardsInPlay);
                        }
                        break;
                    case ScoreConditions.ScoreBySet:
                        {
                            score += CalculateScoreBySet(card.SuitsToScore, card.ScoreValue, me.CardsInPlay);
                        }
                        break;
                    case ScoreConditions.ScoreMissingSuits:
                        {
                            score += CalculateScoreByMissingSuits(card.ScoreValue, me.CardsInPlay);
                        }
                        break;
                    case ScoreConditions.ScoreByMadness:
                        {
                            score += CalculateScoreByMadness(me.MadnessTotal);
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
            }
            return score;
        }

        public static int GetCountOfOneSuitInPlayerCollection(Suits suit, CardCollection playerCollection)
        {
            int cardsOfThisSuit = 0;
            foreach (Card currentCard in playerCollection.CardsInCollection)
            {
                if (currentCard.Suit == suit)
                {
                    cardsOfThisSuit++;
                }
            }
            return cardsOfThisSuit;
        }

        public static int CalculateScoreBySet(List<Suits> suitsToCheck, int pointsPerSet, CardCollection playerCollection)
        {
            int minimumSuits = 6; //Can never have more than this - 5 of a suit plus one copy;

            foreach (Suits currentSuit in suitsToCheck)
            {
                int cardsOfThisSuit = GetCountOfOneSuitInPlayerCollection(currentSuit, playerCollection);
                if (cardsOfThisSuit <= minimumSuits)
                {
                    minimumSuits = cardsOfThisSuit;
                }
            }

            return minimumSuits * pointsPerSet;
        }

        public static int CalculateScoreByOneMajority(Suits suitToCheck, int points, CardCollection playerCollection, CardCollection opponentCollection)
        {
            if (GetCountOfOneSuitInPlayerCollection(suitToCheck, playerCollection) > GetCountOfOneSuitInPlayerCollection(suitToCheck, opponentCollection))
            {
                return points;
            }
            else return 0;
        }

        public static int CalculateScoreByAllMajorities(int points, CardCollection playerCollection, CardCollection opponentCollection)
        {
            int score = 0;

            foreach (Suits suitToCheck in realSuits)
            {
                score += CalculateScoreByOneMajority(suitToCheck, points, playerCollection, opponentCollection);
            }

            return score;
        }

        public static int CalculateScoreByMissingSuits(int points, CardCollection playerCollection)
        {
            int score = 0;

            foreach(Suits suitToCheck in realSuits)
            {
                if(GetCountOfOneSuitInPlayerCollection(suitToCheck, playerCollection) == 0)
                {
                    score += points;
                }
            }
            return score;
        }

        public static int CalculateScoreByMadness(int myMadness)
        {
            return myMadness;
        }
    }
}
