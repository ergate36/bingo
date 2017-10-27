using UnityEngine;
using System.Collections;

public class vsFriend_btn : MonoBehaviour {

    public Transform startpopup;
    public UIGrid grid;
    public UIScrollView sv;
    public UIPanel panel;
    public Transform gridRoot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mFriendConnectInfoComplete){

            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            GameObject root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_vsfriend/friendList/Grid");

            GlobalData.g_global.myFriendList.Sort((temp1, temp2) => temp1.flag.CompareTo(temp2.flag));
            GlobalData.g_global.myFriendList.Reverse();

            foreach (Transform mail in root2.transform)
            {
                Destroy(mail.gameObject);
            }

            for (int i = 0; i < GlobalData.g_global.myFriendList.Count; i++)
            {
                friendData fd = new friendData();
                fd.FriendId = GlobalData.g_global.myFriendList[i].FriendId;
                fd.FriendName = GlobalData.g_global.myFriendList[i].FriendName;
                fd.FriendKey = GlobalData.g_global.myFriendList[i].FriendKey;
                fd.flag = GlobalData.g_global.myFriendList[i].flag;
                setRankList(fd);
            }

            grid.Reposition();
            sv.ResetPosition();
            panel.Refresh();
            grid.repositionNow = true;

        }
	}

    GameObject setRankList(friendData info)
    {
        GameObject rankObject = Instantiate(Resources.Load("common/friend_info")) as GameObject;

        rankObject.GetComponent<friendRankCtrl>().setRankInfo(info);
        rankObject.transform.Find("info_front/detail").GetComponent<detail_btn>().setRankInfo(info);
        rankObject.transform.Find("info_front/vs").GetComponent<VS_btn>().setRankInfo(info);

        Vector3 scale = rankObject.transform.localScale;
        rankObject.transform.parent = gridRoot.transform;
        rankObject.transform.localPosition = Vector3.zero;
        rankObject.transform.localScale = scale;

        rankObject.GetComponent<UIDragScrollView>().scrollView = sv;

        return rankObject;
    }


    void OnClick()
    {
        GlobalData.g_global.detail_index = 1;
        if(startpopup != null){
            iTween.ScaleTo(startpopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        }
        GlobalData.g_global.invite_able = false;

        GameObject popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        Transform popup_ctrl = popup.transform.Find("popup_vsfriend");
        popup_ctrl.gameObject.SetActive(true);

        popup_ctrl.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popup_ctrl.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popup_ctrl.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup_ctrl.gameObject));


        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendConnectInfoIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendConnectInfoRequest);

        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
    }
}
