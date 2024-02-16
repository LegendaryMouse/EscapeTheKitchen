using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public TMP_InputField width; // Input field for width
    public TMP_InputField height; // Input field for height
    public TMP_InputField spawners; // Input field for spawners
    public TMP_InputField bridgeLength; // Input field for bridge length
    public TMP_InputField doorWidth; // Input field for door width
    public TMP_InputField LootChance; // Input field for loot chance

    public EG eg; // Reference to EG script

    private void Start()
    {
        // Set the initial values of the input fields to the corresponding values in the EG script
        width.text = eg.width + "";
        height.text = eg.height + "";
        spawners.text = eg.spawnersAmount + "";
        bridgeLength.text = eg.bridgeWidth + "";
        doorWidth.text = eg.doorWidth + "";
        LootChance.text = eg.chestRoomFrequency + "";
    }

    public void Change()
    {
        // Update the values in the EG script with the values from the input fields
        eg.width = Int32.Parse(width.text);
        eg.height = Int32.Parse(height.text);
        eg.spawnersAmount = Int32.Parse(spawners.text);
        eg.bridgeWidth = Int32.Parse(bridgeLength.text);
        eg.doorWidth = Int32.Parse(doorWidth.text);
        eg.chestRoomFrequency = Int32.Parse(LootChance.text);
        Debug.Log("yes");
    }
}
