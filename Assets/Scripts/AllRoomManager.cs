using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllRoomManager : MonoBehaviour
{
    public int roomCount;
    public int roomLimit;
    public GameObject[] allRooms;

    public int mostFurther;
    public GameObject mostFurtherRoom;

    public bool testMark;
    public float time;

    public GameObject[] item;

    private void Start()
    {
        Invoke("Beb", time);
    }
    private void Update()
    {
        if (testMark)
            SceneManager.LoadScene(4);
    }
    private void Beb()
    {

        if (roomCount / roomLimit < 0.8 | roomCount / roomLimit > 1.2)
            SceneManager.LoadScene(0);

        for (int i = 0; i < allRooms.Length; i++)
        {
            if (allRooms[i])
                if (!(allRooms[i].GetComponent<EG>().currentSignNumber == 3))
                    if (allRooms[i].GetComponent<EG>().further >= mostFurther & !allRooms[i].GetComponent<EG>().leftDoor[0] & !allRooms[i].GetComponent<EG>().rightDoor[0])
                    {
                        mostFurther = allRooms[i].GetComponent<EG>().further;
                        mostFurtherRoom = allRooms[i];
                    }
        }

        //Instantiate(item[Random.Range(0, item.Length)], mostFurtherRoom.transform.position, Quaternion.identity);
        try
        {
            for (int i = 0; i < mostFurtherRoom.GetComponent<EG>().allEnemySpawners.Length; i++)
            {
                Destroy(mostFurtherRoom.GetComponent<EG>().allEnemySpawners[i]);
            }
            mostFurtherRoom.GetComponent<EG>().currentSignNumber = 0;
            GameObject bossSpawner = Instantiate(mostFurtherRoom.GetComponent<EG>().enemySpawner, mostFurtherRoom.transform.position, Quaternion.identity);
            mostFurtherRoom.GetComponent<EG>().allEnemySpawners[0] = bossSpawner;
            bossSpawner.GetComponent<EnemySpawner>().spawnList = item;
        }
        catch
        {
            Debug.Log("no boss room");
            SceneManager.LoadScene(0);
        }
    }
}
