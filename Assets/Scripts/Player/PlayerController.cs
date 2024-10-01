using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] public SpriteRenderer playerSpriteRenderer;
    [SerializeField] public SpriteRenderer LeftArmSpriteRenderer;
    [SerializeField] public SpriteRenderer RightArmSpriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Animator armAnimator;
    [SerializeField] GameObject jumpFlashObject;
    [SerializeField] GameManager gameManager;
    [SerializeField] Collision_Trigger_Controller collisionController;
    
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
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collisionController = GetComponent<Collision_Trigger_Controller>();
        gravityScale = rigid.gravityScale;
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
        SoundManager.Instance.PlayPlayerJumpSound();
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = gravityScale;
        }
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

    public void OffDamaged()
    {
        gameObject.layer = 3;
        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
        LeftArmSpriteRenderer.color = new Color(1, 1, 1, 1);
        RightArmSpriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
