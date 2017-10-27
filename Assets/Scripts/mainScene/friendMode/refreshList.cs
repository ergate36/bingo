using UnityEngine;
using System.Collections;

public class refreshList : MonoBehaviour {
    public UIGrid grid;
    public UIScrollView sv;
    public UIPanel panel;
    public Transform gridRoot;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mFriendConnectInfoComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            GameObject root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_vsfriend/friendList/Grid");

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
        GlobalData.g_global.invite_able = false;

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendConnectInfoIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendConnectInfoRequest);
    }

}
