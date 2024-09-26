using System.Collections;
using UnityEngine;

public class Enemy_Gun_Controller : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] Animator animator;

    private float gravityScale;

    private int curAniHash;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigid.gravityScale;

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
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigid.gravityScale = gravityScale;
        }
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 3f); //1~3초 사이의 랜덤
            yield return new WaitForSeconds(waitTime);

            if (rigid != null && rigid.velocity.y == 0) //땅에 있는 경우에만 점프
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
                rigid.gravityScale = gravityScale;
            }
        }
    }
}
