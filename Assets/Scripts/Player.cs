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

    [Header("Admin")]
    public GameObject[] objects;
    public GameObject[] weapons;
    public GameObject currentWeapon;
    public int weaponNumber = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentWeapon = FindChildWithTag("Weapon");
    }

    private void Update()
    {
        hpUI.text = ("HP " + hp);
        keyUI.text = (""+keysAmount);

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            int i = Random.Range(0, objects.Length);
            Instantiate(objects[i], transform.position + new Vector3(4, 0, 0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon(-1);
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

    public void SwitchWeapon(int weaponNumberF)
    {
        if (weaponNumberF == -1)
        {
            if (weapons[weaponNumber + 1])
                weaponNumber++;
            else
                weaponNumber = 0;
        }
        else
            weaponNumber = weaponNumberF;

        Destroy(FindChildWithTag("Weapon"));

        if (facingRight)
        {
            currentWeapon = Instantiate(weapons[weaponNumber], transform.position, Quaternion.identity);
        }
        else
        {
            Flip();
            currentWeapon = Instantiate(weapons[weaponNumber], transform.position, Quaternion.identity);
        }
        currentWeapon.transform.SetParent(gameObject.transform);
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
}
