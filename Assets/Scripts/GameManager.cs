using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //인스펙터에 Cinemachine Impulse Listener + Cinemachine Impulse Source 추가하기
    //Duration: 지속시간, Amplitude: 흔들림 강도, Frequency: 주파수(흔들림 속도) 알맞게 설정하기 
    public Cinemachine.CinemachineImpulseSource impulseSource; //카메라 흔들리게 하기

    //초기 설정값을 저장하는 변수
    //얘를 안 해 주면 재시작했을 때 목숨이 이상하게 전달됨
    [SerializeField] private int initialPlayerHealth = 3;

    private int playerHealth;
    [SerializeField] private Image[] hearts;

    public int totalPoint; //게임 전체에서 얻은 점수
    //현재는 딱히 의미가 없지만 스테이지가 증가하면 마지막에 총 합 점수를 알 수 있음
    public int stagePoint; //현재 스테이지에서 얻은 점수

    [SerializeField] PlayerController player;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] GameObject playerObject;
    [SerializeField] TextMeshProUGUI scoreText;

    private bool isShieldActive; //쉴드 착용 여부
    private WaitForSeconds delay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //새로운 씬이 로드될 때마다 OnSceneLoaded를 호출하도록 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //게임 시작 할 때마다 (재시작 포함) 목숨을 초기값으로 설정함
        playerHealth = initialPlayerHealth;
    }

    private void Start()
    {
        InitializeGameObjects(); //게임 오브젝트들 초기화

        UpdateScoreUI();
        UpdateHealthUI();
    }

    //씬이 새로 로드될 때마다 플레이어의 목숨을 초기값으로 설정
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerHealth = initialPlayerHealth;
        InitializeGameObjects();
        UpdateHealthUI();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    //게임에 필요한 오브젝트 및 컴포넌트들을 초기화
    private void InitializeGameObjects()
    {
        player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            playerObject = player.gameObject;
            playerAnimator = player.GetComponent<Animator>();
        }

        scoreText = FindObjectOfType<TextMeshProUGUI>();

        //Image컴포넌트를 hearts 배열에 저장
        hearts = GameObject.FindGameObjectsWithTag("Heart")
            .Select(heart => heart.GetComponent<Image>())
            .ToArray();

        impulseSource = FindObjectOfType<Cinemachine.CinemachineImpulseSource>();

        float animationLength = playerAnimator != null ? playerAnimator.GetCurrentAnimatorStateInfo(0).length : 0;
        delay = new WaitForSeconds(animationLength);
    }

    public void TakeDamage()
    {
        if (isShieldActive) //쉴드가 활성화되어 있다면 피해X
            return;

        if (playerHealth >= 0)
        {
            if (player.gameObject.layer == 7)
                return;

            impulseSource.GenerateImpulse();
            SoundManager.Instance.PlayAttackSound();
            playerHealth--;
            UpdateHealthUI();
            PlayerDamaged();
        }
        
        else
        {
            Die();
        }
    }

    private void Die()
    {
        SoundManager.Instance.PlayGameOverSound();
        playerAnimator.SetTrigger("Die");
        StartCoroutine(HandlePlayerDeath());
    }

    private void PlayerDamaged()
    {
        player.gameObject.layer = 7;
        player.playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.LeftArmSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.RightArmSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.Invoke("OffDamaged", 3);
    }

    public void ActivateShield()
    {
        isShieldActive = true;
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
    }

    private IEnumerator HandlePlayerDeath()
    {
        yield return delay;

        if (playerObject != null)
        {
            playerObject.SetActive(false);
        }

        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene("GameOver");
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void AddScore(int points)
    {
        stagePoint += points;
        totalPoint += points;
        UpdateScoreUI();
    }

    //점수 UI 업데이트
    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + stagePoint;
        }
    }

    //목숨 UI 업데이트
    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
