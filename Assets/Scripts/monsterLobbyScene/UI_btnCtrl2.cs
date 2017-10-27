using UnityEngine;
using System.Collections;
using System.Xml;
using System;
public class UI_btnCtrl2 : MonoBehaviour
{

    private monsterSceneUI2 monster_ui;

    private GameObject popup;
    private Transform popupup;
    private GameObject lobbySceneUI;
    private BoxCollider[] buttons;
    public int btnType;
    public int tapType;

    private CardListCtrl2 CLctrl;
    GameObject root;
    private MyCard mCard;
    private XmlCtrl xmlctrl;
    private materialInfo m_mater;
    /*
    
    1 = sell
    3 = input
    4 = output
    5 = upgrade_panel
    6 = upgrade
    8 = mix_panel
    9 = mix
    11 = sell complete 
    12 = info complete 
    15 = close _ errorpopup
    else = close button
    
    */
    void Start()
    {
        monster_ui = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster").GetComponent<monsterSceneUI2>();
        xmlctrl = GameObject.Find("GlobalObject").GetComponent<XmlCtrl>();
        root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
        StopCoroutine("clickSet2");
        StopCoroutine("clickSet");

        monster_ui.m_input_click.gameObject.SetActive(false);
        CLctrl = root.GetComponent<CardListCtrl2>();
    }

    void Update()
    {
    }
    void OnClick()
    {
        if (btnType == 1)
        {
            StopCoroutine("clickSet2");
            StopCoroutine("clickSet");

            monster_ui.m_input_click.gameObject.SetActive(false);

            if (GlobalData.g_global.myCardList.Count == 0)
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));
                monster_ui.m_error_message.GetComponent<UILabel>().text = "보유중인 카드가 없습니다.";
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");

                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

                GlobalData.g_global.closeType = 10;

