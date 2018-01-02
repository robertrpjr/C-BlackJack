using System.Collections.Generic;
using System.Linq;

namespace CSC460BlackJack
{
    /// <summary>
    /// game logic for hand, for description of methods see hand interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Hand<T> : HandInterface<T> where T : CardInterface
    {
        // --- Fields
        private int insuranceBet;
        private int bet;
        private bool isFinished;
        private string result;
        private List<T> cards;
        public const int perfectScore = 21;
        const int aceDifference = 10; //highAce - lowAce
        
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        public int Size
        {
            get {return cards.Count;}
        }

        public int Value //calls the getter automatically
        { 
            get
            {
                int handValue = 0;

                //calculate value of hand by adding value of each card
                foreach (T card in cards)
                {
                    if (card.FaceUp) handValue += card.Value;
                }
                //now that you have the total value of the hand
                //go through it and subtract as many aces as necessary
                //so that the hand will not bust
                foreach(T card in cards)
                {
                    if (handValue > perfectScore && card.Rank == CardRank.ace)
                        handValue -= aceDifference; //hand has busted but has an Ace so reduce handValue by 10
                }
                                               
                return handValue;                
            }                                    
        }

        public int Bet
        {
            get{return bet;}
            set {bet = value; }
        }

        public int InsuranceBet
        {
            get{  return insuranceBet; }
            set { insuranceBet = value; }
        }

        public bool IsFinished
        {
            get {return isFinished;}
            set {isFinished = value;}
        }

        

        public bool IsBlackJack
        {
            get
            {
                bool isBJ = false;
                int temp = 0;
                foreach (T card in cards)
                {
                    temp += card.Value;
                }
                if (temp == perfectScore && cards.Count == 2)  //call HandValue() first to make sure value is up to date
                    isBJ = true;
                return isBJ;
            }
        }

        public T FirstCard
        {
            get
            {
                return cards.First();
            }
        }

        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }



        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public Hand()
        {
            this.cards = new List<T>();
        }

        public Hand(int bet)
        {
            //initialize hand
            this.bet = bet;    //place bet in this instance of hand
            cards = new List<T>();   //setup new card list for this hand  
        }

        public Hand(int bet, T card)
        {
            this.bet = bet;  //place bet in this instance of hand
            cards = new List<T>();  //setup new card list for this hand  
            cards.Add(card);    //pass in split card to list
        }

        //hand must have 2 cards before calling this method
        public bool isSplittable()  //checks if first two cards are equal
        {
            bool isEqual = false;
            if (cards.Count != 2)
                return isEqual;
            if (cards[0].Value == cards[1].Value)
                isEqual = true;
            return isEqual;
            
        }

        public void addCard(T card)
        {
            cards.Add(card);     
        }

        public HandInterface<T> splitCards(int bet)
        {
            T newHandCard = cards.ElementAt(0);
            cards.Remove(newHandCard);
            return  new Hand<T>(bet, newHandCard);
        }

        public bool hasInsurance()
        {
            if (InsuranceBet <= 0)
                return false;
            return true;
        }

        public void clearHand() // clear bet and cards from hand
        {
            bet = 0;
            
            cards.Clear();
        }
        public override string ToString()
        {
            string s = "";
            foreach (T card in cards)
            {
                s += "[" +  card.ToString() + "] ";
            }
            return s;
        }

        public void flipCardsUp()
        {
            foreach (T card in cards)
            {
                card.FaceUp = true;
            }
        }
    }
}
