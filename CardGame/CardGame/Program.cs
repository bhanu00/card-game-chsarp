using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    class Program
    {
        static void Main()
        {
            // Initialize the default Deck and players draw pile and discard pile

            Game.Initialize(out Player p1, out Player p2);

            // Play the game for two players
            Game.Play(p1, p2);

            Console.ReadLine();
        }
    }
}
