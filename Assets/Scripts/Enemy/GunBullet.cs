using UnityEngine;

public class GunBullet : BaseBullet
{
    protected override void Move()
    {
        rb.velocity = Vector2.left * bulletSpeed;
    }
}
