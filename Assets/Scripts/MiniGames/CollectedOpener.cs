using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedOpener : MonoBehaviour
{
    public int counter;

    public void Update()
    {
        if (counter <= 0)
        { 
            Destroy(gameObject);
        }
    }
}
