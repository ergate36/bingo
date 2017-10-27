using UnityEngine;
using System.Collections;

public class Goreview_Yes : MonoBehaviour {
    private GameObject popup;
    private Transform popupup;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(GlobalData.g_global.socketState == (int)SocketClass.STATE.mDailyCheckIng && GlobalData.g_global.reviewIndex==1){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            Application.OpenURL(GlobalData.g_global.updateUrl);
        }
	}

    void OnClick()
    {

        SaveReviewData(2);
        GlobalData.g_global.invite_able = true;

        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_review");
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

        GlobalData.g_global.dailyIndex = 101;
        GlobalData.g_global.reviewIndex = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyCheckIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDailyCheckRequest);
       
    }

    void SaveReviewData(int flag)
    {
        PlayerPrefs.SetInt("Review", flag);
    }

    
}
