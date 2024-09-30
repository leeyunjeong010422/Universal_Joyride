using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool enemyPool;
    [SerializeField] Transform player;
    [SerializeField] float spawnDistance = 20f; //생성 거리
    [SerializeField] float spawnInterval = 5f; //생성 간격

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            //적 생성
            GameObject enemy = enemyPool.GetEnemy();
            if (enemy != null)
            {
                Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, -26.8f, 0);
                enemy.transform.position = spawnPosition; //생성 위치 설정

                //10초 후에 적을 반환
                StartCoroutine(ReturnEnemyAfterDelay(enemy, 10f));
            }
        }
    }

    private IEnumerator ReturnEnemyAfterDelay(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        enemyPool.ReturnEnemy(enemy); //적 반환
    }
}
