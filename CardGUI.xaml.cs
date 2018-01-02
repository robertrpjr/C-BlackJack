using System;
using System.Windows.Media.Imaging;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for CardGUI.xaml
    /// 
    /// wrapper class for card, for description of classes see cardinterface
    /// </summary>
    public partial class CardGUI : CardInterface
    {
        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------
        private CardInterface card;
        //field that holds the image of the card = cardImage


        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        public CardRank Rank
        {
            get
            {
                return card.Rank;
            }
            set
            {
                card.Rank = value;
                // if someone changes the rank of the card, you might need to redraw the card
                updateCardImage();
            }
        }

        public CardSuit Suit
        {
            get
            {
                return card.Suit;
            }
            set
            {
                card.Suit = value;
                // if someone changes the rank of the card, you might need to redraw the card
                updateCardImage();
            }
        }

        public bool FaceUp
        {
            get
            {
                
                return card.FaceUp;
            }
            set
            {
                //when setting the face up value, you redraw the card here
                card.FaceUp = value;
                
                updateCardImage();
            }
        }

        public int Value
        {
            get
            {
                return card.Value;
            }
        }

        public string RankLabel
        {
            get
            {
                return card.RankLabel;
            }
        }

        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        /// <summary>
        /// used only for shuffle method
        /// </summary>
        public CardGUI()
        {
            InitializeComponent();
            card = new Card();
            updateCardImage();
        }

        public CardGUI(CardSuit suit, CardRank rank) : this(suit,rank,true)
        { }

        public CardGUI(CardSuit suit, CardRank rank, bool faceUp)
        {
            InitializeComponent();
            card = new Card(suit, rank, faceUp);

            updateCardImage(); //change to some content representing this card
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------

        public override string ToString()
        {
            return card.ToString();
        }

        private void updateCardImage()
        {
            //this is called a selection statement, its pretty much a short way to do an if statement
            //the idea is: if (card.Faceup) then set filename equal to all of this stuff here... 
            //the ':' is like saying 'else, this is the file name'

            string filename = card.FaceUp ? ((int)card.Suit).ToString() + ((int)card.Rank).ToString() + ".png" : "cardBack.jpg";

            cardImage.Source = new BitmapImage(new Uri(@"/PlayingCards/"+filename, UriKind.Relative));
        }

    }
}
