using UnityEngine;
using System.Collections;
public class CardListCtrl2 : MonoBehaviour
{

    public Transform gridRoot;
    public UIScrollView sv;
    private monsterSceneUI2 monster_ui = null;
    public UIGrid grid;
    public UIPanel panel;
    private GameObject root2;
    GameObject root;
    GameObject ds;
    int cardcount = 0;
    Transform star;
    Transform name;
    Transform monster;
    Transform rare;
    Transform level_label;
    Transform check;
    Transform lvbk;
    // Use this for initialization
    void Awake()
    {
        monster_ui = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster").GetComponent<monsterSceneUI2>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void cardListClear()
    {
        GlobalData.g_global.listCardindex = 0;
        GameObject root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List/Grid");
        grid.repositionNow = true;
        cardcount = 0;
        foreach (Transform card in root.transform)
        {
            Destroy(card.gameObject);
        }
    }

    public void cardList(int tag)
    {
        if (GlobalData.g_global.myCardList.Count == 0)
        {
            return;
        }

        ///////////  1 = 모든 리스트 , 2 = 팝업오픈   3 = 강화 ( use 빼기 ) ,4 = 강화재료 등록

        MyCard cardtemp = new MyCard();

        if (tag == 1)
        {
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();
                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;

                if (tempcard.cardno == 0)
                {
                    continue;
                }

                if (tempcard.cardused > 100)
                {
                    tempcard.cardused = 0;
                    cardtemp = GlobalData.g_global.myCardList[i];
                    cardtemp.cardused = 0;
                    GlobalData.g_global.myCardList[i] = cardtemp;
                }
                setCard(tempcard);
                cardcount++;
            }
        }

        else if (tag == 2)
        {
            StopCoroutine("clickSet2");
            StopCoroutine("clickSet");

            monster_ui.m_input_click.gameObject.SetActive(false);

            for (int i = 1; i <= 4; i++)
            {
                root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel/input_" + i);

                foreach (Transform card in root.transform)
                {
                    DestroyImmediate(card.gameObject);
                }
            }

            GlobalData.g_global.selectedCardId = GlobalData.g_global.myCardList[0].cardId;
            GlobalData.g_global.selectedCardNumber = GlobalData.g_global.myCardList[0].cardno;

            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;

                if (tempcard.cardno == 0)
                {
                    continue;
                }

                if (tempcard.cardused > 100)
                {
                    tempcard.cardused = 0;

                    cardtemp = GlobalData.g_global.myCardList[i];
                    cardtemp.cardused = 0;
                    GlobalData.g_global.myCardList[i] = cardtemp;
                }

                setCard(tempcard);
                cardcount++;

                if (tempcard.cardused != 0)
                {
                    monster_ui.inputindex_flag[tempcard.cardused - 1] = 1;
                    monster_ui.inputindex_cardnum[tempcard.cardused - 1] = tempcard.cardno;
                    //  GlobalData.g_global.selectedCardNum = i;
                    //  GlobalData.g_global.selectedCardId = tempcard.cardId;
                    setoutputCard(tempcard, tempcard.cardused);
                }
            }
        }

        else if (tag == 3)
        {
            int firstindex = 0;
            int firstid = 0;
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;

                if (tempcard.cardno == 0)
                {
                    continue;
                }
                if (tempcard.cardno == monster_ui.upgradeobject)
                {
                    continue;
                }
                if (tempcard.cardused != 0)
                {
                    continue;
                }
                if (tempcard.cardused > 100)
                {
                    tempcard.cardused = 0;
                    cardtemp = GlobalData.g_global.myCardList[i];
                    cardtemp.cardused = 0;
                    GlobalData.g_global.myCardList[i] = cardtemp;
                }
                if (cardcount == 0)
                {
                    firstid = tempcard.cardId;
                    firstindex = tempcard.cardno;
                }
                cardcount++;

                setCard(tempcard);
            }

