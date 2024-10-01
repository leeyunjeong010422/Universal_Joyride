using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource startBGM;
    [SerializeField] private AudioSource gunClip;
    [SerializeField] private AudioSource coinClip;
    [SerializeField] private AudioSource itemClip;
    [SerializeField] private AudioSource attackClip;
    [SerializeField] private AudioSource gameOverClip;
    [SerializeField] private AudioSource mouseClickClip;
    [SerializeField] private AudioSource enemyGunClip;
    [SerializeField] private AudioSource enemyArrowClip;
    [SerializeField] private AudioSource bombClip;
    [SerializeField] private AudioSource enemyDyingClip;
    [SerializeField] private AudioSource bossSoundClip;
    [SerializeField] private AudioSource warningClip;
    [SerializeField] private AudioSource gameClearClip;
    [SerializeField] private AudioSource playerJumpClip;

    //이전 BGM 위치 저장
    private float previousTime = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetStartBGM()
    {
        PlayBGM(startBGM);
    }

    public void PlayBGM(AudioSource bgm)
    {
        bgm.loop = true;
        bgm.Play();
        previousTime = 0f;
    }

    public void StopBGM()
    {
        previousTime = startBGM.time; //startBGM의 현재 시간 저장
        startBGM.Stop();
    }

    public void ResumeBGM()
    {
        startBGM.time = previousTime; //이전 시간으로 복구
        startBGM.Play();
    }

    public void ButtonToggle()
    {
        if (startBGM != null)
        {
            startBGM.mute = !startBGM.mute;
        }
    }

    public bool IsSoundMuted()
    {
        return startBGM != null && startBGM.mute; //음소거 상태 반환
    }

    public void PlayGunSound()
    {
        gunClip.PlayOneShot(gunClip.clip); //총 소리 재생
    }

    public void PlayCoinSound()
    {
        coinClip.PlayOneShot(coinClip.clip); //코인 먹는 소리 재생
    }

    public void PlayItemSound()
    {
        itemClip.PlayOneShot(itemClip.clip); //아이템 먹는 소리 재생
    }

    public void PlayAttackSound()
    {
        attackClip.PlayOneShot(attackClip.clip); //플레이어가 피격 당했을 때 나는 소리 재생
    }

    public void PlayGameOverSound()
    {
        gameOverClip.PlayOneShot(gameOverClip.clip); //게임오버일 때 나는 소리 재생
    }

    public void PlayMouseClickSound()
    {
        mouseClickClip.PlayOneShot(mouseClickClip.clip); //마우스 클릭할 때 나는 소리
    }

    public void PlayEnemyGunSound()
    {
        enemyGunClip.PlayOneShot(enemyGunClip.clip); //총 쏘는 적 공격 소리 재생
    }

    public void PlayEnemyArrowSound()
    {
        enemyArrowClip.PlayOneShot(enemyArrowClip.clip); //활 쏘는 적 공격 소리 재생
    }

    public void PlayBombSound()
    {
        bombClip.PlayOneShot(bombClip.clip); //폭탄 터지는 소리 재생
    }

    public void PlayEnemyDyingSound()
    {
        enemyDyingClip.PlayOneShot(enemyDyingClip.clip); //적 피격 소리 재생
    }

    public void PlayBossSound()
    {
        bossSoundClip.PlayOneShot(bossSoundClip.clip); //보스 공격 소리 재생
    }

    public void PlayWarningSound()
    {
        warningClip.PlayOneShot(warningClip.clip); //경고 소리 재생
    }

    public void PlayGameClaerSound()
    {
        gameClearClip.PlayOneShot(gameClearClip.clip); //게임 클리어 소리 재생
    }

    public void PlayPlayerJumpSound()
    {
        playerJumpClip.PlayOneShot(playerJumpClip.clip); //플레이어 점프 소리 재생
    }
}
