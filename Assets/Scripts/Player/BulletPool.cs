using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 10; //총알의 개수 (풀 크기)

    [SerializeField] private List<GameObject> bulletPool; //총알을 관리? 보관?할 큐 생성
    [SerializeField]private List<GameObject> activeBullets; //활성화된 총알을 관리하는 리스트 생성
    //이걸 따로 또 생성한 이유는 활성화하여 사용하고 비활성화 했을 때 사용했던 내용 자체가 그대로 저장이 되어서
    //다시 활성화 했을 때 이전에 활성화 해서 사용되고 있던 상태 그대로 활성화가 됨
    //그래서 날아가던 총알이 사라지지 않고 활성화 할 때 마다 보이는 버그가 생김

    private void Awake()
    {
        bulletPool = new List<GameObject>();
        activeBullets = new List<GameObject>();

        //총알을 생성하고 비활성화 시키고 큐에 추가함
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.parent = transform;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bulletPool.Count > 0) //총알의 풀이 비어있지 않을 때
        {
            //총알을 꺼내서 활성화
            GameObject bullet = bulletPool[0];
            bulletPool.RemoveAt(0);
            bullet.SetActive(true);
            activeBullets.Add(bullet); // 활성화된 총알 리스트에 추가
            return bullet; //꺼낸 총알 반환
        }
        return null; //사용할 수 있는 총알이 없으면 null 반환
                     //(사용할 수 있는 총알이 10개이고 0.5초마다 생성이기 때문에 없으면 만들어서 제작할 필요 X)
    }

    //총알을 비활성화 시키고 다시 큐에 추가함
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.parent = transform;
        bulletPool.Add(bullet);
        activeBullets.Remove(bullet); //활성화되어 있던 총알 리스트에서 제거
    }

    //모든 활성화된 총알을 비활성화하고 풀로 반환
    public void ResetAllBullets()
    {
        foreach (GameObject bullet in activeBullets)
        {
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
        activeBullets.Clear(); //활성화된 총알 리스트 초기화
    }

}
