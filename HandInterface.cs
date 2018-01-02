namespace CSC460BlackJack
{
    public interface HandInterface<T> where T : CardInterface
    {
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        /// <summary>
        /// return the number of cards in hand
        /// </summary>
        int Size
        {
            get;
        }

        /// <summary>
        /// return the current game value of the hand
        /// </summary>
        int Value //calls the getter automatically
        {
            get;
        }

        /// <summary>
        /// return the amount bet on the hand
        /// </summary>
        int Bet
        {
            get;

            set;
        }

        /// <summary>
        /// the amount of insurance bet on the hand
        /// </summary>
        int InsuranceBet
        {
            get;

            set;
        }

        /// <summary>
        /// boolean for representing whether or not the player has finished playing the hand
        /// </summary>
        bool IsFinished
        {
            get;

            set;
        }

        /// <summary>
        /// boolean representing whether or not the current hand is blackjack
        /// </summary>
        bool IsBlackJack
        {
            get;
        }

        /// <summary>
        /// return the first card of the hand
        /// </summary>
        T FirstCard
        {
            get;
        }  

        /// <summary>
        /// string representing the result of the hand, ie winnner, push, loser
        /// </summary>
        string Result
        {
            get;
            set;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------


        /// <summary>
        /// returns true if the hand can be split
        /// </summary>
        /// <returns></returns>
        bool isSplittable();


        /// <summary>
        /// adds card to the hand
        /// </summary>
        /// <param name="card"></param>
        void addCard(T card);

   
        /// <summary>
        /// Cards must be of equal value to call this method
        /// remove one of the cards from the list, then create a new hand with that card and bet. return the new hand
        /// </summary>
        /// <param name="bet">the amount to bet, must be equal to the current hand</param>
        /// <returns>new hand that contains one of the cards from this hand</returns>
        HandInterface<T> splitCards(int bet);

        // 
        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns true if the hand has an insurance bet placed on it.</returns>
        bool hasInsurance();

        /// <summary>
        /// removes all cards from the current hand
        /// </summary>
        void clearHand();

        /// <summary>
        /// returns a string representation of hand
        /// </summary>
        /// <returns></returns>
        string ToString();

        /// <summary>
        /// make sure all cards are face up
        /// </summary>
        void flipCardsUp();

        

    }

}
