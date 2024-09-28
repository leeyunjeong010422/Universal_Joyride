using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeStartSecene(string sceneName)
    {
        SceneManager.LoadScene("Start");
    }
    public void ChangeGameSecene(string sceneName)
    {
        SceneManager.LoadScene("Game");
    }
}
