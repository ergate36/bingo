using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System;
using System.Text.RegularExpressions;


public class nb_LoginScene : MonoBehaviour
{
    private GameObject popup;
    private Transform popupup;
    private Transform popupdate;
    private Transform popupcheck;

    public bool flag = false;
    //private Transform click;
    private Transform nickname;
    private Transform clickLabel;
    private int myID;
    private Transform tex;
    private Transform teximage;

    public GameObject soundroot;

    public GameObject progress_popup;
    public GameObject nickname_popup;

    void Awake()
    {
        popup = GameObject.Find("loginSceneUI/Camera/Anchor");

        //StartCoroutine("clickSet"); 

        //click.gameObject.SetActive(false);
        //myID = GetMyIDFromPrefs();
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
        //login flow
        
        //start
        if (nbHttp.state == nbHttp.nbHttpState.CreateUserSessionStart)
        {
            //세션 생성중 표시
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "create session";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_CreateSession";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectSocialStart)
        {
            //소셜 로그인 시도중
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "social login";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_SocialLogin";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.CreateUserAccountStart)
        {
            //계정 생성중 표시
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "create account";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_CreateAccount";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectUserAccountStart)
        {
            //계정 접속중 표시
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "connect server";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_ConnectServer";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.BindSocialStart)
        {
            //접속한 계정 소셜 바인드
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "social bind";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_SocialBind";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetStageListStart)
        {
            //스테이지 정보 불러옴
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "stage loading";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_StageLoading";
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetUserPowerUpListStart)
        {
            //유저 아이템 불러옴
            progress_popup.SetActive(true);
            //progress_popup.transform.Find("text").GetComponent<UILabel>().text = "get useritem";
            progress_popup.transform.Find("text").GetComponent<UILocalize>().key = "LoginMsg_GetUserItem";
        }

        //success
        if (nbHttp.state == nbHttp.nbHttpState.CreateUserSessionSuccess)
        {
            Debug.Log("LoginScene Update : CreateUserSession Success, ConnectSocial Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //유저 세션 생성 됨 - 소셜 로그인 시도
            nbHttp.http.ConnectSocial(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.InputSessionLoginKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.CreateUserAccountSuccess)
        {
            Debug.Log("LoginScene Update : CreateUserAccount Success, ConnectUserAccount Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;
            
            //계정 생성됨 - 계정 접속
            nbHttp.http.ConnectUserAccount(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.userAccount.Id,
                nb_GlobalData.g_global.userAccount.Secret);

        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectSocialSuccess)
        {
            Debug.Log("LoginScene Update : ConnectSocial Success, ConnectUserAccount Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //소셜 연동 정보 가져옴 - 계정 접속
            nbHttp.http.ConnectUserAccount(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.userAccount.Id,
                nb_GlobalData.g_global.userAccount.Secret);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectUserAccountSuccess)
        {
            Debug.Log("LoginScene Update : ConnectUserAccount Success, BindSocial Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //계정 접속됨 - 소셜 바인드
            nbHttp.http.BindSocial(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.InputSessionLoginKey);

        }
        else if (nbHttp.state == nbHttp.nbHttpState.BindSocialSuccess)
        {
            Debug.Log("LoginScene Update : BindSocial Success, GetStageList Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //바인드 됨 - 스테이지 정보 가져옴
            nbHttp.http.GetStageList(
                nb_GlobalData.g_global.userSession.SessionKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetStageListSuccess)
        {
            Debug.Log("LoginScene Update : GetStageList Success, GetUserPowerUpList Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //스테이지 정보 가져옴 - 유저 아이템 가져왕
            nbHttp.http.GetUserPowerUpList(
                nb_GlobalData.g_global.userSession.SessionKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetUserPowerUpListSuccess)
        {
            Debug.Log("LoginScene Update : GetUserPowerUpList Success, GetUserGameMoneyList Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //유저 아이템 정보 불러옴 - 게임 머니 불러옴
            nbHttp.http.GetUserGameMoneyList(
                nb_GlobalData.g_global.userSession.SessionKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetUserGameMoneyListSuccess)
        {
            Debug.Log("LoginScene Update : GetUserGameMoneyList Success, GetUserCollectionList Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //유저 게임 머니 불러옴 - 콜렉션 불러옴
            nbHttp.http.GetUserCollectionList(
                nb_GlobalData.g_global.userSession.SessionKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetUserCollectionListSuccess)
        {
            Debug.Log("LoginScene Update : GetUserCollectionList Success, LoadLevel nb_MainScene");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //유저 콜렉션 불러옴 - 메인씬 이동
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            Application.LoadLevel("nb_MainScene");
        }
        else if (nbHttp.state == nbHttp.nbHttpState.DisconnectPreviousSessionSuccess)
        {
            Debug.Log("LoginScene Update : DisconnectPreviousSession, ConnectUserAccount restart");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //이전 세션 무효화 - 세션 연결 재시도
            nbHttp.http.ConnectUserAccount(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.userAccount.Id,
                nb_GlobalData.g_global.userAccount.Secret);
        }


        //fail
        if (nbHttp.state == nbHttp.nbHttpState.ConnectSocialFail)
        {
            if (nb_GlobalData.g_global.fb_active == true)
            {
                Debug.Log("LoginScene Update : ConnectSocialFail, create account by FB nickname");
                nbHttp.state = nbHttp.nbHttpState.Wait;
                //소셜 로그인 실패 - 소셜키로 계정 생성 시도

                //페북 닉네임으로 계정 생성
                nbHttp.http.CreateUserAccount(
                    nb_GlobalData.g_global.userSession.SessionKey,
                    nb_GlobalData.g_global.InputSocialNickname);
            }
            else
            {
                //에뮬 : 닉네임 팝업
                Debug.Log("LoginScene Update : ConnectSocialFail, open nickname popup");
                nbHttp.state = nbHttp.nbHttpState.Wait;

                progress_popup.SetActive(false);
                nickname_popup.SetActive(true);
            }

        }
        else if (nbHttp.state == nbHttp.nbHttpState.ConnectUserAccountFail)
        {
            Debug.Log("LoginScene Update : ConnectUserAccount Fail, DisconnectPreviousSession Start");
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //세션 연결 - 실패 - 이전 세션 무효화
            nbHttp.http.DisconnectPreviousSession(
                nb_GlobalData.g_global.userSession.SessionKey,
                nb_GlobalData.g_global.userAccount.Id,
                nb_GlobalData.g_global.userAccount.Secret);
        }



//        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mServerInfoComplete)
//        {
//            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
//            if (GetNaverFromPrefs() != 2)
//            {
//                loginButton.gameObject.SetActive(true);
//            }
//            else
//            {
//#if UNITY_ANDROID
//                AndroidManager.Instance.callLogin();
//#endif
//                loginButton.gameObject.SetActive(false);
//            }
//        }
//        else if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mPushinfoComplete){
//            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
//            click.gameObject.SetActive(true);
//            //snow.gameObject.SetActive(true);
//            StartCoroutine("clickSet");
//        }
//        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mLoginIngComplete)
//        {
//            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

//            if (GlobalData.g_global.loginState == 0)
//            {
//                SaveMyInfoData((int)GlobalData.g_global.myInfo.userID);
//                if (GetDeviceFromPrefs() == 0)
//                {
//                    SaveMyDevice(1);
//                    GlobalData.g_global.socketState = (int)SocketClass.STATE.mPushinfoRequest;
//                    Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mPushinfoRequest);
//                }
//                else
//                {
//                    click.gameObject.SetActive(true);
//                    //snow.gameObject.SetActive(true);
//                    StartCoroutine("clickSet");    
//                }
//            }

//            else if (GlobalData.g_global.loginState == 1)
//            {
//                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
//                popupdate.gameObject.SetActive(true);
//            }
//            else if (GlobalData.g_global.loginState == 2)
//            {
//                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
//            }
//            else if (GlobalData.g_global.loginState == 3)
//            {
//                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
//                popupup = popup.transform.Find("popup_ConnectionFail");
//                popupup.gameObject.SetActive(true);
//                string[] temptext = GlobalData.g_global.testMessage.Split('/');
//                string resulttext = "";
//                for (int i = 0; i < temptext.Length; i++)
//                {
//                    if (i == 0)
//                    {
//                        resulttext = temptext[i];
//                    }
//                    else
//                    {
//                        resulttext = resulttext + "\n" + temptext[i];
//                    }
//                }
//                popupup.transform.Find("Label").GetComponent<UILabel>().text = resulttext;
//            }
//            else if (GlobalData.g_global.loginState == 4)
//            {

//            }
//        }
//        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mNoticeIngComplete)
//        {
//            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

//            if (GetLastLoginDay() != System.Convert.ToInt32(DateTime.Now.ToString("dd")))
//            {
//                noticeInit();
//            }

//            StartCoroutine(GetWebImage(GlobalData.g_global.urlCount));
//        }
    }

    //private IEnumerator clickSet()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    click.transform.GetComponent<UILabel>().color = new Color(248f / 255f, 175f / 255f, 0);
    //    StartCoroutine("clickSet2");
    //}

    //private IEnumerator clickSet2()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    click.transform.GetComponent<UILabel>().color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

    //    StartCoroutine("clickSet");
    //}

    void Start()
    {
#if UNITY_ANDROID
        AndroidManager.Instance.callInitFaceBook();
#endif
        
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("dailyCheck", 1);
        //StopCoroutine("clickSet");
        //StopCoroutine("clickSet2");

        //// 테스트 코드
        //GlobalData.g_global.serverFlag = 1;
        //GlobalData.g_global.lobbyip = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
        //GlobalData.g_global.lobbyport = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];
        //for (int i = 0; i < GlobalData.g_global.itemip.Length; ++i)
        //{
        //    GlobalData.g_global.itemip[i] = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
        //    GlobalData.g_global.itemport[i] = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];
        //}
        //nbSocket.sCtrl.StartClient();
        //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mServerInfoComplete;
        //// 로그인하면서 접속을 끊어버려서 응답을 못받는다.
        //Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mServerInfoRequest);
        //GlobalData.g_global.myInfo.nickName = "GOODBOY" + (int)UnityEngine.Random.Range(1, 10000);
        //GlobalData.g_global.myInfo.userKey        = "test" + (int)UnityEngine.Random.Range(1, 10000);
        //GlobalData.g_global.login_num = 5;
        //GlobalData.g_global.myInfo.deviceId = "0";
        //// 
        
        //GlobalData.g_global.soundFlg = PlayerPrefs.GetInt("soundFlg");
        //GlobalData.g_global.pushFlg = PlayerPrefs.GetInt("pushFlg");

        //GlobalData.g_global.serverFlag = 1;

        //if (GlobalData.g_global.login_num == 5)
        //{
        //    transform.GetComponent<AudioSource>().Play();
        //    GlobalData.g_global.login_num = 10;
        //    loginStart(GlobalData.g_global.myInfo.nickName);
        //}
        //else
        //{
        //    if (GlobalData.g_global.soundFlag == false)
        //    {
        //        //transform.audio.Play();
        //        soundroot.transform.GetComponent<AudioSource>().Play();
        //    }

        //    GlobalData.g_global.lobbyip = GlobalData.g_global.serverip[GlobalData.g_global.serverIndex];
        //    GlobalData.g_global.lobbyport = GlobalData.g_global.serverport[GlobalData.g_global.serverIndex];

        //    Socket_Ctrl.sCtrl.StartClient();

        //    GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerInfoIng;
        //    Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mServerInfoRequest);
        //} 


        //test//test//test//test//test//test//test//test//test
        //4A-73-6F-6E-4D-04-02-00
        //4 74 115 111 110 77 4 2 
        //18-7B-22-50-6C-61-79-65-72-53-65-73-73-69-6F-6E-49-64-22-3A-22-39-33-22-7D
        //byte[] by = { 24, 123, 34, 80, 108, 97, 121, 101, 114, 83, 101, 115, 115, 105, 111, 110, 73, 100, 34, 58, 34, 57, 51, 34, 125 };
        //byte[] by = { 74, 115, 111, 110, 77, 4, 2, 0 };
        //string s1 = Encoding.UTF8.GetString(by);
        //string s2 = BitConverter.ToString(by);
        
        //byte[] test = { 74, 115 };  //4A-73
        //string s1 = BitConverter.ToString(test);
        //Array.Reverse(test, 0, 2);
        //string s2 = BitConverter.ToString(test);

        //Debug.Log("s1 : " + s1);
        //Debug.Log("s2 : " + s2);

        //List<sbyte> test1 = new List<sbyte>();
        //test1.Add(80); test1.Add(120);
        //test1.Add(127); test1.Add(40);

        //List<List<sbyte>> test = new List<List<sbyte>>();
        //test.Add(test1);

        //Debug.Log("test setting");

        //var cardData0 = test[0];
        //Debug.Log("test[0] " + cardData0.Count);

        //char[,] tempArray = new char[2, 2];
        //for (int x = 0; x < 2; ++x)
        //{
        //    for (int y = 0; y < 2; ++y)
        //    {
        //        tempArray[x, y] = Convert.ToChar(cardData0[(x * 2) + y]);
        //        Debug.Log(x + ", " + y + " : " + ((int)tempArray[x, y]).ToString());
        //    }
        //}
        //Debug.Log("tempArray setting");
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
        //nbSocket.sCtrl.StartClient();
        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mLoginIng;
        //Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mLoginRequest);
    }
}
