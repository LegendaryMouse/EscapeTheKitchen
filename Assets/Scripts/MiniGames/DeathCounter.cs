using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;


public class DeathCounter : MonoBehaviour
{
    public float count;
    public TextMeshProUGUI text;


    private void Start()
    {
        count = PlayerPrefs.GetFloat("count");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            count = 0;
        }

        PlayerPrefs.SetFloat("count", count);
        string rawText = "You died " + count + " times, \n and spent: "+Mathf.RoundToInt(Time.time)+" seconds of your life!";
        text.text = rawText;

    }
}
