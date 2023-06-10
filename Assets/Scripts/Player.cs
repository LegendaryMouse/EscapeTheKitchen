using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Animations")]
    public GameObject damageSound;
    public GameObject dieSound;
    public GameObject particles;
    private Animator animation1;

    [Header("Health")]
    public GameObject sheild;
    public GameObject sheildUI;
    public float potionStrength;
    public float hp;
    public TextMeshProUGUI hpUI;

    [Header("Moving")]
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private bool facingRight = true;

    [Header("Items")]
    public TextMeshProUGUI keyUI;
    public int keysAmount;

    [Header("Inventory")]
    public Slot[] slot;
    public GameObject[] weapons;
    public GameObject currentItem;
    public int currentNumber = 0;
    public GameObject hands;

    private void Start()
    {
        /*for (int i = 0; i < inventoryCapaticy; i++)
        {
            Instantiate(slot)
        }*/

        rb = GetComponent<Rigidbody2D>();

        currentItem = FindChildWithTag("Weapon");
    }

    private void Update()
    {
        hpUI.text = ("HP " + hp);
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
            DropWeapon();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SwitchWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SwitchWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SwitchWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SwitchWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            SwitchWeapon(4);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HPPotion"))
        {
            hp += potionStrength;
            Destroy(collision.gameObject);
            Instantiate(particles, transform.position, Quaternion.identity);
        }
        if (collision.CompareTag("Sheild"))
        {
            sheild.SetActive(true);
            sheildUI.SetActive(true);
            sheildUI.GetComponent<Sheild>().Reset();
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
            sheildUI.GetComponent<Sheild>().ReduceTime(damage);
        }
    }

    public void SwitchWeapon(int currentNumberF)
    {
        if (currentNumberF == -1)
        {
            if (currentNumber < weapons.Length - 1)
            {
                slot[currentNumber].GetComponent<Image>().color = new Color(1,1,1,0.2f);
                currentNumber++;
                slot[currentNumber].GetComponent<Image>().color = new Color(0,1,0,0.2f);
            }
            else
            {
                slot[currentNumber].GetComponent<Image>().color = new Color(1,1,1,0.2f);
                currentNumber = 0;
                slot[currentNumber].GetComponent<Image>().color = new Color(0,1,0,0.2f);
            }
        }
        else
        {
            slot[currentNumber].GetComponent<Image>().color = new Color(1,1,1,0.2f);
            currentNumber = currentNumberF;
            slot[currentNumber].GetComponent<Image>().color = new Color(0,1,0,0.2f);
        }


        if(AllChildWithTag("Weapon").Count > 0)
        {
            for(int i = 0; i < AllChildWithTag("Weapon").Count; i++)
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

    public void DropWeapon()
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
            Destroy(weapon);
            SwitchWeapon(currentNumber);
        }
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
