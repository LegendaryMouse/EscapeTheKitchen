using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // Variables
    public int bulletsPerShot; // Number of bullets fired per shot
    public float offsetDegrees; // Offset for the rotation of the gun
    public float reloadTime; // Time it takes to reload
    public float startReloadTime; // Initial reload time
    public float razbros; // Spread of bullets
    public Transform shotPoint; // Point where bullets are spawned
    public GameObject bullet; // Bullet prefab
    public GameObject shootSound; // Sound played when shooting
    public GameObject emptySound; // Sound played when out of ammo
    public GameObject pickupPrefab; // Prefab for the pickup item
    public GameObject po; // Prefab for the pickup item
    private bool facingRight = true; // Flag for player facing direction

    private void Start()
    {
        po = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(po.transform);

        if (reloadTime <= 0)
        {
            // Check if the fire button is pressed and player has enough ammo

            // Spawn bullets with randomized rotation within the spread range
            for (int i = 0; i < bulletsPerShot; i++)
            {
                GameObject b = Instantiate(bullet, shotPoint.position, transform.rotation);
                b.transform.Rotate(new Vector3(0, 0, Random.Range(-razbros, razbros)));
            }

            // Play shoot sound
            Instantiate(shootSound, transform.position, Quaternion.identity);

            // Reset reload time and decrease player ammo
            reloadTime = startReloadTime;
        }
        else
        {
            // Decrease reload time
            reloadTime -= Time.deltaTime;
        }

    }
}
