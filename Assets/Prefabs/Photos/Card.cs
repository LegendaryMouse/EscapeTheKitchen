using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite[] pics;
    public Mixer mixer;
    private int rand;

    private void Start()
    {
        transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = pics[int.Parse(transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text)-1];
    }

    private void OnMouseDown()
    {
        if (mixer.isCardClosed)
        {
            //transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Animation>().Play("CardOpen");

            if (!(mixer.currentNumber == 0))
            {
                mixer.isCardClosed = false;
                if (mixer.currentNumber == int.Parse(transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text))
                {
                    transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = new Color(0,1,0,0.2f);
                    mixer.currentCard.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = new Color(0,1,0,0.2f);
                    Invoke("CardDelete", mixer.closeTime);
                }
                else
                {
                    transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.red;
                    mixer.currentCard.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.red;
                    mixer.currentNumber = 0;
                    //mixer.currentCard = null;
                    Invoke("CardClose", mixer.closeTime);
                }
            }
            else
            {
                mixer.currentCard = gameObject;
                mixer.currentNumber = int.Parse(transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Rename(int number)
    {
        if (number == -1)
            rand = Random.Range(1, mixer.allCards.Length);
        else
            rand = number;

        transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = rand + "";
        name = rand + "";
    }

    public void CardClose()
    {
        GetComponent<Animation>().Play("CardClose");
        mixer.currentCard.GetComponent<Animation>().Play("CardClose");
        transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.black;
        mixer.currentCard.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().color = Color.black;
        transform.GetChild(0).gameObject.SetActive(true);
        mixer.currentCard.transform.GetChild(0).gameObject.SetActive(true);
        mixer.isCardClosed = true;
    }

    public void CardDelete()
    {
        mixer.isCardClosed = true;
        mixer.currentNumber = 0;
        Destroy(mixer.currentCard);
        Destroy(gameObject);
        

    }
}
