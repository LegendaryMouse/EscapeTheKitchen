using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayer : MonoBehaviour
{

    void Update()
    {
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = new(pos3.x, pos3.y);
        transform.position = pos;
    }
}
