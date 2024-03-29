﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCard : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Cross cross;
    private UserInput userInput;

    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Cross.GenerateDeck();
        cross = FindObjectOfType<Cross>();
        userInput = FindObjectOfType<UserInput>();

        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = cross.cardFaces[i];
                break;
            }
            i++;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }

        if (userInput.slot1)
        {

            if (name == userInput.slot1.name)
            {
                spriteRenderer.color = Color.blue;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
