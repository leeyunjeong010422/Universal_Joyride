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

    private bool isGrounded; // 땅에 있는지 확인하는 변수

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
            isGrounded = true; // 땅에 닿았을 때 true로 설정
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
            isGrounded = false; // 땅에서 벗어났을 때 false로 설정
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
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // 현재 애니메이션 길이만큼 대기
        gameObject.SetActive(false); // 게임 오브젝트 비활성화
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f); // 1~3초 사이의 랜덤
            yield return new WaitForSeconds(waitTime);

            if (isGrounded) // 땅에 있는 경우에만 점프
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                isGrounded = false; // 점프 후에는 땅에 없으므로 false로 설정
            }
        }
    }
}
