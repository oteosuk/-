using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;
using Firebase;
using Firebase.Extensions;

// Developer : Shim eungi
public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;
    
    public static FirebaseAuthManager Instance
    {
        get {
            if ( instance == null )
            {
                instance = new FirebaseAuthManager();
            }
            return instance;
        }
    }
    
    private FirebaseAuth _auth; // login, createAccount
    private FirebaseUser _user;  // complete authenticated user information

    //public DependencyStatus dependencyStatus;
    
    //public string UserId;
    
    public Action<bool> LoginState;


    /*
    private void Awake()
    {
        FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Cound not resolve all Firebase Dependencies" + dependencyStatus);
            }
        });
    }*/

    /*
    private void InitializeFirebase()
    {
        Debug.Log("Set up Firebase Auth");
        _auth = FirebaseAuth.DefaultInstance;
    }
*/
    public void Init()
    {
        _auth = FirebaseAuth.DefaultInstance;
        //UserId = SystemInfo.deviceUniqueIdentifier;
        
        if (_auth.CurrentUser != null)
        {
            LogOut(); // 테스트용
        }
        // auth 가 바뀔때마다 호출
        _auth.StateChanged += OnChanged;
    }

    // event Handler
    private void OnChanged(object sender, EventArgs e)
    {
        // 동일하다면 계정상태에 변화가 없다는 것.
        // 동일하지 않을 때를 가정
        if (_auth.CurrentUser != _user)
        {
            bool signed = (_auth.CurrentUser != _user && _auth.CurrentUser != null);
            if (!signed && _user != null)
            {
                Debug.Log("LogOut");
                LoginState?.Invoke(false);
            }

            _user = _auth.CurrentUser;
            if (signed)
            {
                Debug.Log("LogIn");
                LoginState?.Invoke(true);
            }
        }
        
    }
    
    public void CreateAccount(string username, string email, string password)
    {
        Debug.Log("FirebaseAuthManager CreateAccount");
        string errorMessage = null;
        // 중괄호 안에 { 실행할 내용 }
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if ( task.IsCanceled )
            {
                errorMessage = "Account Cancel";
                Debug.Log("Account Cancel");
                LoadingManager.Instance.AlertForm(errorMessage);
                //return;
            }

            // 이메일이 비정상, 비밀번호가 간단, 이미 가입된 이메일 etc
            if ( task.IsFaulted )
            {
                // 취소 알림창 뜨는 내역
                errorMessage = "Account Fail";
                Debug.Log("Account Fail");
                LoadingManager.Instance.AlertForm(errorMessage);
                //return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("Create Account Complete");

            string emailID;
            string[] emailSplit = email.Split('@');
            emailID = emailSplit[0];
        
            Debug.Log(emailID);
            
            DataBaseManager.Instance.SaveUserInfo(username, emailID, password);
        });
    }

    public void Login(string email, string password)
    {
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            string errorMessage = null;
          
            if ( task.IsCanceled )
            {
                errorMessage = "Login Cancel";
                Debug.Log("Login Cancel");
                LoadingManager.Instance.AlertForm(errorMessage);
                //return;
            }

            // 이메일이 비정상, 비밀번호가 간단, 이미 가입된 이메일 etc
            if ( task.IsFaulted )
            {
                errorMessage = "Login Fail";
                Debug.Log("Login Fail");
                LoadingManager.Instance.AlertForm(errorMessage);
                //return;
            }

            FirebaseUser newUser = task.Result;
            Debug.Log("Login Complete");
            
            // User 정보 DB 에서 불러오는 장소
            
            string emailID;
            string[] emailSplit = email.Split('@');
            emailID = emailSplit[0];
        
            Debug.Log(emailID);
            DataBaseManager.Instance.PsetUpUserInfo(emailID);
            DataBaseManager.Instance.PsetUpScoreBoardInfo(emailID);
            // User Id 전달해서 찾을 것.
            LoadingManager.Instance.OnClickLoadingBtn();
           
        });
        
    }

    public void LogOut()
    {
        
        _auth.SignOut(); // logout
        Debug.Log("Account LogOut");
    }
    
}
