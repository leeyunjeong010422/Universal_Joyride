using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int playerHealth = 3;

    [SerializeField] public Animator playerAnimator;
    [SerializeField] GameObject playerObject;

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
        playerHealth--;

        if (playerHealth <= 0) // 0 이하로 설정
        {
            playerAnimator.SetTrigger("Die");
            StartCoroutine(HandlePlayerDeath()); // 플레이어 사망 처리 코루틴 실행
        }
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
