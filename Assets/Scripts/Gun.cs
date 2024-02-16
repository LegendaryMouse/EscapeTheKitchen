using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Variables
    public int ammoCost; // Cost of ammo per shot
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
    public Player player; // Reference to the player script
    private bool facingRight = true; // Flag for player facing direction
    private Vector2 moveInput; // Player movement input

    private void Update()
    {
        // Rotate gun towards the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + offsetDegrees);

        // Check if it's time to reload
        if (reloadTime <= 0)
        {
            // Check if the fire button is pressed and player has enough ammo
            if (Input.GetMouseButton(0))
            {
                if (player.ammoAmount > ammoCost)
                {
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
                    player.ammoAmount -= ammoCost;
                }
                else
                {
                    // Play empty sound if player is out of ammo
                    Instantiate(emptySound, transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            // Decrease reload time
            reloadTime -= Time.deltaTime;
        }

        // Get player movement input
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Flip the gun if player changes direction
        if (facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
    }

    // Flip the gun horizontally
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
