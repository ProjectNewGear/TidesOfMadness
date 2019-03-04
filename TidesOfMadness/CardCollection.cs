using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TidesOfMadness
{
    public class CardCollection
    {
        public BindingList<Card> CardsInCollection;

        public CardCollection()
        {
            CardsInCollection = new BindingList<Card>();
        }


        public void Shuffle()
        {
            //Randomize the cards in this CardCollection
            //Create a new list and add cards randomly from the old list to the new one
            //Then assign the new one to CardsInCollection
            for (int i = 0; i < 50; i++)
            {
                BindingList<Card> newList = new BindingList<Card>();
                while (CardsInCollection.Count != 0)
                {
                    Random random = new Random();
                    int cardToGrab = random.Next(0, CardsInCollection.Count);
                    newList.Add(CardsInCollection[cardToGrab]);
                    CardsInCollection.RemoveAt(cardToGrab);
                }
                CardsInCollection = newList;
            }
        }

        public Card GetTopCard()
        {
            //Check to see if this CardCollection is not empty
            //If it is, return null (log an error?)
            //If it is not, return the top card

            //TO DO: Add error logging if the list is empty;
            Card cardToReturn;

            if (CardsInCollection.Count > 0)
            {
                int LastCard = CardsInCollection.Count - 1;
                cardToReturn = CardsInCollection[LastCard];
                CardsInCollection.RemoveAt(LastCard);
                return cardToReturn;
            }

            return null;
        }

        public BindingList<Card> GetTopCards(int numberOfCards)
        {
            BindingList<Card> cardsToAdd = new BindingList<Card>();
            for (int i = 0; i < numberOfCards; i++)
            {
                cardsToAdd.Add(GetTopCard());
            }
            return cardsToAdd;
        }

        public void AddCardToCollection(Card cardToAdd)
        {
            CardsInCollection.Add(cardToAdd);
        }

        public void AddCardToCollection(BindingList<Card> cardsToAdd)
        {
            foreach (Card cardToAdd in cardsToAdd)
            {
                AddCardToCollection(cardToAdd);
            }
        }

        public Card GetCardByEnumName(CardNames cardName)
        {
            return this.CardsInCollection.FirstOrDefault(c => c.CardNameEnum == cardName);
        }

        public void MoveCardToAnotherCollection(Card card, CardCollection collection)
        {
            //TO DO: ERROR HANDLING
            this.CardsInCollection.Remove(card);
            collection.AddCardToCollection(card);
        }
    }
}
