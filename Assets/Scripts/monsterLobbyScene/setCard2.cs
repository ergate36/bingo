using UnityEngine;
using System.Collections;
using System;
public class setCard2 : MonoBehaviour
{
    // Use this for initialization
    public int setindex;
    private monsterSceneUI2 monster_ui;

    Transform star;
    Transform name;
    Transform monster;
    Transform rare;
    Transform level_label;
    Transform lvbk;
    int selectCardNumber;

    private CardListCtrl2 CLctrl;
    private int flag = 0;
    void Start()
    {
        monster_ui = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster").GetComponent<monsterSceneUI2>();
        GameObject root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
        selectCardNumber = GlobalData.g_global.selectedCardNumber;
        CLctrl = root.GetComponent<CardListCtrl2>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {

        GlobalData.g_global.sendNum = GlobalData.g_global.selectedCardNumber;

        // monsterFlag  ==   setCard

        if (GlobalData.g_global.monsterFlag == 1)
        {
            GlobalData.g_global.monsterFlag = 0;
            if (monster_ui.inputindex_flag[setindex - 1] == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (GlobalData.g_global.selectedCardNumber == monster_ui.inputindex_cardnum[i])
                    {
                        return;
                    }
                }
                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (monster_ui.inputindex_cardnum[setindex - 1] == GlobalData.g_global.myCardList[i].cardno)
                    {
                        MyCard tempCard1 = GlobalData.g_global.myCardList[i];
                        tempCard1.cardused = 0;
                        GlobalData.g_global.myCardList[i] = tempCard1;

                        GlobalData.g_global.m_setInfo.card_num = GlobalData.g_global.myCardList[i].cardno;
                        GlobalData.g_global.m_setInfo.stat = 2;
                        GlobalData.g_global.m_setInfo.sell_money = 0;
                        GlobalData.g_global.m_setInfo.slot = 0;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardUseIng;
                        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardUseRequest);

                    }
                }
            }

            else
            {
                for (int i = 0; i < 4; i++)
                {
                    if (GlobalData.g_global.selectedCardNumber == monster_ui.inputindex_cardnum[i])
                    {
                        return;
                    }
                }

                GameObject setcard = Instantiate(Resources.Load("card/input_MonsterCard")) as GameObject;

                Vector3 scals = setcard.transform.localScale;
                setcard.transform.parent = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel/input_" + setindex).transform;
                setcard.transform.localPosition = Vector3.zero;
                setcard.transform.localScale = scals;
                // GameObject root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel/input_" + setindex) as GameObject;
            }

