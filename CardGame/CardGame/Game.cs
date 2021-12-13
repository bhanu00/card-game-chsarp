using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    /// <summary>
    /// This class is not following design principles and patterns , It's just simple implementation
    /// we can refactor the code , and can move methods to proper classes
    /// </summary>
    public class Game
    {
        public static void Initialize(out Player p1, out Player p2)
        {
            // create and shuffle initial deck
            var deck = Deck.Create();
            deck = Deck.Shuffle(deck.ToList(), deck.Count());
            int n = deck.Count();

            // Assign player 1 draw pile 
            var p1DrawPile = new List<Card>();
            for (int i = n - 1; i >= n / 2; i--)
            {
                p1DrawPile.Add(deck[i]);
            }

            // assign player 2 draw pile
            var p2DrawPile = new List<Card>();
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                p2DrawPile.Add(deck[i]);
            }

            p1 = new Player("p1", p1DrawPile);
            p2 = new Player("p2", p2DrawPile);
        }

        public static void Play(Player p1, Player p2)
        {
            var n1 = p1.DrawPile.Count();
            var n2 = p2.DrawPile.Count();
            var tempList = new List<Card>();

            while (p1.HasCards && p2.HasCards)
            {
                // if there is no card in draw pile then use discard pile cards
                UseDiscardPileIfDrawPileIsEmpty(p1, p2, ref n1, ref n2);

                var p1Card = p1.DrawPile[n1 - 1];
                p1.DrawPile.RemoveAt(n1 - 1);
                n1--;

                var p2Card = p2.DrawPile[n2 - 1];
                p2.DrawPile.RemoveAt(n2 - 1);
                n2--;

                // compare card values and print the output
                CompareCardValueAndPrintOutput(p1, p2, n1, n2, tempList, p1Card, p2Card);

                // add new line
                Console.WriteLine();
            }

            // Print the final winner name
            PrintTheFinalWinner(p1, p2);
        }

        /// <summary>
        /// use discard pile if draw pile is empty, we can move this method to player file , to 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        public static void UseDiscardPileIfDrawPileIsEmpty(Player p1, Player p2, ref int n1, ref int n2)
        {
            if (!p1.DrawPile.Any())
            {
                // create and shuffle new deck from discarded pile 
                var newDeck = Deck.Shuffle(p1.DiscardedPile, p1.DiscardedPile.Count());
                p1.DrawPile.AddRange(newDeck);

                // after assigning to draw pile, empty the discarded pile
                p1.DiscardedPile.Clear();

                n1 = p1.DrawPile.Count();
            }

            if (!p2.DrawPile.Any())
            {
                // create and shuffle new deck
                var newDeck = Deck.Shuffle(p2.DiscardedPile, p2.DiscardedPile.Count());
                p2.DrawPile.AddRange(newDeck);

                // after assigning to draw pile, empty the discarded pile
                p2.DiscardedPile.Clear();
                n2 = p2.DrawPile.Count();
            }
        }

        /// <summary>
        /// compare card values and prints the output
        /// // TODO refactoring is not done yet
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <param name="tempList"></param>
        /// <param name="p1Card"></param>
        /// <param name="p2Card"></param>
        public static void CompareCardValueAndPrintOutput(Player p1, Player p2, int n1, int n2, List<Card> tempList, Card p1Card, Card p2Card)
        {
            if (p1Card.Value > p2Card.Value)
            {
                // print output
                Console.WriteLine($"Player 1 ({n1 + 1 + p1.DiscardedPile.Count() + tempList.Count()} cards): {p1Card.Value}");
                Console.WriteLine($"Player 2 ({n2 + 1 + p2.DiscardedPile.Count()} cards): {p2Card.Value}");
                Console.WriteLine("Player 1 wins this round");

                // add card to discarded pile
                p1.DiscardedPile.Add(p1Card);
                p1.DiscardedPile.Add(p2Card);
                if (tempList.Any())
                {
                    p1.DiscardedPile.AddRange(tempList);
                    tempList.Clear();
                }

            }
            else if (p1Card.Value < p2Card.Value)
            {
                // print output
                Console.WriteLine($"Player 1 ({n1 + 1 + p1.DiscardedPile.Count()} cards): {p1Card.Value}");
                Console.WriteLine($"Player 2 ({n2 + 1 + p2.DiscardedPile.Count() + tempList.Count()} cards): {p2Card.Value}");
                Console.WriteLine("Player 2 wins this round");

                // add card to discarded pile
                p2.DiscardedPile.Add(p1Card);
                p2.DiscardedPile.Add(p2Card);
                if (tempList.Any())
                {
                    p2.DiscardedPile.AddRange(tempList);
                    tempList.Clear();
                }
            }
            else
            {
                tempList.Add(p1Card);
                tempList.Add(p2Card);
            }
        }

        private static void PrintTheFinalWinner(Player p1, Player p2)
        {
            if (p1.HasCards)
            {
                Console.WriteLine("Player 1 wins the game!");
            }
            else if (p2.HasCards)
            {
                Console.WriteLine("Player 2 wins the game!");
            }
        }
    }
}
