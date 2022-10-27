using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] crossPos;
    public GameObject[] bottomPos;
    public GameObject deckButton;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", };
    public List<string>[] cross;
    public List<string>[] bottoms;

    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    public List<string> discardPile = new List<string>();

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();

    public List<string> deck;

    private int trips;
    private int tripsRemainder;
    private int deckLocation;

    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3 };
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        foreach (string card in deck)
        {
            print(card);
        }

        CrossDeal();
        SortDeckIntoTrips();
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void CrossDeal()
    {

        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            newCard.name = card;
            //newCard.GetComponent<Selectable>().row = i;
            newCard.GetComponent<Selectable>().faceUp = false;

            discardPile.Add(card);

        }

        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }

        discardPile.Clear();
    }

    void CrossSort()
    {

    }

    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 1;
        tripsRemainder = deck.Count % 1;
        deckTrips.Clear();

        int modifier = 0;
        for (int i = 0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 1; j++) //may be here?
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 1; //may be here?
        }

        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            deckTrips.Add(myRemainders);
            trips++;
        }

        deckLocation = 0;
    }

    public void DealFromDeck()
    {
        //add remaining cards to discard pile
        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }

            if (deckLocation < trips)
            {
                //may draw 3 cards
                tripsOnDisplay.Clear();
                float xOffset = 2.5f;
                float zOffset = -0.2f;

                foreach (string card in deckTrips[deckLocation])
                {
                    GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                    xOffset = xOffset + 0.5f;
                    zOffset = zOffset - 0.2f;
                    newTopCard.name = card;
                    tripsOnDisplay.Add(card);
                    newTopCard.GetComponent<Selectable>().faceUp = true;
                    newTopCard.GetComponent<Selectable>().inDeckPile = true;
                }
                deckLocation++;

            }

            else
            {
                //Restack the top deck
                RestackTopDeck();
            }
        }

        void RestackTopDeck()
        {
            foreach (string card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
            SortDeckIntoTrips();
        }
    }
}
