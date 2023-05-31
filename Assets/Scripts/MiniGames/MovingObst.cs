using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObst : MonoBehaviour
{
    Transform tr;
    public Vector2 direction;
    public Vector2 translated;
    public Vector2 startingPosition;
    public bool isMoving = true;
    public bool moveForward = true;
    public float speed;
    public GameObject target;


    private void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        if (target)
        {
            transform.Translate((target.transform.position - transform.position) / Vector2.Distance(target.transform.position, transform.position) * Time.deltaTime / 20 * speed);
        }
        else
        {
            if (moveForward & Vector2.Distance(transform.position, startingPosition + direction) > speed / 1000)
            {
                transform.Translate(direction / 100 * Time.deltaTime * speed);
            }
            else
            {
                moveForward = false;
            }
            if (!moveForward & Vector2.Distance(transform.position, startingPosition) > speed / 1000)
            {
                transform.Translate(-direction / 100 * Time.deltaTime * speed);
            }
            else
            {
                moveForward = true;
            }
        }

    }
}

