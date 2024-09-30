using System.Collections;
using UnityEngine;

//공통 부모 클래스
public abstract class BaseEnemyController : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Animator animator;
    protected bool isGrounded;
    protected WaitForSeconds delay;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        delay = new WaitForSeconds(animationLength);

        StartCoroutine(JumpRoutine());
    }

    protected virtual void Update()
    {
        Run();
    }

    protected virtual void Run()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public virtual void Die()
    {
        SoundManager.Instance.PlayEnemyDyingSound();
        animator.SetTrigger("Die");
        StartCoroutine(AnimationFinished());
    }

    //Die 애니메이션이 끝날 때까지 기다린 후에 비활성화함
    protected IEnumerator AnimationFinished()
    {
        yield return delay;
        gameObject.SetActive(false);
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);

            if (isGrounded)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                isGrounded = false;
            }
        }
    }
}
