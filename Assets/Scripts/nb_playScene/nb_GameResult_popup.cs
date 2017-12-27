using MarigoldModel.Model;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_GameResult_popup : MonoBehaviour
{

    //1. 코인 습득 보상 - 없으면 스킵
    //2. 순위 보상 (코인, 크레딧, 경험치) - 없으면 스킵
    //3. 콜렉션 획득 - 없으면 스킵
    //4. 추가 보상 - 코인, 크레딧 x2 - 없으면 스킵
    //5. 추가 보상 - 경험치 x2 - 없으면 스킵
    //6. 추가 보상 - 상자(코인, 크레딧, 아이템, 콜렉션) - 없으면 스킵
    //6-2. 상자 콜렉션

    private GameObject[] Collection;
    private Collection[] colData;

    private GameObject[] effectParent;

    private GameObject totalCoinIcon;
    private GameObject totalCreditIcon;
    private GameObject totalCoinValue;
    private GameObject totalCreditValue;
    private GameObject totalPowerUp1Value;

    private int currentCoin = 0;
    private int currentCredit = 0;
    private int currentExp = 0;
    private int currentPowerUp1 = 0;

    public float AnimationSpeedRate = 1.0f;

    void Awake()
    {
        Collection = new GameObject[12];
        for (int i = 1; i <= 12; ++i)
        {
            Collection[i - 1] = this.transform.Find("collection/item" + i).gameObject;
        }

        colData = new Collection[12];

        effectParent = new GameObject[6];
        for (int i = 1; i <= 6; ++i)
        {
            effectParent[i - 1] = this.transform.Find("reward/" + i).gameObject;
            effectParent[i - 1].SetActive(false);
        }

        totalCoinIcon = this.transform.Find("bg/i_icon_coin").gameObject;
        totalCreditIcon = this.transform.Find("bg/i_icon_credit").gameObject;
        totalCoinValue = this.transform.Find("bg/t_coin_value").gameObject;
        totalCreditValue = this.transform.Find("bg/t_credit_value").gameObject;
        totalPowerUp1Value = this.transform.Find("bg/t_power1_value").gameObject;
    }

	void Start () 
    {
        //선택 카드 장수 세팅
        this.transform.Find("bg/i_select_card").GetComponent<UISprite>().spriteName = 
            "ui_reward_card" + nb_GlobalData.g_global.sheetInfo.activeSheetCount;
        this.transform.Find("bg/i_select_card").GetComponent<UISprite>().MakePixelPerfect();
        this.transform.Find("bg/i_select_card").gameObject.SetActive(false);

		//콜렉션 세팅
        setCollection();

        //플레이어 경험치 세팅
        //todo:
	}
	
	void Update ()
    {
	
	}

    void setCollection()
    {
        for (int i = 1; i <= 12; ++i)
        {
            string name = "collection_" + nb_GlobalData.g_global.selectStageId + "_" + i;
            Collection col = nb_GlobalData.g_global.util.findCollection(name);
            colData[i - 1] = col;

            Transform icon = Collection[i - 1].transform.Find("icon");
            Transform icon_off = Collection[i - 1].transform.Find("icon_off");
            UILabel count = Collection[i - 1].transform.Find("count").GetComponent<UILabel>();

            icon.GetComponent<UISprite>().atlas.name = col.AtlasName;
            icon.GetComponent<UISprite>().spriteName = col.SpriteName;
            icon.GetComponent<UISprite>().MakePixelPerfect();
            icon.localScale = new Vector3(0.45f, 0.45f);

            Texture texture = Resources.Load("nb_images/collection/" +
                col.AtlasName + "/" + col.SpriteName, typeof(Texture)) as Texture;
            icon_off.GetComponent<UITexture>().mainTexture = texture;
            icon_off.GetComponent<UITexture>().MakePixelPerfect();
            icon_off.GetComponent<UITexture>().color = new Color(0.22f, 0.3f, 0.47f);
            icon_off.GetComponent<UITexture>().alpha = 0.4f;
            icon_off.localScale = new Vector3(0.45f, 0.45f);

            int colCount = nb_GlobalData.g_global.util.getUserCollectionCount(col.Id);
            count.text = "x" + colCount;

            icon.gameObject.SetActive(colCount > 0);
            icon_off.gameObject.SetActive(colCount == 0);

            Collection[i - 1].GetComponent<UISprite>().spriteName =
                colCount > 0 ? "ui_collection_back1" : "ui_collection_back2";
        }
    }

    public void startReward()
    {
        Debug.Log("GameResult - startReward");
        StartCoroutine(showPopup());
    }

    IEnumerator showPopup()
    {
        Debug.Log("GameResult - showPopup");
        yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);

        this.gameObject.SetActive(true);

        GameObject spine = this.transform.Find("reward/card_spine").gameObject;
        spine.SetActive(true);
        SkeletonAnimation ani = spine.GetComponent<SkeletonAnimation>();
        ani.AnimationName = "card_" + nb_GlobalData.g_global.sheetInfo.activeSheetCount;
        yield return new WaitForSeconds(2.65f);
        this.transform.Find("bg/i_select_card").gameObject.SetActive(true);
        
        yield return new WaitForSeconds(AnimationSpeedRate * 1.0f);

        StartCoroutine(startCoinReward());
        yield return null;
    }

    IEnumerator startCoinReward()
    {
        Debug.Log("GameResult - startCoinReward");
        //코인 습득 보상
        effectParent[0].SetActive(true);

        bool check = false;
        long coinValue = 0;
        //foreach (var reward in nb_GlobalData.g_global.clearRewardList)
        //{
        //    if (reward.ClearRewardType == ClearRewardType.COIN)
        //    {
        //        coinValue = reward.AssetCount;
        //        StartCoroutine(AnimateNumber(
        //            effectParent[0].transform.Find("value").GetComponent<UILabel>(), 
        //            0, coinValue, AnimationSpeedRate * 0.25f, true));
        //        check = true;
        //        break;
        //    }
        //}

        //test->

        coinValue = 123;
        StartCoroutine(AnimateNumber(
            effectParent[0].transform.Find("value").GetComponent<UILabel>(),
            0, coinValue, AnimationSpeedRate * 0.25f, true));
        check = true;

        //<-test

        if (check)
        {
            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
    
            StartCoroutine(AnimateNumber(
                totalCoinValue.GetComponent<UILabel>(),
                0, coinValue, AnimationSpeedRate * 0.75f));

            currentCoin = (int)coinValue;

            StartCoroutine(AnimateNumber(
                effectParent[0].transform.Find("value").GetComponent<UILabel>(),
                coinValue, 0, AnimationSpeedRate * 0.75f, true));

            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
        }

        StartCoroutine(startBingoReward());

        yield return null;
    }

    IEnumerator startBingoReward()
    {
        Debug.Log("GameResult - startBingoReward");
        //빙고 순위 보상
        effectParent[0].SetActive(false);
        yield return new WaitForSeconds(AnimationSpeedRate * 1.0f);
        effectParent[1].SetActive(true);
        effectParent[1].transform.Find("spine").gameObject.SetActive(false);

        SkeletonAnimation ani = effectParent[1].transform.Find("spine").GetComponent<SkeletonAnimation>();

        //빙고 한개라도 있는지 체크
        bool check = false;
        for (int i = 0; i < 4; ++i)
        {
            if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 1)
            {
                effectParent[1].transform.Find("spine").gameObject.SetActive(true);
                ani.AnimationName = "bingo_1";
                check = true;
                break;
            }
            else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 2)
            {
                effectParent[1].transform.Find("spine").gameObject.SetActive(true);
                ani.AnimationName = "bingo_2";
                check = true;
                break;
            }
            else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 3)
            {
                effectParent[1].transform.Find("spine").gameObject.SetActive(true);
                ani.AnimationName = "bingo_3";
                check = true;
                break;
            }
            else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] >= 4)
            {
                effectParent[1].transform.Find("spine").gameObject.SetActive(true);
                ani.AnimationName = "bingo_4";
                check = true;
                break;
            }
        }

        if (check)
        {
            //한개라도 있을경우 진행
            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);

            int coinValue = 0;
            int creditValue = 0;
            int expValue = 0;

            //순위 연출
            for (int i = 0; i < 4; ++i)
            {
                if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] != 0)
                {
                    var obj = effectParent[1].transform.Find("rank" + (i + 1)).gameObject;
                    obj.SetActive(true);
                    
                    if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 1)
                    {
                        obj.GetComponent<UILabel>().text = "1st";
                    }
                    else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 2)
                    {
                        obj.GetComponent<UILabel>().text = "2nd";
                    }
                    else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] == 3)
                    {
                        obj.GetComponent<UILabel>().text = "3rd";
                    }
                    else if (nb_GlobalData.g_global.BingoMyBlitzRanking[i] >= 4)
                    {
                        obj.GetComponent<UILabel>().text = 
                            nb_GlobalData.g_global.BingoMyBlitzRanking[i] + "th";
                    }
                    
                    coinValue += nb_GlobalData.g_global.util.getStageClearRewardValue(
                        nb_GlobalData.g_global.selectStageId,
                        nb_GlobalData.g_global.BingoMyBlitzRanking[i],
                        AssetType.GAME_MONEY, 1);

                    creditValue += nb_GlobalData.g_global.util.getStageClearRewardValue(
                        nb_GlobalData.g_global.selectStageId,
                        nb_GlobalData.g_global.BingoMyBlitzRanking[i],
                        AssetType.GAME_MONEY, 2);

                    expValue += nb_GlobalData.g_global.util.getStageClearRewardValue(
                        nb_GlobalData.g_global.selectStageId,
                        nb_GlobalData.g_global.BingoMyBlitzRanking[i],
                        AssetType.EXPERIENCE, 0);

                    iTween.ScaleTo(obj, Vector3.one, AnimationSpeedRate * 0.5f);
                    yield return new WaitForSeconds(AnimationSpeedRate * 0.4f);
                }
            }

            Debug.Log("BingoResult value : " + coinValue + ", " + creditValue + ", " + expValue);

            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);

            var rewardIcon1 = effectParent[1].transform.Find("icon1").gameObject;
            var rewardValue1 = effectParent[1].transform.Find("value1").gameObject;
            var rewardIcon2 = effectParent[1].transform.Find("icon2").gameObject;
            var rewardValue2 = effectParent[1].transform.Find("value2").gameObject;
            var rewardIcon3 = effectParent[1].transform.Find("icon3").gameObject;
            var rewardValue3 = effectParent[1].transform.Find("value3").gameObject;
            //재화 연출 - 코인
            if (coinValue > 0)
            {
                rewardIcon1.SetActive(true);
                rewardValue1.SetActive(true);

                rewardValue1.GetComponent<UILabel>().text = coinValue.ToString();

                int resultCoin = currentCoin + coinValue;

                StartCoroutine(AnimateNumber(
                    rewardValue1.GetComponent<UILabel>(),
                    currentCoin, 0, AnimationSpeedRate * 1.0f));

                StartCoroutine(AnimateNumber(
                    totalCoinValue.GetComponent<UILabel>(),
                    currentCoin, resultCoin, AnimationSpeedRate * 1.0f));

                yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
                currentCoin = resultCoin;
            }

            //재화 연출 - 크레딧
            if (creditValue > 0)
            {
                rewardIcon1.SetActive(false);
                rewardValue1.SetActive(false);
                rewardIcon2.SetActive(true);
                rewardValue2.SetActive(true);

                rewardValue2.GetComponent<UILabel>().text = creditValue.ToString();

                int resultCredit = currentCredit + creditValue;

                StartCoroutine(AnimateNumber(
                    rewardValue2.GetComponent<UILabel>(),
                    currentCredit, 0, AnimationSpeedRate * 1.0f));

                StartCoroutine(AnimateNumber(
                    totalCreditValue.GetComponent<UILabel>(),
                    currentCredit, resultCredit, AnimationSpeedRate * 1.0f));

                yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
                currentCredit = resultCredit;
            }

            //재화 연출 - 경험치
            if (expValue > 0)
            {
                rewardIcon2.SetActive(false);
                rewardValue2.SetActive(false);
                rewardIcon3.SetActive(true);
                rewardValue3.SetActive(true);

                rewardValue3.GetComponent<UILabel>().text = expValue.ToString();

                int resultExp = currentExp + expValue;

                //todo: 경험치 적용
                //

                yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
                currentExp = resultExp;
            }
        }

        StartCoroutine(startMoneyBonus());

        yield return null;
    }

    IEnumerator startMoneyBonus()
    {
        //재화x2 보너스
        effectParent[1].SetActive(false);
        effectParent[2].SetActive(true);

        if (nb_GlobalData.g_global.useDoubleReward == true)
        {
            var icon = effectParent[2].transform.Find("icon").gameObject;
            var move1 = effectParent[2].transform.Find("icon_move1").gameObject;
            var move2 = effectParent[2].transform.Find("icon_move2").gameObject;
            icon.SetActive(true);
            move1.SetActive(true);
            move2.SetActive(true);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.5f);

            iTween.ScaleTo(icon, new Vector3(5, 5), AnimationSpeedRate * 0.5f);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.5f);
            iTween.ScaleTo(icon, new Vector3(4, 4), AnimationSpeedRate * 0.125f);
            yield return new WaitForSeconds(AnimationSpeedRate * 1.75f);
            iTween.ScaleTo(icon, new Vector3(1.25f, 1.25f), AnimationSpeedRate * 0.25f);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.25f);
            icon.SetActive(false);

            iTween.MoveTo(move1, totalCoinIcon.transform.position, AnimationSpeedRate * 0.75f);
            iTween.MoveTo(move2, totalCreditIcon.transform.position, AnimationSpeedRate * 0.75f);

            yield return new WaitForSeconds(AnimationSpeedRate * 0.75f);
            move1.SetActive(false);
            move2.SetActive(false);

            //todo: 번쩍 이펙트

            StartCoroutine(AnimateNumber(
                totalCoinValue.GetComponent<UILabel>(),
                currentCoin, currentCoin * 2, AnimationSpeedRate * 0.75f));
            StartCoroutine(AnimateNumber(
                totalCreditValue.GetComponent<UILabel>(),
                currentCredit, currentCredit * 2, AnimationSpeedRate * 0.75f));

            yield return new WaitForSeconds(AnimationSpeedRate * 0.75f);

            currentCoin = currentCoin * 2;
            currentCredit = currentCredit * 2;

            totalCoinIcon.transform.Find("x2").gameObject.SetActive(true);
            totalCreditIcon.transform.Find("x2").gameObject.SetActive(true);


            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
        }

        StartCoroutine(startExpBonus());

        yield return null;
    }

    IEnumerator startExpBonus()
    {
        //경험치x2 보너스
        effectParent[2].SetActive(false);
        effectParent[3].SetActive(true);

        if (nb_GlobalData.g_global.useDoubleExp == true)
        {
            var expIcon = this.transform.Find("bg/i_level_star").gameObject;
            var icon = effectParent[3].transform.Find("icon").gameObject;
            var move = effectParent[3].transform.Find("icon_move").gameObject;
            icon.SetActive(true);
            move.SetActive(true);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.5f);

            iTween.ScaleTo(icon, new Vector3(5, 5), AnimationSpeedRate * 0.5f);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.5f);
            iTween.ScaleTo(icon, new Vector3(4, 4), AnimationSpeedRate * 0.125f);
            yield return new WaitForSeconds(AnimationSpeedRate * 1.75f);
            iTween.ScaleTo(icon, new Vector3(1.25f, 1.25f), AnimationSpeedRate * 0.25f);
            yield return new WaitForSeconds(AnimationSpeedRate * 0.25f);
            icon.SetActive(false);

            iTween.MoveTo(move, expIcon.transform.position, AnimationSpeedRate * 0.75f);

            yield return new WaitForSeconds(AnimationSpeedRate * 0.75f);
            move.SetActive(false);

            //todo: 번쩍 이펙트

            //todo: 경험치 상승 효과

            yield return new WaitForSeconds(AnimationSpeedRate * 0.75f);

            currentExp = currentExp * 2;

            expIcon.transform.Find("x2").gameObject.SetActive(true);


            yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
        }

        StartCoroutine(startCollectionReward());

        yield return null;
    }

    IEnumerator startCollectionReward()
    {
        //콜렉션 획득
        effectParent[3].SetActive(false);
        effectParent[4].SetActive(true);

        //test:
        MarigoldGame.Commands.ClearRewardCommand test = new MarigoldGame.Commands.ClearRewardCommand();
        test.ClearRewardType = ClearRewardType.COLLECTION;
        test.AssetType = AssetType.COLLECTION;
        test.AssetId = 2;
        MarigoldGame.Commands.ClearRewardCommand test2 = new MarigoldGame.Commands.ClearRewardCommand();
        test2.ClearRewardType = ClearRewardType.COLLECTION;
        test2.AssetType = AssetType.COLLECTION;
        test2.AssetId = 6;

        nb_GlobalData.g_global.clearRewardList.Add(test);
        nb_GlobalData.g_global.clearRewardList.Add(test2);
        
        foreach (var reward in nb_GlobalData.g_global.clearRewardList)
        {
            if (reward.ClearRewardType == ClearRewardType.COLLECTION)
            {
                if (reward.AssetType == AssetType.COLLECTION)
                {
                    GameObject card = effectParent[4].transform.Find("card").gameObject;
                    GameObject found = effectParent[4].transform.Find("found").gameObject;
                    GameObject cardCol = card.transform.Find("collection").gameObject;
                    GameObject foundCol = found.transform.Find("collection").gameObject;

                    string name = "collection_" + nb_GlobalData.g_global.selectStageId + "_" + reward.AssetId;
                    Collection col = nb_GlobalData.g_global.util.findCollection(name);

                    if (nb_GlobalData.g_global.util.getUserCollectionCount(col.Id) > 0)
                    {
                        card.SetActive(false);
                        found.SetActive(true);
                    }
                    else
                    {
                        card.SetActive(true);
                        found.SetActive(false);

                        Texture texture = Resources.Load("nb_images/collection/" +
                            col.AtlasName + "/" + col.SpriteName, typeof(Texture)) as Texture;
                        cardCol.GetComponent<UITexture>().mainTexture = texture;
                        cardCol.GetComponent<UITexture>().MakePixelPerfect();
                        cardCol.GetComponent<UITexture>().color = new Color(0.22f, 0.3f, 0.47f);
                        cardCol.GetComponent<UITexture>().alpha = 0.4f;
                        cardCol.transform.localScale = new Vector3(0.7f, 0.7f);

                        yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
                    }

                    foundCol.GetComponent<UISprite>().atlas.name = col.AtlasName;
                    foundCol.GetComponent<UISprite>().spriteName = col.SpriteName;
                    foundCol.GetComponent<UISprite>().MakePixelPerfect();
                    foundCol.SetActive(true);

                    found.transform.Find("t_name").GetComponent<UILabel>().text = col.Name;

                    card.SetActive(false);
                    found.SetActive(true);

                    yield return new WaitForSeconds(AnimationSpeedRate * 2.0f);
                    int index = -1;
                    for (int i = 0; i < 12; ++i)
                    {
                        if (colData[i].Id == col.Id)
                        {
                            index = i;
                            break;
                        }
                    }

                    if (index == -1) break;

                    foundCol.GetComponent<nb_GameResult_rotate>().enabled = true;

                    iTween.ScaleTo(foundCol, new Vector3(0.45f, 0.45f), AnimationSpeedRate * 0.5f);
                    iTween.MoveTo(foundCol, Collection[index].transform.position, AnimationSpeedRate * 0.75f);
                    yield return new WaitForSeconds(AnimationSpeedRate * 0.75f);
                    iTween.Stop(foundCol);
                    foundCol.SetActive(false);
                    foundCol.transform.localPosition = Vector3.zero;
                    foundCol.transform.localScale = Vector3.one;
                    foundCol.GetComponent<nb_GameResult_rotate>().enabled = false;

                    Transform icon = Collection[index].transform.Find("icon");
                    Transform icon_off = Collection[index].transform.Find("icon_off");
                    UILabel count = Collection[index].transform.Find("count").GetComponent<UILabel>();

                    if (nb_GlobalData.g_global.util.getUserCollectionCount(col.Id) == 0)
                    {
                        icon.gameObject.SetActive(true);
                        icon_off.gameObject.SetActive(false);
                        count.text = "x1";
                    }
                    else
                    {
                        count.text = "x" + (nb_GlobalData.g_global.util.getUserCollectionCount(col.Id) + 1);
                    }

                    yield return new WaitForSeconds(AnimationSpeedRate * 1.75f);
                }
            }
        }

        StartCoroutine(startChestReward());

        yield return null;
    }

    IEnumerator startChestReward()
    {
        //상자 획득
        effectParent[4].SetActive(false);
        effectParent[5].SetActive(true);

        bool check = false;
        foreach (var reward in nb_GlobalData.g_global.clearRewardList)
        {
            if (reward.ClearRewardType == ClearRewardType.CHEST)
            {
                check = true;
                break;
            }
        }

        if (check)  //하나라도 먹은 상자가 잇음
        {
            foreach (var reward in nb_GlobalData.g_global.clearRewardList)
            {
                if (reward.ClearRewardType == ClearRewardType.CHEST)
                {
                    if (reward.AssetType == AssetType.GAME_MONEY)
                    {

                    }
                    else if (reward.AssetType == AssetType.EXPERIENCE)
                    {

                    }
                    else if (reward.AssetType == AssetType.RANDOM_REWARD)
                    {

                    }
                }
            }
        }

        yield return null;
    }

    IEnumerator startChestCollectionReward()
    {
        //상자 콜렉션 획득

        yield return null;
    }

    IEnumerator AnimateNumber(UILabel text, long startNum, long endNum, float time, bool addPlus = false)
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
            if (addPlus)
            {
                text.text = "+" + nb_GlobalData.g_global.util.GetCommaNumber((int)endNum);
            }
            else
            {
                text.text = nb_GlobalData.g_global.util.GetCommaNumber((int)endNum);
            }
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

            if (addPlus)
            {
                text.text = "+" + nb_GlobalData.g_global.util.GetCommaNumber((int)resultNum);
            }
            else
            {
                text.text = nb_GlobalData.g_global.util.GetCommaNumber((int)resultNum);
            }

            StartCoroutine(AnimateNumber(text, resultNum, endNum, remainTime));
        }
    }
}
