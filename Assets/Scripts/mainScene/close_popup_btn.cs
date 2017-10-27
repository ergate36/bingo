using UnityEngine;
using System.Collections;

public class close_popup_btn : MonoBehaviour {

    private GameObject popup;
    private Transform popupup;
    public GameObject mainSceneUI;
    public GameObject mainSceneUI2;

    private BoxCollider[] buttons;
    public GameObject root;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMyGameInfoIngComplete && GlobalData.g_global.connect_index ==33)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            ItemLayer_Ctrl.ictrl.SetItem();
            //Socket_Ctrl.sCtrl.closeSocket();
        }
	}

    void OnClick()
    {
        GlobalData.g_global.detail_index = 0;
        GlobalData.g_global.event_flag = 0;       

        iTween.ScaleTo(root.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

        if(mainSceneUI2 != null){
            buttons = mainSceneUI2.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }


       // Socket_Ctrl.sCtrl.closeSocket();

        if (root != null)
        {
            GlobalData.g_global.invite_able = true;

            GlobalData.g_global.serverFlag = 1;
            GlobalData.g_global.connect_index = 33;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMyGameInfoIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMyGameInfoRequest);

        }
    }
}
