using System.Collections.Generic;

namespace CSC460BlackJack
{
    /// <summary>
    /// Description of methods can be found in owner interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public abstract class Owner<T,R> : OwnerInterface<T,R> where T : HandInterface<R> where R : CardInterface
    {

        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------

        private LinkedList<T> hands;
        private LinkedListNode<T> currentHand;
        

        public const double blackJackMultiplier = 1.5;
        
        private int bank;
        private int debt;

        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------
        
        public bool IsFinished // 
        {
            get
            {
                if (NumberOfHands == 0)
                    
                    return true;
                if (currentHand == null)
                    return true;
                for (LinkedListNode<T> itr = hands.First; itr != null; itr = itr.Next)
                {
                    if (itr.Value.IsFinished == false)
                        return false;
                }
                return true;
            }
        }

        public bool HasBlackJack
        {
            get {return currentHand.Value.IsBlackJack;}
        }

        public T CurrentHand
        {
            get 
            {
                
                return currentHand.Value; 
            }
        }
        
        public bool hasNextHand{
            get
            {
                if (currentHand == null || currentHand.Next == null)
                    return false;
                return true;
            }

    }
        public int Bank
        {
            get { return bank;}

            set { bank = value;}
        }
        
        public int NumberOfHands
        {
            get { return hands.Count; }
        }

        public int Debt
        {
            get
            {
                return debt;
            }

            set
            {
                if (value >= 0)
                    debt = value;
                else
                    debt = 0;
                
            }
        }

        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public Owner(T hand) : this()
        {
                this.insertHand(hand);
        }

        public Owner()
        {
            Bank = 0;
            Debt = 0;
            hands = new LinkedList<T>();
            
            currentHand = null;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------

        public void nextHand()
        {

            currentHand = currentHand.Next;
        }

        public void firstHand()
        {
            currentHand = hands.First;
        }

        public void insertHand(T hand)
        {
            if (NumberOfHands == 0)
            {
                hands.AddFirst(hand);
                currentHand = hands.First;
            }
            else
            {
                hands.AddAfter(currentHand, hand);
            }
        }
        
        public void appendHand(T hand)
        {
            if (NumberOfHands == 0)
            {
                hands.AddFirst(hand);
                currentHand = hands.First;
            }
            else
            {
                hands.AddLast(hand);
            }
        }

        public void settleBetsWithHouse(int dealerHandValue)
        {
            for (LinkedListNode<T> itr = hands.First; itr != null; itr = itr.Next)
            {
                if (itr.Value.Value > dealerHandValue && dealerHandValue <= Hand<Card>.perfectScore 
                    || dealerHandValue > Hand<CardInterface>.perfectScore)
                {
                    Bank += (itr.Value.Bet * 2);
                    itr.Value.Result = "WINNER";
                }
                else if (itr.Value.Value < dealerHandValue)
                {
                    itr.Value.Bet = 0;  //???since this method does not affect the bank, is it necessary - probably not
                    itr.Value.Result = "LOSER";
                }
                else
                {
                    Bank += itr.Value.Bet;  //hands were equal, give the player back the money
                    itr.Value.Result = "PUSH";
                }
                if (itr.Value.InsuranceBet > 0)
                {
                    Bank += itr.Value.InsuranceBet * 3;
                    itr.Value.Result = "INSURED";
                }
                

            }
            hands = new LinkedList<T>();
            currentHand = null;
        }

        
        public void removeCurrentHand()
        {
            if (NumberOfHands == 0)
                return;
            currentHand.Value.clearHand();
            if (currentHand.Next != null)
            {
                currentHand = currentHand.Next;
                hands.Remove(currentHand.Previous);
            }
            else
            {
                hands.Remove(currentHand);
                currentHand = null;
            }
        }

        public void removeAllHands()
        {
            while (hands.Count > 0)
            {
                hands.First.Value.clearHand();
                hands.RemoveFirst();
            }
            currentHand = null;
        }


        public void hit(R card)
        {
            CurrentHand.flipCardsUp();
            CurrentHand.addCard(card);
        }

        public void stay()
        {
            currentHand.Value.IsFinished = true;
        }

        public abstract void placeInsuranceBet(int bet);
        public abstract bool canSplit();
        public abstract bool canSurrender();
        public abstract bool canDoubleDown();
        public abstract bool canHit();
        public abstract bool canStay();

        
    }
}
