namespace CSC460BlackJack
{
    /// <summary>
    /// description of methods can be found in cardinterface
    /// </summary>
    public class Card : CardInterface
    {

        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------

        private CardRank rank;
        private CardSuit suit;
        private bool faceUp;

        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------
        public CardRank Rank
        {
            get
            {
                return rank;
            }
            set
            {
                rank = value;
            }
        }

        public CardSuit Suit
        {
            get
            {
                return suit;
            }
            set
            {
                suit = value;
            }
        }

        public bool FaceUp
        {
            get
            {
                return faceUp;
            }
            set
            {
                faceUp = value;
            }
        }

        public int Value
        {
            get
            {
                switch (rank)     //only let Ace equal 11 in this method.  Set Ace equal to 1 only if hand bust in Hand class
                {

                    case CardRank.ten:
                    case CardRank.jack:
                    case CardRank.queen:
                    case CardRank.king:
                        return 10;
                    default:
                        return (int)rank;
                }
            }
        }

        public string RankLabel
        {
            get
            {
                switch (rank)
                {

                    
                    case CardRank.ace:
                        return "A";
                    case CardRank.jack:
                        return "J";
                    case CardRank.queen:
                        return "Q";
                    case CardRank.king:
                        return "K";
                    default:
                        return ((int)rank).ToString();
                }
            }
        }

        private string suitLabel
        {
            get
            {
                switch (suit)
                {
                    case CardSuit.clubs:
                        return "C";
                    case CardSuit.diamonds:
                        return "D";
                    case CardSuit.hearts:
                        return "H";
                    case CardSuit.spades:
                        return "S";
                }
                return null;
            }
        }


        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public Card()
        {
            this.faceUp = true;
        }
        public Card(CardSuit suit, CardRank rank) : this(suit,rank,true)
        {
            
            
        }

        public Card(CardSuit suit, CardRank rank, bool faceUp)
        {
            this.faceUp = faceUp;
            this.suit = suit;
            this.rank = rank;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return this.suitLabel + this.RankLabel;
        }

    }
}
