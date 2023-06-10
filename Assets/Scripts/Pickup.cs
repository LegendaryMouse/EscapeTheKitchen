using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject prefabSelf;
    public GameObject pickupPrefabSelf;
    Player p;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
             p = collision.GetComponent<Player>();

            for (int i=0; i < p.weapons.Length; i++)
            {
                if (!p.weapons[p.currentNumber])
                {
                    PickupF();
                    break;
                }
                else
                {
                    p.SwitchWeapon(-1); 
                }
            }
        }
    }
    public void PickupF()
    {

        p.weapons[p.currentNumber] = prefabSelf;

        GameObject icon = Instantiate(pickupPrefabSelf, p.slot[p.currentNumber].transform.position, Quaternion.identity);
        icon.GetComponent<Transform>().localScale /= 2;
        icon.GetComponent<Collider2D>().enabled = false;
        icon.GetComponent<Transform>().SetParent(p.slot[p.currentNumber].transform);

        p.SwitchWeapon(p.currentNumber);

        //Debug.Log("added");
        Destroy(gameObject);
    }
}
