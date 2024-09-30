using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESC_Menu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton; //계속하기
    public Button restartButton; //다시하기
    public Button menuButton; //메뉴로 돌아가기
    private bool isPaused = false; //게임이 멈췄는지 여부

    private void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(GoToStart);

        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); //게임을 계속 진행
            }
            else
            {
                Pause(); //게임을 멈춤
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //게임 시간 멈춤
        isPaused = true;
        SoundManager.Instance.StopBGM();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SoundManager.Instance.ResumeBGM();
    }

    public void Restart()
    {
        SoundManager.Instance.PlayMouseClickSound();
        Time.timeScale = 1f;
        SoundManager.Instance.SetStartBGM();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //현재 씬 재시작
    }

    public void GoToStart()
    {
        SoundManager.Instance.PlayMouseClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
}
