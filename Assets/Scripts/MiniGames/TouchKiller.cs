using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchKiller : MonoBehaviour
{
    void OnMouseEnter()
    {
        DeathCounter DC = FindObjectOfType<DeathCounter>();
        DC.count++;
        Debug.Log("hit!");
        SceneManager.LoadScene(2);
    }
}