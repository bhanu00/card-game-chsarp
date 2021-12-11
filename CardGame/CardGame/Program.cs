using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // shuffle initial deck
            var deck = Deck.CreateDeck();
            deck = Deck.ShuffleDeck(deck.ToList(), deck.Count());
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

            var p1 = new Player("p1", p1DrawPile);
            var p2 = new Player("p2", p2DrawPile);

            Play(p1, p2);

            Console.ReadLine();
        }

        private static void Play(Player p1, Player p2)
        {
            var n1 = p1.DrawPile.Count();
            var n2 = p2.DrawPile.Count();
            var tempList = new List<Card>();

            while (p1.HasCards && p2.HasCards)
            {
                if (!p1.DrawPile.Any())
                {
                    // create and shuffle new deck from discarded pile 
                    var newDeck = Deck.ShuffleDeck(p1.DiscardedPile, p1.DiscardedPile.Count());
                    p1.DrawPile.AddRange(newDeck);

                    // after assigning to draw pile, empty the discarded pile
                    p1.DiscardedPile.Clear();

                    n1 = p1.DrawPile.Count();
                }

                if (!p2.DrawPile.Any())
                {
                    // create and shuffle new deck
                    var newDeck = Deck.ShuffleDeck(p2.DiscardedPile, p2.DiscardedPile.Count());
                    p2.DrawPile.AddRange(newDeck);

                    // after assigning to draw pile, empty the discarded pile
                    p2.DiscardedPile.Clear();
                    n2 = p2.DrawPile.Count();
                }

                var p1Card = p1.DrawPile[n1 - 1];
                p1.DrawPile.RemoveAt(n1 - 1);
                n1--;
                var p2Card = p2.DrawPile[n2 - 1];
                p2.DrawPile.RemoveAt(n2 - 1);
                n2--;
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
    }

    /// <summary>
    /// Deck container
    /// </summary>
    class Deck
    {
        static IList<Card> cards;
        static Deck()
        {
            cards = new List<Card>();
        }

        public static IList<Card> CreateDeck()
        {
            var suits = new string[] { "clubs", "diamonds", "hearts", "spades" };
            var ranks = new string[] { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10" }; //, "jack", "queen", "king"
            var values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    cards.Add(new Card(suits[i], ranks[j], values[j]));
                }
            }
            return cards;
        }

        // Shuffle the cards using fisher yates shuffle algorithm
        public static List<Card> ShuffleDeck(List<Card> deck, int n)
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

    /// <summary>
    /// card container
    /// </summary>
    public class Card
    {
        public string Suite { get; set; }

        public string Rank { get; set; }

        public int Value { get; set; }

        public Card(string suite, string rank, int value)
        {
            Suite = suite;
            Rank = rank;
            Value = value;
        }
    }

    /// <summary>
    /// Player container
    /// </summary>
    public class Player
    {
        public string Name { get; set; }

        public List<Card> DrawPile { get; set; }

        public List<Card> DiscardedPile { get; set; }

        public Player(string name, List<Card> cards)
        {
            Name = name;
            DrawPile = cards;
            DiscardedPile = new List<Card>();
        }

        public bool HasCards
        {
            get
            {
                if (DrawPile.Any() || DiscardedPile.Any())
                    return true;
                return false;
            }
        }
    }
}
