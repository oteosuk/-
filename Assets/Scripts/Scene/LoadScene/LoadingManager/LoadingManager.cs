using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Developer - Shim eungi
public class LoadingManager : MonoBehaviour
{
    private static LoadingManager instance = null;
    
     // Scene
    //public GameObject LoadingScene;
    
    
    // Load ( init Scene )
    public Canvas LoadCanvas;
    public CanvasGroup LoadCanvasGroup;
    
    // Login Form
    public Canvas LoginCanvas;
    public CanvasGroup LoginCanvasGroup;

    // Login inputField
    public InputField login_email;
    public InputField login_password;

    // Create Account Form
    public Canvas CreateAccountCanvas;
    public CanvasGroup CreateAccountCanvasGroup;

    // Create Account inputField
    public InputField create_email;
    public InputField create_password;
    public InputField create_username;


    // Alert Form
    public Canvas AlertCanvas;
    public CanvasGroup AlertCanvasGroup;
    public Text AlertMessage;

    // Fadein, FadeOut in Config
    private float alpha100 = 1.0f;
    private float alpha000 = 0.0f;

    public static LoadingManager Instance
    {
        get {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance) {
            instance = this;
            //  DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        LoadCanvasGroup.alpha = alpha100;
        LoadCanvas.enabled = true;

        LoginCanvasGroup.alpha = alpha000;
        LoginCanvas.enabled = false;

        CreateAccountCanvasGroup.alpha = alpha000;
        CreateAccountCanvas.enabled = false;

        AlertCanvasGroup.alpha = alpha000;
        AlertCanvas.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBGM("MainSceneBGM");
        Debug.Log("LoadingManager Log");

        //FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    
    public void OnClickLoadingBtn(){
        Debug.Log("OnClickLoadingBtn 클릭");
        SceneLoader.Instance.LoadScene("MainScene");
    }
    
    public void OnClickLoginCanvasBtn()
    {
        // 로그인 첫화면에서 로그인 캔버스 띄우는 버튼
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickLoginCanvasBtn");
 
        LoginCanvasGroup.alpha = alpha100;
        LoginCanvasGroup.interactable = true;
        LoginCanvas.enabled = true;
        LoadCanvas.enabled = false;


    }
    public void OnClickLoginBtn()
    {
        // Login Canvas 에서 Login 버튼 누름
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickLoginBtn");

        string eText = login_email.text;
        string pText = login_password.text;

        login_email.text = "";
        login_password.text = "";

        //StartCoroutine(FirebaseAuthManager.Instance.Login(eText, pText));
        FirebaseAuthManager.Instance.Login(eText, pText);

    }

    public void OnClickCreateAccountCanvasBtn()
    {
        // Login Canvas 에서 CreateAccount Canvas form 으로 넘어가는 버튼 누름
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickCreateAccountCanvasBtn");

        CreateAccountCanvasGroup.alpha = alpha100;
        CreateAccountCanvasGroup.interactable = true;
        CreateAccountCanvas.enabled = true;
        LoginCanvas.enabled = false;

    }

    public void OnClickCreateAccountBtn()
    {
        // Create Account 버튼 누름

        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickCreateAccountBtn");

        string eText = create_email.text;
        string pText = create_password.text;
        string nText = create_username.text;

        create_email.text = "";
        create_password.text = "";
        create_username.text = "";

        //StartCoroutine(FirebaseAuthManager.Instance.Login(eText, pText));
        FirebaseAuthManager.Instance.CreateAccount(nText, eText, pText);


        // 생성완료 페이지 만들면 좋음
        LoginCanvasGroup.alpha = alpha100;
        LoginCanvasGroup.interactable = true;
        LoginCanvas.enabled = true;
        CreateAccountCanvas.enabled = false;
    }


    public void OnClickCreateAccountCancelBtn()
    {
        // Create Account Canvas 에서 Login Canvas로 넘어가는 버튼 누름
        AudioManager.instance.PlaySFX("Touch");
        Debug.Log("OnClickCreateAccountCancelBtn");

        LoginCanvasGroup.alpha = alpha100;
        LoginCanvasGroup.interactable = true;
        LoginCanvas.enabled = true;
        CreateAccountCanvas.enabled = false;

    }

    public void AlertForm(string eMessage)
    {
        AlertCanvasGroup.alpha = alpha100;
        AlertCanvasGroup.interactable = true;
        AlertCanvas.enabled = true;

        Time.timeScale = 0;
        AlertMessage.text = eMessage;
    }

    public void OnClickAlertFormOkBtn()
    {
        AlertCanvasGroup.alpha = alpha000;
        AlertCanvasGroup.interactable = false;
        AlertCanvas.enabled = false;

        Time.timeScale = 1;
        AlertMessage.text = "";
    }




}
