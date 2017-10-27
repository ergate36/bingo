using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System;
using System.Text.RegularExpressions;


public class LoginScene : MonoBehaviour
{
    private GameObject popup;
    private Transform popupup;
    private Transform popupdate;
    private Transform popupcheck;

    public bool flag = false;
    private Transform click;
    private Transform loginButton;
    private Transform nickname;
    private Transform clickLabel;
    private int myID;
    private MyCard myCard;
    private Transform tex;
    private Transform teximage;
    public Transform snow;

    public GameObject soundroot;

    void Awake()
    {
        popup = GameObject.Find("loginSceneUI/Camera/Anchor");
        popupdate = popup.transform.Find("popup_Update");
        popupdate.gameObject.SetActive(false);

        GameObject uiRoot = GameObject.Find("loginSceneUI/Camera/Anchor/Panel") as GameObject;
        loginButton = uiRoot.transform.Find("LoginButton");
        loginButton.gameObject.SetActive(false);
        tex = uiRoot.transform.Find("url");
        teximage = uiRoot.transform.Find("url/Urltexture");
        tex.gameObject.SetActive(false);
        click = uiRoot.transform.Find("click");
        click.gameObject.SetActive(false);
        myID = GetMyIDFromPrefs();
    }

    protected void OnGUI()
    {
        //GUI.Label(new Rect(0.0f, 0.0f, 100.0f, 100.0f), "socketstate:" + GlobalData.g_global.socketState);
        //GUI.Label(new Rect(0.0f, 20.0f, 100.0f, 100.0f), "loginState:" + GlobalData.g_global.loginState);
        //GUI.Label(new Rect(0.0f, 40.0f, 100.0f, 100.0f), "urlCount:" + GlobalData.g_global.urlCount);
        //GUI.Label(new Rect(0.0f, 60.0f, 100.0f, 100.0f), "mUrlInfo.Count:" + GlobalData.g_global.mUrlInfo.Count);
        //var y = 80.0f;
        //GUI.Label(new Rect(0.0f, y, 200.0f, 100.0f), "lobbyip:" + GlobalData.g_global.lobbyip);
        //y += 20.0f;
        //GUI.Label(new Rect(0.0f, y, 200.0f, 100.0f), "lobbyport:" + GlobalData.g_global.lobbyport);
        //for (int i = 0; i < GlobalData.g_global.itemip.Length; ++i)
        //{
        //    y += 20.0f;
        //    GUI.Label(new Rect(0.0f, y, 200.0f, 100.0f), "itemip:" + GlobalData.g_global.itemip[i]);
        //    y += 20.0f;
        //    GUI.Label(new Rect(0.0f, y, 200.0f, 100.0f), "itemport:" + GlobalData.g_global.itemport[i]);
        //}
    }

