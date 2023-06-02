using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] roomVariations;
    public bool spawned;

    private void Start()
    {
        Collider2D[] check = Physics2D.OverlapCircleAll(transform.position, 1f);

        if (check.Length == 1)
            Invoke("CreateRoom", 0.4f);
        else if (check[1].GetComponent<RoomSpawner>())
        {
            Destroy(gameObject);
            //Destroy(check[1]);
            Invoke("CreateRoom", 0.4f);
        }
    }

    private void CreateRoom()
    {
        int r = Random.Range(0, roomVariations.Length);
        Instantiate(roomVariations[r], transform.position, Quaternion.identity);
        spawned = true;
        //Debug.Log(roomVariations[r] + " " + transform.position);
        //Destroy(gameObject);
    }
}
