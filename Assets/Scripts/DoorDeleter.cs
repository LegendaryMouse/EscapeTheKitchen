using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeleter : MonoBehaviour
{
    public GameObject block;
    public bool UpDown;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")) 
        {
            if (UpDown)
            {
                Instantiate(block, transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                Instantiate(block, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(block, transform.position - new Vector3(0.5f, 0, 0), Quaternion.identity);
                Instantiate(block, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            }
            Debug.Log(transform.position + " " + collision.name);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(transform.GetChild(1).gameObject);
        }
    }
}

