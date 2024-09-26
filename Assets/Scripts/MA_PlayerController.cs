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
    [SerializeField] GameObject shieldObject;
    [SerializeField] GameObject jumpFlashObject;

    [SerializeField] private bool isJumping;

    private float gravityScale;

    private static int runHash = Animator.StringToHash("Run");

    private int curAniHash;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GameManager.Instance.playerAnimator = GetComponent<Animator>();
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

    private void AnimatorPlay()
    {
        int checkAniHash;

        checkAniHash = runHash;

        if (isJumping)
        {
            jumpFlashObject.SetActive(true);
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

        else if (other.CompareTag("Shield"))
        {
            shieldObject.SetActive(true);
            Destroy(other.gameObject);
            GameManager.Instance.ActivateShield();
            StartCoroutine(DeactivateShieldAfterDelay(5f));
        }
    }

    private IEnumerator DeactivateGunAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gunObject.SetActive(false);
    }

    private IEnumerator DeactivateShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shieldObject.SetActive(false);
        GameManager.Instance.DeactivateShield();
    }
}
