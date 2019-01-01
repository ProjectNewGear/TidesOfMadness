using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public static class ScoringLogic
    {
        public static int ScoreOneMajority(CardCollection myPlayedCards, CardCollection opponentPlayedCards, Suits suitToScore, int scoreValue, bool isDoubled)
        {
            int mySuitCards = CountCardsOfSuitInPlay(myPlayedCards, suitToScore);
            int opponentSuitCards = CountCardsOfSuitInPlay(opponentPlayedCards, suitToScore);

            if (mySuitCards > opponentSuitCards)
            {
                return scoreValue * getMultiplier(isDoubled);
            }

            return 0;
        }

        public static int ScoreEachMajority(CardCollection myPlayedCards, CardCollection opponentPlayedCards, List<Suits> suitsToScore, int scoreValue, bool isDoubled)
        {
            int totalScore = 0;

            foreach(Suits currentSuit in suitsToScore)
            {
                totalScore = totalScore + ScoreOneMajority(myPlayedCards, opponentPlayedCards, currentSuit, scoreValue, isDoubled);
            }

            return totalScore;
        }

        public static int ScoreBySet(CardCollection myPlayedCards, List<Suits> suitsInSet, int scorePerSet, bool isDoubled)
        {
            int lowestSuitCount = 4; //No way to go higher than this

            foreach(Suits currentSuit in suitsInSet)
            {
                int cardsOfThisSuit = CountCardsOfSuitInPlay(myPlayedCards, currentSuit);
                if (cardsOfThisSuit < lowestSuitCount)
                {
                    lowestSuitCount = cardsOfThisSuit;
                }
            }

            return lowestSuitCount * scorePerSet * getMultiplier(isDoubled);
        }

        public static int ScoreMissingSuits(CardCollection myPlayedCards, List<Suits> suitsInSet, int scorePerSuit, bool isDoubled)
        {
            int missingSuits = 0;

            foreach (Suits currentSuit in suitsInSet)
            {
                if (CountCardsOfSuitInPlay(myPlayedCards, currentSuit) == 0)
                {
                    missingSuits++;
                }

            }

            return missingSuits * scorePerSuit * getMultiplier(isDoubled);
        }

        public static int ScoreByMadness(Player player, bool isDoubled)
        {
            return player.MadnessTotal * getMultiplier(isDoubled);
        }

        private static int CountCardsOfSuitInPlay(CardCollection playerCards, Suits SuitToCheck)
        {
            int numberOfCards = 0;

            foreach (Card card in playerCards.CardsInCollection)
            {
                if (card.Suit == SuitToCheck)
                {
                    numberOfCards++;
                }
            }

            return numberOfCards;
        }

        private static int getMultiplier(bool isDoubled)
        {
            if (isDoubled)
            {
                return 2;
            }
            return 1;
        }
    }
}
