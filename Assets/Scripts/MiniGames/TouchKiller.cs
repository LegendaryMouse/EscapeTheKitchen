using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchKiller : MonoBehaviour
{
    void OnMouseOver()
    {
        Debug.Log("hit!");
        SceneManager.LoadScene(2);
    }
}