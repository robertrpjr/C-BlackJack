using System;
using System.Collections.Generic;

namespace CSC460BlackJack
{

    // 
    /// <summary>
    /// Subclass of Queue - utilizes enqueue and dequeue methods already in place
    /// for description of methods, see deckinterface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Deck<T>  : Queue<T>, DeckInterface where T : CardInterface, new()
    {


        // --------------------------------------------------------------------------------------------
        // --- Fields
        // --------------------------------------------------------------------------------------------


        private int NUM_DECKS;
        private int cutCardLocation;
        private T[] orderedCards;   //standard 52 card deck in order
        private int deckSize = 52;  //52 cards per deck



        // --------------------------------------------------------------------------------------------
        // --- Getters/Setters
        // --------------------------------------------------------------------------------------------

        public int CutCardLocation
        {
            get
            {

                return cutCardLocation;
            }
        }


        public int Size
        {
            get { return Count; }
        }

        public int NumberOfDecks
        {
            get
            {
                return NUM_DECKS;
            }
        }

        // --------------------------------------------------------------------------------------------
        // --- Constructors
        // --------------------------------------------------------------------------------------------

        public Deck(int numberOfDecks)
        {
            //initialize deck and shuffle
            
            int index = 0;
            NUM_DECKS = numberOfDecks;
            deckSize = NUM_DECKS * 52;
            orderedCards = new T[deckSize];
            for (int i = 0; i < NUM_DECKS; ++i)
            {
                foreach (CardRank r in Enum.GetValues(typeof(CardRank)))  //initialize deck
                {
                    //Uses Generics to create cards so that cards can be either Card or CardGUI
                    foreach (CardSuit s in Enum.GetValues(typeof(CardSuit)))
                    {
                        T temp = new T();
                        temp.Suit = s;
                        temp.Rank = r;
                        orderedCards[index++] = temp;
                    }
                }
            }

            shuffle();

        }

        public Deck() : this(1) { } 



        // --------------------------------------------------------------------------------------------
        // --- Methods
        // --------------------------------------------------------------------------------------------



        public void shuffle()
        {
            // reset deck to contain NUM_DECKS of each card, shuffle and reset cutCard
            while (this.Count > 0)
            {
                this.Dequeue();
            }
            Random rnd = new Random();

            List<int> randomNumList = new List<int>();  //list to hold random numbers
            int myNumber = 0; //keep track of current random number


            ///Creates a list of random numbers from 0 - 51
            while (randomNumList.Count < deckSize)  //fill random number list with unique numbers 
            {
                myNumber = rnd.Next(0, deckSize);    //from 0 to 51 inclusive, do not shuffle cut card.  Instead, place cutCard in deck
                if (!randomNumList.Contains(myNumber))  //if number not in list
                    randomNumList.Add(myNumber);        //add it to the list
            }

            cutCardLocation = rnd.Next(17, 22);  //index position to insert cutCard

            for (int i = 0; i < deckSize; i++)  //shuffle cards; orderedCard at cutCardPostion will be moved to end of deck
            {
                this.Enqueue(orderedCards[randomNumList[i]]);  //choose a random card from the ordered deck
                                                               //and place it in the shuffled deck
            }
        }
    }
}
