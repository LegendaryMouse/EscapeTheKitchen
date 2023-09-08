using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public TMP_InputField width;
    public TMP_InputField height;
    public TMP_InputField spawners;
    public TMP_InputField bridgeLength;
    public TMP_InputField doorWidth;
    public TMP_InputField LootChance;

    public EG eg;

    private void Start()
    {
        width.text = eg.width + "";
        height.text = eg.height + "";
        spawners.text = eg.spawnersAmount + "";
        bridgeLength.text = eg.bridgeWidth + "";
        doorWidth.text = eg.doorWidth + "";
        LootChance.text = eg.chestRoomFrequency + "";
    }

    public void Change()
    {
        eg.width =Int32.Parse(width.text);
        eg.height = Int32.Parse(height.text);
        eg.spawnersAmount = Int32.Parse(spawners.text);
        eg.bridgeWidth = Int32.Parse(bridgeLength.text);
        eg.doorWidth = Int32.Parse(doorWidth.text);
        eg.chestRoomFrequency = Int32.Parse(LootChance.text);
        Debug.Log("yes");
    }
}
