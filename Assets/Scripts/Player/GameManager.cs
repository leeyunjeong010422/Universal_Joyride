using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int playerHealth = 3;

    public int totalPoint;
    public int stagePoint;

    [SerializeField] PlayerController player;
    [SerializeField] public Animator playerAnimator;
    [SerializeField] GameObject playerObject;

    private bool isShieldActive;

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
        player = FindObjectOfType<PlayerController>();

        if (playerObject != null)
        {
            playerAnimator = playerObject.GetComponent<Animator>();
        }
    }

    public void TakeDamage()
    {
        if (isShieldActive) //쉴드가 활성화되어 있다면 피해X
            return;

        if (player.gameObject.layer == 7)
            return;

        playerHealth--;
        player.gameObject.layer = 7;
        player.playerSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.LeftArmSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.RightArmSpriteRenderer.color = new Color(1, 1, 1, 0.4f);
        player.Invoke("OffDamaged", 3);

        if (playerHealth <= 0)
        {
            playerAnimator.SetTrigger("Die");
            StartCoroutine(HandlePlayerDeath());
        }
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
        //애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(playerAnimator.GetCurrentAnimatorStateInfo(0).length);
        playerObject.SetActive(false);
        //Destroy(gameObject);
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void AddScore(int points)
    {
        stagePoint += points;
        totalPoint += points;
    }
}
