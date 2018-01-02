using System.Windows;
using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for ChangeBetWindow.xaml
    /// 
    /// allows the user to set the number of hands, bet per hand, number of decks
    /// </summary>
    public partial class ChangeBetWindow : Window
    {
        private int maxHands = 3;
        private int maxDecks = 8;
        public int numberOfHands;
        public int betPerHand;
        public int playerBank;
        public bool changeBets;
        public int decks;

        /// <summary>
        /// sets all components to their current value
        /// </summary>
        /// <param name="numberOfHands"></param>
        /// <param name="betPerHand"></param>
        /// <param name="playerBank"></param>
        /// <param name="decks"></param>
        public ChangeBetWindow( int numberOfHands,  int betPerHand,  int playerBank, int decks)
        {
            InitializeComponent();

            for (int i = 1; i <= maxHands; i++)
                handComboBox.Items.Add(i);
            for (int i = 1; i <= maxDecks; i++)
                deckComboBox.Items.Add(i);
            this.numberOfHands = numberOfHands;
            this.betPerHand = betPerHand;
            this.playerBank = playerBank;
            this.decks = decks;
            handComboBox.SelectedIndex = numberOfHands-1;
            deckComboBox.SelectedIndex = decks - 1;
            betTextBox.Text = betPerHand.ToString();
            playerBankTextBlock.Text = playerBank.ToString();


        }
      
        
        /// <summary>
        /// update the number of hands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            numberOfHands = (int)handComboBox.SelectedItem;
        }

        /// <summary>
        /// close and don't change anything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            changeBets = false;
            this.Close();
        }

        /// <summary>
        /// change game variables to their newly selected values and start the new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(betTextBox.Text,out betPerHand))
            {
                if (betPerHand < 25)
                {
                    
                    MessageBox.Show(this,"The minimum bet is $25");
                }
                else if (playerBank < (numberOfHands * betPerHand))
                {
                    MessageBox.Show(this,"Not Enough Money in Bank...");
                }
                else
                {
                    changeBets = true;
                    Close();
                }
            }
            else
            {
                betTextBox.Focus();
                betTextBox.SelectAll();
            }

        }

        /// <summary>
        /// update the number of decks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deckComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decks = (int)deckComboBox.SelectedItem;
        }
    }
}
