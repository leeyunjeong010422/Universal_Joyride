using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomParticleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] float spawnInterval = 3f; //3초마다 생성
    [SerializeField] Vector2 spawnArea = new Vector2(0f, 1f); //생성 범위(Y축만 랜덤)
    [SerializeField] float moveSpeed = 80f;
    [SerializeField] float particleLifetime = 5f; //파티클 생존 시간

    private Camera mainCamera;
    private List<(ParticleSystem particle, float spawnTime)> activeParticles = new List<(ParticleSystem, float)>(); // 현재 활성화된 파티클 목록과 생성 시간을 저장

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnParticles());
    }

    private IEnumerator SpawnParticles()
    {
        while (true)
        {
            //카메라 뷰포트에서 오른쪽 끝 부분의 월드 좌표(뷰포트 좌표 (1, y, z))
            Vector3 spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(1, Random.Range(0f, 1f), 10));
            spawnPosition.z = 0;
            spawnPosition.y += Random.Range(-spawnArea.y, spawnArea.y); //Y축 범위에서 랜덤하게 위치 설정

            // 파티클 생성
            ParticleSystem newParticle = Instantiate(particle, spawnPosition, Quaternion.identity);
            newParticle.Play();
            activeParticles.Add((newParticle, Time.time)); //생성된 파티클과 생성 시간 추가

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void FixedUpdate()
    {
        //모든 활성화된 파티클 이동 처리
        for (int i = activeParticles.Count - 1; i >= 0; i--)
        {
            var (particle, spawnTime) = activeParticles[i];

            if (particle != null && particle.isPlaying)
            {
                particle.transform.position += Vector3.left * moveSpeed * Time.deltaTime; //왼쪽으로 이동

                //5초가 지나면 파티클을 제거
                if (Time.time - spawnTime > particleLifetime)
                {
                    activeParticles.RemoveAt(i);
                    Destroy(particle.gameObject);
                }
            }
        }
    }
}
