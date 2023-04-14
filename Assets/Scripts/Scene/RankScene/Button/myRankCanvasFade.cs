using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myRankCanvasFade : MonoBehaviour
{

    private short alpha000 = 0;

    // config 버튼을 클릭했을 때, menuScene을 fade 처리
    public void myRankCanvasFadeOnClick()
    {
        Debug.Log("myRankCanvasFadeOnClick");
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

        RankManager.Instance.OnClickMyRankCancelBtn();
    }

}
