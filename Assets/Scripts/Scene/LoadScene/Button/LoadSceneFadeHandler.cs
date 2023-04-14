using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneFadeHandler : MonoBehaviour
{
    enum button
    {
        LoadCanvasToLoginCanvas,
        LoginCanvasToCreateAccountCanvas,
        createAccount,
        createAccountCancel,
    }
    private short alpha000 = 0;

    // config 버튼을 클릭했을 때, CreateAccountCanvas fade 처리
    public void OnClickCreateAccountCanvasToCreateAccount()
    {
        Debug.Log("OnClickCreateAccountCanvasToCreateAccount");
        StartCoroutine(FadeOn(button.createAccount));

    }

    public void OnClickCreateAccountCanvasToLoginCanvas()
    {
        Debug.Log("OnClickCreateAccountCanvasToCancel");
        StartCoroutine(FadeOn(button.createAccountCancel));

    }

    public void OnClickLoadCanvasToLoginCanvas()
    {
        Debug.Log("OnClickLoadCanvasToLoginCanvasBtn ");
        StartCoroutine(FadeOn(button.LoadCanvasToLoginCanvas));

    }

    public void OnClickLoginCanvasToCreateAccountCanvas()
    {
        Debug.Log("OnClickLoadCanvasToLoginCanvasBtn ");
        StartCoroutine(FadeOn(button.LoginCanvasToCreateAccountCanvas));

    }


    IEnumerator FadeOn(button btn)
    {

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > alpha000)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;

        if(btn == button.createAccount) {
            LoadingManager.Instance.OnClickCreateAccountBtn();
        }

        else if (btn == button.createAccountCancel){
            LoadingManager.Instance.OnClickCreateAccountCancelBtn();
        }

        else if (btn == button.LoadCanvasToLoginCanvas){
            LoadingManager.Instance.OnClickLoginCanvasBtn();
        }

        else if( btn == button.LoginCanvasToCreateAccountCanvas) {
            LoadingManager.Instance.OnClickCreateAccountCanvasBtn();
        }

        else{
            Debug.LogError("createAccount Error");
        }
    }
}
