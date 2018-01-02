namespace CSC460BlackJack
{
    /// <summary>
    /// description of each method can be found in owner interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public class Dealer<T,R> : Owner<T, R> where T : HandInterface<R> where R : CardInterface
    {
        public const int dealerHitValue = 17;

        

        public override bool canSplit()
        {
            return false;
        }

        public override bool canSurrender()
        {
            return false;
        }

        public override bool canDoubleDown()
        {
            return false;
        }

        public override bool canHit()
        {
            return CurrentHand.Value < dealerHitValue;
        }

        public override bool canStay()
        {
            return CurrentHand.Value >= dealerHitValue;
        }
        
        public override void placeInsuranceBet(int bet)
        {
            
        }
    }
}
