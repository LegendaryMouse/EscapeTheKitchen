using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int ammoCost;
    public int bulletsPerShot;
    public float offsetDegrees;
    public float reloadTime;
    public float startReloadTime;
    public float razbros;
    public Transform shotPoint;
    public GameObject bullet;
    public GameObject shootSound;
    public GameObject pickupPrefab;
    [System.NonSerialized] public Player player;

    bool facingRight = true;
    private Vector2 moveInput;

    private void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + offsetDegrees);

        if (reloadTime <= 0 && player.ammoAmount > ammoCost)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    GameObject b = Instantiate(bullet, shotPoint.position, transform.rotation);
                    b.transform.Rotate(new Vector3(0, 0, Random.Range(-razbros, razbros)));
                }
                    Instantiate(shootSound, transform.position, Quaternion.identity);
                reloadTime = startReloadTime;
                player.ammoAmount -= ammoCost;
            }
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }


        moveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
