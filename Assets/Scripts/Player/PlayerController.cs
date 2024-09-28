using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] Animator armAnimator;
    [SerializeField] GameObject gunObject;
    [SerializeField] GameObject shieldObject;
    [SerializeField] GameObject jumpFlashObject;
    [SerializeField] GameManager gameManager;
    
    private bool isJumping;

    //점프할 때 자연스럽게 중력 적용
    //중력이 높을수록 빨리 떨어짐
    //착지할 때 중력을 0으로 바꿔 바닥에 고정함
    private float gravityScale;
    private int curAniHash;

    private static int runHash = Animator.StringToHash("Run");


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigid.gravityScale;
        gunObject.SetActive(false);
        shieldObject.SetActive(false);
        jumpFlashObject.SetActive(false);
    }

    private void Update()
    {
        Run();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        AnimatorPlay();
    }

    private void Run()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        isJumping = true;
    }

    private void AnimatorPlay()
    {
        int checkAniHash;

        checkAniHash = runHash;

        if (isJumping)
        {
            jumpFlashObject.SetActive(true);
            PlayerRayCast();
            animator.Play(0);
            armAnimator.Play(0);
            curAniHash = 0;
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            isJumping = false;
            jumpFlashObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            GameManager.Instance.TakeDamage();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = gravityScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gun"))
        {
            gunObject.SetActive(true);
            Destroy(collision.gameObject);
            StartCoroutine(DeactivateGun(5f));
        }

        else if (collision.CompareTag("Shield"))
        {
            shieldObject.SetActive(true);
            Destroy(collision.gameObject);
            GameManager.Instance.ActivateShield();
            StartCoroutine(DeactivateShield(5f));
        }

        else if (collision.CompareTag("Laser"))
        {
            GameManager.Instance.TakeDamage();
        }

        else if (collision.CompareTag("Bomb"))
        {
            GameManager.Instance.TakeDamage();
        }

        else if (collision.CompareTag("Planet"))
        {
            GameManager.Instance.TakeDamage();
        }

        else if (collision.CompareTag("Coin"))
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            int points = 0;

            if (isBronze)
            {
                points = 50;
            }
            else if (isSilver)
            {
                points = 70;
            }
            else if (isGold)
            {
                points = 100;
            }

            gameManager.AddScore(points);

            Destroy(collision.gameObject);
        }
    }

    private IEnumerator DeactivateGun(float delay)
    {
        yield return new WaitForSeconds(delay);
        gunObject.SetActive(false);
    }

    private IEnumerator DeactivateShield(float delay)
    {
        yield return new WaitForSeconds(delay);
        shieldObject.SetActive(false);
        GameManager.Instance.DeactivateShield();
    }

    //레이캐스트를 사용하여 적을 감지하면 적 피격
    private void PlayerRayCast()
    {
        int enemyLayerMask = LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, enemyLayerMask);
        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            GunEnemyController gunEnemyController = hit.collider.GetComponent<GunEnemyController>();
            ArrowEnemyController arrowEnemyController = hit.collider.GetComponent<ArrowEnemyController>();
            if (gunEnemyController != null)
            {
                gunEnemyController.Die();
            }
            if (arrowEnemyController != null)
            {
                arrowEnemyController.Die();
            }
        }
    }

}
