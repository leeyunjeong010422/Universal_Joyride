using UnityEngine;

//각 발사체 클래스들의 부모클래스
public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }
}
