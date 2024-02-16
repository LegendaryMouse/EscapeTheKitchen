using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;
using static UnityEngine.ParticleSystem;

public enum EnemyType
{
    Tomato,
    Carrot,
    Potato,
    Radish,
    Cabbage,
    Cucumber
}

public class Enemy : MonoBehaviour
{
    [Header("Classes")]
    public EnemyType EnemyType;
    private Tomato tomato;
    private Carrot carrot;
    private Potato potato;
    private Radish radish;
    private Cabbage cabbage;
    private Cucumber cucumber;

    [Header("Moving")]
    public float speed;
    private Rigidbody2D rb;

    [Header("Health")]
    public float hp;
    private float maxHp;
    public bool isDying = false;
    public GameObject damageSound;
    public GameObject dieSound;
    public GameObject particles;
    public GameObject damageText;

    [Header("Attack")]
    public float reloadTime;
    public float startReloadTime;
    public float damage;

    [Header("Special")]
    public Animator animation1;
    private Player player;
    public GameObject[] dieDrop;
    public float dieDropChance;
    public int enemyScore;

    private void Awake()
    {
        if (EnemyType == EnemyType.Tomato)
            tomato = GetComponent<Tomato>();
        if (EnemyType == EnemyType.Carrot)
            carrot = GetComponent<Carrot>();
        if (EnemyType == EnemyType.Potato)
            potato = GetComponent<Potato>();
        if (EnemyType == EnemyType.Radish)
            radish = GetComponent<Radish>();
        if (EnemyType == EnemyType.Cabbage)
            cabbage = GetComponent<Cabbage>();
        if (EnemyType == EnemyType.Cucumber)
            cucumber = GetComponent<Cucumber>();
    }

    private void Start()
    {
        animation1 = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        rb = GetComponent<Rigidbody2D>();

        animation1.Play("MoveDown");

        maxHp = hp;

        speed = speed * Random.Range(0.6f, 1.4f);
    }

    private void Update()
    {
        //if (!isColliding)
        if (!isDying)
            try
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 20)
                    if (EnemyType == EnemyType.Carrot && !carrot.isRushing)
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                        rb.velocity = (-transform.position + player.transform.position) * speed / Vector3.Distance(transform.position, player.transform.position);
                    }
                    else
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                        rb.velocity = (-transform.position + player.transform.position) * speed / Vector3.Distance(transform.position, player.transform.position);

                        {
                            if ((player.transform.position - transform.position).x > 0)
                            {
                                animation1.Play("MoveRight");
                            }
                            else
                            {
                                animation1.Play("MoveLeft");
                            }
                        }
                    }
            }
            catch when (!FindObjectOfType<Player>())
            {
                Debug.Log("Game Over!");
            }

        if (hp <= 0 && !isDying)
        {
            isDying = true;
            //Debug.Log("Dying");
            if (animation1)
                animation1.Play("Die");
            if (GetComponent<Tomato>())
            {
                GameObject zona = Instantiate(GetComponent<Tomato>().zone, transform.position, Quaternion.identity);
                Vector3 scale = new Vector3(GetComponent<Tomato>().explosionRadius * 0.8f, GetComponent<Tomato>().explosionRadius * 0.8f, 1);
                zona.transform.localScale = scale;
            }
            Invoke(nameof(Die), 1f);
            Instantiate(dieSound, transform.position, Quaternion.identity);
            Destroy(GetComponent<Collider2D>());
        }



        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        ParticleSystem.MainModule ps = particles.GetComponent<ParticleSystem>().main;

        if (tomato)
            ps.startColor = new Color(1, 0, 0);
        if (carrot)
            ps.startColor = new Color(0.8f, 0.4f, 0);

        Instantiate(particles, transform.position, Quaternion.identity);
        Instantiate(damageSound, transform.position, Quaternion.identity);


        GameObject text = Instantiate(damageText, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
        text.transform.GetComponentInChildren<TextMeshPro>().color = new Color(1, 1, 0, 1);
        text.transform.GetComponentInChildren<TextMeshPro>().text = "-" + damage;

        DamageSlowing(damage, damage / 5);

        if (tomato && tomato.boss)
        {
            if (hp > maxHp / 5)
            {
                if (Random.Range(0, 100) > 90)
                {
                    Instantiate(tomato.megaMinion, transform.position + new Vector3(Random.Range(-5,1), Random.Range(5, 5), 0), Quaternion.identity);
                }
                else if (Random.Range(0, 100) > 75)
                {
                    Instantiate(tomato.minion, transform.position + new Vector3(Random.Range(-5, -1), Random.Range(5, -5), 0), Quaternion.identity);
                }
            }
            else
            {
                if (Random.Range(0, 100) > 50)
                {
                    Instantiate(tomato.megaMinion, transform.position, Quaternion.identity);
                }
                if (Random.Range(0, 100) > 0)
                {
                    Instantiate(tomato.minion, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void Die()
    {
        DetachParents();
        DieDrop();
        AddScore();
        Destroy(gameObject);

        if (tomato)
        {
            tomato.Explosion();
        }
    }

    private void AddScore()
    {
        player.ammoAmount += enemyScore;
    }

    private void DieDrop()
    {
        float rand = Random.Range(0, 100);
        if (dieDropChance > rand)
        {
            Instantiate(dieDrop[Random.Range(0, dieDrop.Length)], transform.position, Quaternion.identity);
            if (dieDropChance - rand > 100)
                Instantiate(dieDrop[Random.Range(0, dieDrop.Length)], transform.position, Quaternion.identity);
            if (dieDropChance - rand > 200)
                Instantiate(dieDrop[Random.Range(0, dieDrop.Length)], transform.position, Quaternion.identity);
            if (dieDropChance - rand > 300)
                Instantiate(dieDrop[Random.Range(0, dieDrop.Length)], transform.position, Quaternion.identity);
        }
    }

    private void DamageSlowing(float slowing, float slowingTime)
    {
        if (speed >= slowing)
        {
            speed -= slowing;
            StartCoroutine(SpeedNormalizer(slowing, slowingTime));
        }
        else
        {
            StartCoroutine(SpeedNormalizer(speed, slowingTime));
            speed = 0f;
        }
    }

    private IEnumerator SpeedNormalizer(float speedUp, float time)
    {
        yield return new WaitForSeconds(time);
        speed += speedUp;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (reloadTime <= 0)
            {
                collision.collider.GetComponent<Player>().TakeDamage(damage);
                reloadTime = startReloadTime;
            }
        }
        if (collision.collider.CompareTag("Obst"))
        {
            if (reloadTime <= 0)
            {
                collision.collider.GetComponent<Obst>().TakeDamage(damage);
                reloadTime = startReloadTime;
            }
        }

    }

    public void DetachParents()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i <= transform.childCount + 1; i++)
            {
                try
                {
                    //Debug.Log("detaching");
                    transform.GetChild(0).SetParent(null);
                }
                catch
                {

                }
            }
        }
    }
}

