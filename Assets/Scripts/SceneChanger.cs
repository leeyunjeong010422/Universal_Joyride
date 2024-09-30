using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void ChangeStartSecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SceneManager.LoadScene("Start");
    }

    public void ChangeGameSecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SceneManager.LoadScene("Game");
    }
}
