using System.Linq;
using Xunit;

namespace CardGame
{
    public class DeckTest
    {
        [Fact]
        public void Deck_ShouldHave_FourtyCards()
        {
            // Arrange
            var deckDefaultLength = 40;

            // Act
            var deck = Deck.Create();

            // Assert
            Assert.Equal(deckDefaultLength, deck.Count());
        }

        [Fact]
        public void Shuffle_ShouldShuffleThe_Deck()
        {
            // Arrange
            var deck = Deck.Create();

            // Act
            var shuffledDeck = Deck.Shuffle(deck.ToList(), deck.Count());

            // Assert
            Assert.NotSame(deck, shuffledDeck);
            // TODO
            // Need to check both collections list that they have different order
        }
    }
}
