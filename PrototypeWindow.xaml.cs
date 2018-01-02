using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for PrototypeWindow.xaml
    /// 
    /// quick and dirty prototype for demonstration this week.
    /// 
    /// comments to come soon.
    /// </summary>
    public partial class PrototypeWindow : Window
    {

        TextBlock[] betBlocks;
        TextBlock[] handBlocks;
        //Hand[] hands;
        Deck<CardGUI> deck;

        List<Owner<Hand<Card>,Card>> owners;


        public PrototypeWindow()
        {
            InitializeComponent();

            //***** tests the dealer hand
            DealerGUI dealer = new DealerGUI();
            HandDealerGUI dHand = new HandDealerGUI();
            cardTestGrid.Children.Add(dealer); //how to add the hand to the owner
            dHand.addCard(new CardGUI(CardSuit.clubs, CardRank.ace));
            dHand.addCard(new CardGUI(CardSuit.hearts, CardRank.queen, false));
            dHand.addCard(new CardGUI(CardSuit.diamonds, CardRank.queen));
            dHand.flipCardsUp();
            dealer.appendHand(dHand);

            //dHand.clearHand();
            //********** tests the player hand
            //PlayerGUI player = new PlayerGUI(5000);
            //HandGUI hand = new HandPlayerGUI(500);
            //cardTestGrid.Children.Add(player);
            //hand.addCard(new CardGUI(CardSuit.clubs, CardRank.ace));
            //hand.addCard(new CardGUI(CardSuit.diamonds, CardRank.ace));
            //player.appendHand(hand);
            //player.split(new CardGUI(CardSuit.diamonds, CardRank.eight), new CardGUI(CardSuit.spades, CardRank.eight));
            //hand.addCard(new CardGUI(CardSuit.diamonds, CardRank.eight));
            //hand.addCard(new CardGUI(CardSuit.diamonds, CardRank.ace));
            //hand.InsuranceBet = 250;

            //HandGUI hand2 = new HandPlayerGUI(250);
            //hand2.addCard(new CardGUI(CardSuit.diamonds, CardRank.two));
            //hand2.addCard(new CardGUI(CardSuit.diamonds, CardRank.four));
            //hand2.addCard(new CardGUI(CardSuit.spades, CardRank.three));
            //hand.clearHand();
            // player.appendHand(hand);
            //player.appendHand(hand2);
            //player.split(new CardGUI(CardSuit.diamonds, CardRank.eight), new CardGUI(CardSuit.spades, CardRank.eight));

            betBlocks = new TextBlock[4];
            betBlocks[0] = betTextBlock;
            betBlocks[1] = betTextBlock_Copy;
            betBlocks[2] = betTextBlock_Copy1;
            betBlocks[3] = betTextBlock_Copy2;

            handBlocks = new TextBlock[4];
            handBlocks[0] = handTextBlock;
            handBlocks[1] = handTextBlock_Copy;
            handBlocks[2] = handTextBlock_Copy1;
            handBlocks[3] = handTextBlock_Copy2;
            owners = new List<Owner<Hand<Card>,Card>>();
            owners.Add(new Player<Hand<Card>, Card>(5000));
            owners.Add(new Dealer<Hand<Card>, Card>());

            optionCanvas.Visibility = Visibility.Hidden;
            endGameButton.Visibility = Visibility.Hidden;
            bankTextBlock.Text = owners[0].Bank.ToString();
            deck = new Deck<CardGUI>();

        }

        private void placeBetButton_Click(object sender, RoutedEventArgs e)
        {
            int bet;
            if (!int.TryParse(betTextBox.Text, out bet) || bet <= 0 )
            {
                betTextBox.Focus();
                betTextBox.SelectAll();
            }
            else
            {
                int prevBet;
                int diff = bet;
                if (int.TryParse(betTextBlock.Text,out prevBet))
                {
                    diff -= prevBet;
                }
                
                if (diff <= owners[0].Bank)
                {
                    owners[0].Bank -= diff;
                    betTextBlock.Text = bet.ToString();
                    bankTextBlock.Text = owners[0].Bank.ToString();
                }
                else
                {
                    betTextBox.Focus();
                    betTextBox.SelectAll();
                }
            }
            
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {

            //    if (deck.Count <= deck.CutCardLocation)
            //        deck.shuffle();
            //    int bet;
            //    if (!int.TryParse(betTextBlock.Text, out bet) || bet <= 0)
            //    {
            //        betTextBox.Focus();
            //        betTextBox.SelectAll();
            //    }
            //    else
            //    {
            //        Hand playerHand = new Hand(bet);
            //        owners[0].insertHand(playerHand);
            //        owners[1] = new Dealer();
            //        owners[1].insertHand(new Hand(0));


            //        for (int i =0; i<2; ++i)
            //        {
            //            foreach(Owner owner in owners)
            //            {
            //                owner.CurrentHand.addCard(deck.Dequeue());
            //            }
            //        }
            //        handBlocks[0].Text = owners[0].CurrentHand.ToString();
            //        dealerHandTextBlock.Text = owners[1].CurrentHand.ToString();
            //        startGameButton.Visibility = Visibility.Hidden;
            //        placeBetButton.Visibility = Visibility.Hidden;
            //        enableOptions();
            //        optionCanvas.Visibility = Visibility.Visible;
            //    }



        }

        private void enableOptions()
        {
            if (owners[0].IsFinished)
                endGame();
            if (owners[0].canSurrender())
                surrenderButton.IsEnabled = true;
            else
                surrenderButton.IsEnabled = false;
            if (owners[0].canSplit())
                splitButton.IsEnabled = true;
            else
                splitButton.IsEnabled = false;
            if (owners[0].canDoubleDown())
                doubleDownButton.IsEnabled = true;
            else
                doubleDownButton.IsEnabled = false;
            if (owners[0].canHit())
                hitButton.IsEnabled = true;
            else
                hitButton.IsEnabled = false;
            if (owners[0].canStay())
                stayButton.IsEnabled = true;
            else
                stayButton.IsEnabled = false;

        }

        private void surrenderButton_Click(object sender, RoutedEventArgs e)
        {
        //    owners[0].surrender();

        //    if (owners[0].IsFinished)
        //        endGame();
        //    else
        //    {
        //        enableOptions();
        //        handBlocks[0].Text = owners[0].CurrentHand.ToString();
        //    }

        }

        private void splitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void doubleDownButton_Click(object sender, RoutedEventArgs e)
        {
            CardInterface newCard = deck.Dequeue();
            int prevValue = owners[0].CurrentHand.Value;
            betBlocks[0].Text = (owners[0].CurrentHand.Bet * 2).ToString();
            
            //owners[0].doubleDown(newCard);
            bankTextBlock.Text = owners[0].Bank.ToString();
            

            //if (prevValue + newCard.Value > Hand.perfectScore)
            //{
            //    handBlocks[0].Text += " + " + newCard.ToString();
            //}
            //else
            //{
            //    handBlocks[0].Text += " [" + newCard.ToString() + "]";
            //}
            
            if (owners[0].IsFinished)
                endGame();
            else
                enableOptions();
        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            CardInterface newCard = deck.Dequeue();
            //owners[0].hit(newCard);
            if (owners[0].IsFinished)
            {
                handBlocks[0].Text += " + " + newCard.ToString(); 
                endGame();
            }
            else
            {
                enableOptions();
                handBlocks[0].Text = owners[0].CurrentHand.ToString();
            }
        }

        private void stayButton_Click(object sender, RoutedEventArgs e)
        {
            owners[0].stay();
            if (owners[0].IsFinished)
                endGame();
            else
            {
                enableOptions();
                handBlocks[0].Text = owners[0].CurrentHand.ToString();
            }
        }

        private void endGame()
        {
            endGameButton.Visibility = Visibility.Visible;
            optionCanvas.Visibility = Visibility.Hidden;
            CardInterface nextCard = null;
            while (owners[1].canHit() && owners[0].NumberOfHands > 0)
            {
                nextCard = deck.Dequeue();
                dealerHandTextBlock.Text = owners[1].CurrentHand.ToString();
                //owners[1].hit(nextCard);
                
            }
            if (owners[1].NumberOfHands == 0)
            {
                dealerHandTextBlock.Text += " + " + nextCard.ToString();
                //owners[1].insertHand(new Hand(0, new Card(CardSuit.clubs, CardRank.two)));
            }
            else
            {
                dealerHandTextBlock.Text = owners[1].CurrentHand.ToString();
            }
        }

        private void endGameButton_Click(object sender, RoutedEventArgs e)
        {
            owners[0].settleBetsWithHouse(owners[1].CurrentHand.Value);
            handBlocks[0].Text = "";
            dealerHandTextBlock.Text = "";
            bankTextBlock.Text = owners[0].Bank.ToString();
            betTextBlock.Text = "0";
            placeBetButton.Visibility = Visibility.Visible;
            startGameButton.Visibility = Visibility.Visible;
            optionCanvas.Visibility = Visibility.Hidden;
            endGameButton.Visibility = Visibility.Hidden;
        }
    }
}
