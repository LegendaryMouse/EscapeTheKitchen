using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{


    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
        Debug.Log(sceneNumber);
    }
}
