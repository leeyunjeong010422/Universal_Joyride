using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; //적 프리팹 배열 (여러 종류의 적 추가 가능)
    [SerializeField] private int poolSize = 5; //풀 크기
    private Queue<GameObject> enemyPool;

    private void Awake()
    {
        enemyPool = new Queue<GameObject>();

        //풀에 적 생성
        for (int i = 0; i < poolSize; i++)
        {
            //랜덤하게 적 프리팹 선택
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.parent = transform;
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    //적을 가져오는 메서드
    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        return null; //사용할 수 있는 적이 없음
    }

    //적을 반환하는 메서드
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy); //큐에 다시 추가
    }
}