                return;
            }

            if (GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardused != 0)
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));
                monster_ui.m_error_message.GetComponent<UILabel>().text = "Equipped cards can't be sold.";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/errorpopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                GlobalData.g_global.closeType = 10;
                return;
            }
            else
            {
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 10;

                monster_ui.m_sellpopup.gameObject.SetActive(true);
                monster_ui.m_sellpopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_sellpopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_sellpopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_sellpopup.gameObject));

                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (GlobalData.g_global.selectedCardNumber == GlobalData.g_global.myCardList[i].cardno)
                    {
                        GlobalData.g_global.selectedCardId = GlobalData.g_global.myCardList[i].cardId;
                        GlobalData.g_global.selectedCardNum = i;
                        break;
                    }
                }
                /////141016
                int resultc = xmlctrl.MRxml[GlobalData.g_global.selectedCardId].Sell;

                for (int i = 0; i < xmlctrl.MRxml.Count; i++)
                {
                    if (xmlctrl.MCxml[GlobalData.g_global.selectedCardId - 1].Rank == xmlctrl.MRxml[i].Rank)
                    {
                        resultc = xmlctrl.MRxml[i].Sell;
                        break;
                    }
                }

                monster_ui.m_sell_price.GetComponent<UILabel>().text = resultc.ToString();
                GlobalData.g_global.m_setInfo.sell_money = resultc;
            }
        }
        else if (btnType == 3)
        {
            /*
            if (GlobalData.g_global.myCardList.Count == 0)
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));
                monster_ui.m_error_message.GetComponent<UILabel>().text = "보유중인 카드가 없습니다.";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 10;
                return;
            }
            */
            GlobalData.g_global.closeType = 10;

            for (int j = 0; j < GlobalData.g_global.myCardList.Count; j++)
            {
                if (GlobalData.g_global.myCardList[j].cardno == GlobalData.g_global.selectedCardNumber)
                {
                    if (GlobalData.g_global.myCardList[j].cardused == 0)
                    {
                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = true;
                        }
                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = true;
                        }
                        StopCoroutine("clickSet2");
                        StopCoroutine("clickSet");

                        monster_ui.m_input_click.gameObject.SetActive(true);
                        //                        monster_ui.m_infopopup.gameObject.SetActive(false);
                        iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
                        GlobalData.g_global.monsterFlag = 1;
                        StartCoroutine("clickSet");

                    }
                    else
                    {
                        monster_ui.m_errorPopup.gameObject.SetActive(true);
                        monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                        iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                        iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

                        monster_ui.m_error_message.GetComponent<UILabel>().text = "The card has already been equipped.";

                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = false;
                        }
                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = false;
                        }
                        GlobalData.g_global.closeType = 10;
                    }
                }
            }

        }

        else if (btnType == 4)
        {
            //output
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.selectedCardNumber == GlobalData.g_global.myCardList[i].cardno)
                {
                    GameObject destroyObject = monster_ui.inputindex[GlobalData.g_global.myCardList[i].cardused - 1].Find("input_MonsterCard(Clone)").gameObject;
                    Destroy(destroyObject);

                }
            }

            //   GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardused = 0;
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }

            GlobalData.g_global.m_setInfo.card_num = GlobalData.g_global.selectedCardNumber;
            GlobalData.g_global.m_setInfo.stat = 2;
            GlobalData.g_global.m_setInfo.sell_money = 0;
            GlobalData.g_global.m_setInfo.slot = 0;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardUseIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardUseRequest);

            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.selectedCardNumber == GlobalData.g_global.myCardList[i].cardno)
                {
                    monster_ui.inputindex_flag[GlobalData.g_global.myCardList[i].cardused - 1] = 0;
                    monster_ui.inputindex_cardnum[GlobalData.g_global.myCardList[i].cardused - 1] = 0;
                    mCard = GlobalData.g_global.myCardList[i];
                    mCard.cardused = 0;
                    GlobalData.g_global.myCardList[i] = mCard;
                }
            }

            iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            CLctrl.cardListClear();
            CLctrl.cardList(1);
        }

        else if (btnType == 5)
        {
            int upgradeLevel = 0;
            monster_ui.m_panel_index = 99;
            monster_ui.m_upgrade_aLevel.gameObject.SetActive(true);
            monster_ui.m_upgrade_btn.gameObject.SetActive(true);

            GlobalData.g_global.listType = 99;
            monster_ui.upgradeobject = GlobalData.g_global.selectedCardNumber;
            monster_ui.upgradeobjectId = xmlctrl.MCxml[GlobalData.g_global.selectedCardId - 1].Rank;

            // 강화 대상 객체 set

            Transform star;
            Transform name;
            Transform monster;
            Transform rare;
            Transform level_label;
            Transform lvbk;
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

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
            //monster_ui.m_infopopup.gameObject.SetActive(false);

            iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            monster_ui.m_upgradepanel.gameObject.SetActive(true);

            monster_ui.m_arrow.gameObject.SetActive(false);
            //   monster_ui.m_beforeBar.GetComponent<UISlider>().value = (float)GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardexp / 100;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (monster_ui.upgradeobject == GlobalData.g_global.myCardList[i].cardno)
                {
                    upgradeLevel = GlobalData.g_global.myCardList[i].cardlevel;
                    monster_ui.m_upgrade_bLevel.GetComponent<UILabel>().text = "Lv " + GlobalData.g_global.myCardList[i].cardlevel.ToString();
                    monster_ui.m_arrow.gameObject.SetActive(true);


                    monster_ui.m_upgrade_aLevel.GetComponent<UILabel>().text = "Lv " + (GlobalData.g_global.myCardList[i].cardlevel + 1).ToString();
                }
            }

            for (int i = 0; i < xmlctrl.MRxml.Count; i++)
            {
                if (monster_ui.upgradeobjectId == xmlctrl.MRxml[i].Rank && xmlctrl.MRxml[i].Level == upgradeLevel)
                {
                    monster_ui.m_upgrade_cost.GetComponent<UILabel>().text = xmlctrl.MRxml[i].Enchant.ToString();
                    break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (monster_ui.upgrade_input_flag[i] == 1)
                {
                    GameObject destroyObject = monster_ui.upgrade_inputindex[i].Find("input_MonsterCard(Clone)").gameObject;
                    Destroy(destroyObject);
                    monster_ui.upgrade_input_flag[i] = 0;
                }
            }

            GameObject card = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/upgrade_panel/upgrade_target");
            star = card.transform.Find("star");
            name = card.transform.Find("name");
            monster = card.transform.Find("monster");
            rare = card.transform.Find("rare");
            level_label = card.transform.Find("Label");
            lvbk = card.transform.Find("lvbk");

            rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
            name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "name";
            star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank;
            monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
            level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardlevel.ToString();
            lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;


            int cardindex = GlobalData.g_global.selectedCardNumber;
            int index = 0;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (cardindex == GlobalData.g_global.myCardList[i].cardno)
                {
                    index = GlobalData.g_global.myCardList[i].cardId-1;
                    GlobalData.g_global.selectedCardId = index;
                    cardindex = i;
                    break;
                }
            }


            if (GlobalData.g_global.myCardList[cardindex].AT1 != 0)
            {
                int at1 = GlobalData.g_global.myCardList[cardindex].AT1;
                double atv1 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv1 + (XmlCtrl.x_xml.MCxml[index].AT1Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT1Add, 1);

                if (at1 == 1)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 2)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 3)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 4)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 5)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 6)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 7)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 8)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 9)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 10)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 11)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 12)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 13)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 14)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv1 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 15)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv1 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 16)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at1 == 17)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at1 == 18)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 19)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank score " + atv1 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 20)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv1 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 21)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at1 == 22)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv1 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }
            else
            {
                monster_ui.m_uat1_label.GetComponent<UILabel>().text = "";
            }
            if (GlobalData.g_global.myCardList[cardindex].AT2 != 0)
            {
                int at2 = GlobalData.g_global.myCardList[cardindex].AT2;
                double atv2 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv2 + (XmlCtrl.x_xml.MCxml[index].AT2Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT2Add, 1);

                if (at2 == 1)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 2)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 3)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 4)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 5)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 6)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 7)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 8)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 9)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 10)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 11)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 12)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 13)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 14)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv2 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 15)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv2 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 16)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at2 == 17)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at2 == 18)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 19)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv2 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 20)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv2 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 21)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at2 == 22)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv2 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }
            else
            {
                monster_ui.m_uat2_label.GetComponent<UILabel>().text = "";
            }

            if (GlobalData.g_global.myCardList[cardindex].AT3 != 0)
            {
                int at3 = GlobalData.g_global.myCardList[cardindex].AT3;
                double atv3 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv3 + (XmlCtrl.x_xml.MCxml[index].AT3Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT3Add, 1);

                if (at3 == 1)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 2)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 3)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 4)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 5)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 6)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 7)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 8)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 9)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 10)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 11)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 12)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 13)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 14)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv3 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 15)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv3 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 16)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at3 == 17)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at3 == 18)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 19)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv3 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 20)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv3 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 21)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at3 == 22)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv3 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }

            else
            {
                monster_ui.m_uat3_label.GetComponent<UILabel>().text = "";
            }
        }

        else if (btnType == 6)
        {
            int upgradeLevel = 0;
            GlobalData.g_global.closeType = 100;
            for (int j = 0; j < GlobalData.g_global.myCardList.Count; j++)
            {
                if (monster_ui.upgradeobject == GlobalData.g_global.myCardList[j].cardno)
                {

                    if (GlobalData.g_global.myCardList[j].cardlevel == 15)
                    {
                        monster_ui.m_errorPopup.gameObject.SetActive(true);
                        monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                        iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                        iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

                        monster_ui.m_error_message.GetComponent<UILabel>().text = "You can't upgrade any more.";

                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = false;
                        }
                        lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/upgrade_panel");
                        buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                        for (int i = 0; i < buttons.Length; ++i)
                        {
                            buttons[i].enabled = false;
                        }
                        GlobalData.g_global.closeType = 20;
                        return;
                    }

                }
            }

            if (GlobalData.g_global.myInfo.coinAmount < int.Parse(monster_ui.m_upgrade_cost.GetComponent<UILabel>().text))
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

                monster_ui.m_error_message.GetComponent<UILabel>().text = "Coin is low.";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/upgrade_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 20;
                return;
            }
            else
            {
                monster_ui.m_upgrade_btn.GetComponent<BoxCollider>().enabled = false;
                GlobalData.g_global.m_setInfo.card_id = GlobalData.g_global.selectedCardId;
                GlobalData.g_global.m_setInfo.card_num = monster_ui.upgradeobject;
                GlobalData.g_global.m_setInfo.card_rank = monster_ui.upgradeobjectId;
                GlobalData.g_global.m_setInfo.sell_money = int.Parse(monster_ui.m_upgrade_cost.GetComponent<UILabel>().text);
                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (GlobalData.g_global.myCardList[i].cardno == monster_ui.upgradeobject)
                    {
                        upgradeLevel = GlobalData.g_global.myCardList[i].cardlevel + 1;
                        GlobalData.g_global.m_setInfo.card_level = GlobalData.g_global.myCardList[i].cardlevel + 1;
                    }
                }
                GlobalData.g_global.m_setInfo.stat = 0;
            }

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardMixIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardMixRequest);

            /// 141111
            monster_ui.callAni();

            GlobalData.g_global.myInfo.coinAmount -= int.Parse(monster_ui.m_upgrade_cost.GetComponent<UILabel>().text);

            GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item/gold_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();

            //monster_ui.m_upgrade_aLevel.gameObject.SetActive(false);

            for (int i = 0; i < xmlctrl.MRxml.Count; i++)
            {
                if (monster_ui.upgradeobjectId == xmlctrl.MRxml[i].Rank && xmlctrl.MRxml[i].Level == upgradeLevel)
                {
                    monster_ui.m_upgrade_cost.GetComponent<UILabel>().text = xmlctrl.MRxml[i].Enchant.ToString();
                    break;
                }
            }
            monster_ui.m_arrow.gameObject.SetActive(false);

            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.myCardList[i].cardno == monster_ui.upgradeobject)
                {
                    mCard = GlobalData.g_global.myCardList[i];

                    mCard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel + 1;

                    GlobalData.g_global.myCardList[i] = mCard;
                    monster_ui.m_upgrade_bLevel.GetComponent<UILabel>().text = "Lv" + GlobalData.g_global.m_setInfo.card_level.ToString();
                    
                    if (GlobalData.g_global.m_setInfo.card_level != 15)
                    {
                        monster_ui.m_arrow.gameObject.SetActive(true);
                        monster_ui.m_upgrade_aLevel.GetComponent<UILabel>().text = "Lv " + (GlobalData.g_global.m_setInfo.card_level + 1).ToString();
                    }
                    else
                    {
                        monster_ui.m_upgrade_aLevel.GetComponent<UILabel>().text = "";
                    }
                    monster_ui.m_upgrade_target.Find("Label").GetComponent<UILabel>().text = GlobalData.g_global.m_setInfo.card_level.ToString();
                }
            }

            int cardindex = monster_ui.upgradeobject;
            int index = 0;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (cardindex == GlobalData.g_global.myCardList[i].cardno)
                {
                    index = GlobalData.g_global.myCardList[i].cardId -1;
                    GlobalData.g_global.selectedCardId = index;
                    cardindex = i;
                    break;
                }
            }



            if (GlobalData.g_global.myCardList[cardindex].AT1 != 0)
            {
                int at1 = GlobalData.g_global.myCardList[cardindex].AT1;
                double atv1 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv1 + (XmlCtrl.x_xml.MCxml[index].AT1Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT1Add, 1);
                if (at1 == 1)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 2)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 3)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 4)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 5)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 6)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 7)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 8)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 9)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 10)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 11)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 12)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 13)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 14)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv1 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 15)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv1 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 16)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at1 == 17)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at1 == 18)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv1 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 19)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank score " + atv1 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 20)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv1 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at1 == 21)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at1 == 22)
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv1 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }
            else
            {
                monster_ui.m_uat1_label.GetComponent<UILabel>().text = "";
            }
            if (GlobalData.g_global.myCardList[cardindex].AT2 != 0)
            {
                int at2 = GlobalData.g_global.myCardList[cardindex].AT2;
                double atv2 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv2 + (XmlCtrl.x_xml.MCxml[index].AT2Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT2Add, 1);

                if (at2 == 1)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 2)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 3)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 4)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 5)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 6)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 7)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 8)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 9)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 10)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 11)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 12)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 13)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 14)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv2 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 15)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv2 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 16)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at2 == 17)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at2 == 18)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv2 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 19)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv2 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 20)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv2 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at2 == 21)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at2 == 22)
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv2 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }
            else
            {
                monster_ui.m_uat2_label.GetComponent<UILabel>().text = "";
            }

            if (GlobalData.g_global.myCardList[cardindex].AT3 != 0)
            {
                int at3 = GlobalData.g_global.myCardList[cardindex].AT3;
                double atv3 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv3 + (XmlCtrl.x_xml.MCxml[index].AT3Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                double atv_plus = Math.Round(XmlCtrl.x_xml.MCxml[index].AT3Add, 1);

                if (at3 == 1)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 2)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 3)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 4)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 5)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 6)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 7)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 8)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 9)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 10)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 11)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 12)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 13)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 14)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv3 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 15)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv3 + "%  add score [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 16)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at3 == 17)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else if (at3 == 18)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv3 + "% increase [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 19)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv3 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 20)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv3 + "% add coin [ff2e5e](+" + atv_plus + ")";
                }
                else if (at3 == 21)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add coin [ff2e5e](+" + atv_plus / 2 + " - " + atv_plus + ")";
                }
                else if (at3 == 22)
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add coin [ff2e5e](+" + atv_plus / 10 + " - " + atv_plus + ")";
                }
                else
                {
                    monster_ui.m_uat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv3 + "%  increase [ff2e5e](+" + atv_plus + ")";
                }
            }

            else
            {
                monster_ui.m_uat3_label.GetComponent<UILabel>().text = "";
            }

        }

        else if (btnType == 8)
        {
            monster_ui.m_mix_btn.GetComponent<BoxCollider>().enabled = true;



            if (GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardused != 0 && GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardused < 5)
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

                monster_ui.m_error_message.GetComponent<UILabel>().text = "Equipped cards can't be evolution";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 30;

            }

            else
            {
                monster_ui.m_panel_index = 99;
                GlobalData.g_global.listType = 2;
                monster_ui.mixobject = GlobalData.g_global.selectedCardNumber;
                monster_ui.mixobjectId = GlobalData.g_global.selectedCardId;

                Transform star;
                Transform name;
                Transform monster;
                Transform rare;
                Transform level_label;
                Transform lvbk;

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

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
                //monster_ui.m_infopopup.gameObject.SetActive(false);

                iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
                monster_ui.m_mixpanel.gameObject.SetActive(true);

                GameObject card = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/mix_panel/mix_target");

                star = card.transform.Find("star");
                name = card.transform.Find("name");
                monster = card.transform.Find("monster");
                rare = card.transform.Find("rare");
                level_label = card.transform.Find("Label");
                lvbk = card.transform.Find("lvbk");


                rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
                name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "name";

                star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank;
                monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;
                level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum].cardlevel.ToString();

                lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rare;



                monster_ui.m_mixcard_name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Name + "name";
                monster_ui.m_mixcard_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank + "_2";
                if (XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank == 6)
                {
                    monster_ui.m_mixcard_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank;
                }




                int cardindex = GlobalData.g_global.selectedCardNumber;
                int index = 0;
                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (cardindex == GlobalData.g_global.myCardList[i].cardno)
                    {
                        index = GlobalData.g_global.myCardList[i].cardId-1;
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
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "% increase";
                    }
                    else if (at1 == 2)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv1 + "% increase";
                    }
                    else if (at1 == 3)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv1 + "% increase";
                    }
                    else if (at1 == 4)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv1 + "% increase";
                    }
                    else if (at1 == 5)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv1 + "% increase";
                    }
                    else if (at1 == 6)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv1 + "% increase";
                    }
                    else if (at1 == 7)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv1 + "% increase";
                    }
                    else if (at1 == 8)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv1 + "% increase";
                    }
                    else if (at1 == 9)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv1 + "% increase";
                    }
                    else if (at1 == 10)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv1 + "% increase";
                    }
                    else if (at1 == 11)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv1 + "% increase";
                    }
                    else if (at1 == 12)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv1 + "% increase";
                    }
                    else if (at1 == 13)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv1 + "% increase";
                    }
                    else if (at1 == 14)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv1 + "%  add score";
                    }
                    else if (at1 == 15)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv1 + "%  add score";
                    }
                    else if (at1 == 16)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score";
                    }
                    else if (at1 == 17)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score";
                    }
                    else if (at1 == 18)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv1 + "% increase";
                    }
                    else if (at1 == 19)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank score " + atv1 + "% add coin";
                    }
                    else if (at1 == 20)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv1 + "% add coin";
                    }
                    else if (at1 == 21)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add coin";
                    }
                    else if (at1 == 22)
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv1 + "%  increase";
                    }
                }
                else
                {
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "";
                }
                if (GlobalData.g_global.myCardList[cardindex].AT2 != 0)
                {
                    int at2 = GlobalData.g_global.myCardList[cardindex].AT2;
                    double atv2 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv2 + (XmlCtrl.x_xml.MCxml[index].AT2Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                    if (at2 == 1)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv2 + "% increase";
                    }
                    else if (at2 == 2)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv2 + "% increase";
                    }
                    else if (at2 == 3)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv2 + "% increase";
                    }
                    else if (at2 == 4)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv2 + "% increase";
                    }
                    else if (at2 == 5)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv2 + "% increase";
                    }
                    else if (at2 == 6)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv2 + "% increase";
                    }
                    else if (at2 == 7)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv2 + "% increase";
                    }
                    else if (at2 == 8)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv2 + "% increase";
                    }
                    else if (at2 == 9)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv2 + "% increase";
                    }
                    else if (at2 == 10)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv2 + "% increase";
                    }
                    else if (at2 == 11)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv2 + "% increase";
                    }
                    else if (at2 == 12)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv2 + "% increase";
                    }
                    else if (at2 == 13)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv2 + "% increase";
                    }
                    else if (at2 == 14)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv2 + "%  add score";
                    }
                    else if (at2 == 15)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv2 + "%  add score";
                    }
                    else if (at2 == 16)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score";
                    }
                    else if (at2 == 17)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score";
                    }
                    else if (at2 == 18)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv2 + "% increase";
                    }
                    else if (at2 == 19)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv2 + "% add coin";
                    }
                    else if (at2 == 20)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv2 + "% add coin";
                    }
                    else if (at2 == 21)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add coin";
                    }
                    else if (at2 == 22)
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv2 + "%  increase";
                    }
                }
                else
                {
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "";
                }

                if (GlobalData.g_global.myCardList[cardindex].AT3 != 0)
                {
                    int at3 = GlobalData.g_global.myCardList[cardindex].AT3;
                    double atv3 = Math.Round(GlobalData.g_global.myCardList[cardindex].ATv3 + (XmlCtrl.x_xml.MCxml[index].AT3Add * (GlobalData.g_global.myCardList[cardindex].cardlevel - 1)), 1);
                    if (at3 == 1)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv3 + "% increase";
                    }
                    else if (at3 == 2)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv3 + "% increase";
                    }
                    else if (at3 == 3)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv3 + "% increase";
                    }
                    else if (at3 == 4)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]30coin " + atv3 + "% increase";
                    }
                    else if (at3 == 5)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]50coin " + atv3 + "% increase";
                    }
                    else if (at3 == 6)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv3 + "% increase";
                    }
                    else if (at3 == 7)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv3 + "% increase";
                    }
                    else if (at3 == 8)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv3 + "% increase";
                    }
                    else if (at3 == 9)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv3 + "% increase";
                    }
                    else if (at3 == 10)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv3 + "% increase";
                    }
                    else if (at3 == 11)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv3 + "% increase";
                    }
                    else if (at3 == 12)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv3 + "% increase";
                    }
                    else if (at3 == 13)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]골드ticket " + atv3 + "% increase";
                    }
                    else if (at3 == 14)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv3 + "%  add score";
                    }
                    else if (at3 == 15)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv3 + "%  add score";
                    }
                    else if (at3 == 16)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score";
                    }
                    else if (at3 == 17)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score";
                    }
                    else if (at3 == 18)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv3 + "% increase";
                    }
                    else if (at3 == 19)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv3 + "% add coin";
                    }
                    else if (at3 == 20)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv3 + "% add coin";
                    }
                    else if (at3 == 21)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add coin";
                    }
                    else if (at3 == 22)
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add coin";
                    }
                    else
                    {
                        monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv3 + "%  increase";
                    }
                }

                else
                {
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "";
                }


                for (int i = 0; i < xmlctrl.MRxml.Count; i++)
                {
                    if (xmlctrl.MCxml[monster_ui.mixobjectId].Rank == xmlctrl.MRxml[i].Rank && xmlctrl.MRxml[i].Level == 15)
                    {
                        monster_ui.m_mix_cost.GetComponent<UILabel>().text = xmlctrl.MRxml[i].Enchant.ToString();
                        break;
                    }
                }
            }

           

        }

        else if (btnType == 9)       // 141016
        {
            if (GlobalData.g_global.myInfo.coinAmount < int.Parse(monster_ui.m_mix_cost.GetComponent<UILabel>().text))
            {
                monster_ui.m_errorPopup.gameObject.SetActive(true);
                monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
                iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

                monster_ui.m_error_message.GetComponent<UILabel>().text = "coin이 부족합니다.";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/mix_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }
                GlobalData.g_global.closeType = 40;
                return;
            }


            GlobalData.g_global.m_setInfo.card_id = monster_ui.mixobjectId;
            GlobalData.g_global.m_setInfo.card_num = monster_ui.mixobject;
            GlobalData.g_global.m_setInfo.stat = 1;
            GlobalData.g_global.m_setInfo.card_level = 1;
            GlobalData.g_global.m_setInfo.card_exp = 0;
            GlobalData.g_global.m_setInfo.card_rank = 0;
            GlobalData.g_global.m_setInfo.sell_money = int.Parse(monster_ui.m_mix_cost.GetComponent<UILabel>().text);
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardMixIng;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardMixRequest);


        }

        else if (btnType == 11)
        {
            //sell complete

            if (tapType == 0)
            {


                GlobalData.g_global.m_setInfo.slot = 0;
                GlobalData.g_global.m_setInfo.card_num = GlobalData.g_global.selectedCardNumber;
                GlobalData.g_global.m_setInfo.stat = 0;
                // GlobalData.g_global.m_setInfo.sell_money = 0;
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardUseIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardUseRequest);

                monster_ui.soundObj = monster_ui.monsterUI.Find("sound");
                monster_ui.soundObj.GetComponent<AudioSource>().PlayOneShot(monster_ui.soundObj.GetComponent<AudioSource>().clip);

                MyCard temp = GlobalData.g_global.myCardList[GlobalData.g_global.selectedCardNum];

                GlobalData.g_global.myCardList.RemoveAt(GlobalData.g_global.selectedCardNum);

                GlobalData.g_global.myInfo.coinAmount += GlobalData.g_global.m_setInfo.sell_money;

                GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item/gold_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();

                CLctrl.cardListClear();
                CLctrl.cardList(1);

                //    monster_ui.m_sellpopup.gameObject.SetActive(false);
                monster_ui.m_sell_yes.gameObject.SetActive(false);
                monster_ui.m_sell_no.gameObject.SetActive(false);
                monster_ui.m_sell_check.gameObject.SetActive(true);
                monster_ui.m_sell_Label.GetComponent<UILabel>().text = "Selling has been completed.";

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

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
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }


            }

            else
            {
                //monster_ui.m_sellpopup.gameObject.SetActive(false);
                iTween.ScaleTo(monster_ui.m_sellpopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
            }
        }

        else if (btnType == 12)
        {
            //info complete
            // popup close 
            // monster_ui.m_infopopup.gameObject.SetActive(false);
            iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }

        else if (btnType == 15)
        {
            //button true



            if (GlobalData.g_global.closeType == 10)
            {
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

            }
            else if (GlobalData.g_global.closeType == 30)
            {
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

            }
            else if (GlobalData.g_global.closeType == 20)
            {
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/upgrade_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
            }
            else if (GlobalData.g_global.closeType == 40)
            {
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/mix_panel");
                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

            }


            iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            iTween.ScaleTo(monster_ui.m_upgradeComplete.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            monster_ui.m_mix_btn.GetComponent<BoxCollider>().enabled = false;
        }
        // close
        else if (btnType == 16)
        {
            iTween.ScaleTo(monster_ui.m_sellpopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            monster_ui.m_sell_Label.GetComponent<UILabel>().text = "Do you want to sell?";
            monster_ui.m_sell_check.gameObject.SetActive(false);
            monster_ui.m_sell_yes.gameObject.SetActive(true);
            monster_ui.m_sell_no.gameObject.SetActive(true);
            iTween.ScaleTo(monster_ui.m_infopopup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/infopopup_panel");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
            buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }

        else
        {
           // Socket_Ctrl.sCtrl.closeSocket();
            monster_ui.m_panel_index = 0;
            GlobalData.g_global.listType = 1;
            monster_ui.m_upgrade_cost.GetComponent<UILabel>().text = "0";


            if (tapType == 1)
            {
                GlobalData.g_global.invite_able = true;

                for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
                {
                    if (GlobalData.g_global.myCardList[i].cardused == 1)
                    {
                        GlobalData.g_global.playCardIndex = 1;
                        GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                        break;
                    }
                    else if (GlobalData.g_global.myCardList[i].cardused == 2)
                    {
                        if (GlobalData.g_global.playCardIndex == 1)
                        {
                            break;
                        }
                        GlobalData.g_global.playCardIndex = 2;
                        GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                    }
                    else if (GlobalData.g_global.myCardList[i].cardused == 3)
                    {
                        if (GlobalData.g_global.playCardIndex != 0 && GlobalData.g_global.playCardIndex < 3)
                        {
                            continue;
                        }
                        GlobalData.g_global.playCardIndex = 3;
                        GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                    }
                    else if (GlobalData.g_global.myCardList[i].cardused == 4)
                    {
                        if (GlobalData.g_global.playCardIndex != 0 && GlobalData.g_global.playCardIndex < 4)
                        {
                            continue;
                        }
                        GlobalData.g_global.playCardIndex = 4;
                        GlobalData.g_global.myPlayCard = GlobalData.g_global.myCardList[i].cardId;
                    }
                    if (i == GlobalData.g_global.myCardList.Count - 1 && GlobalData.g_global.playCardIndex == 0)
                    {
                        GlobalData.g_global.myPlayCard = 0;
                    }
                }


                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item");

                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase");

                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (monster_ui.inputindex_flag[i] == 1)
                    {
                        GameObject destroyObject = monster_ui.inputindex[i].Find("input_MonsterCard(Clone)").gameObject;

                        Destroy(destroyObject);
                        monster_ui.inputindex_flag[i] = 0;
                    }
                }
                GameObject cardSlot = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase/layer1/cardSlot");
                cardSlot.GetComponent<LobbyCardSlot>().defaultSet();

                //monster_ui.monsterUI.gameObject.SetActive(false);
                iTween.ScaleTo(monster_ui.monsterUI.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            }

            else
            {
                //monster_ui.m_upgrade_aLevel.gameObject.SetActive(false);
                monster_ui.m_upgrade_btn.gameObject.SetActive(false);
                monster_ui.m_mixpanel.gameObject.SetActive(false);

                monster_ui.m_upgradepanel.gameObject.SetActive(false);
                monster_ui.m_inputpanel.gameObject.SetActive(true);
                lobbySceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster");

                buttons = lobbySceneUI.GetComponentsInChildren<BoxCollider>();

                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = true;
                }
                /*
                for (int i = 0; i < 4; i++)
                {
                    if (monster_ui.inputindex_flag[i] == 1)
                    {
                        GameObject destroyObject = monster_ui.inputindex[i].FindChild("input_MonsterCard(Clone)").gameObject;
                        Destroy(destroyObject);
                        monster_ui.inputindex_flag[i] = 0;
                    }
                }
                */

                for (int i = 0; i < 10; i++)
                {
                    monster_ui.upgradeMe[i] = 0;
                    monster_ui.upgradeExp[i] = 0;
                }

                if (monster_ui.upgradeobject != 0)
                {
                    GlobalData.g_global.selectedCardNumber = monster_ui.upgradeobject;
                    monster_ui.upgradeobject = 0;
                }

                if (monster_ui.mixobject != 0)
                {
                    GlobalData.g_global.selectedCardNumber = monster_ui.mixobject;
                    monster_ui.mixobject = 0;
                }
                monster_ui.mix_flag = 0;
                CLctrl.cardListClear();
                CLctrl.cardList(2);

            }
        }
    }


    private IEnumerator clickSet()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 v;
        v = transform.position;
        v.x = 0;
        v.y = 0;
        v.z = 0;
        monster_ui.m_input_click.transform.localPosition = v;
        StartCoroutine("clickSet2");
    }
    private IEnumerator clickSet2()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 v;
        v = transform.position;
        v.x = 1000f;
        v.y = 0;
        v.z = 0;
        monster_ui.m_input_click.transform.localPosition = v;

        StartCoroutine("clickSet");
    }
}
