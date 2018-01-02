namespace CSC460BlackJack
{
    /// <summary>
    ///     /// description of each method can be found in owner interface
    /// 
    /// this is a wrapper class for the owner (owners can be player or dealer)
    /// uses the wrapped class for the implementation logic and then does the graphical 
    /// stuff here
    /// </summary>
    public class DealerGUI : OwnerGUI
    {
        public DealerGUI() : base(new Dealer<HandGUI, CardGUI>())
        {

        }
    }
}
