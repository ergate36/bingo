using UnityEngine;
using System.Collections;

public class detail_btn : MonoBehaviour {

    public Transform vsFriend;
    public Transform detailFriend;
    public GameObject popup;
    public int FriendId;
    public string FriendName;
    public string FriendKey;
    public int flag;
    private Transform profile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(GlobalData.g_global.socketState==(int)SocketClass.STATE.mFriendInfoComplete && GlobalData.g_global.detail_index ==1){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
            detailFriend = popup.transform.Find("popup_frienddetail");

            detailFriend.gameObject.SetActive(true);
            detailFriend.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(detailFriend.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(detailFriend.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", detailFriend.gameObject));

            Transform[] card = new Transform[4];
            Transform name;
            Transform coinPoint;
            Transform scorePoint;
            Transform coinRank;
            Transform scoreRank;
            Transform battleRank;
            Transform win;
            Transform lose;

            profile = detailFriend.transform.Find("profile");

            StartCoroutine(GetWebImage(GlobalData.g_global.selectFriendKey));

            for (int i = 0; i < 4; i++)
            {
                card[i] = detailFriend.transform.Find("list_MonsterCard" + (i + 1));
                if (GlobalData.g_global.f_Info.slot[i] != 0)
                {
                    card[i].transform.Find("monster").GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.f_Info.slot[i] - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.f_Info.slot[i] - 1].Rare;
                    card[i].transform.Find("name").GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.f_Info.slot[i] - 1].Name + "name";
                    card[i].transform.Find("rare").GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.f_Info.slot[i] - 1].Rare;
                    card[i].transform.Find("star").GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.f_Info.slot[i] - 1].Rank;
                }
                else
                {
                    card[i].transform.Find("monster").GetComponent<UISprite>().spriteName = "";
                    card[i].transform.Find("name").GetComponent<UISprite>().spriteName = "";
                    card[i].transform.Find("rare").GetComponent<UISprite>().spriteName = "nocard";
                    card[i].transform.Find("star").GetComponent<UISprite>().spriteName = "";
                }
            }

            name = detailFriend.transform.Find("name");
            coinPoint = detailFriend.transform.Find("coinPoint");
            coinRank = detailFriend.transform.Find("coinRank");
            scorePoint = detailFriend.transform.Find("scorePoint");
            scoreRank = detailFriend.transform.Find("scoreRank");

            battleRank = detailFriend.transform.Find("battlerank");
            win = detailFriend.transform.Find("win");
            lose = detailFriend.transform.Find("lose");


            name.GetComponent<UILabel>().text = "ID : " + GlobalData.g_global.selectFriendName;
            coinPoint.GetComponent<UILabel>().text = "coin: "+GlobalData.g_global.f_Info.coin.ToString();
            scorePoint.GetComponent<UILabel>().text = "score: " + GlobalData.g_global.f_Info.score.ToString();
            coinRank.GetComponent<UILabel>().text = "rank: "+GlobalData.g_global.f_Info.coin_rank.ToString();
            scoreRank.GetComponent<UILabel>().text = "rank: "+GlobalData.g_global.f_Info.score_rank.ToString();

            battleRank.GetComponent<UILabel>().text = "rank: " + GlobalData.g_global.f_Info.battle_rank.ToString();
            win.GetComponent<UILabel>().text = "win: " + GlobalData.g_global.f_Info.win.ToString();
            lose.GetComponent<UILabel>().text = "lose: " + GlobalData.g_global.f_Info.lose.ToString();

        }
	}
    void OnClick()
    {
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        vsFriend = popup.transform.Find("popup_vsfriend");
        iTween.ScaleTo(vsFriend.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        
        /*
        BoxCollider[] buttons = vsFriend.GetComponentsInChildren<BoxCollider>();

        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
        */

        GlobalData.g_global.selectFriendKey = FriendKey;
        GlobalData.g_global.selectFriendName = FriendName;

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInfoIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mFriendInfoRequest);
    }

    public IEnumerator GetWebImage(string friendKey)
    {
        WWW www = new WWW("https://graph.facebook.com/" + friendKey + "/picture");
        yield return www;
        if (www.error == null)
        {
            profile.GetComponent<UITexture>().mainTexture = www.texture;
        }
    }

    public void setRankInfo(friendData fd)
    {
        FriendId = fd.FriendId;
        FriendName=fd.FriendName;
        FriendKey=fd.FriendKey;
        flag=fd.flag;
    }

}
