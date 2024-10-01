using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float fireRate; //총알 발사 간격

    private WaitForSeconds fireRateDelay;
    private WaitForSeconds deactivateDelay;

    private void Start()
    {
        fireRateDelay = new WaitForSeconds(fireRate);
        deactivateDelay = new WaitForSeconds(1.3f);

        StartCoroutine(FireBullets());
    }

    public IEnumerator FireBullets()
    {
        while (true) //계속해서 총알을 발사함
        {
            //유니티에서 지정한 간격만큼 대기
            yield return fireRateDelay;

            //총알을 가져옴
            GameObject bullet = bulletPool.GetBullet();

            if (bullet != null)
            {
                //총알을 총의 위치, 총의 방향과 일치시킴
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;

                bullet.SetActive(true); //발사할 총알 활성화

                SoundManager.Instance.PlayGunSound(); //발사할 때마다 사운드 발생

                StartCoroutine(DeactivateDelay(bullet)); //총알을 1초 후에 비활성화하고 풀로 반환
            }
        }
    }

    //지정한 시간만큼 대기 후에 사용한 총알을 비활성화하고 다시 풀로 반환해줌
    private IEnumerator DeactivateDelay(GameObject bullet)
    {
        yield return deactivateDelay;
        bulletPool.ReturnBullet(bullet); //총알을 풀로 반환하여 비활성화함
    }
}