            GlobalData.g_global.selectedCardId = firstid;
            GlobalData.g_global.selectedCardNumber = firstindex;
        }
        else if (tag == 4)
        {

            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;

                if (tempcard.cardno == 0)
                {
                    continue;
                }

                if (tempcard.cardno == monster_ui.upgradeobject)
                {
                    continue;
                }

                if (tempcard.cardused == 0 || tempcard.cardused > 100)
                {
                    cardcount++;
                    setCard(tempcard);
                }
            }
        }


        else if (tag == 5)
        {
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;


                if (tempcard.cardno == 0)
                {
                    continue;
                }
                if (tempcard.cardno == monster_ui.mixobject)
                {
                    continue;
                }
                if (tempcard.cardused != 0)
                {
                    continue;
                }
                if (XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank == XmlCtrl.x_xml.MCxml[tempcard.cardId - 1].Rank)
                {
                    if (tempcard.cardlevel == 30)
                    {
                        if (cardcount == 0)
                        {
                            GlobalData.g_global.selectedCardId = tempcard.cardId;
                            GlobalData.g_global.selectedCardNumber = tempcard.cardno;
                        }
                        cardcount++;
                        setCard(tempcard);

                    }
                }
                //continue;
            }
        }

        else if (tag == 6)
        {
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;
                if (tempcard.cardno == 0)
                {
                    continue;
                }
                if (tempcard.cardno == monster_ui.mixobject)
                {
                    continue;
                }

                if (tempcard.cardused != 0 && tempcard.cardused < 100)
                {
                    continue;
                }

                if (XmlCtrl.x_xml.MCxml[GlobalData.g_global.selectedCardId - 1].Rank == XmlCtrl.x_xml.MCxml[tempcard.cardId - 1].Rank && tempcard.cardlevel == 30)
                {
                    cardcount++;
                    setCard(tempcard);
                }

                //continue;
            }
        }
        else
        {
            for (int i = 0; i < GlobalData.g_global.myCardList.Count; ++i)
            {
                MyCard tempcard = new MyCard();

                tempcard.cardno = GlobalData.g_global.myCardList[i].cardno;
                tempcard.cardId = GlobalData.g_global.myCardList[i].cardId;
                tempcard.cardexp = GlobalData.g_global.myCardList[i].cardexp;
                tempcard.cardlevel = GlobalData.g_global.myCardList[i].cardlevel;
                tempcard.cardused = GlobalData.g_global.myCardList[i].cardused;
                tempcard.cardRank = GlobalData.g_global.myCardList[i].cardRank;
                tempcard.cardRare = GlobalData.g_global.myCardList[i].cardRare;

                tempcard.AT1 = GlobalData.g_global.myCardList[i].AT1;
                tempcard.AT2 = GlobalData.g_global.myCardList[i].AT2;
                tempcard.AT3 = GlobalData.g_global.myCardList[i].AT3;
                tempcard.ATv1 = GlobalData.g_global.myCardList[i].ATv1;
                tempcard.ATv2 = GlobalData.g_global.myCardList[i].ATv2;
                tempcard.ATv3 = GlobalData.g_global.myCardList[i].ATv3;

                if (tempcard.cardno == 0)
                {
                    continue;
                }
                if (tempcard.cardused != 0)
                {
                    continue;
                }
                if (tempcard.cardused > 100)
                {
                    tempcard.cardused = 0;

                    cardtemp = GlobalData.g_global.myCardList[i];
                    cardtemp.cardused = 0;
                    GlobalData.g_global.myCardList[i] = cardtemp;
                }
                if (tempcard.cardlevel == 30)
                {
                    cardcount++;
                    setCard(tempcard);
                }
            }
        }

        /*
        if(cardcount == 0){
            GlobalData.g_global.myCardList.Clear();
        }
        */

        GameObject rootcount = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/layer_base") as GameObject;
        Transform m_cardCount = rootcount.transform.Find("card_count");
        m_cardCount.GetComponent<UILabel>().text = cardcount.ToString() + " / 30";

        grid.Reposition();
        sv.ResetPosition();
        panel.Refresh();

    }
    public void reFresh()
    {
        grid.repositionNow = true;
    }

    public GameObject setCard(MyCard Cardinfo)
    {
        GameObject cardObject = Instantiate(Resources.Load("card/list_MonsterCard2")) as GameObject;
        cardObject.GetComponent<myCardlist2>().setCardCtrl(Cardinfo);

        Vector3 scale = cardObject.transform.localScale;
        cardObject.transform.parent = gridRoot.transform;
        cardObject.transform.localPosition = Vector3.zero;
        cardObject.transform.localScale = scale;
        cardObject.GetComponent<selectCard2>().cardnum = GlobalData.g_global.listCardindex;
        cardObject.GetComponent<selectCard2>().cardid = Cardinfo.cardId;
        cardObject.GetComponent<selectCard2>().cardNumber = Cardinfo.cardno;
        GlobalData.g_global.listCardindex++;
        if (Cardinfo.cardused != 0)
        {  // 사용중
            cardObject.GetComponent<selectCard2>().cardflag = 0;
        }
        else
        {
            cardObject.GetComponent<selectCard2>().cardflag = 1;
        }

        cardObject.GetComponent<UIDragScrollView>().scrollView = sv;

        return cardObject;
    }

    public void setoutputCard(MyCard cardobject, int setindex)
    {
        GameObject setcard = Instantiate(Resources.Load("card/input_MonsterCard")) as GameObject;
        monster_ui.inputindex_cardnum[setindex - 1] = cardobject.cardno;
        monster_ui.inputindex_flag[setindex - 1] = 1;
        Vector3 scals = setcard.transform.localScale;
        setcard.transform.parent = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel/input_" + setindex).transform;
        setcard.transform.localPosition = Vector3.zero;
        setcard.transform.localScale = scals;

        GameObject root3 = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/input_panel/input_" + setindex) as GameObject;

        star = root3.transform.Find("input_MonsterCard(Clone)/star");
        name = root3.transform.Find("input_MonsterCard(Clone)/name");
        monster = root3.transform.Find("input_MonsterCard(Clone)/monster");
        rare = root3.transform.Find("input_MonsterCard(Clone)/rare");
        level_label = root3.transform.Find("input_MonsterCard(Clone)/Label");
        lvbk = root3.transform.Find("input_MonsterCard(Clone)/lvbk");

        rare.GetComponent<UISprite>().spriteName = "card" + XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Rare;
        name.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Name + "name";
        star.GetComponent<UISprite>().spriteName = "star" + XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Rank;
        monster.GetComponent<UISprite>().spriteName = XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Name + "_" + XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Rare;
        lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + XmlCtrl.x_xml.MCxml[cardobject.cardId - 1].Rare;

        for (int i = 0; i < GlobalData.g_global.myCardList.Count; i++)
        {
            if (cardobject.cardno == GlobalData.g_global.myCardList[i].cardno)
            {
                level_label.GetComponent<UILabel>().text = GlobalData.g_global.myCardList[i].cardlevel.ToString();
                break;
            }
        }
    }
}
