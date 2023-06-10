using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EG : MonoBehaviour
{
    public GameObject prefabSelf;
    public bool isSpawned;

    public bool haveBridges;

    public bool[] spawnDoor;

    public int moreRoomsChance;

    public GameObject enemySpawner;
    public GameObject[] allEnemySpawners;
    public int spawnersAmount;

    public int width;
    public int height;
    public int doorWidth;
    public int bridgeWidth;
    private float widthFixer = 0.5f;
    private float heightFixer = 0.5f;

    public GameObject wallBlock;
    public GameObject doorBlock;
    public GameObject currentBlock;
    public Collider2D[] blocksToDelete;
    public GameObject[] leftDoor;
    public GameObject[] rightDoor;
    public GameObject[] upDoor;
    public GameObject[] downDoor;
    public GameObject leftRoom;
    public GameObject rightRoom;
    public GameObject upRoom;
    public GameObject downRoom;
    public AllRoomManager manager;
    public float spawnSpeed;
    public int further;
    public float updateFrequency;
    public bool enemySpawned;

    private void Start()
    {

        manager = GameObject.FindWithTag("Manager").GetComponent<AllRoomManager>();
        manager.allRooms[manager.roomCount] = gameObject;
        moreRoomsChance = moreRoomsChance * (1 - manager.roomCount / manager.roomLimit);
        name = "" + manager.roomCount;
        manager.roomCount++;

        //all child delete
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        allEnemySpawners = new GameObject[spawnersAmount];
        leftDoor = new GameObject[doorWidth];
        rightDoor = new GameObject[doorWidth];
        upDoor = new GameObject[doorWidth];
        downDoor = new GameObject[doorWidth];

        if (width % 2 == 0)
        {
            widthFixer = 0.5f;
        }
        if (height % 2 == 0)
        {
            heightFixer = 0.5f;
        }

        leftRoom = null;
        rightRoom = null;
        upRoom = null;
        downRoom = null;

        if (!isSpawned)
        {

            Invoke("RoomCreate", spawnSpeed);
            Invoke("EnemiesNCollider", spawnSpeed * manager.roomLimit);
            Invoke("DoorFixer", spawnSpeed * manager.roomLimit);
        }
    }

    public void RoomCreate()
    {
        GenerateWalls();

        if (manager.roomCount < manager.roomLimit)
        {
            if (spawnDoor[0])
                SpawnDoor0();
            if (spawnDoor[1])
                SpawnDoor1();
            if (spawnDoor[2])
                SpawnDoor2();
            if (spawnDoor[3])
                SpawnDoor3();
        }
        if (manager.roomCount < manager.roomLimit & haveBridges)
        {
            if (spawnDoor[0])
            {
                for (int i = 0; i < bridgeWidth; i++)
                {
                    currentBlock = Instantiate(wallBlock, transform.position + new Vector3(-width / 2 - widthFixer - i, doorWidth / 2 - heightFixer + 1, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                    currentBlock = Instantiate(wallBlock, transform.position + new Vector3(-width / 2 - widthFixer - i, -doorWidth / 2 + heightFixer - 1, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                }
            }
            if (spawnDoor[1])
            {
                for (int i = 0; i < bridgeWidth; i++)
                {
                    currentBlock = Instantiate(wallBlock, transform.position - new Vector3(-width / 2 - widthFixer - i, doorWidth / 2 - heightFixer + 1, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                    currentBlock = Instantiate(wallBlock, transform.position - new Vector3(-width / 2 - widthFixer - i, -doorWidth / 2 + heightFixer - 1, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                }
            }
            if (spawnDoor[2])
            {
                for (int i = 0; i < bridgeWidth; i++)
                {
                    currentBlock = Instantiate(wallBlock, transform.position + new Vector3(doorWidth / 2 - heightFixer + 1, height / 2 + widthFixer + i, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                    currentBlock = Instantiate(wallBlock, transform.position + new Vector3(-doorWidth / 2 + heightFixer - 1, height / 2 + widthFixer + i, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                }
            }
            if (spawnDoor[3])
            {
                for (int i = 0; i < bridgeWidth; i++)
                {
                    currentBlock = Instantiate(wallBlock, transform.position - new Vector3(doorWidth / 2 - heightFixer + 1, height / 2 + widthFixer + i, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                    currentBlock = Instantiate(wallBlock, transform.position - new Vector3(-doorWidth / 2 + heightFixer - 1, height / 2 + widthFixer + i, 0), Quaternion.identity);
                    currentBlock.transform.SetParent(gameObject.transform);
                }
            }
        }
    }

    private void Update()
    {
        StartCoroutine(SometimesChecker());
    }

    private IEnumerator SometimesChecker()
    {
        if (!enemySpawned)
        {
            Collider2D[] allInRoom = Physics2D.OverlapAreaAll(new Vector2(-width / 2.2f + transform.position.x, -height / 2.2f + transform.position.y), new Vector2(width / 2.2f + transform.position.x, height / 2.2f + transform.position.y));
            for (int i = 0; i < allInRoom.Length; i++)
            {
                if (!(name == "0"))
                    if (allInRoom[i].CompareTag("Player"))
                    {
                        for (int b = 0; b < spawnersAmount; b++)
                        {
                            allEnemySpawners[b].GetComponent<EnemySpawner>().SpawnWithWarning();
                        }
                        enemySpawned = true;
                    }
            }

        }
        yield return new WaitForSeconds(updateFrequency);
    }

    public void SpawnDoor0()
    {
        blocksToDelete = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - width / 2 - widthFixer, transform.position.y - (doorWidth / 2.2f)), new Vector2(transform.position.x - width / 2 + widthFixer, transform.position.y + (doorWidth / 2.2f)));
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            leftDoor[i] = Instantiate(doorBlock, blocksToDelete[i].transform.position, Quaternion.identity);
            leftDoor[i].transform.SetParent(gameObject.transform);
            Destroy(blocksToDelete[i].gameObject);
        }
        //left room generation
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) - new Vector2(width + bridgeWidth, 0), 1))
        {
            leftRoom = Instantiate(prefabSelf, transform.position - new Vector3(width + bridgeWidth, 0, 0), Quaternion.identity);
            EG eg = leftRoom.GetComponent<EG>();

            eg.width = width;
            eg.height = height;
            eg.doorWidth = doorWidth;
            eg.bridgeWidth = bridgeWidth;
            eg.moreRoomsChance = moreRoomsChance;
            eg.further = further + 1;
            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 100) < moreRoomsChance)
                {
                    eg.spawnDoor[i] = true;
                }
                else
                {
                    eg.spawnDoor[i] = false;
                }
            }
            eg.spawnDoor[1] = true;
        }
        else
            leftRoom = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) - new Vector2(width + bridgeWidth, 0), 1).gameObject;
        //end
    }
    public void SpawnDoor1()
    {
        blocksToDelete = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + width / 2 - widthFixer, transform.position.y - (doorWidth / 2.2f)), new Vector2(transform.position.x + width / 2 + widthFixer, transform.position.y + (doorWidth / 2.2f)));
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            rightDoor[i] = Instantiate(doorBlock, blocksToDelete[i].transform.position, Quaternion.identity);
            rightDoor[i].transform.SetParent(gameObject.transform);
            Destroy(blocksToDelete[i].gameObject);
        }
        //right room generation
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + new Vector2(width + bridgeWidth, 0), 1))
        {
            rightRoom = Instantiate(prefabSelf, transform.position + new Vector3(width + bridgeWidth, 0, 0), Quaternion.identity);
            EG eg = rightRoom.GetComponent<EG>();
            eg.width = width;
            eg.height = height;
            eg.doorWidth = doorWidth;
            eg.bridgeWidth = bridgeWidth;
            eg.moreRoomsChance = moreRoomsChance;
            eg.further = further + 1;
            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 100) < moreRoomsChance)
                {
                    eg.spawnDoor[i] = true;
                }
                else
                {
                    eg.spawnDoor[i] = false;
                }
            }
            eg.spawnDoor[0] = true;
        }
        else
            rightRoom = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + new Vector2(width + bridgeWidth, 0), 1).gameObject;
        //end
    }
    public void SpawnDoor2()
    {
        blocksToDelete = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - (doorWidth / 2.2f), transform.position.y + height / 2 - heightFixer), new Vector2(transform.position.x + (doorWidth / 2.2f), transform.position.y + height / 2 + heightFixer));
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            upDoor[i] = Instantiate(doorBlock, blocksToDelete[i].transform.position, Quaternion.identity);
            upDoor[i].transform.SetParent(gameObject.transform);
            Destroy(blocksToDelete[i].gameObject);
        }
        //up room generation
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + new Vector2(0, height + bridgeWidth), 1))
        {
            upRoom = Instantiate(prefabSelf, transform.position + new Vector3(0, height + bridgeWidth, 0), Quaternion.identity);
            EG eg = upRoom.GetComponent<EG>();
            eg.width = width;
            eg.height = height;
            eg.doorWidth = doorWidth;
            eg.bridgeWidth = bridgeWidth;
            eg.moreRoomsChance = moreRoomsChance;
            eg.further = further + 1;
            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 100) < moreRoomsChance)
                {
                    eg.spawnDoor[i] = true;
                }
                else
                {
                    eg.spawnDoor[i] = false;
                }
            }
            eg.spawnDoor[3] = true;
        }
        else
            upRoom = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) + new Vector2(0, height + bridgeWidth), 1).gameObject;
        //end
    }
    public void SpawnDoor3()
    {
        blocksToDelete = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - (doorWidth / 2.2f), transform.position.y - height / 2 - heightFixer), new Vector2(transform.position.x + (doorWidth / 2.2f), transform.position.y - height / 2 + heightFixer));
        for (int i = 0; i < blocksToDelete.Length; i++)
        {
            downDoor[i] = Instantiate(doorBlock, blocksToDelete[i].transform.position, Quaternion.identity);
            downDoor[i].transform.SetParent(gameObject.transform);
            Destroy(blocksToDelete[i].gameObject);
        }
        //down room generation
        if (!Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) - new Vector2(0, height + bridgeWidth), 1))
        {
            downRoom = Instantiate(prefabSelf, transform.position - new Vector3(0, height + bridgeWidth, 0), Quaternion.identity);
            EG eg = downRoom.GetComponent<EG>();
            eg.width = width;
            eg.height = height;
            eg.doorWidth = doorWidth;
            eg.bridgeWidth = bridgeWidth;
            eg.moreRoomsChance = moreRoomsChance;
            eg.further = further + 1;
            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 100) < moreRoomsChance)
                {
                    eg.spawnDoor[i] = true;
                }
                else
                {
                    eg.spawnDoor[i] = false;
                }
            }
            eg.spawnDoor[2] = true;
        }
        else
            downRoom = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y) - new Vector2(0, height + bridgeWidth), 1).gameObject;
        //end
    }
    public void GenerateWalls()
    {

        //upWallGenerator
        for (int i = 0; i < width; i++)
        {
            currentBlock = Instantiate(wallBlock, new Vector2(transform.position.x - width / 2 + i + widthFixer, transform.position.y + height / 2 - heightFixer), Quaternion.identity);
            currentBlock.transform.SetParent(gameObject.transform);
        }
        //downWallGenerator
        for (int i = 0; i < width; i++)
        {
            currentBlock = Instantiate(wallBlock, new Vector2(transform.position.x - width / 2 + i + widthFixer, transform.position.y - height / 2 + heightFixer), Quaternion.identity);
            currentBlock.transform.SetParent(gameObject.transform);
        }
        //leftWallGenerator
        for (int i = 0; i < height - 2; i++)
        {
            currentBlock = Instantiate(wallBlock, new Vector2(transform.position.x - width / 2 + widthFixer, transform.position.y - height / 2 + heightFixer + i + 1), Quaternion.identity);
            currentBlock.transform.SetParent(gameObject.transform);
        }
        //rightWallGenerator
        for (int i = 0; i < height - 2; i++)
        {
            currentBlock = Instantiate(wallBlock, new Vector2(transform.position.x + width / 2 - widthFixer, transform.position.y - height / 2 + heightFixer + i + 1), Quaternion.identity);
            currentBlock.transform.SetParent(gameObject.transform);
        }
    }
    public void DoorFixer()
    {
        if (leftRoom && !leftRoom.GetComponent<EG>().rightDoor[0])
        {
            leftRoom.GetComponent<EG>().SpawnDoor1();
        }
        if (rightRoom && !rightRoom.GetComponent<EG>().leftDoor[0])
        {
            rightRoom.GetComponent<EG>().SpawnDoor0();
        }
        if (upRoom && !upRoom.GetComponent<EG>().downDoor[0])
        {
            upRoom.GetComponent<EG>().SpawnDoor3();
        }
        if (downRoom && downRoom.GetComponent<EG>() && !downRoom.GetComponent<EG>().upDoor[0])
        {
            downRoom.GetComponent<EG>().SpawnDoor2();
        }
    }
    public void EnemiesNCollider()
    {
        //spawnersCreator
        if (!(name == "0"))
            for (int i = 0; i < spawnersAmount; i++)
            {
                allEnemySpawners[i] = Instantiate(enemySpawner, transform.position + new Vector3(Random.Range(-width / 2.4f, width / 2.4f), Random.Range(-height / 2.4f, height / 2.4f), 1), Quaternion.identity);
                allEnemySpawners[i].transform.SetParent(transform);
            }
    }
}

