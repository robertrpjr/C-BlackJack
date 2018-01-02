namespace CSC460BlackJack
{
    public interface CardInterface 
    {
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        /// <summary>
        /// gets/sets the rank (Ex. ace, jack, two...) 
        /// </summary>
        CardRank Rank { get; set;  }

        /// <summary>
        /// gets/sets the suid (clubs, diamonds, hearts, spades)
        /// </summary>
        CardSuit Suit { get; set; }

        /// <summary>
        /// boolean representing whether or not the card is face up
        /// </summary>
        bool FaceUp { get; set;}

        /// <summary>
        /// returns the game value of the card (for blackjack, ace = 11, queen = 10, etc)
        /// </summary>
        int Value { get; }

        /// <summary>
        /// returns a one character string representing the rank, 
        /// </summary>
        string RankLabel { get; }

        

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------

            /// <summary>
            /// 
            /// </summary>
            /// <returns>a string representing the object</returns>
        string ToString();

    }

}
