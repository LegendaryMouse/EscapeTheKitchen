using UnityEngine;
using UnityEngine.UIElements;

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
    private Tomato tomato;
    private Carrot carrot;
    private Potato potato;
    private Radish radish;
    private Cabbage cabbage;
    private Cucumber cucumber;

    public float hp;
    public float speed;
    public float damage;

    public float reloadTime;
    public float startReloadTime;

    public Animator animation1;
    public GameObject damageSound;
    public GameObject dieSound;
    public GameObject particles;

    public EnemyType EnemyType;

    Player player;
    Score score;

    public bool isDying = false;

    private void Awake()
    {
        if(EnemyType == EnemyType.Tomato)
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
        animation1.Play("Walk");
        player = FindObjectOfType<Player>();
        score = FindObjectOfType<Score>();
    }
    private void Update()
    {
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
