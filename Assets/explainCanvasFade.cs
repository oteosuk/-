using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explainCanvasFade : MonoBehaviour
{
    private short alpha000 = 0;
    
    
    // config 버튼을 클릭했을 때, menuScene을 fade 처리
    public void explainCanvasFadeOnClick()
    {
        Debug.Log("explainCanvas FadeOn ");
        StartCoroutine(FadeOn());
        
    }

    IEnumerator FadeOn()
    {
        
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        Time.timeScale = 1; // 화면 재개
        
        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
        //MainManager.Instance.OnClickExplainCancelBtn();

    }
}
