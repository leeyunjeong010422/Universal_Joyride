using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BR_PlayerController : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public Slider hpBar;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer rightSpriteRenderer; 
    [SerializeField] SpriteRenderer leftSpriteRenderer;

    public Cinemachine.CinemachineImpulseSource impulseSource; //카메라 흔들리게 하기

    private void Start()
    {
        hpBar = GameObject.Find("PlayerHP").GetComponent<Slider>();
        currentHP = maxHP;
        UpdateHPBar();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("BossAttack") && !isInvincible) //무적 상태가 아닐 때만 데미지 받기
        {
            TakeDamage(10);
            StartCoroutine(ActivateInvincibility(3f)); //3초간 무적 상태 유지
        }

        if (other.CompareTag("BossRandomParticle") && !isInvincible)
        {
            TakeDamage(10);
            StartCoroutine(ActivateInvincibility(3f));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPBar();

        if (currentHP <= 0)
        {
            SoundManager.Instance.StopBGM();
            SoundManager.Instance.PlayGameOverSound();
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.value = currentHP;
        }
    }

    private IEnumerator ActivateInvincibility(float duration)
    {
        isInvincible = true;

        impulseSource.GenerateImpulse();
        //반투명 효과 시작
        Color originalColor = spriteRenderer.color; //원래 색상 저장
        Color rightOriginalColor = rightSpriteRenderer.color;
        Color leftOriginalColor = leftSpriteRenderer.color;

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f); //알파 값 0.5로 설정
        rightSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        leftSpriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);

        yield return new WaitForSeconds(duration); //duration 동안 대기

        //원래 색상으로 복구
        spriteRenderer.color = originalColor;
        rightSpriteRenderer.color = rightOriginalColor;
        leftSpriteRenderer.color = leftOriginalColor;

        isInvincible = false; // 무적 상태 비활성화
    }
}
