using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public string sceneToLoadOnAnyKey;
    private void Update()
    {
        //현재 씬이 GameWay일 때 아무 키나 눌렀을 때 씬 전환
        if (SceneManager.GetActiveScene().name == "GameWay" && Input.anyKeyDown && !string.IsNullOrEmpty(sceneToLoadOnAnyKey))
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void ChangeGameWaySecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SceneManager.LoadScene("GameWay");
    }

    public void ChangeStartSecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SoundManager.Instance.SetStartBGM();
        SceneManager.LoadScene("Start");
    }

    public void ChangeStartGameSecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SceneManager.LoadScene("Game");
    }

    public void ChangeGameSecene(string sceneName)
    {
        SoundManager.Instance.PlayMouseClickSound();
        SoundManager.Instance.SetStartBGM();
        SceneManager.LoadScene("Game");
    }
}
