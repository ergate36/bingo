using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using MarigoldModel.Model;

public class nb_minigame : MonoBehaviour
{
    public Transform gameMoneyGroup;


    [HideInInspector]
    public Transform[] card;

    [HideInInspector]
    public bool bShuffle = false;

    private Transform betParent;

    [HideInInspector]
    public int betIndex = 0;

    [HideInInspector]
    public bool bCardOpen = false;

    private long gamblePriceId = 0;
    private long defaultAssetId = 0;
    private int defaultPrice = 0;

    [HideInInspector]
    public List<MiniGamblePriceSet> priceSetList;
    [HideInInspector]
    public int selectPriceIndex = 0;

    private List<long> groupIdList;
    private List<MiniGambleGroupSet> groupSetList;
    private long selectGroupIndex = 0;

    private int selectCardIndex = 0;

    private nb_userMoney beforeMoney;

    private float shuffleTime = 0;
    private int[] shuffleIndex = { 0, 1, 2, 3, 4 };

    void Awake()
    {
        card = new Transform[5];
        for (int i = 0; i < 5; ++i)
        {
            card[i] = this.transform.Find("card" + (i + 1).ToString());
        }

        betParent = this.transform.Find("i_betbar");

        priceSetList = new List<MiniGamblePriceSet>();
        groupIdList = new List<long>();
        groupSetList = new List<MiniGambleGroupSet>();

        beforeMoney.id = 0;
        beforeMoney.value = 0;
    }
    
    void Start()
    {
        setNewGamblePrice();
        if (gamblePriceId == 0) return; //미니게임 없는 스테이지

        setNewGambleGroup();
        drawSelectPrice();
    }

    void Update()
    {        
        if (nbHttp.state == nbHttp.nbHttpState.PlayMiniGambleSuccess)
        {
            nbHttp.state = nbHttp.nbHttpState.Wait;

            Debug.Log("StartCoroutine(miniGameCardOpen)");
            StartCoroutine(miniGameCardOpen(selectCardIndex));
        }

        if (shuffleTime > 0)
        {
            float shuffleAfter = Time.time - shuffleTime;
            if (shuffleAfter > 3.0f)
            {
                //3초동안 안누르면 아무거나 선택됨
                bCardOpen = true;
                runOpenCard(Random.Range(0, 5));
            }
        }
    }

    public bool runShuffleCard()
    {
        //재화 체크
        int cost = priceSetList[selectPriceIndex].Multiplier * defaultPrice;

        if (nb_GlobalData.g_global.util.getGameMoney((GameMoneyId)defaultAssetId) < cost)
        {
            Debug.Log("enough gamemoney id : " + defaultAssetId);
            return false;
        }

        if (nb_GlobalData.g_global.miniGameState != MiniGameState.TEST)
        {
            nb_GlobalData.g_global.miniGameState = MiniGameState.ANIMATE;
        }

        betParent.gameObject.SetActive(false);

        beforeMoney.id = (GameMoneyId)defaultAssetId;
        beforeMoney.value = nb_GlobalData.g_global.util.getGameMoney(beforeMoney.id);

        nb_GlobalData.g_global.util.useGameMoney(
            (GameMoneyId)defaultAssetId, cost);

        Debug.Log("shuffle card // use cost : " + cost);
        redrawGameMoney();

        for (int i = 0; i < 5; ++i)
        {
            StartCoroutine(miniGameEffectWait(i));
        }

        return true;
    }

    private IEnumerator miniGameEffectWait(int cardIndex)
    {
        Vector3 center = card[2].position;

        if (card[cardIndex] != null)
        {
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_close";

            yield return new WaitForSeconds(0.8f);

            iTween.MoveTo(card[cardIndex].Find("spine").gameObject, center, 0.2f);

            yield return new WaitForSeconds(0.8f);

            iTween.MoveTo(card[cardIndex].Find("spine").gameObject, card[cardIndex].position, 0.2f);

            yield return new WaitForSeconds(0.2f);

            card[cardIndex].Find("i_card").gameObject.SetActive(true);
            card[cardIndex].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card1";
            card[cardIndex].Find("i_card/icon").gameObject.SetActive(false);
            card[cardIndex].Find("i_card/value").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(false);

            card[cardIndex].Find("spine2").gameObject.SetActive(true);

            bShuffle = true;
            shuffleTime = Time.time;
        }

        yield return null;
    }

