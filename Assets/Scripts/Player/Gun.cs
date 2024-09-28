using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float fireRate; //총알 발사 간격

    private void Start()
    {
        StartCoroutine(FireBullets());
    }

    public IEnumerator FireBullets()
    {
        while (true) //계속해서 총알을 발사함
        {
            //유니티에서 지정한 간격만큼 대기
            yield return new WaitForSeconds(fireRate);

            //총알을 가져옴
            GameObject bullet = bulletPool.GetBullet();

            if (bullet != null)
            {
                //총알을 총의 위치, 총의 방향과 일치시킴
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;

                bullet.SetActive(true); //발사할 총알 활성화

                StartCoroutine(DeactivateDelay(bullet, 1f)); //총알을 1초 후에 비활성화하고 풀로 반환
            }
        }
    }

    //지정한 시간만큼 대기 후에 사용한 총알을 비활성화하고 다시 풀로 반환해줌
    private IEnumerator DeactivateDelay(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        bulletPool.ReturnBullet(bullet); //총알을 풀로 반환하여 비활성화함
    }
}
