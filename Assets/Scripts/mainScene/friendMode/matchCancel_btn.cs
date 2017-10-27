using UnityEngine;
using System.Collections;

public class matchCancel_btn : MonoBehaviour {
    public Transform waitPopup;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mFriendInviteCancelComplete){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            iTween.ScaleTo(waitPopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        }
	}

    void OnClick()
    {
        GlobalData.g_global.invite_able = true;
        GameObject root = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        BoxCollider[] buttons = root.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteCancelIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteCancelRequest);
    }

}
