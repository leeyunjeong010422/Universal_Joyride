using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource startBGM;
    [SerializeField] private AudioSource gunClip;

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
}
