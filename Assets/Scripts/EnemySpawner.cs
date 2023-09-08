using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnList;
    public bool spawned;
    public GameObject warningSignPrefab;
    [System.NonSerialized] public float timeDelay = 1f;
    GameObject warningSign;

    private void Start()
    {
        //Spawn();
    }
    public void SpawnWithWarning()
    {
        warningSign = Instantiate(warningSignPrefab, transform.position, Quaternion.identity);
        Invoke("Spawn", timeDelay);
    }
        public void Spawn()
    {
        Destroy(warningSign);
        int rand = Random.Range(0, spawnList.Length);
        Instantiate(spawnList[rand], transform.position, Quaternion.identity);
        //rand = Random.Range(0, spawnList.Length);
        //Instantiate(spawnList[rand], transform.position, Quaternion.identity);
        spawned = true;
        //Destroy(gameObject);
    }
}
