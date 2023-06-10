using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMover : MonoBehaviour
{
    GameObject cam;
    GameObject player;
    public Vector2 pos;
    //public bool isTranslating;
    //public Vector2 translated;
    public float time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            player = GameObject.FindGameObjectWithTag("Player");
            //isTranslating = true;
            //cam.transform.Translate(pos);
            player.transform.Translate(pos / 4);
            //Debug.Log("moved");
        }
    }
    public void Update()
    {
        //if (isTranslating & translated.x <= pos.x & translated.y <= pos.y)
        {
            //cam.transform.Translate(pos * Time.deltaTime * time);
            //translated += pos * Time.deltaTime * time;
        }
    }
}
