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
        // Get reference to the Enemy script and the Animator component
        enemy = GetComponent<Enemy>();
        animation1 = enemy.GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the Carrot is currently rushing
        if (isRushing)
        {
            // Reduce the rush time
            rushTime -= Time.deltaTime;

            // Move towards the target position at rush speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, rushSpeed * Time.deltaTime);

            // Play the "Drive" animation if the enemy is not dying
            if (!enemy.isDying)
                enemy.animation1.Play("Drive");

            // Flip the Carrot based on the target position
            if (transform.position.x - targetPosition.x > 0)
            {
                Flip(90);
            }
            else if (transform.position.x - targetPosition.x < 0)
            {
                Flip(-90);
            }
        }

        // Check if the rush time has elapsed
        if (rushTime <= 0)
        {
            isRushing = false;
            // Play the "Walk" animation if the enemy is not dying
            if (!enemy.isDying)
                enemy.animation1.Play("Walk");

            // Reset the rotation
            Flip(0);
        }

        // Check for targets within the rush range if not currently rushing
        if (!isRushing)
            targets = Physics2D.OverlapCircleAll(transform.position, rushRange);

        // Loop through the targets and initiate rush if a Player is detected
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i])
                if (targets[i].gameObject.CompareTag("Player"))
                {
                    if (!isRushing)
                    {
                        // Set the target position and start rushing towards it
                        targetPosition = targets[i].gameObject.transform.position;
                        transform.position = Vector2.MoveTowards(transform.position, targetPosition, -transform.localScale.y);
                        isRushing = true;
                        rushTime = startRushTime;
                    }
                }
        }
    }

    // Function to flip the Carrot's rotation
    private void Flip(float z)
    {
        transform.rotation = Quaternion.Euler(0, 0, z);
        //Debug.Log(z + " flipped");
    }
}
