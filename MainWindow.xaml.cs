using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.IsolatedStorage;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------


        /*
         * FIELDS DEFINED IN XAML
         * 
         * DealerGUI dealer;
         * PlayerGUI player;
         * 
         * Canvas optionCanvas; //holds all the buttons for hit, stay, etc.
         * Button surrenderButton;
         * Button splitButton;
         * Button doubleDownButton;
         * Button hitButton;
         * Button stayButton;
         * 
         * TextBlock handsTextBlock
         * TextBlock betTextBlock
         * 
         */

        private OwnerGUI player;
        private Deck<CardGUI> deck;
        private int sleeptime = 750; //in milliseconds
        private bool legsBroken;
        private double interestRate = .02;
        private const string FileName = @"SavedGame.bin";
      


        // --------------------------------------------------------------------------------------------
        // --- Constructor
        // --------------------------------------------------------------------------------------------

        public MainWindow()
        {
            InitializeComponent();
            player = new PlayerGUI(5000);
            SaveGame prevGame;
            handsTextBlock.Text = (1).ToString();
            betTextBlock.Text = (25).ToString();
            decksTextBlock.Text = (1).ToString();
            legsBroken = false;
            
            /*
             * Reads saved game data from a file if it exists
             * otherwise, used the defaults above
             */
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User| IsolatedStorageScope.Assembly, null, null))
            {
                if (isoStore.FileExists(FileName))
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to continue your previous game?", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(FileName, FileMode.Open, isoStore))
                        {
                                
                            BinaryFormatter deserializer = new BinaryFormatter();
                            prevGame = (SaveGame)deserializer.Deserialize(isoStream);
                            legsBroken = prevGame.LegsBroken;
                            player.Bank = prevGame.PlayerBank;
                            player.Debt = prevGame.PlayerDebt;
                            handsTextBlock.Text = prevGame.Hands.ToString();
                            betTextBlock.Text = prevGame.BetPerHand.ToString();
                            decksTextBlock.Text = prevGame.Decks.ToString();
                            
                        }
                        if (prevGame.LegsBroken)
                        {
                            MessageBoxResult r = MessageBox.Show("\"Hey, aren't you the guy that owes me $" + prevGame.PlayerDebt + "?\" \n\nGreasy Gary recognized you because of the wheelchair and threw you back into the river");
                            Close();
                        }
                    }
                }
            }
            //can put any number of decks you want, limited to 8 by changebet window
            
            deck = new Deck<CardGUI>(int.Parse(decksTextBlock.Text));
            
            playerGrid.Children.Add(player);
            
            optionCanvas.Visibility = Visibility.Hidden;
            insuranceCanvas2.Visibility = Visibility.Hidden;
        }

        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------


        /// <summary>
        /// looks at the players current hand and takes action appropriately
        /// </summary>
        private async void enableOptions()
        {
            //if player hasnt finished all their hands, check the current hand
            if (!player.IsFinished)
            {
                player.CurrentHand.handBorder.BorderThickness = new Thickness(0);
                player.CurrentHand.handBorder.BorderBrush = null;
                
                // if the current hand is blackjack, pay it and remove it
                if (player.CurrentHand.IsBlackJack)
                {
                    /*
                     * ADD SOME DISPLAY TO LET USER KNOW THEY GOT BLACKJACK
                     */
                    optionCanvas.Visibility = Visibility.Hidden;
                    player.CurrentHand.handBorder.BorderBrush = Brushes.Black;
                    player.CurrentHand.handBorder.BorderThickness = new Thickness(2);
                    player.CurrentHand.Result = "BlackJack!";
                    await Task.Delay(sleeptime * 3);
                    player.Bank += (int)(player.CurrentHand.Bet * 2.5);
                    player.removeCurrentHand();
                    optionCanvas.Visibility = Visibility.Visible;
                    enableOptions();
                    return;
                }

                //if the current hand has been marked as finished, then move to the next hand
                if (player.CurrentHand.IsFinished)
                {
                    player.nextHand();
                    enableOptions();
                    return;
                }
                //put a border around the current hand
                player.CurrentHand.handBorder.BorderBrush = Brushes.Black;
                player.CurrentHand.handBorder.BorderThickness = new Thickness(2);

                //enable/disable appropriate buttons
                surrenderButton.IsEnabled = player.canSurrender();
                splitButton.IsEnabled = player.canSplit();
                doubleDownButton.IsEnabled = player.canDoubleDown();
                hitButton.IsEnabled = player.canHit();
                stayButton.IsEnabled = player.canStay();
                optionCanvas.Visibility = Visibility.Visible;
            }
            //WHEN WE HAVE FINISHED PLAYING ALL PLAYER HANDS
            else
            {
                endGame();
            }

        }
        /// <summary>
        /// Deals 2 cards to each hand and checks dealers hand for blackjack, offers insurance if necessary
        /// </summary>
        private async void startGame()
        {
            garysLoanButton.Visibility = Visibility.Hidden;
            insuranceCanvas2.Visibility = Visibility.Hidden;
            nextGameCanvas.Visibility = Visibility.Hidden;
            //make the initial set up a little faster
            int sleeptime = this.sleeptime / 2;

            //shuffle if you need to
            if (deck.Size < deck.CutCardLocation || deck.NumberOfDecks != int.Parse(decksTextBlock.Text))
            {
                deck = new Deck<CardGUI>(int.Parse(decksTextBlock.Text));
            }
            //remove the cards from the previous hand
            dealer.removeAllHands();
            player.removeAllHands();
            List<HandPlayerGUI> pHands = new List<HandPlayerGUI>();

            await Task.Delay(sleeptime);
            HandDealerGUI dHand = new HandDealerGUI();
            
            dealer.insertHand(dHand);

            for (int i = 0; i < int.Parse(handsTextBlock.Text); ++i)
            {
                player.Bank -= int.Parse(betTextBlock.Text);
                pHands.Add(new HandPlayerGUI(int.Parse(betTextBlock.Text), deck.Dequeue()));
                player.appendHand(pHands.ElementAt(i));
                await Task.Delay(sleeptime);
            }

            //CHANGE OUT THESE LINES TO GIVE THE DEALER AN ACE

            dHand.addCard(deck.Dequeue());  //commented for testing
            //dHand.addCard(new CardGUI(CardSuit.hearts, CardRank.ace,true));
            await Task.Delay(sleeptime);
            for (int i = 0; i < int.Parse(handsTextBlock.Text); ++i)
            {
                pHands.ElementAt(i).addCard(deck.Dequeue());
                await Task.Delay(sleeptime);
            }

            CardGUI card = deck.Dequeue();
            
            card.FaceUp = false;
            dHand.addCard(card); //commented for testing
                                 //dHand.addCard(new CardGUI(CardSuit.hearts, CardRank.ace, false));

  

            if (dealer.CurrentHand.FirstCard.Value == 11)
            {
                insuranceCanvas2.Visibility = Visibility.Visible;
                optionCanvas.Visibility = Visibility.Hidden;
                Hand1CheckBox.IsChecked = false;
                Hand2CheckBox.IsChecked = false;
                Hand3CheckBox.IsChecked = false;
                switch (pHands.Count)
                {
                    case 1:
                        Hand1CheckBox.Visibility = Visibility.Visible;
                        Hand2CheckBox.Visibility = Visibility.Hidden;
                        Hand3CheckBox.Visibility = Visibility.Hidden;
                        break;
                    case 2:
                        Hand1CheckBox.Visibility = Visibility.Visible;
                        Hand2CheckBox.Visibility = Visibility.Visible;
                        Hand3CheckBox.Visibility = Visibility.Hidden;;
                        break;
                    case 3:
                        Hand1CheckBox.Visibility = Visibility.Visible;
                        Hand2CheckBox.Visibility = Visibility.Visible;
                        Hand3CheckBox.Visibility = Visibility.Visible;
                        break;
                }

            }
            // if dealer has a 10 showing, wait for a second and end the game
            else if (dealer.CurrentHand.IsBlackJack)
            {
                /*
                 * ADD DELAY HERE FOR DRAMATIC EFFECT
                 * 
                 */
                await Task.Delay(this.sleeptime * 3);
                endGame();
            }
            // if none of that happened, then go on and start playing the game
            else
            {
                enableOptions();
                optionCanvas.Visibility = Visibility.Visible;
            }
                
        }

        /// <summary>
        /// Once all the player hands have been played, play the dealer hand appropriately and 
        /// mark all hands as winners/losers
        /// </summary>
        private async void endGame()
        {
            optionCanvas.Visibility = Visibility.Hidden;
            await Task.Delay(sleeptime);
            dealer.CurrentHand.flipCardsUp();
            await Task.Delay(sleeptime * 2);
            //check that hands are still in the game
            if (player.NumberOfHands > 0)
            {
                while (dealer.canHit())
                {
                    
                    dealer.hit(deck.Dequeue());
                    await Task.Delay(sleeptime * 2);
                }
                player.settleBetsWithHouse(dealer.CurrentHand.Value);
            }
            nextGameCanvas.Visibility = Visibility.Visible;
            garysLoanButton.Visibility = Visibility.Visible;
            player.Debt += (int)(player.Debt * interestRate);
            
        }

        // --------------------------------------------------------------------------------------------
        // --- Button Handlers
        // --------------------------------------------------------------------------------------------


        /// <summary>
        /// gives half of the bet back to the player and removes the hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void surrenderButton_Click(object sender, RoutedEventArgs e)
        {
            player.Bank += (player.CurrentHand.Bet / 2);
            
            player.removeCurrentHand();

            enableOptions();
        }

        /// <summary>
        /// splits the hand into two hands and adds a new card to each
        /// Aces only get one card each, but aces can be split again
        /// No limit on number of splits, bc greasy gary likes to play wild
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void splitButton_Click(object sender, RoutedEventArgs e)
        {
            optionCanvas.Visibility = Visibility.Hidden;
            bool aces = player.CurrentHand.FirstCard.Rank == CardRank.ace;
            HandGUI newHand = (HandGUI)player.CurrentHand.splitCards(player.CurrentHand.Bet);
            player.Bank -= player.CurrentHand.Bet;
            player.insertHand(newHand);
            await Task.Delay(sleeptime);
            player.CurrentHand.addCard(deck.Dequeue());
            await Task.Delay(sleeptime);
            newHand.addCard(deck.Dequeue());
            await Task.Delay(sleeptime);
            // if splitting aces, mark hand as finished if the new card wasn't an ace or blackjack
            player.CurrentHand.IsFinished = (aces && player.CurrentHand.Value != 12 && player.CurrentHand.Value != 21);
            newHand.IsFinished = aces && newHand.Value != 12 && newHand.Value != 21;
            //player.updateBank();


            optionCanvas.Visibility = Visibility.Visible;
            enableOptions();
            
        }

        /// <summary>
        /// doubles the players bet on a hand and takes on more card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void doubleDownButton_Click(object sender, RoutedEventArgs e)
        {
            optionCanvas.Visibility = Visibility.Hidden;
            player.Bank -= player.CurrentHand.Bet;
            player.CurrentHand.Bet *= 2;
            
            player.hit(deck.Dequeue());

            if (player.CurrentHand.Value > Hand<CardGUI>.perfectScore)
            {
                player.CurrentHand.Result = "BUST";
                await Task.Delay(sleeptime);
                player.removeCurrentHand();
                
            }
            else
            {
                player.stay();
            }

            optionCanvas.Visibility = Visibility.Visible;
            enableOptions();
            
        }

        /// <summary>
        /// adds one more card to the hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void hitButton_Click(object sender, RoutedEventArgs e)
        {
            player.hit(deck.Dequeue());
            
            /*
             * 1) check for bust
             * 
             * 2) if busted, remove hand from game
             */
            if (player.CurrentHand.Value > Hand<CardGUI>.perfectScore)
            {
                //tell user something
                optionCanvas.Visibility = Visibility.Hidden;
                player.CurrentHand.Result = "BUST";
                await Task.Delay(sleeptime);
                player.removeCurrentHand();
                
                
            }
            optionCanvas.Visibility = Visibility.Visible;
            enableOptions();
            
        }

        /// <summary>
        /// mark hand as finished and move on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stayButton_Click(object sender, RoutedEventArgs e)
        {

            player.stay();
            
            enableOptions();
        }

        
        /// <summary>
        /// enables user to select the number of hands, amount to bet, and number of decks to play with
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeBetButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeBetWindow win = new ChangeBetWindow(int.Parse(handsTextBlock.Text), int.Parse(betTextBlock.Text), player.Bank, int.Parse(decksTextBlock.Text));
            win.Owner = this;
            win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            win.ShowDialog();

            if (win.changeBets)
            {
                handsTextBlock.Text = win.numberOfHands.ToString();
                betTextBlock.Text = win.betPerHand.ToString();
                decksTextBlock.Text = win.decks.ToString();
                startGame();
            }

        }

        /// <summary>
        /// starts a new game with the same options as the previous one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sameBetButton_Click(object sender, RoutedEventArgs e)
        {
            if (player.Bank < (int.Parse(handsTextBlock.Text) * int.Parse(betTextBlock.Text)))
                MessageBox.Show("You ain't got enough cash, Homie. Go stop by Greasy Gary's if you need a loan.");
            
            else
                startGame();
        }

        /// <summary>
        /// brings up the loan shark window, details in GreasyGarysLoanEmporium.xaml.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void garysLoanButton_Click(object sender, RoutedEventArgs e)
        {
            GreasyGarysLoanEmporium ggloanWindow = new GreasyGarysLoanEmporium(player.Bank, player.Debt);
            ggloanWindow.Owner = this;
            ggloanWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ggloanWindow.ShowDialog();
            if (ggloanWindow.legsBroken)
            {
                legsBroken = ggloanWindow.legsBroken;
                Close();
            }
            else
            {
                player.Debt += ggloanWindow.newLoan;
                player.Bank += ggloanWindow.newLoan;
            }

        }

        /// <summary>
        /// adds either insurance or even money to the hands that have been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void insuranceButton_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            checkBoxes.Add(Hand1CheckBox);
            checkBoxes.Add(Hand2CheckBox);
            checkBoxes.Add(Hand3CheckBox);
            
            player.firstHand();

            //first get the total number of insurance bets, and make sure the player has enough money
            int count = 0;
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes.ElementAt(i).IsChecked == true)
                {
                    if (player.CurrentHand.IsBlackJack == false)
                        ++count;
                    player.nextHand();
                }
            }
            player.firstHand();

            if ((count * (int)(player.CurrentHand.Bet * 0.5)) > player.Bank)
            {
                MessageBox.Show("Not enough money in the bank."); return;
            }

            insuranceCanvas2.Visibility = Visibility.Hidden;

            //either place an insurance bet, or collect even money on each checked hand
            foreach (CheckBox cb in checkBoxes)
            {
                if (cb.IsChecked == true)
                {
                    if (player.CurrentHand.IsBlackJack == false)
                    {
                        player.placeInsuranceBet((int)(player.CurrentHand.Bet * 0.5));
                        player.nextHand();
                    }
                    else
                    {
                        player.CurrentHand.handBorder.BorderBrush = Brushes.Black;
                        player.CurrentHand.handBorder.BorderThickness = new Thickness(2);
                        
                        player.CurrentHand.Result = "EVEN $";
                        await Task.Delay(sleeptime * 3);
                        player.Bank += player.CurrentHand.Bet * 2;
                        
                        player.removeCurrentHand();
                    }
                }
                else if (player.hasNextHand)
                    player.nextHand();
            }
            
            player.firstHand();
            
            // if the dealer had a 10 underneath, then end the game and settle all bets
            if(dealer.CurrentHand.IsBlackJack)
            {
                endGame();
            }
            //if they didn't then collect all of the insurance bets 
            else
            {
                for (int i = 0; i < player.NumberOfHands; ++i)
                {
                    if (player.CurrentHand.hasInsurance())
                    {
                        player.CurrentHand.Result = "- $" + player.CurrentHand.InsuranceBet.ToString();
                        await Task.Delay(sleeptime);
                        player.placeInsuranceBet(0);
                        player.CurrentHand.Result = "";
                    }
                    player.nextHand();
                }
                    
                player.firstHand();
                optionCanvas.Visibility = Visibility.Visible;
                enableOptions();
            }
               
            
        }

        /// <summary>
        /// saves the game data to a file when the window is closed, also warns the user that they will lose if they quit during a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //saves game info to a file when the window is closed
            if (player.NumberOfHands > 0)
            {
                MessageBoxResult result = MessageBox.Show("If you quit now, you will lose the current hand, are you sure you want to quit?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(FileName, FileMode.Create, isoStore))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(isoStream, new SaveGame(player.Bank, player.Debt, int.Parse(handsTextBlock.Text),int.Parse(betTextBlock.Text), int.Parse(decksTextBlock.Text), legsBroken));
                }
            }
        }
    }
}
