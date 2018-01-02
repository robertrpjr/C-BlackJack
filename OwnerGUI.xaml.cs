using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for OwnerGUI.xaml
    /// 
    /// description of each method can be found in owner interface
    /// 
    /// this is a wrapper class for the owner (owners can be player or dealer)
    /// uses the wrapped class for the implementation logic and then does the graphical 
    /// stuff here
    /// </summary>
    /// 
    public abstract partial class OwnerGUI : UserControl, OwnerInterface<HandGUI,CardGUI>
    {
        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------
        private OwnerInterface<HandGUI, CardGUI> owner;

        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        private OwnerGUI()
        {
            InitializeComponent();
        }

        public OwnerGUI(OwnerInterface<HandGUI,CardGUI> owner) : this() 
        {
            this.owner = owner;
            updateBank();
            updateDebt();
        }
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        
      
        public int Bank
        {
            get
            {
                return owner.Bank;
            }

            set
            {
                owner.Bank = value;
                updateBank();
            }
        }

        public HandGUI CurrentHand
        {
            get
            {
                return owner.CurrentHand;
            }
        }

        public bool hasNextHand
        {
            get
            {
                return owner.hasNextHand;
            }
        }

        public bool IsFinished
        {
            get
            {
                return owner.IsFinished;
            }
        }

        public int NumberOfHands
        {
            get
            {
                return owner.NumberOfHands;
            }
        }

        public int Debt
        {
            get
            {
                return owner.Debt;
            }
            set
            {
                owner.Debt = value;
                updateDebt();
            }
        }
        

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------


        public void hit(CardGUI card)
        {
            owner.hit(card);
        }

        public virtual void appendHand(HandGUI hand)
        {

            int count = owner.NumberOfHands;
            owner.appendHand(hand);

            //inserthand into grid or whatever

            handPanel.Children.Add(hand);
            

        }

        public void insertHand(HandGUI hand)
        {
            int count = owner.NumberOfHands;
            owner.insertHand(hand);

            handPanel.Children.Insert(handPanel.Children.IndexOf(CurrentHand) + 1, hand);

        }

        public void nextHand()
        {
            owner.nextHand();
        }

        private void updateBank()
        {
            if(owner.Bank == 0)
            {
                playerBank.Text = "";
            }
            else
                playerBank.Text = "Bank: $" + owner.Bank;
        }

        private void updateDebt()
        {
            if (owner.Debt == 0)
            {
                playerDebt.Text = "";
            }
            else
                playerDebt.Text = "Debt: $" + owner.Debt;
        }

        public void removeCurrentHand()
        {
            owner.CurrentHand.clearHand();
            handPanel.Children.Remove(CurrentHand);
            owner.removeCurrentHand();
            
        }

        public virtual void settleBetsWithHouse(int dealerHandValue)
        {
            owner.settleBetsWithHouse(dealerHandValue);
            updateBank();
        }

        public void stay()
        {
            owner.stay();
        }

        public void placeInsuranceBet(int bet)
        {
            owner.placeInsuranceBet(bet);
            updateBank();
        }

        public bool canSplit()
        {
            return owner.canSplit();
        }

        public bool canSurrender()
        {
            return owner.canSurrender();
        }

        public bool canDoubleDown()
        {
            return owner.canDoubleDown();
        }

        public bool canHit()
        {
            return owner.canHit();
        }

        public bool canStay()
        {
            return owner.canStay();
        }

        public void removeAllHands()
        {

            while(handPanel.Children.Count > 0)
            {
                handPanel.Children.RemoveAt(0);
            }
            owner.removeAllHands();
        }

        public void firstHand()
        {
            owner.firstHand();
        }
    }
}
