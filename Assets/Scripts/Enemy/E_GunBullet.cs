using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GunBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float bulletSpeed;

    private E_GunBulletPool bulletPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.left * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
