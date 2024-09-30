using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemyFire : BaseEnemyFire
{
    //필요한 추가 기능이 있다면 구현
    //현재는 BaseEnemyFire에 있는 거로만 사용

    protected override IEnumerator FireBullets()
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
                
                GunSound();

                StartCoroutine(DeactivateDelay(bullet, 2f));
            }
        }
    }
}
