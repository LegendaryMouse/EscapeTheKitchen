using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public GameObject player;
    Player playerScript;
    public TextMeshProUGUI text;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {

        text.text = ("HP " + playerScript.hp);
    }
}
