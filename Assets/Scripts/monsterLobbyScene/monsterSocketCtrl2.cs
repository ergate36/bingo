using UnityEngine;
using System.Collections;
using System;

public class monsterSocketCtrl2 : MonoBehaviour
{

    private monsterSceneUI2 monster_ui;

    private GameObject popup;
    private Transform popupup;
    private GameObject lobbySceneUI;
    private BoxCollider[] buttons;
    private CardListCtrl2 CLctrl;
    GameObject root;
    private MyCard mCard;
    private XmlCtrl xmlctrl;
    private materialInfo m_mater;
    private int a_id;
    private int a_index;

    // Use this for initialization
    void Start()
    {
        monster_ui = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster").GetComponent<monsterSceneUI2>();
        xmlctrl = GameObject.Find("GlobalObject").GetComponent<XmlCtrl>();
        root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
        CLctrl = root.GetComponent<CardListCtrl2>();
    }

    // Update is called once per frame

    void Update()
    {

        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMonsterCardMixIngComplete && GlobalData.g_global.m_setInfo.stat == 1)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (GlobalData.g_global.myCardList[i].cardno == monster_ui.mixobject)
                {
                    a_index = i;
                    int id = GlobalData.g_global.mixResultId;
                    a_id = id -1;
                    mCard = GlobalData.g_global.myCardList[i];
                    mCard.cardno = monster_ui.mixobject;
                    mCard.cardused = 0;
                    mCard.cardlevel = 1;
                    mCard.cardId = id;
                    mCard.cardexp = 0;
                    mCard.cardRare = xmlctrl.MCxml[id - 1].Rare;
                    mCard.cardRank = xmlctrl.MCxml[id - 1].Rank;

                    mCard.AT1 = xmlctrl.MCxml[id - 1].Ability1;
                    mCard.AT2 = xmlctrl.MCxml[id - 1].Ability2;
                    mCard.AT3 = xmlctrl.MCxml[id - 1].Ability3;
                    mCard.ATv1 = xmlctrl.MCxml[id - 1].Lv1ability1value;
                    mCard.ATv2 = xmlctrl.MCxml[id - 1].Lv1ability2value;
                    mCard.ATv3 = xmlctrl.MCxml[id - 1].Lv1ability3value;

                    GlobalData.g_global.myCardList[i] = mCard;
                    break;
                }
            }
            GameObject card = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/mix_panel/mix_target");

            Transform star = card.transform.Find("star");
            Transform name = card.transform.Find("name");
            Transform monster = card.transform.Find("monster");
            Transform rare = card.transform.Find("rare");
            Transform level_label = card.transform.Find("Label");
            Transform lvbk = card.transform.Find("lvbk");


            rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[a_id].Rare;
            name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[a_id].Name + "name";

            star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[a_id ].Rank;
            monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[a_id].Name + "_" + XmlCtrl.x_xml.MCxml[a_id].Rare;
            level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[a_index].cardlevel.ToString();

            lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[a_id].Rare;



            monster_ui.m_mixcard_name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[a_id ].Name + "name";
            monster_ui.m_mixcard_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[a_id ].Rank + "_2";
            if (XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank == 6)
            {
                monster_ui.m_mixcard_rank.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[a_id ].Rank;
            }



            monster_ui.m_errorPopup.gameObject.SetActive(true);
            monster_ui.m_errorPopup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(monster_ui.m_errorPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", monster_ui.m_errorPopup.gameObject));

            monster_ui.m_error_message.GetComponent<UILabel>().text = "Evolution has been completed.";
            monster_ui.soundObj2 = monster_ui.monsterUI.Find("sound_bell");
            monster_ui.soundObj2.GetComponent<AudioSource>().PlayOneShot(monster_ui.soundObj2.GetComponent<AudioSource>().clip);
            GlobalData.g_global.closeType = 10;





            int cardindex = GlobalData.g_global.selectedCardNumber;
            int index = 0;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
            {
                if (cardindex == GlobalData.g_global.myCardList[i].cardno)
                {
                    index = GlobalData.g_global.myCardList[i].cardId - 1;
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
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "%  increase";
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
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]rank score " + atv1 + "%  add score";
                }
                else if (at1 == 15)
                {
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]game score " + atv1 + "%  add score";
                }
                else if (at1 == 16)
                {
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score";
                }
                else if (at1 == 17)
                {
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score";
                }
                else if (at1 == 18)
                {
                    monster_ui.m_mat1_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]monster score " + atv1 + "% increase";
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
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]rank score " + atv2 + "%  add score";
                }
                else if (at2 == 15)
                {
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]game score " + atv2 + "%  add score";
                }
                else if (at2 == 16)
                {
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score";
                }
                else if (at2 == 17)
                {
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score";
                }
                else if (at2 == 18)
                {
                    monster_ui.m_mat2_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]monster score " + atv2 + "% increase";
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
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]rank score " + atv3 + "%  add score";
                }
                else if (at3 == 15)
                {
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]game score " + atv3 + "%  add score";
                }
                else if (at3 == 16)
                {
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score";
                }
                else if (at3 == 17)
                {
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score";
                }
                else if (at3 == 18)
                {
                    monster_ui.m_mat3_label.GetComponent<UILabel>().text = "[52E4DC]Score bonus : [ffffff]monster score " + atv3 + "% increase";
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




            //이펙트
        }

    }
}
