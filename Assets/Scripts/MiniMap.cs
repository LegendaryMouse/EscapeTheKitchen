using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject miniMap;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            miniMap.SetActive(true);
        }
        else
        {
            miniMap.SetActive(false);
        }
    }
}
