using System;

namespace CSC460BlackJack
{
    /// <summary>
    /// Class used to save the game variables to a .bin file
    /// 
    /// Exiting the game is like getting up from the table in a casino.
    /// </summary>
    [Serializable()]
    class SaveGame
    {

        public int PlayerBank;
        public int PlayerDebt;
        public int Hands;
        public int BetPerHand;
        public int Decks;
        public bool LegsBroken;

        public SaveGame(int playerBank, int playerDebt, int hands, int betPerHand,int decks, bool legsBroken)
        {
            PlayerBank = playerBank;
            PlayerDebt = playerDebt;
            Hands = hands;
            BetPerHand = betPerHand;
            Decks = decks;
            LegsBroken = legsBroken;
        }
    }
}
