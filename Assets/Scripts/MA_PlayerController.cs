using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] Animator armAnimator;
    [SerializeField] GameObject gunObject;

    [SerializeField] private bool isJumping;

    private float gravityScale;

    private static int runHash = Animator.StringToHash("Run");

    private int curAniHash;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigid.gravityScale;
        GameManager.Instance.playerAnimator = GetComponent<Animator>();
        gunObject.SetActive(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            isJumping = false;
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

    private void AnimatorPlay()
    {
        int checkAniHash;

        checkAniHash = runHash;

        if (isJumping)
        {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            gunObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(DeactivateGunAfterDelay(5f));
        }
    }

    private IEnumerator DeactivateGunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gunObject.SetActive(false);
    }
}
