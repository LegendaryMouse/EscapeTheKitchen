using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMe : MonoBehaviour
{
    public CollectedOpener[] collectedOpener;
    public int price = 1;

    private void OnMouseEnter()
    {
        for (int i = 0; i < collectedOpener.Length; i++)
        {
            collectedOpener[i].counter -= price;
        }
        Destroy(gameObject);
    }
}
