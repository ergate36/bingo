using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class nbFB : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject selectPopup = GameObject.Find("loginSceneUI/Camera/Anchor/popup_select") as GameObject;
        if (nb_GlobalData.g_global.FB_TEST == true)
        {
            selectPopup.SetActive(true);
        }
        else
        {
            nb_GlobalData.g_global.fb_active = true;
            startFB();
        }
	}

    public void startFB()
    {
        //페북 테스트일때만 사용
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log("userId : " + aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            // 유저 이름(닉네임) 불러오기
            FB.API("me?fields=name", HttpMethod.GET, NameCallBack);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();

            var perms = new List<string>() { "public_profile", "email", "user_friends" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void NameCallBack(IGraphResult result)
    {
        string userName = (string)result.ResultDictionary["name"];
        Debug.Log("nickName : " + userName);
        PlayerPrefs.SetString("nickName", userName);

        //닉네임
        nb_GlobalData.g_global.InputSocialNickname = userName;

        var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

        Debug.Log("FB Login : CreateUserSession Start");

        //페북 유저 아이디를 세션 로그인키로 사용
        nb_GlobalData.g_global.InputSessionLoginKey = aToken.UserId;

        nbHttp.http.CreateUserSession();
    }

}
