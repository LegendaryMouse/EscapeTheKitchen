using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public float score;
    public TextMeshProUGUI text;


    private void Start()
    {
        score = PlayerPrefs.GetFloat("score");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            score = 0;
        }

        PlayerPrefs.SetFloat("score", score);
        string rawText = "Score: " + score;
        text.text = rawText;

    }
}
