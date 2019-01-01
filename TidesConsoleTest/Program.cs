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
            GameDriver controller = new GameDriver();
            controller.InitializeGame();

            //foreach(Card card in controller.Deck.CardsInCollection)
            //{
            //    Console.WriteLine(card.GetDisplayName());
            //}
            //controller.Deck.Shuffle();
            //Console.WriteLine();
            //foreach (Card card in controller.Deck.CardsInCollection)
            //{
            //    Console.WriteLine(card.GetDisplayName());
            //}
            //Console.ReadKey();

            Console.WriteLine("Player's hand: ");

            foreach(Card card in controller.GameState.HumanPlayer.CardsInHand.CardsInCollection)
            {
                Console.WriteLine(card.GetDisplayName());
            }

            Console.WriteLine();
            Console.WriteLine("Opponent's hand: ");

            foreach (Card card in controller.GameState.AIPlayer.CardsInHand.CardsInCollection)
            {
                Console.WriteLine(card.GetDisplayName());
            }

            Console.ReadLine();
        }
    }
}
