using UnityEngine;

public class ArrowBullet : BaseBullet
{
    protected override void Move()
    {
        rb.velocity = Vector2.left * bulletSpeed;
    }
}
