namespace CardGame
{
    /// <summary>
    /// card container
    /// </summary>
    public class Card
    {
        /// <summary>
        /// card Suite
        /// </summary>
        public string Suite { get; set; }

        /// <summary>
        /// Card Rank
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// Card Value
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// creates an instance of <see cref="Card"/>
        /// </summary>
        /// <param name="suite"></param>
        /// <param name="rank"></param>
        /// <param name="value"></param>
        public Card(string suite, string rank, int value)
        {
            Suite = suite;
            Rank = rank;
            Value = value;
        }
    }
}
