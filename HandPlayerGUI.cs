using System.Windows.Controls;

namespace CSC460BlackJack
{
    /// <summary>
    /// wraps the hand class, see handinterface for description of methods
    /// </summary>
    public class HandPlayerGUI : HandGUI
    {
        //each card spans across multiple columns/rows
        private int cardSpan = 3;
        private int standardSpace = 4;

        public HandPlayerGUI() : base() {
            while (handGrid.RowDefinitions.Count < standardSpace + cardSpan)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
                handGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public HandPlayerGUI(int bet) :base(bet)
        {
            while (handGrid.RowDefinitions.Count < standardSpace + cardSpan)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
                handGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public HandPlayerGUI(int bet, CardGUI card) : base(bet, card)
        {
            while (handGrid.RowDefinitions.Count < standardSpace + cardSpan)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
                handGrid.RowDefinitions.Add(new RowDefinition());
            }
        }
        
        

        public override void addCard(CardGUI card)
        {
            int cardCount = Size;

            base.addCard(card);
            //add another row/column if there aren't enough already (starts with 6)
            while (handGrid.RowDefinitions.Count < cardCount + cardSpan)
            {
                handGrid.ColumnDefinitions.Add(new ColumnDefinition());
                handGrid.RowDefinitions.Add(new RowDefinition());
            }
            //add new card into grid and set the row/column of the top corner
            //next set how many rows/columns the object spans
            handGrid.Children.Add(card);
            Grid.SetColumn(card, cardCount);
            Grid.SetRow(card, cardCount);
            Grid.SetColumnSpan(card, cardSpan);
            Grid.SetRowSpan(card, cardSpan);

        }

        public override HandInterface<CardGUI> splitCards(int bet)
        {
            HandInterface<CardGUI> temp = base.splitCards(bet);
            CardGUI card = temp.FirstCard;
            handGrid.Children.Remove(card);
            Grid.SetColumn(FirstCard,0);
            Grid.SetRow(FirstCard, 0);
            temp.clearHand();
            return new HandPlayerGUI(bet, card);
        }
    }
}