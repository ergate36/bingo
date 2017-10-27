using UnityEngine;
using System.Collections;
using System.Text;

public class LinkFaceBook : MonoBehaviour {
    public int index;
    public Transform closepopup;
    public Transform buttnroot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        //  201 , 301
        GlobalData.g_global.reviewIndex = 1000;

        if(index == 201){
            // result   coin 500
            if (GlobalData.g_global.gameType == 1)
            {
#if UNITY_ANDROID
                AndroidManager.Instance.callPostToWall("Real-time Network War Game!\nThreatening and aggressive Bingo!\nWin the victory with your friends in the fiery contest through Bingo Monster!");
#endif
                //친구 
            }
            else
            {
#if UNITY_ANDROID
                AndroidManager.Instance.callPostToWall("Real-time Network War Game!\nThreatening and aggressive Bingo!\nWin the victory with your friends in the fiery contest through Bingo Monster!");
#endif
                // 코인
            }

            BoxCollider[] buttons = buttnroot.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            iTween.ScaleTo(closepopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));            
        }
        else if (index == 301)
        {
#if UNITY_ANDROID
            AndroidManager.Instance.callPostToWall("Real-time Network War Game!\nThreatening and aggressive Bingo!\nWin the victory with your friends in the fiery contest through Bingo Monster!");
#endif

            GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_startmode");
            BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            iTween.ScaleTo(closepopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        }

        GlobalData.g_global.dailyIndex = index;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyCheckIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDailyCheckRequest);


    }
}
