namespace CSC460BlackJack
{
    /// <summary>
    /// details all the basic commands for a card owner, 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public interface OwnerInterface<T,R> where T: HandInterface<R> where R: CardInterface
    {

        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

     
        /// <summary>
        /// returns true if owner has 0 hands, or if every hand is finished
        /// </summary>
        bool IsFinished  
        {
            get;
        }

        /// <summary>
        /// return the current hand
        /// </summary>
        T CurrentHand
        {
            get;
        }

        /// <summary>
        /// returns true if the player has another hand to play
        /// </summary>
        bool hasNextHand
        {
            
            get;
        }

        /// <summary>
        /// returns or modifies the player bank
        /// </summary>
        int Bank
        {
            get;

            set;
        }

        /// <summary>
        /// returns true if owner has 0 hands, or if every hand is finished
        /// </summary>
        int NumberOfHands
        {
            get;
        }

        /// <summary>
        /// gets or sets the current amount owed to greasy gary
        /// </summary>
        int Debt
        {
            get;
            set;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------


        /// <summary>
        /// changes current hand to the next hand
        /// </summary>
        void nextHand();

        /// <summary>
        /// inserts a hand after the current hand
        /// must set bet in hand before calling this method 
        /// </summary>
        /// <param name="hand"></param>
        void insertHand(T hand);

        /// <summary>
        /// adds hand to the end of players hands
        /// </summary>
        /// <param name="hand"></param>
        void appendHand(T hand);

        /// <summary>
        /// compare each hand to the dealers and and mark as winner/push/loser
        /// </summary>
        /// <param name="dealerHandValue"></param>
        void settleBetsWithHouse(int dealerHandValue);

        /// <summary>
        /// removes the current hand from the owners list of hands
        /// </summary>
        void removeCurrentHand();

        /// <summary>
        /// removes all hands from the player (used at end of game)
        /// </summary>
        void removeAllHands();

        /// <summary>
        /// add ann insurance bet to the current hand
        /// </summary>
        /// <param name="bet"></param>
        void placeInsuranceBet(int bet);

        /// can be called to check whether or not the current hand can be split
        /// <returns>true if current hand can be split, false otherwise</returns>
        bool canSplit();   //??????????????always return false in Dealer /e

        
        /// can be called to check whether or not the owner can surrender the current hand
        /// <returns>true if current hand can be surrendered, false otherwise</returns>
        bool canSurrender();

        /// returns true if the owner can double down on the current hand
        bool canDoubleDown();

        /// doubles the bet of the current hand and takes one more card
        /// must be checked for preconditions using the canDoubleDown method
        /// <param name="card"> the card to be added to the hand</param>
        ///void doubleDown(R card);

        /// returns true if the owner can hit the current hand
        /// <returns>boolean representing whether or not you can hit hand</returns>
        bool canHit();

        /// adds one card to the hand and checks for bust
        
        void hit(R card);
        
        /// returns true if the owner can stay on the current hand
        /// <returns>boolean representing whether or not you can stay</returns>
        bool canStay();

        /// <summary>
        /// mark current hand as finished
        /// </summary>
        void stay();

        /// <summary>
        /// change current hand to the first hand of player 
        /// </summary>
        void firstHand();

    }
}
