using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rankMenuCanvasFade : MonoBehaviour
{
   
    private short alpha000 = 0;
    
    // config 버튼을 클릭했을 때, menuScene을 fade 처리
    public void rankMenuCanvasToMyRankFadeOnClick()
    {
        Debug.Log("RankScene - menuCanvasFadeOnClick ");
        StartCoroutine(MyRankFadeOn());
        
    }

    public void rankMenuCanvasToUserRankFadeOnClick()
    {
        Debug.Log("RankScene - menuCanvasFadeOnClick ");
        StartCoroutine(UserRankFadeOn());

    }

    IEnumerator MyRankFadeOn()
    {
       
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        RankManager.Instance.OnClickMyRankLoadBtn();
    }

    IEnumerator UserRankFadeOn()
    {

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        RankManager.Instance.OnClickUserRankLoadBtn();
    }
}
