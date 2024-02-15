using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Animations")]
    public GameObject damageSound; // Sound effect for damage
    public GameObject dieSound; // Sound effect for death
    public GameObject particles; // Particle effect
    private Animator animation1; // Player Animation

    [Header("Health")]
    public GameObject sheild; // Player sheild
    private float maxHp; // Player Animation
    public GameObject[] allHearts;
    public List<GameObject> allHalfsHearts;
    public float heartHp;
    public GameObject[] allSheilds;
    public List<GameObject> allHalfsSheilds;
    public int sheildStrength;
    public float potionStrength;
    public float hp;

    [Header("Moving")]
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;
    public Animator anime;

    [Header("Items")]
    public TextMeshProUGUI keyUI;
    public int keysAmount;
    public TextMeshProUGUI ammoUI;
    public int ammoAmount;

    [Header("Inventory")]
    public Slot[] slot;
    public GameObject[] weapons;
    public GameObject currentItem;
    public int currentNumber = 0;
    public GameObject hands;

    [Header("Cheats")]
    public GameObject cheatGun;

    private void Start()
    {
        maxHp = hp;
        heartHp = maxHp / allHearts.Length;

        for (int i = allHearts.Length - 1; i >= 0; i--)
        {
            allHalfsHearts.Add(allHearts[i].transform.GetChild(1).gameObject);
            allHalfsHearts.Add(allHearts[i].transform.GetChild(0).gameObject);
        }
        for (int i = allSheilds.Length - 1; i >= 0; i--)
        {
            allHalfsSheilds.Add(allSheilds[i].transform.GetChild(1).gameObject);
            allHalfsSheilds.Add(allSheilds[i].transform.GetChild(0).gameObject);
        }

        rb = GetComponent<Rigidbody2D>();

        currentItem = FindChildWithTag("Weapon");
    }

    private void Update()
    {
        for (int i = 0; i < allHearts.Length * 2; i++)
        {
            if (hp < maxHp - (heartHp / 2 * i))
            {
                allHalfsHearts[i].SetActive(false);
            }
            else
            {
                allHalfsHearts[i].SetActive(true);
            }
        }

        if (sheildStrength <= 0)
            sheild.SetActive(false);

        ammoUI.text = "" + ammoAmount;

        if (currentItem.GetComponent<Gun>())
            currentItem.GetComponent<Gun>().player = gameObject.GetComponent<Player>();

        for (int i = 0; i < allSheilds.Length * 2; i++)
        {
            if (sheildStrength <= i)
            {
                allHalfsSheilds[i].SetActive(false);
            }
            else
            {
                allHalfsSheilds[i].SetActive(true);
            }
        }

        keyUI.text = ("" + keysAmount);

        moveInput = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;


        if (facingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput.x > 0)
        {
            Flip();
        }


        if (hp <= 0)
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropWeapon(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(4);
        }
        if(Input.GetKey(KeyCode.Z) & Input.GetKey(KeyCode.X) & Input.GetKeyDown(KeyCode.C))
        {
            CheatSpawn();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if (moveVelocity == new Vector2(0, 0))
        {
            anime.SetBool("isIdle", true);
        }
        else
        {
            anime.SetBool("isIdle", false);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hp < maxHp)
            if (collision.CompareTag("HPPotion"))
            {
                {
                    hp += potionStrength;
                    Destroy(collision.gameObject);
                    Instantiate(particles, transform.position, Quaternion.identity);
                }
            }
        if (sheildStrength < 4)
            if (collision.CompareTag("Sheild"))
            {
                sheild.SetActive(true);
                sheildStrength += 2;
                Destroy(collision.gameObject);
                Instantiate(particles, transform.position, Quaternion.identity);
            }
        if (collision.CompareTag("Key"))
        {
            keysAmount++;
            Destroy(collision.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void TakeDamage(float damage)
    {
        if (!sheild.activeInHierarchy)
        {
            hp -= damage;
            Instantiate(particles, transform.position, Quaternion.identity);
            Instantiate(damageSound, transform.position, Quaternion.identity);
        }
        else
        {
            sheildStrength--;
        }
    }

    public void SwitchWeapon(int currentNumberF)
    {
        if (currentNumberF == -1)
        {
            if (currentNumber < weapons.Length - 1)
            {
                slot[currentNumber].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                currentNumber++;
                slot[currentNumber].GetComponent<Image>().color = new Color(0, 1, 0, 0.2f);
            }
            else
            {
                slot[currentNumber].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                currentNumber = 0;
                slot[currentNumber].GetComponent<Image>().color = new Color(0, 1, 0, 0.2f);
            }
        }
        else
        {
            slot[currentNumber].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            currentNumber = currentNumberF;
            slot[currentNumber].GetComponent<Image>().color = new Color(0, 1, 0, 0.2f);
        }


        if (AllChildWithTag("Weapon").Count > 0)
        {
            for (int i = 0; i < AllChildWithTag("Weapon").Count; i++)
            {
                Destroy(AllChildWithTag("Weapon")[i]);
            }
        }

        if (facingRight)
        {
            if ((weapons[currentNumber]))
                currentItem = Instantiate(weapons[currentNumber], transform.position, Quaternion.identity);
            else
                currentItem = Instantiate(hands, transform.position, Quaternion.identity);
        }
        else
        {
            Flip();

            if ((weapons[currentNumber]))
                currentItem = Instantiate(weapons[currentNumber], transform.position, Quaternion.identity);
            else
                currentItem = Instantiate(hands, transform.position, Quaternion.identity);
        }

        currentItem.transform.SetParent(gameObject.transform);
    }

    public void DropWeapon(bool saveWeapon)
    {
        if (saveWeapon)
        {
            if (currentItem.GetComponent<Gun>())
            {
                weapons[currentNumber] = null;
                Destroy(slot[currentNumber].transform.GetChild(0).gameObject);
                GameObject weapon = FindChildWithTag("Weapon");
                Instantiate(weapon.GetComponent<Gun>().pickupPrefab, transform.position - new Vector3(0, 2, 0), Quaternion.identity);
                Destroy(weapon);
                SwitchWeapon(currentNumber);
            }
            if (currentItem.GetComponent<Knife>())
            {
                weapons[currentNumber] = null;
                Destroy(slot[currentNumber].transform.GetChild(0).gameObject);
                GameObject weapon = FindChildWithTag("Weapon");
                Instantiate(weapon.GetComponent<Knife>().pickupPrefab, transform.position - new Vector3(0, 2, 0), Quaternion.identity);
                Destroy(weapon);
                SwitchWeapon(currentNumber);
            }
        }
        else
        {
            weapons[currentNumber] = null;
            Destroy(slot[currentNumber].transform.GetChild(0).gameObject);
            GameObject weapon = FindChildWithTag("Weapon");
            Destroy(weapon);
            SwitchWeapon(currentNumber);
        }
    }

    public void CheatSpawn()
    {
        Instantiate(cheatGun, transform.position, Quaternion.identity);
        Debug.Log("cheater");
    }

    public GameObject FindChildWithTag(string tag)
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform transform in allChildren)
        {
            if (transform.CompareTag(tag))
            {
                return transform.gameObject;
            }
        }
        return null;
    }

    public List<GameObject> AllChildWithTag(string tag)
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        List<GameObject> all = new List<GameObject>();
        foreach (Transform transform in allChildren)
        {
            if (transform.CompareTag(tag))
            {
                all.Add(transform.gameObject);
            }
        }
        return all;
    }
}
