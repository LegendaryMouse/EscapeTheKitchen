using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float rushTime;
    public float startRushTime;
    public float rushSpeed;
    public float rushRange;
    public bool isRushing = false;
    private Collider2D[] targets;
    private Enemy enemy;
    private Animator animation1;
    private Vector3 targetPosition;
    //private bool facingRight = true;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        animation1 = enemy.GetComponent<Animator>();
    }
    private void Update()
    {
        if (isRushing)
        {
            rushTime -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, rushSpeed * Time.deltaTime);
            if (!enemy.isDying)
                enemy.animation1.Play("Drive");

            if (transform.position.x - targetPosition.x > 0)
            {
                Flip(90);
            }
            else if (transform.position.x - targetPosition.x < 0)
            {
                Flip(-90);
            }
        }

        if (rushTime <= 0)
        {
            isRushing = false;
            if (!enemy.isDying)
                enemy.animation1.Play("Walk");
            Flip(0);
        }
        if (!isRushing)
            targets = Physics2D.OverlapCircleAll(transform.position, rushRange);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i])
                if (targets[i].gameObject.CompareTag("Player"))
                {
                    if (!isRushing)
                    {
                        targetPosition = targets[i].gameObject.transform.position;
                        transform.position = Vector2.MoveTowards(transform.position, targetPosition, -transform.localScale.y);
                        isRushing = true;
                        rushTime = startRushTime;
                    }
                }
        }
    }

    private void Flip(float z)
    {
        transform.rotation = Quaternion.Euler(0, 0, z);
        //Debug.Log(z + " flipped");
    }
}
