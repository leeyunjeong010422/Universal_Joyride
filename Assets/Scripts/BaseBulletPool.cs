using System.Collections.Generic;
using UnityEngine;

//공통 풀링 클래스 (적들의 발사체 풀링)
public abstract class BaseBulletPool : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected int poolSize;
    protected Queue<GameObject> bulletPool;

    protected virtual void Awake()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = transform;
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        return null;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.parent = transform;
        bulletPool.Enqueue(bullet);
    }
}
