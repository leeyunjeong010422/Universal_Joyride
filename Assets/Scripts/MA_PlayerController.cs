using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;

    [SerializeField] private bool isGrounded;
    [SerializeField] private int maxJumpCount = 2; //2단 점프까지만 가능
    private int jumpCount;

    private float gravityScale;

    private static int runHash = Animator.StringToHash("Run");

    private int curAniHash;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigid.gravityScale;
        jumpCount = 0;
    }

    private void Update()
    {
        Run();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
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
        isGrounded = false;
        jumpCount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = gravityScale;
            isGrounded = false;
        }
    }

    private void AnimatorPlay()
    {
        int checkAniHash;

            checkAniHash = runHash;

        if (curAniHash != checkAniHash)
        {
            curAniHash = checkAniHash;
            animator.Play(curAniHash);
        }
    }
}
