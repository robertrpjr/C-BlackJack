using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for DeckGUI.xaml
    /// 
    /// ended up not using this class as we felt displaying the deck was not necessary
    /// </summary>
    public partial class DeckGUI : UserControl, DeckInterface
    {
        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------

        private Deck<CardGUI> deck;
        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public DeckGUI() : this(1)
        {
        }

        public DeckGUI(int numberOfDecks)
        {
            InitializeComponent();
            deck = new Deck<CardGUI>(numberOfDecks);
            //perform GUI related tasks here (if we need to)
        }
        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        public int CutCardLocation
        {
            get
            {
                return deck.CutCardLocation;
            }
        }

        public int NumberOfDecks
        {
            get
            {
                return deck.NumberOfDecks;
            }
        }

        public int Size
        {
            get
            {
                return deck.Size; //same method as deck.Size
            }
        }
        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------

        public void shuffle()
        {
            deck.shuffle();
            //perform GUI related tasks here
        }
    }
}
