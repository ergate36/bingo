using UnityEngine;
using System.Collections;

public class RankCtrl_battle : MonoBehaviour {

    Transform info_front;
    Transform info_back;
    Transform info_back2;
    Transform label_rank;
    Transform label_name;
    Transform spr_rank;
    Transform fb;
    Transform win;
    Transform lose;

    Transform ticket;
    Transform ticket_time;
    Transform ticket_root;
    Transform profile;
    //back


    bool m_isFront = true;

    [HideInInspector]
    public bool m_isMyInfo = false;

    //  [HideInInspector]
    //  public RankInfo rankInfo;

    void Awake()
    {
    }

    void Start()
    {

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

    public void setRankInfo(BattleRankInfo info)
    {


        info_front = transform.Find("info_front");
        info_back = transform.Find("Background");
        label_rank = transform.Find("info_front/label_rank");
        spr_rank = transform.Find("info_front/spr_rank");
        fb = transform.Find("info_front/fb");

        label_name = transform.Find("info_front/label_name");
        win = transform.Find("info_front/win");
        lose = transform.Find("info_front/lose");
        profile = transform.Find("info_front/Texture");
        StartCoroutine(GetWebImage(info.userKey));

        fb.gameObject.SetActive(false);
        for (int i = 0; i < GlobalData.g_global.myFriendList.Count; i++ )
        {
            if (GlobalData.g_global.myFriendList[i].FriendKey == info.userKey)
            {
                fb.gameObject.SetActive(true);
                break;
            }
        }

        int rank = info.rank;

        // front
        label_rank.GetComponent<UILabel>().text = info.rank.ToString();
        label_name.GetComponent<UILabel>().text = info.nickname;
        win.GetComponent<UILabel>().text = info.win.ToString();
        lose.GetComponent<UILabel>().text = info.lose.ToString();

        m_isFront = false;

        if (rank <= 3 && rank != 0)
        {
            string spritename = rank.ToString();
            spr_rank.GetComponent<UISprite>().spriteName = spritename;
            spr_rank.GetComponent<UISprite>().spriteName = spritename;

            label_rank.gameObject.SetActive(false);

        }
        else if (rank == 0)
        {
            label_rank.gameObject.SetActive(true);
            spr_rank.gameObject.SetActive(false);
        }
        else
        {
            spr_rank.gameObject.SetActive(false);
        }

    }


}
