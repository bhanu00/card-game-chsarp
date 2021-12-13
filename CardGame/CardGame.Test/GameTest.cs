using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CardGame
{
    public class GameTest
    {
        [Fact]
        public void IfDrawPile_IsEmpty_DiscardPile_Shoulebe_DrawPile()
        {
            // Arrange
            var p1cards = new List<Card>();
            p1cards.Add(new Card("club", "ace", 1));
            p1cards.Add(new Card("club", "2", 2));
            var p1 = new Player("p1", p1cards);
            int n1 = p1.DrawPile.Count();

            var p2cards = new List<Card>();
            p2cards.Add(new Card("diamonds", "3", 3));
            p2cards.Add(new Card("diamonds", "ace", 1));

            var p2 = new Player("p2", p2cards);
            int n2 = p1.DrawPile.Count();

            p1.DiscardedPile.AddRange(p1.DrawPile);
            p2.DiscardedPile.AddRange(p2.DrawPile);

            p1.DrawPile.Clear();
            p2.DrawPile.Clear();

            // Act
            Game.UseDiscardPileIfDrawPileIsEmpty(p1, p2, ref n1, ref n2);

            // Assert
            Assert.Equal(n1, p1.DrawPile.Count());
            Assert.Equal(n2, p2.DrawPile.Count());
            Assert.Empty(p1.DiscardedPile);
            Assert.Empty(p2.DiscardedPile);
            // TODO
            // new draw pile will be shuffled
        }

        [Fact]
        public void WhenCompareCards_HigherCard_ShouldWin()
        {
            // Arrange
            var p1cards = new List<Card>();
            var p1Card = new Card("club", "ace", 1);
            p1cards.Add(p1Card);
            p1cards.Add(new Card("club", "2", 2));
            var p1 = new Player("p1", p1cards);
            int n1 = p1.DrawPile.Count();

            var p2cards = new List<Card>();
            var p2Card = new Card("diamonds", "3", 3);
            p2cards.Add(p2Card);
            p2cards.Add(new Card("diamonds", "ace", 1));

            var p2 = new Player("p2", p2cards);
            int n2 = p1.DrawPile.Count();

            var tmpList = new List<Card>();
            // Act
            Game.CompareCardValueAndPrintOutput(p1, p2, n1, n2, tmpList, p1Card, p2Card);

            // Assert
            Assert.NotEmpty(p2.DiscardedPile);
        }

        [Fact]
        public void NextTurnWinner_ShouldWin4Cards_ForEqualCardValues_InPreviousTurn()
        {
            // Arrange
            var p1cards = new List<Card>();
            var p1Card = new Card("club", "ace", 1);
            p1cards.Add(p1Card);
            p1cards.Add(new Card("club", "2", 2));
            var p1 = new Player("p1", p1cards);
            int n1 = p1.DrawPile.Count();

            var p2cards = new List<Card>();
            var p2Card = new Card("diamonds", "ace", 1);
            p2cards.Add(p2Card);
            p2cards.Add(new Card("diamonds", "ace", 1));

            var p2 = new Player("p2", p2cards);
            int n2 = p1.DrawPile.Count();

            var tmpList = new List<Card>();

            // Act
            Game.CompareCardValueAndPrintOutput(p1, p2, n1, n2, tmpList, p1Card, p2Card);
            p1Card = new Card("club", "2", 2);
            p2Card = new Card("diamonds", "ace", 1);
            Game.CompareCardValueAndPrintOutput(p1, p2, n1, n2, tmpList, p1Card, p2Card);

            // Assert
            Assert.NotEmpty(p1.DiscardedPile);
            Assert.Equal(4, p1.DiscardedPile.Count());
            Assert.Empty(tmpList);
            // TODO check for next round winner , add templist cards to hid discard list
        }
    }
}
