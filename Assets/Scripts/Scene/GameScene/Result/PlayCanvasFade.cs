using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCanvasFade : MonoBehaviour
{
    public static PlayCanvasFade instance;
    private void Awake()
    {
        instance = this;
    }
    private float alpha000 = 0;
    public void PlayCanvasFadeOnClick()
    {
        Debug.Log("PlayCanvasFadeOnClick ");
        StartCoroutine(FadeOn());
       
    }

    IEnumerator FadeOn()
    {
       
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        GameManager.Instance.ResultSceneLoad();
    }
}
