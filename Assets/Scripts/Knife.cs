using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float offsetDegrees;
    public float reloadTime;
    public float startReloadTime;
    public Transform shotPoint;
    public GameObject bullet;
    public GameObject shootSound;
    public GameObject pickupPrefab;
    Player player;

    public bool haveKnife = true;

    bool facingRight = true;
    private Vector2 moveInput;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + offsetDegrees);

        if (reloadTime <= 0)
        {
            if (Input.GetMouseButtonDown(0) && haveKnife)
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                Instantiate(shootSound, transform.position, Quaternion.identity);
                reloadTime = startReloadTime;
                haveKnife = false;
                player.DropWeapon();
                player.SwitchWeapon(-1);
                
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