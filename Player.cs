namespace CSC460BlackJack
{


    public class Player<T,R> : Owner<T,R> where T : HandInterface<R> where R : CardInterface
    {
        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------


        
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------


        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public Player(int bankRoll)
        {
            this.Bank = bankRoll;
        }
        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------
        //see comments in the owner class for descriptions
        

        public override bool canSplit()
        {
            if(NumberOfHands == 0)
            {
                return false;
            }
            return (CurrentHand.isSplittable() && Bank >= CurrentHand.Bet);
        }

        public override bool canSurrender()
        {
            if (NumberOfHands == 0)
            {
                return false;
            }
            return (CurrentHand.Size == 2) ;
        }
        
        public override bool canDoubleDown()
        {
            if (NumberOfHands == 0)
            {
                return false;
            }
            return (CurrentHand.Size == 2 && Bank >= CurrentHand.Bet); 
        }
        

        public override bool canHit()
        {
            if (NumberOfHands == 0)
            {
                return false;
            }
            return true;
        }
        

        public override bool canStay()
        {
            if (NumberOfHands == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Place insurance bet on current hand
        /// valid bet amounts must be verified before calling this method
        /// be sure to also validate the total bet amount before calling
        /// </summary>
        /// <param name="bet">side bet for insurance. Bet must not be greater than half the amount of current bet on hand </param>
        public override void placeInsuranceBet(int bet)
        {
            int maxBet = (int)(CurrentHand.Bet * 0.5); //The max insurance bet is half of the original bet
            Bank -= bet;
            CurrentHand.InsuranceBet = bet;
        }
    }
}
