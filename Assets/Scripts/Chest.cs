using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject[] allLoot;
    private bool looted;
    public int keyPrice;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") & collision.GetComponent<Player>().keysAmount >= keyPrice)
        {
            if (!looted)
            {
                Instantiate(allLoot[Random.Range(0, allLoot.Length)], transform.position, Quaternion.identity);
                collision.GetComponent<Player>().keysAmount--;
                looted = true;
            }
        }
    }
}
