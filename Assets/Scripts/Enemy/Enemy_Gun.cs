using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Gun : MonoBehaviour
{
    [SerializeField] E_GunBulletPool bulletPool;
    [SerializeField] float fireRate;

    private void Start()
    {
        StartCoroutine(E_FireBullets());
    }

    private IEnumerator E_FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            GameObject B_bullet = bulletPool.GetBullet();

            if (B_bullet != null)
            {
                B_bullet.transform.position = transform.position;
                B_bullet.transform.rotation = transform.rotation;

                B_bullet.SetActive(true);

                StartCoroutine(DeactivateDelay(B_bullet, 2f));
            }
        }
    }

    private IEnumerator DeactivateDelay(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        bulletPool.ReturnBullet(bullet);
    }
}
