using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnList;
    public bool spawned;

    private void Start()
    {
        //Spawn();
    }

    public void Spawn()
    {
        int rand = Random.Range(0, spawnList.Length);
        Instantiate(spawnList[rand], transform.position, Quaternion.identity);
        rand = Random.Range(0, spawnList.Length);
        Instantiate(spawnList[rand], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
