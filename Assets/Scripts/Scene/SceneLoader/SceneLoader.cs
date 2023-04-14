using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Developer - Shim Eungi
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

     // SceneLoader가 존재하지않으면 instance 생성 아니면 기존 SceneLoader 를 obj
    public static SceneLoader Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();
                if(obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    [SerializeField]
    private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField]
    private Image progressBar;

    private string loadSceneName;

    private static SceneLoader Create()
    {
        var sceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
        return Instantiate(sceneLoaderPrefab);

        //var prefab = Resources.Load<GameObject>("SceneLoader");
        //var sceneLoader = prefab.GetComponent<SceneLoader>();
        //return Instantiate(sceneLoader);
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        LoadScene("LoadingScene");
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("SceneLoader - LoadScene start");
        //SceneManager.LoadScene(sceneName);

        Debug.Log(gameObject);
        gameObject.SetActive(true);
      

        //Clear(sceneName);
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName; // load 할 Scene 이름
        StartCoroutine(Load(loadSceneName));
        Debug.Log("SceneLoader - LoadScene end");
    }

    // 비동기적으로 다음 씬 전환

    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0.0f; // 로딩 게이지 초기화
        yield return StartCoroutine(Fade(true));

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.unscaledDeltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }

        if(!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    private void Clear(string sceneName){
        Debug.Log("Current Scene : " + sceneName);
    }
}

