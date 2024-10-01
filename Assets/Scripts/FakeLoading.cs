using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//수업 때 사용한 코드 참고
public class FakeLoading : MonoBehaviour
{
    [SerializeField] Image loadingImage;
    [SerializeField] Slider loadingBar;

    private void Start()
    {
        //보스룸으로 자동 로딩 시작
        StartCoroutine(LoadBossRoom());
    }

    private IEnumerator LoadBossRoom()
    {
        //로딩 이미지 활성화
        loadingImage.gameObject.SetActive(true);
        loadingBar.value = 0;

        //페이크 로딩 시간 설정
        float fakeLoadingTime = 3f;
        float elapsedTime = 0f;

        //페이크 로딩 바 진행
        while (elapsedTime < fakeLoadingTime)
        {
            elapsedTime += Time.deltaTime;
            loadingBar.value = elapsedTime / fakeLoadingTime;
            yield return null;
        }

        //비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BossRoom");
        asyncLoad.allowSceneActivation = false;

        //실제 로딩 진행 상황 업데이트
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress < 0.9f)
            {
                loadingBar.value = asyncLoad.progress;
            }
            else
            {
                loadingBar.value = 1; //로딩 완료로 표시
                break; //로딩 완료
            }

            yield return null;
        }

        //씬 전환
        asyncLoad.allowSceneActivation = true;

        //로딩 이미지 비활성화
        loadingImage.gameObject.SetActive(false);
    }
}
