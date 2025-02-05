using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int poolSize = 10; //�Ѿ��� ���� (Ǯ ũ��)

    [SerializeField] private List<GameObject> bulletPool; //�Ѿ��� ����? ����?�� ť ����
    [SerializeField]private List<GameObject> activeBullets; //Ȱ��ȭ�� �Ѿ��� �����ϴ� ����Ʈ ����
    //�̰� ���� �� ������ ������ Ȱ��ȭ�Ͽ� ����ϰ� ��Ȱ��ȭ ���� �� ����ߴ� ���� ��ü�� �״�� ������ �Ǿ
    //�ٽ� Ȱ��ȭ ���� �� ������ Ȱ��ȭ �ؼ� ���ǰ� �ִ� ���� �״�� Ȱ��ȭ�� ��
    //�׷��� ���ư��� �Ѿ��� ������� �ʰ� Ȱ��ȭ �� �� ���� ���̴� ���װ� ����

    private void Awake()
    {
        bulletPool = new List<GameObject>();
        activeBullets = new List<GameObject>();

        //�Ѿ��� �����ϰ� ��Ȱ��ȭ ��Ű�� ť�� �߰���
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
        if (bulletPool.Count > 0) //�Ѿ��� Ǯ�� ������� ���� ��
        {
            //�Ѿ��� ������ Ȱ��ȭ
            GameObject bullet = bulletPool[0];
            bulletPool.RemoveAt(0);
            bullet.SetActive(true);
            activeBullets.Add(bullet); // Ȱ��ȭ�� �Ѿ� ����Ʈ�� �߰�
            return bullet; //���� �Ѿ� ��ȯ
        }
        return null; //����� �� �ִ� �Ѿ��� ������ null ��ȯ
                     //(����� �� �ִ� �Ѿ��� 10���̰� 0.5�ʸ��� �����̱� ������ ������ ���� ������ �ʿ� X)
    }

    //�Ѿ��� ��Ȱ��ȭ ��Ű�� �ٽ� ť�� �߰���
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.parent = transform;
        bulletPool.Add(bullet);
        activeBullets.Remove(bullet); //Ȱ��ȭ�Ǿ� �ִ� �Ѿ� ����Ʈ���� ����
    }

    //��� Ȱ��ȭ�� �Ѿ��� ��Ȱ��ȭ�ϰ� Ǯ�� ��ȯ
    public void ResetAllBullets()
    {
        foreach (GameObject bullet in activeBullets)
        {
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
        activeBullets.Clear(); //Ȱ��ȭ�� �Ѿ� ����Ʈ �ʱ�ȭ
    }

}
