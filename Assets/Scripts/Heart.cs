using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public GameObject player;
    Player ps;
    public GameObject[] allHearts;
    public List<GameObject> allHalfsHearts;
    public float maxHp;
    public float hp;
    public float heartHp;

    void Start()
    {
        ps = player.GetComponent<Player>();
        maxHp = ps.hp;
        heartHp = maxHp / allHearts.Length;

        for (int i = allHearts.Length - 1; i >= 0; i--)
        {
            allHalfsHearts.Add(allHearts[i].transform.GetChild(1).gameObject);
            allHalfsHearts.Add(allHearts[i].transform.GetChild(0).gameObject);
        }
    }
    void Update()
    {
        hp = ps.hp;

        for(int i = 0; i < allHearts.Length * 2; i++)
        {
            if(ps.hp < maxHp - (heartHp / 2 * i))
            {
                allHalfsHearts[i].SetActive(false);
            }
        }
    }
}
