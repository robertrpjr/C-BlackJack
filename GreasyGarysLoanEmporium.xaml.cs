using System.Windows;

namespace CSC460BlackJack
{
    /// <summary>
    /// Interaction logic for GreasyGarysLoanEmporium.xaml
    /// </summary>
    public partial class GreasyGarysLoanEmporium : Window
    {
        public bool legsBroken;
        private int playerBank;
        public int newLoan;
        private int debt;
        public GreasyGarysLoanEmporium(int playerBank, int debt)
        {
            InitializeComponent();
            this.playerBank = playerBank;
            this.debt = debt;
            legsBroken = false;
        }

        /// <summary>
        /// when the player agrees, check that the amount they selected and whether they want to pay or take out loans is valid
        /// break the players legs (close the game) if they try to take out too much money
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void agreeButton_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (int.TryParse(loanTextBox.Text,out newLoan) && newLoan >= 0)
            {
                if ((bool)payRadio.IsChecked)
                {
                    if (newLoan > playerBank)
                    {
                        if (newLoan <= debt)
                        {
                            MessageBox.Show("You tried to pay Greasy Gary with IOU's. Unfortunately, Greasy Gary only accepts payments in cold hard cash, so he broke your legs and threw you in the river");
                            legsBroken = true;
                            Close();
                            return;
                        }
                        else
                        {
                            loanTextBox.Focus();
                            loanTextBox.SelectAll();
                            return;
                        }
                    }
                    if (newLoan > debt)
                    {
                        MessageBox.Show("You paid Greasy Gary more than you owe him. After pocketing the change, Greasy Gary says that he appreciates your generosity, but still won't hesitate to break your legs if you ever come up short.");
                        
                    }
                    newLoan = 0 - newLoan;

                    Close();
                    return;
                }
                if (debt > 100000) 
                {
                    if ((bool)borrowRadio.IsChecked && playerBank < debt / 3)
                    {
                        MessageBox.Show("Greasy Gary decided that he wanted the money you already owe him.  After realizing you couldn't pay him back, he broke your legs and threw you in the river");
                        legsBroken = true;
                        Close();
                        return;
                    }
                }
                if (newLoan > 100000)
                {
                    MessageBox.Show("Theres no way I'm giving a scumbag like you more than a hundred grand, I ought to break your legs just for asking!");
                    return;
                }
                Close();
                
            }
            else
            {
                loanTextBox.Focus();
                loanTextBox.SelectAll();
            }

        }

        /// <summary>
        /// close the window and disregard any changes that were made;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void declineButton_Click(object sender, RoutedEventArgs e)
        {
            newLoan = 0;
            Close();

        }
    }
}
