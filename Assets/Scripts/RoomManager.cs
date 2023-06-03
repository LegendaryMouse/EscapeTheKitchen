using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors;
    public List<GameObject> enemySpawners;
    public List<GameObject> enemies;
    public List<Collider2D> all;

    public bool enemiesSpawned = false;
    public bool doorOpened = false;

    private void Start()
    {
        all = new List<Collider2D>(Physics2D.OverlapAreaAll(transform.position - new Vector3(14, 8), transform.position + new Vector3(14, 8)));

        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].GetComponent<DoorDeleter>())
            {
                doors.Add(all[i].gameObject);
            }
            if (all[i].GetComponent<EnemySpawner>())
            {
                enemySpawners.Add(all[i].gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !enemiesSpawned)
        {
            for (int i = 0; i < enemySpawners.Count; i++)
            {
                if (enemySpawners[i])
                    enemySpawners[i].GetComponent<EnemySpawner>().Spawn();
            }
            enemiesSpawned = true;
        }
        if (collision.CompareTag("Player") && !doorOpened)
        {
            all = new List<Collider2D>(Physics2D.OverlapAreaAll(transform.position - new Vector3(13, 7), transform.position + new Vector3(13, 7)));
            enemies.Clear();
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].GetComponent<Enemy>())
                {
                    enemies.Add(all[i].gameObject);
                }
            }
            if (enemies.Count == 0)
            {
                for (int i = 0; i < doors.Count; i++)
                {
                    if (doors[i])
                        if (doors[i].transform.GetChild(0).CompareTag("Door"))
                        {
                            Debug.Log(doors[i].transform.GetChild(0));
                            Destroy(doors[i].transform.GetChild(0).gameObject);
                            Destroy(doors[i].transform.GetChild(1).gameObject);
                        }
                }
                doorOpened = true;
            }
        }
    }
}
