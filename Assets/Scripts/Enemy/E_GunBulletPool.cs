using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GunBulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 30;

    private Queue<GameObject> gunBulletPool;

    private void Awake()
    {
        gunBulletPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject E_bullet = Instantiate(bulletPrefab);
            E_bullet.transform.parent = transform;
            E_bullet.SetActive(false);
            gunBulletPool.Enqueue(E_bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (gunBulletPool.Count > 0)
        {
            GameObject E_bullet = gunBulletPool.Dequeue();
            E_bullet.SetActive(true);
            return E_bullet;
        }
        return null;
    }

    public void ReturnBullet(GameObject E_bullet)
    {
        E_bullet.SetActive(false);
        E_bullet.transform.parent = transform;
        gunBulletPool.Enqueue(E_bullet);
    }
}
