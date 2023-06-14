using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int scene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangeScene(scene);
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        //Debug.Log(sceneNumber);
    }
}
