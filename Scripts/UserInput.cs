using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject slot1;

    private Cross cross;

    // Start is called before the first frame update
    void Start()
    {
        cross = FindObjectOfType<Cross>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                //what has been hit? - deck/card/empty slot
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Cross"))
                {
                    Cross();
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom();
                }
            }
        }
    }

    void Deck()
    {
        print("Clicked on deck!");
        cross.DealFromDeck();
        
    }

    void Card(GameObject selected)
    {
        print("Clicked on card!");

        //if the card clicked on is facedown
        //if the card clicked on is not blocked
        //flip it over

        //if the card clicked on is in the deck pile with the trips
        //if it is not blocked
        //select it

        //if the card is face up
        //if there is no card currently selected
        //select the card

        if (slot1 == this.gameObject)
        {
            slot1 = selected;
        }

        else if (slot1 != selected)
        {
            //if there is already a card selected (and it is not the same card)
            //if the new card is eligible to stack on the old card
            //stack it
            //else
            //select the new card
            slot1 = selected;
        }   
        //else if there is already a card selected and it is the same card
        //if the time is short enough it is a double click
        //if the card is eligible to fly up top then do it

    }

    void Cross()
    {
        print("Clicked on cross!");
    }

    void Bottom()
    {
        print("Clicked on bottom!");
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();
        //compare them to see is they stack
        return false;
    }
}
