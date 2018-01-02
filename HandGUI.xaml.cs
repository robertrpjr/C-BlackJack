using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for HandGUI.xaml
    /// 
    /// used to wrap the class containing the logic, adds graphical representation
    /// for description of methods see handinterface
    /// </summary>
    public abstract partial class HandGUI : UserControl, HandInterface<CardGUI>
    {

        private HandInterface<CardGUI> hand;

        public HandGUI()
        {
            InitializeComponent();
            hand = new Hand<CardGUI>();
            resultTextBlock.Text = "";

        }

        

        public HandGUI(int bet, CardGUI card) : this()
        {
            Bet = bet;
            addCard(card);
        }

        public HandGUI(int bet) : this()
        {
            
            Bet = bet;
        }

        public int Bet
        {
            get
            {
                return hand.Bet;  //Hand that implements CardGUI
            }

            set
            {
                hand.Bet = value;
                if (Bet == 0)
                {
                    betTextBlock.Text = "";
                }
                else
                {
                    betTextBlock.Text = "Bet: $" + Bet.ToString();
                }
            }
        }

        public int InsuranceBet
        {
            get
            {
                return hand.InsuranceBet;
            }

            set
            {
                hand.InsuranceBet = value;
                if (hand.InsuranceBet == 0)
                    insuranceTextBlock.Text = "";
                else
                {
                    insuranceTextBlock.Text = "Insurance: $" + InsuranceBet.ToString();
                }
            }
        }

        public bool IsBlackJack
        {
            get
            {
                return hand.IsBlackJack;
            }
        }

        public bool IsFinished
        {
            get
            {
                return hand.IsFinished;
            }

            set
            {
                hand.IsFinished = value;
            }
        }

        public int Size
        {
            get
            {
                return hand.Size;
            }
        }

        public int Value
        {
            get
            {
                return hand.Value;
            }
        }

        public CardGUI FirstCard
        {
            get
            {
                return hand.FirstCard;
            }
        }

        public string Result
        {
            get
            {
                return hand.Result;
            }

            set
            {
                hand.Result = value;
                resultTextBlock.Text = value;
            }
        }

        public virtual void addCard(CardGUI card)
        {
            
            hand.addCard(card);
            updateValue();
        }

        public void clearHand()
        {
            while(handGrid.Children.Count > 0)
            {
                
                handGrid.Children.RemoveAt(0);
            }
            hand.clearHand();
        }

        public void flipCardsUp()
        {
            hand.flipCardsUp();
            updateValue();
        }

        public bool hasInsurance()
        {
            return hand.hasInsurance();
        }

        public bool isSplittable()
        {
            return hand.isSplittable();
        }

        public virtual HandInterface<CardGUI> splitCards(int bet)
        {
            
            return hand.splitCards(bet);
        }

        private void updateValue()
        {
            valueTextBlock.Text = "Hand Value: " + hand.Value.ToString();
        }

        //public void payInsurance()
        //{
        //    insuranceTextBlock.Text = "Paid insurance $" + (2 * InsuranceBet);
        //}
    }
}
