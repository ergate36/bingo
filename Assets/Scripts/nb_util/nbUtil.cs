using MarigoldModel.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nbUtil
{
    public string GetCommaNumber(int data)
    {
        if (data == 0) return "0";
        return string.Format("{0:#,###}", data);
    }

    public long getGameMoney(GameMoneyId id)
    {
        foreach (var money in nb_GlobalData.g_global.myMoney)
        {
            if (money.id == id)
            {
                return money.value;
            }
        }

        return 0;
    }

    public void addGameMoney(GameMoneyId id, long addValue)
    {
        long current = 0;

        foreach (var money in nb_GlobalData.g_global.myMoney)
        {
            if (money.id == id)
            {
                current = money.value;
                nb_GlobalData.g_global.myMoney.Remove(money);
                break;
            }
        }

        nb_userMoney newMoney;
        newMoney.id = id;
        newMoney.value = current + addValue;
        nb_GlobalData.g_global.myMoney.Add(newMoney);
    }

    public void useGameMoney(GameMoneyId id, long useValue)
    {
        long current = 0;

        foreach (var money in nb_GlobalData.g_global.myMoney)
        {
            if (money.id == id)
            {
                if (money.value < useValue)
                {
                    Debug.Log("cannot use money - id : " + id);
                    return;
                }
                else
                {
                    current = money.value;
                    nb_GlobalData.g_global.myMoney.Remove(money);
                    break;
                }
            }
        }

        nb_userMoney newMoney;
        newMoney.id = id;
        newMoney.value = current - useValue;
        nb_GlobalData.g_global.myMoney.Add(newMoney);
    }

    public Collection findCollection(int id)
    {
        foreach(var col in nb_GlobalData.g_global.collectionList)
        {
            if (col.Id == id)
            {
                return col;
            }
        }

        return null;
    }

    public Collection findCollection(string spriteName)
    {
        foreach (var col in nb_GlobalData.g_global.collectionList)
        {
            if (col.SpriteName == spriteName)
            {
                return col;
            }
        }

        return null;
    }

    public int getUserCollectionCount(long colId)
    {
        foreach (var col in nb_GlobalData.g_global.userCollectionList)
        {
            if (col.CollectionId == colId)
            {
                return (int)col.Count;
            }
        }

        return 0;
    }

    public int getStageClearRewardValue(int stageId, int rank, AssetType type, int assetId)
    {
        foreach (var reward in nb_GlobalData.g_global.stageClearRankingRewardList)
        {
            if (reward.StageId == stageId &&
                reward.Ranking == rank &&
                reward.AssetType == type &&
                reward.AssetId == assetId)
            {
                return (int)reward.AssetCount;
            }
        }

        return 0;
    }
}