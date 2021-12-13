using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    /// <summary>
    /// Deck container
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// available cards in deck
        /// </summary>
        static IList<Card> cards;

        /// <summary>
        /// creating basic deck of 52 cards
        /// </summary>
        /// <returns></returns>
        public static IList<Card> Create()
        {
            cards = new List<Card>();
            var suits = new string[] { "clubs", "diamonds", "hearts", "spades" };
            var ranks = new string[] { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10" }; //, "jack", "queen", "king"
            var values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //, 11, 12, 13 

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    cards.Add(new Card(suits[i], ranks[j], values[j]));
                }
            }
            return cards;
        }

        /// <summary>
        /// Shuffle the cards using fisher yates shuffle algorithm
        /// </summary>
        /// <param name="deck">List of cards known as deck</param>
        /// <param name="n">length of deck</param>
        /// <returns></returns>
        public static List<Card> Shuffle(List<Card> deck, int n)
        {
            Random random = new Random();

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);

                // swap the array elements
                var temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
            return deck;
        }
    }
}
