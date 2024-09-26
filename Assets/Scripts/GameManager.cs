using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int playerHealth = 3;

    [SerializeField] public Animator playerAnimator;
    [SerializeField] GameObject playerObject;

    private bool isShieldActive;

    private void Awake()
    {
        // 싱글톤 패턴 구현
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

    public void TakeDamage()
    {
        if (isShieldActive) // 쉴드가 활성화되어 있다면 피해를 입지 않음
            return;

        playerHealth--;

        if (playerHealth <= 0) // 0 이하로 설정
        {
            playerAnimator.SetTrigger("Die");
            StartCoroutine(HandlePlayerDeath()); // 플레이어 사망 처리 코루틴 실행
        }
    }

    public void ActivateShield()
    {
        isShieldActive = true; // 쉴드 활성화
    }

    public void DeactivateShield()
    {
        isShieldActive = false; // 쉴드 비활성화
    }

    private IEnumerator HandlePlayerDeath()
    {
        // 애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        playerObject.SetActive(false); // 플레이어 오브젝트 비활성화
        Destroy(gameObject); // 게임 매니저 비활성화 (필요한 경우)
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }
}