    public void runOpenCard(int cardIndex)
    {
        shuffleTime = 0;
        selectCardIndex = cardIndex;

        //통신 끝날때까지 화살표 먼저 지움
        for (int i = 0; i < 5; ++i)
        {
            if (cardIndex == i) continue;
            card[i].Find("spine2").gameObject.SetActive(false);
        }

        sendMiniGamblePlay();
    }

    private IEnumerator miniGameCardOpen(int cardIndex)
    {
        if (card[cardIndex] != null)
        {
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_open";
            yield return new WaitForSeconds(0.2f);

            card[cardIndex].Find("i_card").gameObject.SetActive(true);
            card[cardIndex].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card2";
            card[cardIndex].Find("i_card/icon").gameObject.SetActive(true);
            card[cardIndex].Find("i_card/icon").GetComponent<UISprite>().spriteName =
                getAssetIconStr(AssetType.GAME_MONEY, nb_GlobalData.g_global.miniGameRewardId);
            card[cardIndex].Find("i_card/value").gameObject.SetActive(true);
            card[cardIndex].Find("i_card/value").GetComponent<UILabel>().text = 
                nb_GlobalData.g_global.util.GetCommaNumber(
                (int)nb_GlobalData.g_global.miniGameRewardValue);
            card[cardIndex].Find("spine").gameObject.SetActive(false);

            StartCoroutine(miniGameRewardAction(cardIndex));
        }

        yield return null;
    }