    void Update()
    {
        
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mServerInfoComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            if (GetNaverFromPrefs() != 2)
            {
                loginButton.gameObject.SetActive(true);
            }
            else
            {
#if UNITY_ANDROID
                AndroidManager.Instance.callLogin();
#endif
                loginButton.gameObject.SetActive(false);
            }
        }
        else if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mPushinfoComplete){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            click.gameObject.SetActive(true);
            //snow.gameObject.SetActive(true);
            StartCoroutine("clickSet");
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mLoginIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            if (GlobalData.g_global.loginState == 0)
            {
                SaveMyInfoData((int)GlobalData.g_global.myInfo.userID);
                if (GetDeviceFromPrefs() == 0)
                {
                    SaveMyDevice(1);
                    GlobalData.g_global.socketState = (int)SocketClass.STATE.mPushinfoRequest;
                    Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mPushinfoRequest);
                }
                else
                {
                    click.gameObject.SetActive(true);
                    //snow.gameObject.SetActive(true);
                    StartCoroutine("clickSet");    
                }
            }

            else if (GlobalData.g_global.loginState == 1)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
                popupdate.gameObject.SetActive(true);
            }
            else if (GlobalData.g_global.loginState == 2)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            }
            else if (GlobalData.g_global.loginState == 3)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
                popupup = popup.transform.Find("popup_ConnectionFail");
                popupup.gameObject.SetActive(true);
                string[] temptext = GlobalData.g_global.testMessage.Split('/');
                string resulttext = "";
                for (int i = 0; i < temptext.Length; i++)
                {
                    if (i == 0)
                    {
                        resulttext = temptext[i];
                    }
                    else
                    {
                        resulttext = resulttext + "\n" + temptext[i];
                    }
                }
                popupup.transform.Find("Label").GetComponent<UILabel>().text = resulttext;
            }
            else if (GlobalData.g_global.loginState == 4)
            {

            }
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mNoticeIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            if (GetLastLoginDay() != System.Convert.ToInt32(DateTime.Now.ToString("dd")))
            {
                noticeInit();
            }

            StartCoroutine(GetWebImage(GlobalData.g_global.urlCount));
        }
    }

    private void noticeInit()
    {
        for (int i = 0; i < GlobalData.g_global.mUrlInfo.Count; i++)
        {
            PlayerPrefs.SetInt("noticeView" + GlobalData.g_global.mUrlInfo[i].notice_Num, 0);
        }
        SaveToday(System.Convert.ToInt32(DateTime.Now.ToString("dd")));
    }

    int GetNoticeViewFromPrefs(int urlCount)
    {
        return PlayerPrefs.GetInt("noticeView" + GlobalData.g_global.mUrlInfo[urlCount].notice_Num);
    }

    private IEnumerator clickSet()
    {
        yield return new WaitForSeconds(0.5f);
        click.transform.GetComponent<UILabel>().color = new Color(248f / 255f, 175f / 255f, 0);
        StartCoroutine("clickSet2");
    }

    private IEnumerator clickSet2()
    {
        yield return new WaitForSeconds(0.5f);

        click.transform.GetComponent<UILabel>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

        StartCoroutine("clickSet");
    }


    public IEnumerator GetWebImage(int urlCount)
    {
        if (GlobalData.g_global.urlCount < GlobalData.g_global.mUrlInfo.Count)
        {
            if (GetNoticeViewFromPrefs(urlCount) == 0)
            {
                WWW www = new WWW(GlobalData.g_global.mUrlInfo[urlCount].img_url);
            
                yield return www;
                tex.gameObject.SetActive(true);
                teximage.GetComponent<UITexture>().mainTexture = www.texture;
            }
            else
            {
                GlobalData.g_global.urlCount++;
                StartCoroutine(GetWebImage(GlobalData.g_global.urlCount));
            }
        }
        else
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect(); 
            Application.LoadLevel("MainScene");
        }
    }

    void Start()
    {
#if UNITY_ANDROID
        AndroidManager.Instance.callInitFaceBook();
#endif
        
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("dailyCheck", 1);
        StopCoroutine("clickSet");
        StopCoroutine("clickSet2");

        // 테스트 코드
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.lobbyip = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
        GlobalData.g_global.lobbyport = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];
        for (int i = 0; i < GlobalData.g_global.itemip.Length; ++i)
        {
            GlobalData.g_global.itemip[i] = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
            GlobalData.g_global.itemport[i] = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];
        }
        Socket_Ctrl.sCtrl.StartClient();
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerInfoIng;
        // 로그인하면서 접속을 끊어버려서 응답을 못받는다.
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mServerInfoRequest);
        GlobalData.g_global.myInfo.nickName = "GOODBOY" + (int)UnityEngine.Random.Range(1, 10000);
        GlobalData.g_global.myInfo.userKey        = "test" + (int)UnityEngine.Random.Range(1, 10000);
        GlobalData.g_global.login_num = 5;
        GlobalData.g_global.myInfo.deviceId = "0";
        // 
        
        GlobalData.g_global.soundFlg = PlayerPrefs.GetInt("soundFlg");
        GlobalData.g_global.pushFlg = PlayerPrefs.GetInt("pushFlg");

        GlobalData.g_global.serverFlag = 1;

        if (GlobalData.g_global.login_num == 5)
        {
            transform.GetComponent<AudioSource>().Play();
            GlobalData.g_global.login_num = 10;
            loginStart(GlobalData.g_global.myInfo.nickName);
        }
        else
        {
            if (GlobalData.g_global.soundFlag == false)
            {
                //transform.audio.Play();
                soundroot.transform.GetComponent<AudioSource>().Play();
            }

            GlobalData.g_global.lobbyip = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
            GlobalData.g_global.lobbyport = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];

            Socket_Ctrl.sCtrl.StartClient();

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerInfoIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mServerInfoRequest);
        } 
    }

    void SaveToday(int t_day)
    {
        PlayerPrefs.SetInt("Day", t_day);
    }

    int GetLastLoginDay()
    {
        return PlayerPrefs.GetInt("Day");
    }

    int GetMyIDFromPrefs()
    {
        return PlayerPrefs.GetInt("userID");
    }

    int GetNaverFromPrefs()
    {
        return PlayerPrefs.GetInt("NaverFlag");
    }

    void SaveMyInfoData(int userId)
    {
        PlayerPrefs.SetInt("userID", userId);
    }

    int GetDeviceFromPrefs()
    {
        return PlayerPrefs.GetInt("device");
    }

    void SaveMyDevice(int Device)
    {
        PlayerPrefs.SetInt("device", Device);
    }



    string GetMyNicknameFromPrefs()
    {
        return PlayerPrefs.GetString("userNickname");
    }

    public void loginStart(string nickname)
    {
       // GlobalData.g_global.myEmail = "TEST";
        Socket_Ctrl.sCtrl.StartClient();
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mLoginIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mLoginRequest);
    }
}
