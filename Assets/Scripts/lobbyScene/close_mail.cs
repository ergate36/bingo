using UnityEngine;
using System.Collections;

public class close_mail : MonoBehaviour {

	// Use this for initialization
    private GameObject popup;
    private Transform popupup;
    private GameObject lobbyUI;
    private BoxCollider[] buttons;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mMyGameInfoIngComplete){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            ItemLayer_Ctrl.ictrl.SetItem();
            GlobalData.g_global.serverFlag = 2;
            popup = GameObject.Find("lobbySceneUI/Camera/Anchor") as GameObject;

            popupup = popup.transform.Find("popup_mail");
            //  popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));



            lobbyUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item");
            buttons = lobbyUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }

            lobbyUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase");
            buttons = lobbyUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }
    }
    void OnClick()
    {

        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMyGameInfoIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMyGameInfoRequest);
    }
}
