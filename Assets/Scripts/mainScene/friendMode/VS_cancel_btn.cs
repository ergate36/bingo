using UnityEngine;
using System.Collections;

public class VS_cancel_btn : MonoBehaviour {
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
        GlobalData.g_global.invite_able = true;
        GameObject root = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        BoxCollider[] buttons = root.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        GlobalData.g_global.selectFriendId = 0;
        iTween.ScaleTo(vsFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        iTween.ScaleTo(detailFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        iTween.ScaleTo(invitePanel.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
    }
}
