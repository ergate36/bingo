using UnityEngine;
using System.Collections;

public class ResultPopupCtrl : MonoBehaviour
{

    // to show at Inspector
    public Transform layer_black;
    public Transform layer_Result;
    public Transform layer_ranking;
    public Transform layer_batting;

    public Transform openFace;
    public Transform btnBase;


    private Transform rank;

    private Transform[] label_rankPlayers;
    private Transform label_gold;
    private Transform label_rankscore;

    private Transform label_ticket;
    private Transform label_monster;
    private Transform label_goldticket;
    private Transform label_gamescore;
    private Transform label_rankPoint;
    private Transform label_total;
    private Transform label_rank_gold;
    private Transform spr_best;

    private Transform btn_goToReward;

    GameObject sound_result;

    private Transform popup_bonus;

    bool[] myRankInfo;
    int myRankCount;

    struct GameResultData
    {
        public string[] rankPlayers;

        public int gold;
        public int daub;
        public int other;
        public int total;
        public int ticket;
        public int goldticket;
        public int bingoPoint;
        public int rankpoint;
        public int bonusGold;
    };

    private GameResultData resultData;

    void Awake()
    {
        popup_bonus = transform.Find("layer_bonus");
        myRankInfo = new bool[3];

        sound_result = GameObject.Find("Sound_Result");

        layer_Result.gameObject.SetActive(false);
        layer_ranking.gameObject.SetActive(false);
        layer_batting.gameObject.SetActive(false);

        resultData = new GameResultData();
        resultData.rankPlayers = new string[3];

        btn_goToReward = layer_Result.Find("gotoReward_btn");


        label_gamescore = layer_Result.Find("label_attrib/gamescore");
        label_monster = layer_Result.Find("label_attrib/monster");
        label_rankscore = layer_Result.Find("label_attrib/rankscore");
        label_rank_gold = layer_Result.Find("label_attrib/rank_gold");
        label_total = layer_Result.Find("label_attrib/total");
        label_gold = layer_Result.Find("reward_gold/amount");
        label_ticket = layer_Result.Find("reward_ticket/amount");
        label_goldticket = layer_Result.Find("reward_goldticket/amount");
        spr_best = layer_Result.Find("best_spr");

        spr_best.gameObject.SetActive(false);
        label_gamescore.gameObject.SetActive(false);
        label_monster.gameObject.SetActive(false);
        label_rankscore.gameObject.SetActive(false);
        label_total.gameObject.SetActive(false);
        btn_goToReward.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mScoreUpdateIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
        }
        
    }


    public IEnumerator initResut()
    {
        StartCoroutine(sendResult());
        yield return null;
    }
    void SaveReviewData(int flag)
    {
        PlayerPrefs.SetInt("Review", flag);
    }
    int GetReviewData()
    {
        return PlayerPrefs.GetInt("Review");
    }
    private IEnumerator sendResult()
    {
        if(GetReviewData() == 0){
            SaveReviewData(10);
        }
        spr_best.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);

        Transform soundObj = transform.Find("sound");
        soundObj.GetComponent<AudioSource>().PlayOneShot(soundObj.GetComponent<AudioSource>().clip);


        int monsterScore = 0;
        int totalScore = 0;
        int rankScore = 0;
        int gameScore = 0;
        
        int gameGold = 0;
        int rankGold = 0;
        int monsterCoin = 0;
        int totalGold = 0;
        
        gameGold = GlobalData.g_global.myInfo.game_gold;
        gameScore = GlobalData.g_global.myInfo.GameScore;

        rankScore =XmlCtrl.x_xml.Rxml[GlobalData.g_global.myrank].Bscore;
        rankGold = XmlCtrl.x_xml.Rxml[GlobalData.g_global.myrank].Bgold;

        int goldFlag = 0;
         // 착용 몬스터 카드에따른 점수 , 골드 

        for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++ )
        {
            if (GlobalData.g_global.myCardList[i].cardused != 0 && GlobalData.g_global.myCardList[i].cardused < 5)
            {
                for (int k = 0; k < 3; k++ )
                {
                    int taggg = 0;
                    double addValue = 0;
                    int cardid = 0;
                    if(k==0){
                        taggg = GlobalData.g_global.myCardList[i].AT1;
                        cardid = GlobalData.g_global.myCardList[i].cardId;
                        addValue = (int)GlobalData.g_global.myCardList[i].ATv1 + ((int)XmlCtrl.x_xml.MCxml[cardid].AT1Add * (GlobalData.g_global.myCardList[i].cardlevel - 1));
                    }
                    else if (k == 1)
                    {
                        taggg = GlobalData.g_global.myCardList[i].AT2;
                        cardid = GlobalData.g_global.myCardList[i].cardId;
                        addValue = (int)GlobalData.g_global.myCardList[i].ATv2 + ((int)XmlCtrl.x_xml.MCxml[cardid].AT2Add * (GlobalData.g_global.myCardList[i].cardlevel - 1));

                    }
                    else
                    {
                        taggg = GlobalData.g_global.myCardList[i].AT3;
                        cardid = GlobalData.g_global.myCardList[i].cardId;
                        addValue = (int)GlobalData.g_global.myCardList[i].ATv3 + ((int)XmlCtrl.x_xml.MCxml[cardid].AT3Add * (GlobalData.g_global.myCardList[i].cardlevel - 1));
                    }

                    switch (taggg)
                    {
                        case 14:
                            rankScore = XmlCtrl.x_xml.Rxml[GlobalData.g_global.myrank].Bscore + (int)(XmlCtrl.x_xml.Rxml[GlobalData.g_global.myrank].Bscore * (addValue/100));
                            break;
                        case 15:
                            gameScore = gameScore + (int)(gameScore * addValue/100);
                            break;
                        case 16:
                            monsterScore = monsterScore + Random.Range((int)addValue / 2, (int)addValue); 
                        //랜덤 값 추가 
                            break;
                        case 17:
                            monsterScore = monsterScore + Random.Range((int)addValue / 10, (int)addValue);   
                        // 랜덤 값 추가 
                            break;
                        case 18:
                            monsterScore += (int)(monsterScore * (addValue / 100));
                            break;
                        case 19:
                            rankGold = (int)(rankGold * (addValue / 100));
                            goldFlag = 1;
                            break;
                        case 20:
                            gameGold = (int)(gameGold * (addValue / 100) + gameGold);
                            break;
                        case 21:
                            monsterCoin += Random.Range((int)addValue / 2, (int)addValue);
                            // 랜덤 골드 획득   // 몬스터 골드
                            break;
                        case 22:
                            monsterCoin += Random.Range((int)addValue / 10, (int)addValue);
                            // 랜덤 골드 획득  // 몬스터 골드
                            break;
                        case 23:
                            monsterCoin += monsterCoin + monsterCoin * ((int)addValue / 100);
                            break;
                    }
                }
            }
        }
        
        if(goldFlag == 0){
            rankGold = 0;
        }

        totalScore = monsterScore + rankScore + gameScore;
        totalGold = monsterCoin + rankGold + gameGold;

        layer_black.gameObject.SetActive(true);
        layer_Result.gameObject.SetActive(true);
        iTween.MoveTo(layer_Result.gameObject, iTween.Hash("x", 0f, "easeType", "easeOutElastic", "time", 0.6f));

        label_gamescore.gameObject.SetActive(true);
        label_gamescore.GetComponent<UILabel>().text = gameScore.ToString();
        iTween.PunchScale(label_gamescore.gameObject, Vector3.one, 0.5f);
        //yield return new WaitForSeconds(0.5f);

        label_rankscore.gameObject.SetActive(true);
        label_rankscore.GetComponent<UILabel>().text = rankScore.ToString();
        iTween.PunchScale(label_rankscore.gameObject, Vector3.one, 0.5f);

        label_rank_gold.GetComponent<UILabel>().text = "rank+" + rankGold.ToString();
        iTween.PunchScale(label_rank_gold.gameObject, Vector3.one, 0.5f);

        //label_rank.GetComponent<UILabel>().text = GlobalData.g_global.myrank.ToString() + "st";

        label_gold.GetComponent<UILabel>().text = "+" + (gameGold + rankGold.ToString()).ToString();

        //yield return new WaitForSeconds(0.5f);

        label_monster.gameObject.SetActive(true);
        
        label_monster.GetComponent<UILabel>().text = monsterScore.ToString();
        iTween.PunchScale(label_monster.gameObject, Vector3.one, 0.5f);

        label_rank_gold.GetComponent<UILabel>().text = "monster+" + monsterCoin;
        iTween.PunchScale(label_rank_gold.gameObject, Vector3.one, 0.5f);
        label_gold.GetComponent<UILabel>().text = "+" + totalGold.ToString();

        //yield return new WaitForSeconds(0.5f);
        label_rank_gold.gameObject.SetActive(false);

        label_total.gameObject.SetActive(true);
        label_total.GetComponent<UILabel>().text = totalScore.ToString();
        iTween.PunchScale(label_total.gameObject, Vector3.one, 0.5f);

        label_ticket.GetComponent<UILabel>().text = "+" + GlobalData.g_global.myInfo.game_ticket.ToString();
        
        int bestScore = 0;
        
        bestScore = GlobalData.g_global.myInfo.week_bestScore;


        if (bestScore < totalScore)
        {
            yield return new WaitForSeconds(0.5f);
            spr_best.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(0.5f);
        btn_goToReward.gameObject.SetActive(true);


        if (GlobalData.g_global.win == 0)
        {
            openFace.gameObject.SetActive(true);
            openFace.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(openFace.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(openFace.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", openFace.gameObject));

            BoxCollider[] buttons = btnBase.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }

            buttons = openFace.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }



        GlobalData.g_global.myInfo.GameScore = totalScore;
        GlobalData.g_global.myInfo.game_gold = totalGold;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mScoreUpdateIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mScoreUpdateRequest);
    }
}
