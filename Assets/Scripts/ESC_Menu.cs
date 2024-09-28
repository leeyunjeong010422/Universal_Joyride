using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESC_Menu : MonoBehaviour
{
    public GameObject pauseMenuUI; // UI 패널
    public Button resumeButton; // 계속하기 버튼
    public Button restartButton; // 다시하기 버튼
    public Button menuButton; // 메뉴로 돌아가기 버튼
    private bool isPaused = false; // 게임이 멈췄는지 여부

    private void Start()
    {
        // 버튼 클릭 이벤트 연결
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        menuButton.onClick.AddListener(GoToStart);

        // UI 초기 상태 설정
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        // ESC 키 입력 확인
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume(); // 게임을 계속 진행
            }
            else
            {
                Pause(); // 게임을 멈춤
            }
        }
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // UI 활성화
        Time.timeScale = 0f; // 게임 시간 멈춤
        isPaused = true; // 상태 업데이트
        SoundManager.Instance.StopBGM();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // UI 비활성화
        Time.timeScale = 1f; // 게임 시간 재개
        isPaused = false; // 상태 업데이트
        SoundManager.Instance.ResumeBGM();
    }

    public void Restart()
    {
        Time.timeScale = 1f; // 게임 시간 재개
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 재시작
    }

    public void GoToStart()
    {
        Time.timeScale = 1f; // 게임 시간 재개
        SceneManager.LoadScene("Start"); // "Start" 씬으로 전환
    }
}
