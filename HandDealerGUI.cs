using System.Windows.Controls;

namespace CSC460BlackJack
{
    // Interaction logic for DealerHandGUI.xaml

        /// <summary>
        /// for description of methods see hand interface
        /// </summary>
    public partial class HandDealerGUI : HandGUI
    {
        private int startingSlots = 5;

        public HandDealerGUI() : base()
        {
            //make 5 initial slots for cards at the beginning of the game
            while (handGrid.ColumnDefinitions.Count < startingSlots)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        
        public override void addCard(CardGUI card)
        {

            int cardCount = Size;
            base.addCard(card);
            //adds the cards all in a row - if there aren't enough slots, then a new one is made
            while (cardCount + 1 > handGrid.ColumnDefinitions.Count)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            handGrid.Children.Add(card);
            Grid.SetColumn(card, cardCount);

        }
    }
}