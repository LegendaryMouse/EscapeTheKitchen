using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera cam;
    public Vector2 pos;
    public bool isTranslating;
    public Vector2 translated;

    private void OnMouseEnter()
    {
        isTranslating = true;
        Debug.Log("moved");
    }
    public void Update()
    {
        if(isTranslating & translated.x <= pos.x & translated.y <= pos.y)
        {
            cam.transform.Translate(pos * Time.deltaTime / 10);
            translated += pos * Time.deltaTime / 10;
        }
    }
}
