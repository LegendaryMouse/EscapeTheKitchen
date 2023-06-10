using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mixer : MonoBehaviour
{
    public Collider2D[] allCards;
    public Vector2 downLeft;
    public Vector2 xOffset;
    public Vector2 yOffset;
    public int cardsInString;
    public int cardsAmount;
    public GameObject card;
    public int mixIterations;


    int counter = 1;
    int numerator = 1;

    public int currentNumber = 0;
    public GameObject currentCard;
    public float closeTime;
    public bool isCardClosed = true;
    //bool win = false;


    private void Start()
    {
        for(int i=0;i<cardsAmount-1;i++)
        {
            Instantiate(card, transform);
        }

        allCards = Physics2D.OverlapCircleAll(transform.position, 40);

        for (int i = 0; i < allCards.Length; i++)
        {
            if (i < cardsInString)
                allCards[i].transform.position = downLeft + xOffset * i;
            else if (i < cardsInString * 2)
                allCards[i].transform.position = downLeft + yOffset * 1 + xOffset * (i - cardsInString * 1);
            else if (i < cardsInString * 3)
                allCards[i].transform.position = downLeft + yOffset * 2 + xOffset * (i - cardsInString * 2);
            else if (i < cardsInString * 4)
                allCards[i].transform.position = downLeft + yOffset * 3 + xOffset * (i - cardsInString * 3);
            else if (i < cardsInString * 5)
                allCards[i].transform.position = downLeft + yOffset * 4 + xOffset * (i - cardsInString * 4);

            if (counter == 3)
            {
                counter = 1;
                numerator++;
            }
            if (counter < 3)
            {
                allCards[i].GetComponent<Card>().Rename(numerator);
                counter++;
            }
        }

        CardMixer(mixIterations);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Menu();
        }
    }

    public void CardMixer(int mixIterations)
    {
        for(int i = 0; i < mixIterations; i++)
        {
            int rand1 = Random.Range(0, allCards.Length);
            int rand2 = Random.Range(0, allCards.Length);

            Vector3 pos1 = allCards[rand1].transform.position;
            Vector3 pos2 = allCards[rand2].transform.position;

            allCards[rand1].transform.position = pos2;
            allCards[rand2].transform.position = pos1;

            //Debug.Log(pos1 + " " + pos2);
        }
        

    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
}
