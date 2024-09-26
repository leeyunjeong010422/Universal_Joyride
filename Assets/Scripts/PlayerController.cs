using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] GameObject armObject;
    [SerializeField] GameObject gunObject;

    private float gravityScale;
    private bool isGrounded; // 땅에 있는지 여부를 확인하는 변수

    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");

    private int curAniHash;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigid.gravityScale;

        armObject.SetActive(true);
        gunObject.SetActive(false);
    }

    private void Update()
    {
        Run();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 땅에 닿으면 isGrounded를 true로 설정
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // 땅에서 떨어지면 isGrounded를 false로 설정
            rigid.gravityScale = gravityScale;
        }
    }

    private void AnimatorPlay()
    {
        int checkAniHash;

        if (rigid.velocity.y > 0.5f)
        {
            checkAniHash = jumpHash;
            armObject.SetActive(false);
        }
        else if (rigid.velocity.y < -0.5f && !isGrounded)
        {
            checkAniHash = fallHash; // 땅에 있지 않을 때만 fall 애니메이션 실행
            armObject.SetActive(false);
        }
        else
        {
            checkAniHash = runHash;
            armObject.SetActive(true);
        }

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            gunObject.SetActive(true);
        }
    }
}
