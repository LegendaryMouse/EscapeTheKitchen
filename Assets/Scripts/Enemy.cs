using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

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

    public Animator animation1;
    private Player player;
    private Score score;

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

        animation1.Play("Walk");
    }
    private void Update()
    {
        if (!isDying)
            try
            {
                if (EnemyType == EnemyType.Carrot && !carrot.isRushing)
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
        Instantiate(particles, transform.position, Quaternion.identity);
        Instantiate(damageSound, transform.position, Quaternion.identity);

        GameObject text = Instantiate(damageText, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
        text.transform.GetComponentInChildren<TextMeshPro>().text = "-"+damage;
        //text.GetComponent<Animation>().Play();
    }
    private void Die()
    {
        if (EnemyType == EnemyType.Tomato)
        {
            tomato.Explosion();
            score.score += 2;
        }
        if (EnemyType == EnemyType.Carrot)
        {
            score.score += 5;
        }
        Destroy(gameObject);
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
    }
}
