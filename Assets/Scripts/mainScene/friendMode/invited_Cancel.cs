using UnityEngine;
using System.Collections;

public class invited_Cancel : MonoBehaviour {
    public Transform vsFriend;
    public Transform detailFriend;
    public Transform invitePanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");

        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }

        GlobalData.g_global.invite_able = true;
        GlobalData.g_global.selectInvite = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteAgreeRequest;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInviteAgreeRequest);
        
        iTween.ScaleTo(vsFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        iTween.ScaleTo(detailFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        iTween.ScaleTo(invitePanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
    }
}
