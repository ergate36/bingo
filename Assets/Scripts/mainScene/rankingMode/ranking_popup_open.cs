using UnityEngine;
using System.Collections;

public class ranking_popup_open : MonoBehaviour {

    private GameObject popup;
    private Transform popupup;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    private RankInfo myInfo;


    public UIGrid grid;
    public UIScrollView sv;
    public UIPanel panel;
    public Transform gridRoot;

    public UIGrid grid2;
    public UIScrollView sv2;
    public UIPanel panel2;
    public Transform gridRoot2;


    public UIGrid grid3;
    public UIScrollView sv3;
    public UIPanel panel3;
    public Transform gridRoot3;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(GlobalData.g_global.socketState ==(int)SocketClass.STATE.mRankingComplete && GlobalData.g_global.rank_state == 0){

            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
            Transform popup2 = pn.transform.Find("worldRanking");
            popup2.gameObject.SetActive(true);

            makeRanking_score();
           
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mRankingComplete && GlobalData.g_global.rank_state == 1)
        {
            GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
            Transform popup2 = pn.transform.Find("coinRanking");
            popup2.gameObject.SetActive(true);

            makeRanking_coin();
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mRankingComplete && GlobalData.g_global.rank_state == 2)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
            Transform popup2 = pn.transform.Find("battleRanking");
            popup2.gameObject.SetActive(true);
            popup2 = pn.transform.Find("rankList");
            popup2.gameObject.SetActive(true);
            makeRanking_battle();
            mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
	}

    private void setImage()
    {
        GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/battleranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "battle_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "battle_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "battle_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "battle_2";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/coinranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "coin_2";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/worldranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "score_1";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "score_1";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "score_1";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "score_1";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/rewardranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "reward_2";

    }


    void OnClick()
    {
        GlobalData.g_global.invite_able = false;
        GlobalData.g_global.detail_index = 2;
        GlobalData.g_global.rank_state = 0;

        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_ranking");
        popupup.gameObject.SetActive(true);
        popupup.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
        
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_loading");
        popupup.gameObject.SetActive(true);
        GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
        Transform popup2 = pn.transform.Find("worldRanking");
        popup2.gameObject.SetActive(true);


        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mRankingRequest;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mRankingRequest);
    }

    private void makeRanking_score()
    {

        GameObject root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/worldRanking/Grid");
        foreach (Transform mail in root2.transform)
        {
            Destroy(mail.gameObject);
        }


        for (int i = 0; i < GlobalData.g_global.r_rankInfo.Count; i++)
        {
            myInfo = new RankInfo();
            myInfo.nickname = GlobalData.g_global.r_rankInfo[i].nickname;
            myInfo.rank = GlobalData.g_global.r_rankInfo[i].rank;
            myInfo.weekly_best = GlobalData.g_global.r_rankInfo[i].weekly_best;
            myInfo.userKey = GlobalData.g_global.r_rankInfo[i].userKey;
            if (i == 0)
            {
                setRankList_score(myInfo, true);
            }
            else
            {
                setRankList_score(myInfo, false);
            }
        }

        grid.Reposition();
        sv.ResetPosition();
        panel.Refresh();
        grid.repositionNow = true;

        GlobalData.g_global.rank_state = 1;

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mRankingRequest;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mRankingRequest);


    }

    GameObject setRankList_score(RankInfo info, bool myInfo)
    {

        if (myInfo)
        {
            GameObject rankObject = Instantiate(Resources.Load("common/personal_me_rank")) as GameObject;

            rankObject.GetComponent<RankCtrl>().m_isMyInfo = true;
            rankObject.GetComponent<RankCtrl>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey,info.nickname);
            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv;

            return rankObject;

        }
        else
        {
            GameObject rankObject = Instantiate(Resources.Load("common/personal_rank")) as GameObject;
            rankObject.GetComponent<RankCtrl>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey, info.nickname);

            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv;

            return rankObject;
        }
    }


    private void makeRanking_battle()
    {
        GameObject root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/battleRanking/Grid");
        foreach (Transform mail in root2.transform)
        {
            Destroy(mail.gameObject);
        }


        for (int i = 0; i < GlobalData.g_global.b_rankInfo.Count; i++)
        {
            BattleRankInfo b_myInfo = new BattleRankInfo();

            b_myInfo.nickname = GlobalData.g_global.b_rankInfo[i].nickname;
            b_myInfo.rank = GlobalData.g_global.b_rankInfo[i].rank;
            b_myInfo.win = GlobalData.g_global.b_rankInfo[i].win;
            b_myInfo.lose = GlobalData.g_global.b_rankInfo[i].lose;
            b_myInfo.userKey = GlobalData.g_global.b_rankInfo[i].userKey;
            if (i == 0)
            {
                //b_myInfo.rank++;
                setRankList_battle(b_myInfo, true);
            }
            else
            {
                setRankList_battle(b_myInfo, false);
            }
        }
        grid3.Reposition();
        sv3.ResetPosition();
        panel3.Refresh();
        grid3.repositionNow = true;


        StartCoroutine(waitTime());
    }
    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(3f);
        
        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = true;
        }
        GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
        Transform popup2 = pn.transform.Find("coinRanking");
        popup2.gameObject.SetActive(false);

        popup2 = pn.transform.Find("battleRanking");
        popup2.gameObject.SetActive(false);

        popup2 = pn.transform.Find("worldRanking");
        popup2.gameObject.SetActive(true);
        popup2 = pn.transform.Find("rankList");
        popup2.gameObject.SetActive(false);


        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_loading");
        popupup.gameObject.SetActive(false);


        setImage();

    }
    GameObject setRankList_battle(BattleRankInfo info, bool myInfo)
    {
        if (myInfo == true)
        {
            GameObject rankObject = Instantiate(Resources.Load("common/battle_me_rank")) as GameObject;
            rankObject.GetComponent<RankCtrl_battle>().m_isMyInfo = true;
            rankObject.GetComponent<RankCtrl_battle>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey, info.nickname);

            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot3.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv3;

            return rankObject;
        }
        else
        {
            GameObject rankObject = Instantiate(Resources.Load("common/battle_rank")) as GameObject;
            rankObject.GetComponent<RankCtrl_battle>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey, info.nickname);

            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot3.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv3;

            return rankObject;
        }
    }


    private void makeRanking_coin()
    {
        GameObject root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/coinRanking/Grid");
        foreach (Transform mail in root2.transform)
        {
            Destroy(mail.gameObject);
        }


        for (int i = 0; i < GlobalData.g_global.c_rankInfo.Count; i++)
        {
            CoinRankInfo c_myInfo = new CoinRankInfo();

            c_myInfo.nickname = GlobalData.g_global.c_rankInfo[i].nickname;
            c_myInfo.rank = GlobalData.g_global.c_rankInfo[i].rank;
            c_myInfo.weekly_best = GlobalData.g_global.c_rankInfo[i].weekly_best;
            c_myInfo.userKey = GlobalData.g_global.c_rankInfo[i].userKey;
            if (i == 0)
            {
                //b_myInfo.rank++;
                setRankList_coin(c_myInfo, true);
            }
            else
            {
                setRankList_coin(c_myInfo, false);
            }
        }
        grid2.Reposition();
        sv2.ResetPosition();
        panel2.Refresh();
        grid2.repositionNow = true;

        GlobalData.g_global.rank_state = 2;

        GlobalData.g_global.socketState = (int)SocketClass.STATE.mRankingRequest;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mRankingRequest);

    }

    GameObject setRankList_coin(CoinRankInfo info, bool myInfo)
    {
        if (myInfo == true)
        {
            GameObject rankObject = Instantiate(Resources.Load("common/coin_me_rank")) as GameObject;
            rankObject.GetComponent<RankCtrl_coin>().m_isMyInfo = true;
            rankObject.GetComponent<RankCtrl_coin>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey, info.nickname);

            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot2.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv2;

            return rankObject;
        }
        else
        {
            GameObject rankObject = Instantiate(Resources.Load("common/coin_rank")) as GameObject;
            rankObject.GetComponent<RankCtrl_coin>().setRankInfo(info);
            rankObject.transform.Find("info_front/detail_btn").GetComponent<detail_Btn>().setRankInfo(info.userKey, info.nickname);

            Vector3 scale = rankObject.transform.localScale;
            rankObject.transform.parent = gridRoot2.transform;
            rankObject.transform.localPosition = Vector3.zero;
            rankObject.transform.localScale = scale;

            rankObject.GetComponent<UIDragScrollView>().scrollView = sv2;

            return rankObject;
        }
    }










}
