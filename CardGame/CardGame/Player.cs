using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    /// <summary>
    /// Player container
    /// </summary>
    public class Player
    {
        /// <summary>
        /// player name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// player's draw pile
        /// </summary>
        public List<Card> DrawPile { get; set; }

        /// <summary>
        /// player's discard pile
        /// </summary>
        public List<Card> DiscardedPile { get; set; }

        /// <summary>
        /// creates an instance of <see cref="Player"/>
        /// </summary>
        /// <param name="name">player name</param>
        /// <param name="cards">player cards</param>
        public Player(string name, List<Card> cards)
        {
            Name = name;
            DrawPile = cards;
            DiscardedPile = new List<Card>();
        }

        /// <summary>
        /// Check if a player has card or not in either draw pile or discard pile
        /// </summary>
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
