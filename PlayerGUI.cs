namespace CSC460BlackJack
{
    /// <summary>

    /// this is a wrapper class for the owner (owners can be player or dealer)
    /// uses the wrapped class for the implementation logic and then does the graphical 
    /// stuff here
    /// </summary>
    class PlayerGUI : OwnerGUI, OwnerInterface<HandGUI,CardGUI>
    {
        
        public PlayerGUI(int bank) : base(new Player<HandGUI, CardGUI>(bank))
        {
            
        }


        
    }
}
