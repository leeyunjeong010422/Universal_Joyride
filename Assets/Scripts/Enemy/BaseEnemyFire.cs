using System.Collections;
using UnityEngine;

//적의 발사 행위에 대한 부모 클래스
public abstract class BaseEnemyFire : MonoBehaviour
{
    [SerializeField] protected BaseBulletPool bulletPool;
    [SerializeField] protected float fireRate;

    protected virtual void Start()
    {
        StartCoroutine(FireBullets());
    }

    protected virtual IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);

            GameObject bullet = bulletPool.GetBullet();

            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;

                bullet.SetActive(true);

                StartCoroutine(DeactivateDelay(bullet, 2f));
            }
        }
    }

    protected virtual IEnumerator DeactivateDelay(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        bulletPool.ReturnBullet(bullet);
    }

    public void GunSound()
    {
        SoundManager.Instance.PlayEnemyGunSound();
    }

    public void ArrowSound()
    {
        SoundManager.Instance.PlayEnemyArrowSound();
    }
}
