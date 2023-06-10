using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
    public bool isColliding;

    [Header("Health")]
    public float hp;
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
    private Score score;
    public GameObject[] dieDrop;
    public float dieDropChance;
    public float enemyScore;

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
        score = FindObjectOfType<Score>();

        rb = GetComponent<Rigidbody2D>();

        animation1.Play("MoveDown");
    }

    private void Update()
    {
        //if (!isColliding)
        if (!isDying)
            try
            {
                if (EnemyType == EnemyType.Carrot && !carrot.isRushing)
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

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
            animation1.Play("Die");
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
        text.transform.localScale = new Vector3(0.5f + (damage / 10), 0.5f + (damage / 10), 1);
        text.transform.GetComponentInChildren<TextMeshPro>().color = new Color(damage / 10, 1 - (damage / 10), 0);
        text.transform.GetComponentInChildren<TextMeshPro>().text = "-" + damage;

        DamageSlowing(damage, damage / 5);
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
        score.score += enemyScore;
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
        if (speed > slowing)
        {
            speed -= slowing;
            StartCoroutine(SpeedNormalizer(slowing, slowingTime));
        }
        else
        {
            StartCoroutine(SpeedNormalizer(speed, slowingTime));
            speed = -0.1f;
        }
    }

    private IEnumerator SpeedNormalizer(float speedUp, float time)
    {
        yield return new WaitForSeconds(time);
        speed += speedUp;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isColliding = true;
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    public void DetachParents()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i <= transform.childCount + 1; i++)
            {
                try
                {
                    Debug.Log("detaching");
                    transform.GetChild(0).SetParent(null);
                }
                catch
                {
                    
                }
            }
        }
    }

}

