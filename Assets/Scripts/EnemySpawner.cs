using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnList; // Array of enemy prefabs to spawn
    public bool spawned; // Flag to check if an enemy has been spawned
    public GameObject warningSignPrefab; // Prefab for the warning sign
    [System.NonSerialized] public float timeDelay = 1f; // Delay before spawning an enemy
    private GameObject warningSign; // Reference to the instantiated warning sign

    private void Start()
    {
        //Spawn();
    }

    public void SpawnWithWarning()
    {
        warningSign = Instantiate(warningSignPrefab, transform.position, Quaternion.identity); // Instantiate the warning sign prefab
        Invoke("Spawn", timeDelay); // Invoke the Spawn() method after the specified time delay
    }

    public void Spawn()
    {
        Destroy(warningSign); // Destroy the warning sign
        int rand = Random.Range(0, spawnList.Length); // Generate a random index within the range of the spawnList array
        Instantiate(spawnList[rand], transform.position, Quaternion.identity); // Instantiate a random enemy prefab from the spawnList array at the spawner's position
        spawned = true; // Set the spawned flag to true
        //Destroy(gameObject);
    }
}
