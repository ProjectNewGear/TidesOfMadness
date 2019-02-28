using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidesOfMadness;

namespace TidesConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //GameDriver controller = new GameDriver();
            //controller.InitializeGame();

            ////foreach(Card card in controller.Deck.CardsInCollection)
            ////{
            ////    Console.WriteLine(card.GetDisplayName());
            ////}
            ////controller.Deck.Shuffle();
            ////Console.WriteLine();
            ////foreach (Card card in controller.Deck.CardsInCollection)
            ////{
            ////    Console.WriteLine(card.GetDisplayName());
            ////}
            ////Console.ReadKey();

            //Console.WriteLine("Player's hand: ");

            //foreach(Card card in controller.GameState.HumanPlayer.CardsInHand.CardsInCollection)
            //{
            //    Console.WriteLine(card.GetDisplayName());
            //}

            //Console.WriteLine();
            //Console.WriteLine("Opponent's hand: ");

            //foreach (Card card in controller.GameState.AIPlayer.CardsInHand.CardsInCollection)
            //{
            //    Console.WriteLine(card.GetDisplayName());
            //}

            //Console.ReadLine();

            CardCollection deck = DeckGenerator.GenerateDeck();


            CardCollection testHand = new CardCollection();

            testHand.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Pnakotic_Manuscripts));
            testHand.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Cthulhu));
            testHand.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Deep_Ones));
            testHand.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Rlyeh));
            testHand.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Elder_Things));

            Card testCard = deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Miskatonic_University);

            CardCollection testHand2 = new CardCollection();

            testHand2.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Miskatonic_University));
            testHand2.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Nyarlathotep));
            testHand2.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Shub_Niggurath));
            testHand2.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Mountains_Of_Madness));
            testHand2.AddCardToCollection(deck.CardsInCollection.First(c => c.CardNameEnum == CardNames.Hastur));

            Player testPlayer = new Player("me");
            testPlayer.CardsInPlay = testHand;

            Player testPlayer2 = new Player("you");
            testPlayer2.CardsInPlay = testHand2;

            int myScore = ScoreCalculator.CalculateScore(testPlayer, testPlayer2);
            int yourScore = ScoreCalculator.CalculateScore(testPlayer2, testPlayer);

            Console.WriteLine(myScore);
            Console.WriteLine(yourScore);

            Console.ReadKey();


        }
    }
}
