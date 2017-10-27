using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using System.Diagnostics;
#if UNITY_ANDROID
using LitJson;
#endif
using System;


public class AndroidManager : MonoBehaviour
{

#if UNITY_ANDROID
    private static AndroidManager _instance;
    public AndroidJavaObject activity;
//#endif
    public static AndroidManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(AndroidManager)) as AndroidManager;
                if (_instance == null)
                {
                    _instance = new GameObject("AndroidManager").AddComponent<AndroidManager>();
                }
            }
            return _instance;
        }
    }
    
    public void Awake()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }


    public void openOfferWall()
    {
        activity.Call("openOfferWall");
    }

    public void initFlag()
    {
        activity.Call("initFlag");
    }


    public void getTapjoyPoint(string coin)
    {

        //GlobalData.g_global.invite_able = true;
        //Socket_Ctrl.sCtrl.StartClient();
        //int temp = Convert.ToInt32(coin);
        //GlobalData.g_global.getCoin = temp;
        //if (temp != 0)
        //{
        //    GlobalData.g_global.myInfo.coinAmount += temp;
        //    ItemLayer_Ctrl.ictrl.SetItem();
        //    temp += 1;
        //    GlobalData.g_global.reviewIndex = 222;
        //    GlobalData.g_global.dailyIndex = temp;
        //    GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyCheckIng;
        //    Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDailyCheckRequest);
        //}
        //else
        //{
        //    GlobalData.g_global.reviewIndex = 333;
        //}

    }

    public void callLogin()
    {
        activity.Call("Login_U");
    }

    public void callPostToWall(string aa)
    {
        activity.Call("PostToMeWall_U",aa);
    }

    public void requestLogin(string flag)
    {
        if (flag == "1")
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.NaverconnectionFail;
            return;
        }

        GlobalData.g_global.soundFlag = true;
        GlobalData.g_global.login_num = 5;
        GlobalData.g_global.flag = int.Parse(flag);
        PlayerPrefs.SetInt("NaverFlag", GlobalData.g_global.flag);
        if(GlobalData.g_global.just_one == 0){
            GlobalData.g_global.just_one = 100;
            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            Application.LoadLevel("LoginScene");

        }

    }
    public void callInitFaceBook()
    {
        activity.Call("InitFacebook_U");
    }

    public void Buy(string ItemID)
    {
        activity.Call("Buy", ItemID);
    }


    public void PurchaseOK(string ItemID)
    {
        int flagg = 0;
        if (ItemID == "bingo_1")
        {
            flagg = 0;
            GlobalData.g_global.myInfo.gemCount += 10;
        }
        else if (ItemID == "bingo_2")
        {
            flagg = 1;
            GlobalData.g_global.myInfo.gemCount += 30;
        }
        else if (ItemID == "bingo_3")
        {
            flagg = 2;
            GlobalData.g_global.myInfo.gemCount += 100;
        }
        else if (ItemID == "bingo_4")
        {
            flagg = 3;
            GlobalData.g_global.myInfo.gemCount += 300;
        }
        else if (ItemID == "bingo_5")
        {
            flagg = 4;
            GlobalData.g_global.myInfo.gemCount += 500;
        }
        else
        {
            flagg = 5;
            GlobalData.g_global.myInfo.gemCount += 1000;
        }

        GlobalData.g_global.shopIndex = flagg + 1;
        GlobalData.g_global.shopTapindex = 0;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemShopIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mItemShopRequest);
    }

    public void responseBuy(string flag)
    {
        if(flag=="100"){

            return;
        }
        int flagg = int.Parse(flag);
    }

    public void requestBuy(string ItemID,string id)
    {
        activity.Call("requestBuy", ItemID,id);
    }

    public void callLogout()
    {
        activity.Call("Logout_U");
    }

    public void callDisconnect()
    {
        activity.Call("Logout_U");
    }
    
    
    public void RequestFriends_info()
    {
        activity.Call("requestFriends_U");
    }

    public void ResultFriends_J(string jsonFriends)
    {
        JsonData jData = JsonMapper.ToObject(jsonFriends);
        friendData fd = new friendData();
        GlobalData.g_global.myFriendList.Clear();
        
        for (int i = 0; i < jData.Count; i++)
        {
            fd.FriendKey = jData[i]["FacebookId"].ToString();
            fd.FriendName = jData[i]["Name"].ToString();

            GlobalData.g_global.myFriendList.Add(fd);
        }
    }

    public void responseMyinfo_userId(string userId)
    {
        GlobalData.g_global.myInfo.userKey = userId;
        SaveMyuserKeyData(userId);
        //requestLogin("2");
    }

    public void responseMyinfo_device(string device)
    {
        GlobalData.g_global.myInfo.deviceId = device;
    }

    public void responseMyinfo_userName(string userName)
    {
        GlobalData.g_global.myInfo.nickName = userName;
        requestLogin("2");
    }

    public void requestExit(string a)
    {
        ProcessThreadCollection pt = Process.GetCurrentProcess().Threads;

        foreach (ProcessThread p in pt)
        {
            p.Dispose();
        }

        System.Diagnostics.Process.GetCurrentProcess().Kill();

        Application.Quit();
    }

    void SaveMyuserKeyData(string userEmail)
    {
        PlayerPrefs.SetString("userKey", userEmail);
    }

#endif    
}
