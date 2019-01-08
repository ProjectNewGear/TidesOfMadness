using System;
using System.Collections.Generic;
using System.Text;

namespace TidesOfMadness
{
    public class AIPlayer : Player
    {
        Random random;

        public AIPlayer(string name): base(name)
        {
            random = new Random();
        }

        public Card ChooseCardToPlay()
        {
            //Choose which card to play

            //EASY: Choose at random
            //HARD: Choose based on cards in play - possibly multiple pick order lists

            //NEVER pick Shub Niggurath first, since it won't do anything

            Card cardToReturn = null;

            

            while (cardToReturn == null)
            {
                int cardToGrabIndex = random.Next(0, this.CardsInHand.CardsInCollection.Count);
                Card cardToCheck = this.CardsInHand.CardsInCollection[cardToGrabIndex];
                if ((cardToCheck.CardNameEnum == CardNames.Shub_Niggurath && this.GetCardsInHand().Count == 5) == false)
                {
                    cardToReturn = cardToCheck;
                }
            }
            return cardToReturn;
        }

        public MadnessBonus ChooseMadnessBonus()
        {
            //Choose to gain extra points or remove a madness token
            //The more Madness the AI has, the more likely it is to remove a Madness token
            Random random = new Random();

            int controlValue = random.Next(1, 10);

            if (controlValue <= this.MadnessTotal)
            {
                return MadnessBonus.GainPoints;
            }
            return MadnessBonus.RemoveMadness;
        }

        public List<Card> ChooseCardsToReturnToHand()
        {
            //Choose the five cards to return to hand at the end of the round
            //NEVER leave Shub Niggurath on the table, since it won't do anything
            return null;
        }
    }
}
