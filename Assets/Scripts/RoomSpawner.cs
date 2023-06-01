using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] roomVariations;

    private void Start()
    {
        Collider2D[] check = Physics2D.OverlapCircleAll(transform.position, 1f);

        if (check.Length == 1)
            Invoke("CreateRoom", 0.4f);
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<RoomSpawner>())
            if (collision.GetComponent<RoomSpawner>().isSpawned == true)
            {
                Destroy(gameObject);
            }
    }*/

    private void CreateRoom()
    {
        int r = Random.Range(0, roomVariations.Length);
        Instantiate(roomVariations[r], transform.position, Quaternion.identity);
        //Debug.Log(roomVariations[r] + " " + transform.position);
        //Destroy(gameObject);
    }
}
