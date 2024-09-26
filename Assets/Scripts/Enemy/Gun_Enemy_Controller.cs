using System.Collections;
using UnityEngine;

public class Gun_Enemy_Controller : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;
    [SerializeField] GameObject armObject;
    [SerializeField] GameObject gunObject;

    private bool isGrounded;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        armObject.SetActive(true);
        gunObject.SetActive(true);

        StartCoroutine(JumpRoutine());
    }

    private void Update()
    {
        Run();
    }

    private void Run()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Die();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        armObject.SetActive(false);
        gunObject.SetActive(false);
        StartCoroutine(AnimationFinished());
    }

    private IEnumerator AnimationFinished()
    {
        //애니메이션의 길이만큼 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f); //1~3초 사이의 랜덤으로 점프
            yield return new WaitForSeconds(waitTime);

            if (isGrounded)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                isGrounded = false;
            }
        }
    }
}
