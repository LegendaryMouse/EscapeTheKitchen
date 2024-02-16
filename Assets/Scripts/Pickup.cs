using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject prefabSelf; // The prefab to be picked up
    public GameObject pickupPrefabSelf; // The prefab for the pickup icon
    Player p; // Reference to the Player script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            p = collision.GetComponent<Player>(); // Get the Player component from the collision object

            for (int i = 0; i < p.weapons.Length; i++)
            {
                if (!p.weapons[p.currentNumber])
                {
                    PickupF(); // Call the PickupF() function if the current weapon slot is empty
                    break;
                }
                else
                {
                    p.SwitchWeapon(-1); // Switch to the next weapon slot if the current slot is occupied
                }
            }
        }
    }

    public void PickupF()
    {
        p.weapons[p.currentNumber] = prefabSelf; // Assign the picked up prefab to the current weapon slot

        // Instantiate the pickup icon prefab and display it in the corresponding weapon slot
        GameObject icon = Instantiate(pickupPrefabSelf, p.slot[p.currentNumber].transform.position, Quaternion.identity);
        icon.GetComponent<Transform>().localScale /= 2;
        icon.GetComponent<Collider2D>().enabled = false;
        icon.GetComponent<Transform>().SetParent(p.slot[p.currentNumber].transform);

        p.SwitchWeapon(p.currentNumber); // Switch to the current weapon slot

        Destroy(gameObject); // Destroy the pickup object
    }
}
