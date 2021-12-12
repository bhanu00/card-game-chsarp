using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Initialize(out Player p1, out Player p2);

            Play(p1, p2);

            Console.ReadLine();
        }

        private static void Initialize(out Player p1, out Player p2)
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

        /// <summary>
        /// Play the game and prints the output
        /// </summary>
        /// <param name="p1">player 1</param>
        /// <param name="p2">player 2</param>
        private static void Play(Player p1, Player p2)
        {
            var n1 = p1.DrawPile.Count();
            var n2 = p2.DrawPile.Count();
            var tempList = new List<Card>();

            while (p1.HasCards && p2.HasCards)
            {
                UseDiscardPileIfDrawPileIsEmpty(p1, p2, ref n1, ref n2);

                var p1Card = p1.DrawPile[n1 - 1];
                p1.DrawPile.RemoveAt(n1 - 1);
                n1--;
                var p2Card = p2.DrawPile[n2 - 1];
                p2.DrawPile.RemoveAt(n2 - 1);
                n2--;
                CompareCardValueAndPrintOutput(p1, p2, n1, n2, tempList, p1Card, p2Card);
                Console.WriteLine();
            }

            if (p1.HasCards)
            {
                Console.WriteLine("Player 1 wins the game!");
            }
            else if (p2.HasCards)
            {
                Console.WriteLine("Player 2 wins the game!");
            }
        }

        private static void CompareCardValueAndPrintOutput(Player p1, Player p2, int n1, int n2, List<Card> tempList, Card p1Card, Card p2Card)
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

        private static void UseDiscardPileIfDrawPileIsEmpty(Player p1, Player p2, ref int n1, ref int n2)
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
    }
}
