using UnityEngine;
using System.Collections;

public class VS_btn : MonoBehaviour {
    public Transform vsFriend;
    public Transform detailFriend;
    public Transform invitePanel;
    public GameObject popup;

    public int FriendId;
    public string FriendName;
    public string FriendKey;
    public int flag;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {

        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        vsFriend = popup.transform.Find("popup_vsfriend");
        detailFriend = popup.transform.Find("popup_frienddetail");
        invitePanel = popup.transform.Find("popup_invite");

        GlobalData.g_global.selectFriendId = FriendId;

        // popupopen
        iTween.ScaleTo(vsFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        iTween.ScaleTo(detailFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        invitePanel.gameObject.SetActive(true);
        invitePanel.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(invitePanel.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(invitePanel.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", invitePanel.gameObject));
        invitePanel.Find("Label").GetComponent<UILabel>().text = "do you want to\n invite " + FriendName + " ?";

        for (int i = 1; i < 5; i++)
        {
            GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_invite/Sprite" + i);
            check.transform.GetComponent<UISprite>().spriteName = "";
            if( i== 1){
                check.transform.GetComponent<UISprite>().spriteName = "check_big";
                GlobalData.g_global.selectSheet = 1;
            }
        }
    }

    public void setRankInfo(friendData fd)
    {
        FriendId = fd.FriendId;
        FriendName = fd.FriendName;
        FriendKey = fd.FriendKey;
        flag = fd.flag;

    }

}