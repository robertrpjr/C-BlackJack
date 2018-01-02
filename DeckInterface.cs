namespace CSC460BlackJack
{
    /// <summary>
    /// interface for deck class
    /// </summary>
    interface DeckInterface
    {

        /// <summary>
        /// returns the location of the cut card
        /// </summary>
        int CutCardLocation
        {
            get;
        }

        /// <summary>
        /// returns the current number of decks being used
        /// </summary>
        int NumberOfDecks
        {
            get;
        }

        /// <summary>
        /// returns the current size of the deck (the number of cards left
        /// </summary>
        int Size
        {
            get;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------



        // 
        /// <summary>
        /// 
        /// put all cards into the deck in a random order
        /// </summary>
        void shuffle();

     
      
    }
}
