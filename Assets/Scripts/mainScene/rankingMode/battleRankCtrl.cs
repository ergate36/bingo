﻿using UnityEngine;
using System.Collections;

public class battleRankCtrl : MonoBehaviour {

    BattleRankInfo myInfo;
    BattleRankInfo[] rankList;

    public UIGrid grid;
    public UIScrollView sv;
    public UIPanel panel;

    public Transform gridRoot;
    // Use this for initialization
    void Start()
    {

    }


    void OnClick()
    {
            GameObject pn = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking") as GameObject;
            Transform popup = pn.transform.Find("battleRanking");
            popup.gameObject.SetActive(true);
            setImage();
            popup = pn.transform.Find("worldRanking");
            popup.gameObject.SetActive(false);
            popup = pn.transform.Find("coinRanking");
            popup.gameObject.SetActive(false);
            popup = pn.transform.Find("rankList");
            popup.gameObject.SetActive(false);
    }

    private void setImage()
    {
        GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/battleranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "battle_1";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "battle_1";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "battle_1";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "battle_1";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/coinranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "coin_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "coin_2";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/worldranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "score_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "score_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "score_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "score_2";

        check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_ranking/rewardranking_btn");
        check.transform.GetComponentInChildren<UISprite>().spriteName = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().normalSprite = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().hoverSprite = "reward_2";
        check.transform.GetComponentInChildren<UIButton>().pressedSprite = "reward_2";

    }

}