            monster_ui.inputindex_cardnum[setindex - 1] = GlobalData.g_global.selectedCardNumber;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.myCardList[i].cardno == GlobalData.g_global.selectedCardNumber)
                {
                    GlobalData.g_global.selectedCardId = GlobalData.g_global.myCardList[i].cardId;
                    level_label = transform.Find("input_MonsterCard(Clone)/Label");
                    level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[i].cardlevel.ToString();
                }
            }

            star = transform.Find("input_MonsterCard(Clone)/star");
            name = transform.Find("input_MonsterCard(Clone)/name");
            monster = transform.Find("input_MonsterCard(Clone)/monster");
            rare = transform.Find("input_MonsterCard(Clone)/rare");
            lvbk = transform.Find("input_MonsterCard(Clone)/lvbk");
            rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
            name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "name";

            star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank;
            monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
            lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;

            // 14141414
            StartCoroutine(sendWait());
            for (int i = 0; i < 4; i++)
            {
                monster_ui.inputindex[i].GetComponent<BoxCollider>().enabled = true;
            }
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.myCardList[i].cardno == GlobalData.g_global.selectedCardNumber)
                {
                    MyCard tempCard = GlobalData.g_global.myCardList[i];
                    tempCard.cardused = setindex;
                    GlobalData.g_global.myCardList[i] = tempCard;

                }
            }
            StopCoroutine("clickSet2");
            StopCoroutine("clickSet");

            monster_ui.m_input_click.gameObject.SetActive(false);
            monster_ui.inputindex_flag[setindex - 1] = 1;

            CLctrl.cardListClear();
            CLctrl.cardList(1);

        }

        else
        {
            if (monster_ui.inputindex_flag[setindex - 1] == 1)
            {                //객체가 있으면
                GlobalData.g_global.selectedCardNumber = monster_ui.inputindex_cardnum[setindex - 1];

                monster_ui.m_infopopup.gameObject.SetActive(true);

                monster_ui.m_infopopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_infopopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_infopopup.gameObject));

                GameObject lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                BoxCollider[] buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 10;
                int cardindex = GlobalData.g_global.selectedCardNumber;
                int index = 0;
                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (cardindex == GlobalData.g_global.myCardList[i].cardno)
                    {
                        index = GlobalData.g_global.myCardList[i].cardId-1;
                        GlobalData.g_global.selectedCardId = index+1;
                        cardindex = i;
                        break;
                    }
                }


                if (GlobalData.g_global.myCardList[cardindex].AT1 != 0)
                {
                    int at1 = GlobalData.g_global.myCardList[cardindex].AT1;
                    double atv1 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv1 + (XmlCtrl.x_xml.MCxml[index].AT1Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                    if (at1 == 1)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "% increase";
                    }
                    else if (at1 == 2)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv1 + "% increase";
                    }
                    else if (at1 == 3)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv1 + "% increase";
                    }
                    else if (at1 == 4)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv1 + "% increase";
                    }
                    else if (at1 == 5)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv1 + "% increase";
                    }
                    else if (at1 == 6)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv1 + "% increase";
                    }
                    else if (at1 == 7)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv1 + "% increase";
                    }
                    else if (at1 == 8)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv1 + "% increase";
                    }
                    else if (at1 == 9)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv1 + "% increase";
                    }
                    else if (at1 == 10)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv1 + "% increase";
                    }
                    else if (at1 == 11)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv1 + "% increase";
                    }
                    else if (at1 == 12)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv1 + "% increase";
                    }
                    else if (at1 == 13)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv1 + "% increase";
                    }
                    else if (at1 == 14)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv1 + "%  add score";
                    }
                    else if (at1 == 15)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv1 + "%  add score";
                    }
                    else if (at1 == 16)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score";
                    }
                    else if (at1 == 17)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score";
                    }
                    else if (at1 == 18)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv1 + "% increase";
                    }
                    else if (at1 == 19)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank score " + atv1 + "% add coin";
                    }
                    else if (at1 == 20)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv1 + "% add coin";
                    }
                    else if (at1 == 21)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add coin";
                    }
                    else if (at1 == 22)
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv1 + "%  increase";
                    }
                }
                else
                {
                    monster_ui.m_at1_label.GetComponent<UILabel>().text = "";
                }
                if (GlobalData.g_global.myCardList[cardindex].AT2 != 0)
                {
                    int at2 = GlobalData.g_global.myCardList[cardindex].AT2;
                    double atv2 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv2 + (XmlCtrl.x_xml.MCxml[index].AT2Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                    if (at2 == 1)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv2 + "% increase";
                    }
                    else if (at2 == 2)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv2 + "% increase";
                    }
                    else if (at2 == 3)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv2 + "% increase";
                    }
                    else if (at2 == 4)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv2 + "% increase";
                    }
                    else if (at2 == 5)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv2 + "% increase";
                    }
                    else if (at2 == 6)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv2 + "% increase";
                    }
                    else if (at2 == 7)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv2 + "% increase";
                    }
                    else if (at2 == 8)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv2 + "% increase";
                    }
                    else if (at2 == 9)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv2 + "% increase";
                    }
                    else if (at2 == 10)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv2 + "% increase";
                    }
                    else if (at2 == 11)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv2 + "% increase";
                    }
                    else if (at2 == 12)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv2 + "% increase";
                    }
                    else if (at2 == 13)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv2 + "% increase";
                    }
                    else if (at2 == 14)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv2 + "%  add score";
                    }
                    else if (at2 == 15)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv2 + "%  add score";
                    }
                    else if (at2 == 16)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score";
                    }
                    else if (at2 == 17)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score";
                    }
                    else if (at2 == 18)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv2 + "% increase";
                    }
                    else if (at2 == 19)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv2 + "% add coin";
                    }
                    else if (at2 == 20)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv2 + "% add coin";
                    }
                    else if (at2 == 21)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add coin";
                    }
                    else if (at2 == 22)
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv2 + "%  increase";
                    }
                }
                else
                {
                    monster_ui.m_at2_label.GetComponent<UILabel>().text = "";
                }

                if (GlobalData.g_global.myCardList[cardindex].AT3 != 0)
                {
                    int at3 = GlobalData.g_global.myCardList[cardindex].AT3;
                    double atv3 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv3 + (XmlCtrl.x_xml.MCxml[index].AT3Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                    if (at3 == 1)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv3 + "% increase";
                    }
                    else if (at3 == 2)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv3 + "% increase";
                    }
                    else if (at3 == 3)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv3 + "% increase";
                    }
                    else if (at3 == 4)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv3 + "% increase";
                    }
                    else if (at3 == 5)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv3 + "% increase";
                    }
                    else if (at3 == 6)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv3 + "% increase";
                    }
                    else if (at3 == 7)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv3 + "% increase";
                    }
                    else if (at3 == 8)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv3 + "% increase";
                    }
                    else if (at3 == 9)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv3 + "% increase";
                    }
                    else if (at3 == 10)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv3 + "% increase";
                    }
                    else if (at3 == 11)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv3 + "% increase";
                    }
                    else if (at3 == 12)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv3 + "% increase";
                    }
                    else if (at3 == 13)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv3 + "% increase";
                    }
                    else if (at3 == 14)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv3 + "%  add score";
                    }
                    else if (at3 == 15)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv3 + "%  add score";
                    }
                    else if (at3 == 16)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score";
                    }
                    else if (at3 == 17)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score";
                    }
                    else if (at3 == 18)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv3 + "% increase";
                    }
                    else if (at3 == 19)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv3 + "% add coin";
                    }
                    else if (at3 == 20)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv3 + "% add coin";
                    }
                    else if (at3 == 21)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add coin";
                    }
                    else if (at3 == 22)
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv3 + "%  increase";
                    }
                }

                else
                {
                    monster_ui.m_at3_label.GetComponent<UILabel>().text = "";
                }



                Transform star;
                Transform name;
                Transform monster;
                Transform rare;
                Transform level_label;
                Transform lvbk;
                GameObject card = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel/list_MonsterCard");
                star = card.transform.Find("star");
                lvbk = card.transform.Find("lvbk");
                name = card.transform.Find("name");
                monster = card.transform.Find("monster");
                rare = card.transform.Find("rare");
                level_label = card.transform.Find("Label");

                rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[index].Rare;
                name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[index].Name + "name";

                star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[index].Rank;

                monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[index].Name + "_" + XmlCtrl.x_xml.MCxml[index].Rare;
                lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[index].Rare;

                monster_ui.m_card_name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[index].Name + "name";

                monster_ui.m_card_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[index].Rank + "_2";
                if (XmlCtrl.x_xml.MCxml[index].Rank == 6)
                {
                    monster_ui.m_card_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[index].Rank;
                }

                level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[cardindex].cardlevel.ToString();

                int temp = GlobalData.g_global.myCardList[cardindex].cardlevel;

                if (temp != 15)
                {
                    monster_ui.m_info_mix.gameObject.SetActive(false);
                    monster_ui.m_info_upgrade.gameObject.SetActive(true);
                }
                else
                {
                    monster_ui.m_info_mix.gameObject.SetActive(true);
                    monster_ui.m_info_upgrade.gameObject.SetActive(false);
                    if (GlobalData.g_global.myCardList[cardindex].cardRank == 6)
                    {
                        monster_ui.m_info_mix.gameObject.SetActive(false);
                    }
                }
            }
        }
    }



    private IEnumerator sendWait()
    {
        yield return new WaitForSeconds(0.5f);
        GlobalData.g_global.m_setInfo.stat = 1;
        GlobalData.g_global.m_setInfo.card_num = GlobalData.g_global.sendNum;
        GlobalData.g_global.m_setInfo.sell_money = 0;
        GlobalData.g_global.m_setInfo.slot = setindex;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardUseIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardUseRequest);
    }

}
