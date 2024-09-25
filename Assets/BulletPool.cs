using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 10; //총알의 개수 (풀 크기)

    private Queue<GameObject> bulletPool; //총알을 관리? 보관?할 큐 생성

    private void Awake()
    {
        bulletPool = new Queue<GameObject>();

        //총알을 생성하고 비활성화 시키고 큐에 추가함
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0) //총알의 풀이 비어있지 않을 때
        {
            //총알을 꺼내서 활성화
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet; //꺼낸 총알 반환
        }
        return null; //사용할 수 있는 총알이 없으면 null 반환
                     //(사용할 수 있는 총알이 10개이고 0.5초마다 생성이기 때문에 없으면 만들어서 제작할 필요 X)
    }

    //총알을 비활성화 시키고 다시 큐에 추가함
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