    private IEnumerator miniGameRewardAction(int cardIndex)
    {
        //Debug.Log("[minigame]miniGameRewardAction start");
        if (card[cardIndex] != null)
        {
            GameObject pref = Instantiate(Resources.Load("game/minigame_reward")) as GameObject;
            if(pref == null)
            {
                Debug.Log("[minigame]reward pref null");
                yield return null;
            }

            //for (int i = 0; i < 5; ++i)
            //{
            //    card[i].Find("spine2").gameObject.SetActive(false);
            //}
            card[cardIndex].Find("spine2").gameObject.SetActive(false);

            pref.transform.parent = card[cardIndex].Find("effect");
            pref.transform.localPosition = new Vector3(0, 30, 0);
            pref.transform.localScale = Vector3.one;
            pref.transform.Find("icon").GetComponent<UISprite>().spriteName =
                getAssetIconStr(AssetType.GAME_MONEY, nb_GlobalData.g_global.miniGameRewardId);
            pref.transform.Find("value").GetComponent<UILabel>().text = "+" + 
                nb_GlobalData.g_global.util.GetCommaNumber(
                (int)nb_GlobalData.g_global.miniGameRewardValue);

            Vector3 pos = new Vector3(0, 0.24f, 0);
            iTween.MoveBy(pref, pos, 1.0f);

            yield return new WaitForSeconds(2.0f);
            
            redrawGameMoney();

            for (int i = 0; i < card[cardIndex].Find("effect").childCount; ++i)
            {
                //이펙트 삭제
                GameObject.Destroy(card[cardIndex].Find("effect").GetChild(i).gameObject);
            }

            //초기화
            card[cardIndex].Find("i_card").gameObject.SetActive(false);
            card[cardIndex].Find("spine").gameObject.SetActive(true);
            card[cardIndex].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_close";

            yield return new WaitForSeconds(0.8f);

            for (int i = 0; i < 5; ++i)
            {
                card[i].Find("i_card").gameObject.SetActive(false);
                card[i].Find("spine").gameObject.SetActive(true);
                card[i].Find("spine").GetComponent<SkeletonAnimation>().AnimationName = "card_open";
            }

            setNewGambleGroup();

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 5; ++i)
            {
                card[i].Find("i_card").gameObject.SetActive(true);
                card[i].Find("i_card/icon").gameObject.SetActive(true);
                card[i].Find("i_card/value").gameObject.SetActive(true);
                card[i].Find("spine").gameObject.SetActive(false);
                card[i].Find("i_card").GetComponent<UISprite>().spriteName = "ui_minigame_card2";
            }

            bShuffle = false;
            bCardOpen = false;

            betParent.gameObject.SetActive(true);
            this.transform.Find("i_shuffle_btn").gameObject.SetActive(true);

            if (nb_GlobalData.g_global.miniGameState != MiniGameState.TEST)
            {
                nb_GlobalData.g_global.miniGameState = MiniGameState.WAIT;
            }
        }
        yield return null;
    }

    void setNewGamblePrice()
    {
        //미니 게임 id 가져옴
        foreach (var stage in nb_GlobalData.g_global.stageList)
        {
            if (stage.Id == nb_GlobalData.g_global.selectStageId)
            {
                gamblePriceId = stage.MiniGamblePriceId;
                Debug.Log("[minigame]gamblePriceId : " + gamblePriceId);
                break;
            }
        }

        if (gamblePriceId == 0)
        {
            //미니겜블 정보가 음슴
            Debug.Log("[minigame]gamblePriceId null");
            return;
        }

        //기본 가격 정보 가져옴
        foreach (var price in nb_GlobalData.g_global.miniGamblePriceList)
        {
            if (price.Id == gamblePriceId)
            {
                defaultAssetId = price.PriceAssetId;
                defaultPrice = price.PriceAssetCount;
                //Debug.Log("[minigame]defaultAssetId : " + defaultAssetId + " / defaultPrice : " + defaultPrice);
                break;
            }
        }

        //가격 셋 설정
        priceSetList.Clear();
        foreach (var priceSet in nb_GlobalData.g_global.miniGamblePriceSetList)
        {
            if (priceSet.MiniGamblePriceId == gamblePriceId)
            {
                priceSetList.Add(priceSet);
            }
        }
        Debug.Log("priceSetList!!! : " + 
            priceSetList[0].Id + ", " +
            priceSetList[1].Id + ", " +
            priceSetList[2].Id + ", " +
            priceSetList[3].Id + ", " +
            priceSetList[4].Id + "!@#!@#!@#");
        //Debug.Log("[minigame]priceSetList count : " + priceSetList.Count);
    }

    void setNewGambleGroup()
    {        
        //리워드 그룹 정보 가져옴
        groupIdList.Clear();
        foreach (var group in nb_GlobalData.g_global.miniGambleGroupList)
        {
            if (group.MiniGamblePriceId == gamblePriceId)
            {
                groupIdList.Add(group.Id);
            }
        }
        Debug.Log("[minigame]groupIdList count : " + groupIdList.Count);
        
        //무작위로 그룹 선택
        selectGroupIndex = Random.Range(0, groupIdList.Count);
        Debug.Log("[minigame]selectGroupIndex : " + selectGroupIndex);

        //그룹 셋 설정
        groupSetList.Clear();
        int count = 0;
        foreach (var groupSet in nb_GlobalData.g_global.miniGambleGroupSetList)
        {
            if (groupSet.MiniGambleGroupId == groupIdList[(int)selectGroupIndex])
            {
                groupSetList.Add(groupSet);
                ++count;
            }

            if (count == 5) break;
        }
        //Debug.Log("[minigame]groupSetList count : " + groupSetList.Count);


        //선택한 그룹의 리워드 표시
        drawSelectGroup(true);

        //기본 선택 가격 표시
        drawSelectPrice();
    }

    public void drawSelectGroup(bool isShuffle)
    {
        //미니게임 카드 섞음
        if (isShuffle)
        {
            for (int count = 0; count < 10; ++count)
            {
                for (int i = 0; i < 5; ++i)
                {
                    int rand = Random.Range(0, 5);
                    int temp = shuffleIndex[i];
                    shuffleIndex[i] = shuffleIndex[rand];
                    shuffleIndex[rand] = temp;
                }
            }
        }

        for (int i = 1; i <= 5; ++i)
        {
            var icon = this.transform.Find("card" + i.ToString() + "/i_card/icon").
                GetComponent<UISprite>();
            var value = this.transform.Find("card" + i.ToString() + "/i_card/value").
                GetComponent<UILabel>();

            icon.spriteName = getAssetIconStr(
                groupSetList[shuffleIndex[i - 1]].AssetType, groupSetList[shuffleIndex[i - 1]].AssetId);
            icon.MakePixelPerfect();

            //갯수
            value.text = nb_GlobalData.g_global.util.GetCommaNumber(
                groupSetList[shuffleIndex[i - 1]].AssetCount * priceSetList[selectPriceIndex].Multiplier);
        }

    }

    bool isList(int[] list, int value)
    {
        foreach (var member in list)
        {
            if (member == value)
            {
                return true;
            }
        }
        return false;
    }


    public void sendMiniGamblePlay()
    {
        if (priceSetList.Count == 0) return;
        if (groupIdList.Count == 0) return;

        beforeMoney.id = (GameMoneyId)defaultAssetId;
        beforeMoney.value = nb_GlobalData.g_global.util.getGameMoney(beforeMoney.id);

        nbHttp.http.PlayMiniGamble(
            nb_GlobalData.g_global.userSession.SessionKey,
            nb_GlobalData.g_global.selectStageId,
            priceSetList[selectPriceIndex].Id,
            groupIdList[(int)selectGroupIndex]
            );
    }

    public void drawSelectPrice()
    {
        var icon = betParent.Find("i_price_icon").GetComponent<UISprite>();
        var value = betParent.Find("t_price_value").GetComponent<UILabel>();

        icon.spriteName = getAssetIconStr(AssetType.GAME_MONEY, defaultAssetId);
        icon.MakePixelPerfect();
        icon.transform.localScale = new Vector3(0.6f, 0.6f);

        if (priceSetList.Count != 0)
        {
            value.text = nb_GlobalData.g_global.util.GetCommaNumber(
                priceSetList[selectPriceIndex].Multiplier * defaultPrice);
        }

        var up = betParent.Find("i_price_up_btn");
        var down = betParent.Find("i_price_down_btn");

        up.gameObject.SetActive(selectPriceIndex != priceSetList.Count - 1); 
        down.gameObject.SetActive(selectPriceIndex != 0); 
    }

    private string getAssetIconStr(AssetType type, long id)
    {
        if (type == AssetType.GAME_MONEY)
        {
            if (id == 1)
            {
                return "ui_money_gold";
            }
            else if (id == 2)
            {
                return "ui_money_credit";
            }
        }
        else if (type == AssetType.EXPERIENCE)
        {
            return "ui_money_exp";
        }
        else if (type == (AssetType)7)  //todo
        {
            return "ui_item_normal3";
        }

        return "";
    }

    void redrawGameMoney()
    {
        Transform gold = gameMoneyGroup.Find("gold_group/t_value");
        Transform credit = gameMoneyGroup.Find("credit_group/t_value");

        if (beforeMoney.id == GameMoneyId.COIN)
        {
            StartNumberCountMove(
                gold.GetComponent<UILabel>(),
                beforeMoney.value,
                nb_GlobalData.g_global.util.getGameMoney(beforeMoney.id),
                0.5f);
        }
        else if (beforeMoney.id == GameMoneyId.CREDIT)
        {
            StartNumberCountMove(
                credit.GetComponent<UILabel>(),
                beforeMoney.value,
                nb_GlobalData.g_global.util.getGameMoney(beforeMoney.id),
                0.5f);
        }

        beforeMoney.id = 0;
        beforeMoney.value = 0;

        nb_GlobalData.g_global.miniGameRewardType = 0;
        nb_GlobalData.g_global.miniGameRewardId = 0;
        nb_GlobalData.g_global.miniGameRewardValue = 0;
    }

    private void StartNumberCountMove(UILabel text, long startNum, long endNum, float time)
    {
        StopCoroutine("AnimateNumber");
        StartCoroutine(AnimateNumber(text, startNum, endNum, time));
    }

    IEnumerator AnimateNumber(UILabel text, long startNum, long endNum, float time)
    {
        //Debug.Log("AnimateNumber : " + startNum + ", " + endNum + ", " + time);

        float delta = 0.02f;
        long remain = endNum - startNum;

        //Debug.Log("AnimateNumber remain : " + remain);

        float abs = time;
        if (abs < 0)
        {
            abs *= -1;
        }

        if (remain == 0 || abs < delta + 0.01f || startNum == endNum)
        {
            //Debug.Log("remain == 0 || abs < delta + 0.01f || startNum == endNum");
            text.text = nb_GlobalData.g_global.util.GetCommaNumber((int)endNum);
            yield return null;
        }
        else
        {
            long addNum = (long)((float)remain / time * delta);
            //Debug.Log("AnimateNumber addNum : " + addNum);

            if (addNum > 0)
            {
                if (addNum > remain)
                {
                    addNum = remain;
                }
            }
            else
            {
                if (addNum < remain)
                {
                    addNum = remain;
                }
            }

            long resultNum = startNum + addNum;
            float remainTime = time - delta;
            //Debug.Log("AnimateNumber resultNum/remainTime : " + resultNum + "/" + remainTime);

            yield return new WaitForSeconds(delta);

            text.text = nb_GlobalData.g_global.util.GetCommaNumber((int)resultNum);

            StartCoroutine(AnimateNumber(text, resultNum, endNum, remainTime));
        }
    }
}