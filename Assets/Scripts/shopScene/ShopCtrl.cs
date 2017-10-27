using UnityEngine;
using System.Collections;
using System;

public class ShopCtrl : MonoBehaviour
{

    private Transform[] btn_Taps;

    private Transform layerScollView;
    private Transform gridObject;

    private UIScrollView scrollView;

    private Transform rootPanel;

    private Transform popup_confirm;
    private Transform popup_confirm_back;

    private Transform[] label_myInfo;
    private Transform popup_monsterBuy;
    public Transform cardpopup;

    [HideInInspector]
    public Shop.TapType currentTapType = Shop.TapType.Shop_Gem;
    [HideInInspector]
    public int currentitemIndex = -1;
    public Transform popup_error;

    private Transform soundObj;
    private ShopCtrl shopCtrl;
    void Awake()
    {

        GameObject shopRoot = GameObject.Find("popup_shop");
        shopCtrl = shopRoot.GetComponent<ShopCtrl>();

        rootPanel = transform.Find("root_panel");

        gridObject = rootPanel.Find("layer_srollview/Grid");
        layerScollView = rootPanel.Find("layer_srollview");

        scrollView = layerScollView.GetComponent<UIScrollView>();

        popup_confirm = transform.Find("confirm_panel");
        popup_confirm_back = transform.Find("confirm_panel_back");

        btn_Taps = new Transform[4];
        btn_Taps[(int)Shop.TapType.Shop_Gem] = rootPanel.Find("layer_tap/btn_gem");
        btn_Taps[(int)Shop.TapType.Shop_Gold] = rootPanel.Find("layer_tap/btn_coin");
        btn_Taps[(int)Shop.TapType.Shop_BingoTicket] = rootPanel.Find("layer_tap/btn_ticket");
        btn_Taps[(int)Shop.TapType.Shop_Card] = rootPanel.Find("layer_tap/btn_goldticket");

        label_myInfo = new Transform[4];
        label_myInfo[(int)Shop.TapType.Shop_Gem] = rootPanel.Find("layer_base/label_gem");
        label_myInfo[(int)Shop.TapType.Shop_Gold] = rootPanel.Find("layer_base/label_gold");
        label_myInfo[(int)Shop.TapType.Shop_BingoTicket] = rootPanel.Find("layer_base/label_ticket");
        label_myInfo[(int)Shop.TapType.Shop_Card] = rootPanel.Find("layer_base/label_goldticket");

        popup_monsterBuy = transform.Find("popup_MonsterBuy");

        soundObj = transform.Find("sound");

        //씬불러올때 초기화 시킴
        currentitemIndex = 0;
        currentTapType = 0;
    }

    void Start()
    {

    }

    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mItemShopIngComplete && GlobalData.g_global.shopshop == 1)
        {
            //해당 상품에 대한 값 increase 
            //Socket_Ctrl.sCtrl.closeSocket();
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            if (currentTapType == Shop.TapType.Shop_BingoTicket)
            {
                if (currentitemIndex == 0)
                {
                    GlobalData.g_global.myInfo.coinAmount = GlobalData.g_global.myInfo.coinAmount - Shop.ticketCondition[(int)currentitemIndex];
                }
                else
                {
                    GlobalData.g_global.myInfo.gemCount = GlobalData.g_global.myInfo.gemCount - Shop.ticketCondition[(int)currentitemIndex];
                }
                GlobalData.g_global.myInfo.ticketCount = GlobalData.g_global.myInfo.ticketCount + Shop.ticketAmount[(int)currentitemIndex] + Shop.ticketBonus[(int)currentitemIndex];

            }

            else if (currentTapType == Shop.TapType.Shop_Gem)
            {
                GlobalData.g_global.myInfo.gemCount = GlobalData.g_global.myInfo.gemCount + Shop.gemItemCount[currentitemIndex] + Shop.jamBonus[currentitemIndex];

                GlobalData.g_global.mail_index = 3;

                GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailGetIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailGetRequest);


            }

            else if (currentTapType == Shop.TapType.Shop_Gold)
            {
                GlobalData.g_global.myInfo.gemCount = GlobalData.g_global.myInfo.gemCount - Shop.coinCondition[(int)currentitemIndex];
                GlobalData.g_global.myInfo.coinAmount = GlobalData.g_global.myInfo.coinAmount + Shop.goldItemCount[currentitemIndex] + Shop.coinBonus[currentitemIndex];
            }

            setMyInfo();

            //soundObj.audio.PlayOneShot(soundObj.audio.clip);

            Transform text_ment = popup_confirm.Find("text_ment");
            Transform btn_buy = popup_confirm.Find("btn_buy");
            Transform btn_exit = popup_confirm.Find("btn_exit");
            Transform btn_check = popup_confirm.Find("btn_check");

            text_ment.GetComponent<UILabel>().text = "Buy has been completed.";

            btn_buy.gameObject.SetActive(false);
            btn_exit.gameObject.SetActive(false);
            btn_check.gameObject.SetActive(true);
        }

        else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMonsterGachaIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            GlobalData.g_global.cardCount++;
            //soundObj.audio.PlayOneShot(soundObj.audio.clip);
            if (currentitemIndex == 0)
            {
                GlobalData.g_global.myInfo.coinAmount -= 3000;
            }
            else if (currentitemIndex == 1)
            {
                GlobalData.g_global.myInfo.gemCount -= 30;
            }
            else if (currentitemIndex == 2)
            {
                GlobalData.g_global.myInfo.gemCount -= 250;
            }

            setMyInfo();
            //Socket_Ctrl.sCtrl.closeSocket();
            int id = GlobalData.g_global.monsterGachaId;

            popup_confirm.gameObject.SetActive(false);
            // popup_confirm.transform.GetComponent<Popup_Effector>().unactivePopup(0.5f);
            popup_monsterBuy.gameObject.SetActive(true);
            Transform eff_card = popup_monsterBuy.Find("check_btn");
            eff_card.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(waitTime(eff_card));
            int at1 = XmlCtrl.x_xml.MCxml[id - 1].Ability1;
            int at2 = XmlCtrl.x_xml.MCxml[id - 1].Ability2;
            int at3 = XmlCtrl.x_xml.MCxml[id - 1].Ability3;
            float ab1v1 = XmlCtrl.x_xml.MCxml[id - 1].Lv1ability1value;
            float ab1v2 = XmlCtrl.x_xml.MCxml[id - 1].Lv1ability2value;
            float ab1v3 = XmlCtrl.x_xml.MCxml[id - 1].Lv1ability3value;
            float ab30v1 = XmlCtrl.x_xml.MCxml[id - 1].AT1Add;
            float ab30v2 = XmlCtrl.x_xml.MCxml[id - 1].AT2Add;
            float ab30v3 = XmlCtrl.x_xml.MCxml[id - 1].AT3Add;
            string Name = XmlCtrl.x_xml.MCxml[id - 1].Name;
            int Rank = XmlCtrl.x_xml.MCxml[id - 1].Rank;
            int Rare = XmlCtrl.x_xml.MCxml[id - 1].Rare;

            MyCard myCard = new MyCard();
            id = id - 1;
            myCard.cardused = 0;
            myCard.cardId = id;
            myCard.cardlevel = 1;
            myCard.cardno = GlobalData.g_global.monsterGachaKey;
            myCard.cardexp = 0;
            myCard.cardRank = XmlCtrl.x_xml.MCxml[id].Rank;
            myCard.cardRare = XmlCtrl.x_xml.MCxml[id].Rare;
            myCard.AT1 = XmlCtrl.x_xml.MCxml[id].Ability1;
            myCard.AT2 = XmlCtrl.x_xml.MCxml[id].Ability2;
            myCard.AT3 = XmlCtrl.x_xml.MCxml[id].Ability3;
            myCard.ATv1 = XmlCtrl.x_xml.MCxml[id].Lv1ability1value + XmlCtrl.x_xml.MCxml[id].AT1Add;
            myCard.ATv2 = XmlCtrl.x_xml.MCxml[id].Lv1ability2value + XmlCtrl.x_xml.MCxml[id].AT2Add;
            myCard.ATv3 = XmlCtrl.x_xml.MCxml[id].Lv1ability3value + XmlCtrl.x_xml.MCxml[id].AT3Add;

            GlobalData.g_global.myCardList.Add(myCard);


            double atv1 = Math.Round(XmlCtrl.x_xml.MCxml[id].Lv1ability1value, 1);

            Transform m_at1_label = cardpopup.transform.Find("at1_label");
            Transform m_at2_label = cardpopup.transform.Find("at2_label");
            Transform m_at3_label = cardpopup.transform.Find("at3_label");

            Transform star;
            Transform name;
            Transform monster;
            Transform rare;
            Transform level_label;
            Transform lvbk;
            Transform m_name;
            Transform m_star;
            m_star = cardpopup.transform.Find("star");
            m_name = cardpopup.transform.Find("name");

            m_name.GetComponent<UISprite>().spriteName = Name + "name";

            m_star.GetComponent<UISprite>().spriteName = "star" + Rank+"_2";
            if(Rank ==6){
                m_star.GetComponent<UISprite>().spriteName = "star" + Rank;
            }

            star = cardpopup.transform.Find("list_MonsterCard/star");

            name = cardpopup.transform.Find("list_MonsterCard/name");

            monster = cardpopup.transform.Find("list_MonsterCard/monster");

            rare = cardpopup.transform.Find("list_MonsterCard/rare");

            level_label = cardpopup.transform.Find("list_MonsterCard/Label");
            lvbk = cardpopup.transform.Find("list_MonsterCard/lvbk");


            rare.GetComponent<UISprite>().spriteName = "card" + Rare;
            name.GetComponent<UISprite>().spriteName = Name + "name";

            star.GetComponent<UISprite>().spriteName = "star" + Rank;
            monster.GetComponent<UISprite>().spriteName = Name + "_" + Rare;
            lvbk.GetComponent<UISprite>().spriteName = "lvbk_" + Rare;
            level_label.GetComponent<UILabel>().text = "1";

            if (at1 != 0)
            {
                if (at1 == 1)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv1 + "% increase";
                }
                else if (at1 == 2)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv1 + "% increase";
                }
                else if (at1 == 3)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv1 + "% increase";
                }
                else if (at1 == 4)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin30 " + atv1 + "% increase";
                }
                else if (at1 == 5)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin50 " + atv1 + "% increase";
                }
                else if (at1 == 6)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv1 + "% increase";
                }
                else if (at1 == 7)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv1 + "% increase";
                }
                else if (at1 == 8)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv1 + "% increase";
                }
                else if (at1 == 9)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv1 + "% increase";
                }
                else if (at1 == 10)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv1 + "% increase";
                }
                else if (at1 == 11)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv1 + "% increase";
                }
                else if (at1 == 12)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv1 + "% increase";
                }
                else if (at1 == 13)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]cointicket " + atv1 + "% increase";
                }
                else if (at1 == 14)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv1 + "%  add score";
                }
                else if (at1 == 15)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv1 + "%  add score";
                }
                else if (at1 == 16)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add score";
                }
                else if (at1 == 17)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add score";
                }
                else if (at1 == 18)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv1 + "% increase";
                }
                else if (at1 == 19)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank score " + atv1 + "% add coin";
                }
                else if (at1 == 20)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv1 + "% add coin";
                }
                else if (at1 == 21)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 2 + " - " + atv1 + " random add coin";
                }
                else if (at1 == 22)
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv1 / 10 + " - " + atv1 + " random add coin";
                }
                else
                {
                    m_at1_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv1 + "%  increase";
                }
            }
            else
            {
                m_at1_label.GetComponent<UILabel>().text = "";
            }
            if (at2 != 0)
            {
                double atv2 = Math.Round(XmlCtrl.x_xml.MCxml[id].Lv1ability2value, 1);

                if (at2 == 1)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv2 + "% increase";
                }
                else if (at2 == 2)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv2 + "% increase";
                }
                else if (at2 == 3)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv2 + "% increase";
                }
                else if (at2 == 4)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin30 " + atv2 + "% increase";
                }
                else if (at2 == 5)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin50 " + atv2 + "% increase";
                }
                else if (at2 == 6)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv2 + "% increase";
                }
                else if (at2 == 7)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv2 + "% increase";
                }
                else if (at2 == 8)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv2 + "% increase";
                }
                else if (at2 == 9)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv2 + "% increase";
                }
                else if (at2 == 10)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv2 + "% increase";
                }
                else if (at2 == 11)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv2 + "% increase";
                }
                else if (at2 == 12)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv2 + "% increase";
                }
                else if (at2 == 13)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]cointicket " + atv2 + "% increase";
                }
                else if (at2 == 14)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv2 + "%  add score";
                }
                else if (at2 == 15)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv2 + "%  add score";
                }
                else if (at2 == 16)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add score";
                }
                else if (at2 == 17)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add score";
                }
                else if (at2 == 18)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv2 + "% increase";
                }
                else if (at2 == 19)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv2 + "% add coin";
                }
                else if (at2 == 20)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv2 + "% add coin";
                }
                else if (at2 == 21)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 2 + " - " + atv2 + " random add coin";
                }
                else if (at2 == 22)
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv2 / 10 + " - " + atv2 + " random add coin";
                }
                else
                {
                    m_at2_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv2 + "%  increase";
                }
            }
            else
            {
                m_at2_label.GetComponent<UILabel>().text = "";
            }

            if (at3 != 0)
            {
                double atv3 = Math.Round(XmlCtrl.x_xml.MCxml[id].Lv1ability3value, 1);
                if (at3 == 1)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freedaub " + atv3 + "% increase";
                }
                else if (at3 == 2)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]doubledaub " + atv3 + "% increase";
                }
                else if (at3 == 3)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]autodaub " + atv3 + "% increase";
                }
                else if (at3 == 4)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin30 " + atv3 + "% increase";
                }
                else if (at3 == 5)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]coin50 " + atv3 + "% increase";
                }
                else if (at3 == 6)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]directbingo " + atv3 + "% increase";
                }
                else if (at3 == 7)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]ticket " + atv3 + "% increase";
                }
                else if (at3 == 8)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]freeze " + atv3 + "% increase";
                }
                else if (at3 == 9)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]blind " + atv3 + "% increase";
                }
                else if (at3 == 10)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]sheetswap " + atv3 + "% increase";
                }
                else if (at3 == 11)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]mixsheet " + atv3 + "% increase";
                }
                else if (at3 == 12)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]defence " + atv3 + "% increase";
                }
                else if (at3 == 13)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]Probability of Item : [ffffff]cointicket " + atv3 + "% increase";
                }
                else if (at3 == 14)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]rank score " + atv3 + "%  add score";
                }
                else if (at3 == 15)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]game score " + atv3 + "%  add score";
                }
                else if (at3 == 16)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add score";
                }
                else if (at3 == 17)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add score";
                }
                else if (at3 == 18)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]score bonus : [ffffff]monster score " + atv3 + "% increase";
                }
                else if (at3 == 19)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]rank coin " + atv3 + "% add coin";
                }
                else if (at3 == 20)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]game coin " + atv3 + "% add coin";
                }
                else if (at3 == 21)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 2 + " - " + atv3 + " random add coin";
                }
                else if (at3 == 22)
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]" + atv3 / 10 + " - " + atv3 + " random add coin";
                }
                else
                {
                    m_at3_label.GetComponent<UILabel>().text = "[52E4DC]coin bonus : [ffffff]monster coin " + atv3 + "%  increase";
                }
            }

            else
            {
                m_at3_label.GetComponent<UILabel>().text = "";
            }
        }

    }

    public void popupShop(bool active, int taptype)
    {
        rootPanel.gameObject.SetActive(false);
        popup_confirm.gameObject.SetActive(false);
        popup_monsterBuy.gameObject.SetActive(false);
        if (active)
        {
            rootPanel.gameObject.SetActive(true);

            setMyInfo();
            rootPanel.GetComponent<Popup_Effector>().activePopup();
            InitShop(taptype);
        }
    }
    private void buyCard()
    {
        GlobalData.g_global.shopTapindex = Shop.cardItemCount[currentitemIndex];
        GlobalData.g_global.shopIndex = currentitemIndex + 1;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterGachaIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterGachaRequest);

    }


    public IEnumerator waitTime(Transform temp)
    {
        yield return new WaitForSeconds(1.5f);
        temp.GetComponent<BoxCollider>().enabled = true;
    }


    public void onSendBuyItem()
    {
        if (currentTapType == Shop.TapType.Shop_Card)
        {

            //  popup_monsterBuy.transform.GetComponent<Popup_Effector>().activePopup();
            //int mId = (int)Shop.TapType.Shop_Card;

            if (currentitemIndex == 0)
            {
                int coinAmount = GlobalData.g_global.myInfo.coinAmount;

                if (coinAmount < Shop.cardCondition[currentitemIndex])
                {
                    popup_confirm.gameObject.SetActive(false);
                    popup_confirm_back.gameObject.SetActive(false);
                    BoxCollider[] buttons;
                    buttons = rootPanel.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    InitShop((int)Shop.TapType.Shop_Gold);

                }
                else
                {
                    buyCard();
                }
            }
            else
            {
                int coinAmount = GlobalData.g_global.myInfo.gemCount;

                if (coinAmount < Shop.cardCondition[currentitemIndex])
                {
                    popup_confirm.gameObject.SetActive(false);
                    popup_confirm_back.gameObject.SetActive(false);
                    BoxCollider[] buttons;
                    buttons = rootPanel.GetComponentsInChildren<BoxCollider>();
                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    InitShop((int)Shop.TapType.Shop_Gem);
                }
                else
                {
                    buyCard();
                }
            }
        }

        else
        {



            int coinAmount = GlobalData.g_global.myInfo.gemCount;

            int count = 0;

            switch (currentTapType)
            {
                case Shop.TapType.Shop_Gold:
                    {
                        int coinAmount2 = GlobalData.g_global.myInfo.gemCount;

                        if (coinAmount < Shop.coinCondition[currentitemIndex])
                        {
                            popup_confirm.gameObject.SetActive(false);
                            popup_confirm_back.gameObject.SetActive(false);
                            BoxCollider[] buttons;
                            buttons = rootPanel.GetComponentsInChildren<BoxCollider>();
                            for (int i = 0; i < buttons.Length; ++i)
                            {
                                buttons[i].enabled = true;
                            }

                            InitShop((int)Shop.TapType.Shop_Gem);
                            return;
                        }
                        else
                        {
                            count = Shop.goldItemCount[currentitemIndex];
                        }

                    } break;
                case Shop.TapType.Shop_BingoTicket:
                    {
                        int coinAmount2 = GlobalData.g_global.myInfo.gemCount;
                        int coinAmount3 = GlobalData.g_global.myInfo.coinAmount;
                        if (currentitemIndex == 0)
                        {
                            if (coinAmount3 < Shop.ticketCondition[currentitemIndex])
                            {
                                popup_confirm.gameObject.SetActive(false);
                                popup_confirm_back.gameObject.SetActive(false);
                                BoxCollider[] buttons;
                                buttons = rootPanel.GetComponentsInChildren<BoxCollider>();
                                for (int i = 0; i < buttons.Length; ++i)
                                {
                                    buttons[i].enabled = true;
                                }

                                InitShop((int)Shop.TapType.Shop_Gold);
                                return;
                            }
                            else
                            {
                                count = Shop.bingoTicketItemCount[currentitemIndex];
                            }
                        }

                        else
                        {
                            if (coinAmount < Shop.ticketCondition[currentitemIndex])
                            {
                                popup_confirm.gameObject.SetActive(false);
                                popup_confirm_back.gameObject.SetActive(false);
                                BoxCollider[] buttons;
                                buttons = rootPanel.GetComponentsInChildren<BoxCollider>();
                                for (int i = 0; i < buttons.Length; ++i)
                                {
                                    buttons[i].enabled = true;
                                }

                                InitShop((int)Shop.TapType.Shop_Gem);
                                return;
                            }
                            else
                            {
                                count = Shop.bingoTicketItemCount[currentitemIndex];
                            }
                        }
                    } break;
            }
            if (currentTapType == Shop.TapType.Shop_Gem)
            {
               // AndroidManager.Instance.requestBuy(currentitemIndex.ToString(), GlobalData.g_global.myInfo.userID.ToString());
                
#if UNITY_ANDROID
                AndroidManager.Instance.Buy("bingo_" + (currentitemIndex+1).ToString());
#endif
            }
            else
            {
                GlobalData.g_global.shopIndex = currentitemIndex + 1;
                GlobalData.g_global.shopTapindex = (int)currentTapType;
                GlobalData.g_global.serverFlag = 1;
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemShopIng;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mItemShopRequest);
            }
        }

    }

    //=========================================================================================//
    // public functions
    //=========================================================================================//
    public void setMyInfo()
    {
        label_myInfo[(int)Shop.TapType.Shop_Gem].GetComponent<UILabel>().text = GlobalData.g_global.myInfo.gemCount.ToString();
        label_myInfo[(int)Shop.TapType.Shop_Gold].GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();
        label_myInfo[(int)Shop.TapType.Shop_BingoTicket].GetComponent<UILabel>().text = GlobalData.g_global.myInfo.ticketCount.ToString();
        GameObject.Find("gold_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();
        GameObject.Find("gem_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.gemCount.ToString();
        GameObject.Find("bingoticket_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.ticketCount.ToString() + " /20";
    }

    public void onItemClick(Shop.TapType type, int index)
    {
        currentTapType = type;
        currentitemIndex = index;

        if (currentTapType == Shop.TapType.Shop_Card)
        {
            if (GlobalData.g_global.myCardList.Count > 30)
            {
                popup_error.gameObject.SetActive(true);
                return;
            }
        }

        Transform text_ment = popup_confirm.Find("text_ment");
        Transform btn_buy = popup_confirm.Find("btn_buy");
        Transform btn_exit = popup_confirm.Find("btn_exit");
        Transform btn_check = popup_confirm.Find("btn_check");

        // text_ment.GetComponent<UILabel>().text = "구입하시겠습니까?";

        btn_buy.gameObject.SetActive(true);
        btn_exit.gameObject.SetActive(true);
        btn_check.gameObject.SetActive(false);


        popup_confirm.gameObject.SetActive(true);
        popup_confirm.GetComponent<Popup_Effector>().activePopup();
        popup_confirm.GetComponent<ShopConfirmCtrl>().setConfirmAttrib(type, index);
    }

    public IEnumerator setActiveShopBlackLayer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public IEnumerable setActiveConfirmBlackLayer(float time)
    {
        yield return new WaitForSeconds(time);
    }


    public void InitShop(int taptype)
    {
        currentTapType = (Shop.TapType)taptype;
        activeTapButton(taptype);
        setShopItems(taptype);
    }

    //=========================================================================================//
    // private functions
    //=========================================================================================//


    private void setShopItems(int tapType)
    {
        int itemCount = 0;
        string[] imagepath = { "" };
        switch (tapType)
        {
            case (int)Shop.TapType.Shop_Gem:
                {
                    itemCount = (int)Shop.Gem.Gem_MAX;
                    imagepath = Shop.gemImagePath;
                } break;
            case (int)Shop.TapType.Shop_Gold:
                {
                    itemCount = (int)Shop.Coin.coinItem_MAX;
                    imagepath = Shop.coinImagePath;
                } break;
            case (int)Shop.TapType.Shop_BingoTicket:
                {
                    itemCount = (int)Shop.Ticket.Ticket_MAX;
                    imagepath = Shop.ticketImagePath;
                } break;
            case (int)Shop.TapType.Shop_Card:
                {
                    itemCount = (int)Shop.Card.Card_MAX;
                    imagepath = Shop.cardImagePath;
                } break;
        }

        // remove prev children
        int childCount = gridObject.childCount;
        for (int i = 0; i < childCount; ++i)
        {
            Destroy(gridObject.GetChild(i).gameObject);
        }

        for (int index = 0; index < itemCount; ++index)
        {
            GameObject shopItem = createShopItem(imagepath, index, tapType);
            shopItem.GetComponent<ShopItemCtrl>().taptype = (Shop.TapType)tapType;
            shopItem.GetComponent<ShopItemCtrl>().shopItemIndex = index;
        }

        gridObject.GetComponent<UIGrid>().Reposition();
        scrollView.ResetPosition();
        layerScollView.GetComponent<UIPanel>().Refresh();
    }

    private GameObject createShopItem(string[] imagePath, int index, int s_tapType)
    {
        GameObject shopItem = Instantiate(Resources.Load("shop/shop_item")) as GameObject;
        Transform itemImage = shopItem.transform.Find("itemImage");
        itemImage.GetComponent<UISprite>().spriteName = imagePath[index];
        itemImage.GetComponent<UISprite>().MakePixelPerfect();

        Transform shop_type_label = shopItem.transform.Find("shop_type_label");
        Transform shop_type_sprite = shopItem.transform.Find("shop_type_sprite");
        Transform item_price_label = shopItem.transform.Find("item_price_label");
        Transform item_price_type = shopItem.transform.Find("item_price_type");
        Transform bonus_label = shopItem.transform.Find("bonus_Label");
        Transform bonus_spr = shopItem.transform.Find("bonus_spr");

        switch (s_tapType)
        {
            case (int)Shop.TapType.Shop_Gem:
                {
                    if (Shop.jamBonus[index] == 0)
                    {
                        bonus_label.gameObject.SetActive(false);
                        bonus_spr.gameObject.SetActive(false);
                    }
                    else
                    {
                        bonus_label.GetComponent<UILabel>().text = "+" + Shop.jamBonus[index].ToString();
                    }
                    shop_type_label.GetComponent<UILabel>().text = Shop.gemAmount[index].ToString();
                    shop_type_sprite.GetComponent<UISprite>().spriteName = "card_gem";
                    shop_type_sprite.GetComponent<UISprite>().MakePixelPerfect();

                    item_price_label.GetComponent<UILabel>().text = Shop.gemCondition[index].ToString();
                    item_price_type.GetComponent<UISprite>().spriteName = "cash";
                    item_price_type.GetComponent<UISprite>().MakePixelPerfect();

                } break;
            case (int)Shop.TapType.Shop_Gold:
                {
                    if (Shop.coinBonus[index] == 0)
                    {
                        bonus_label.gameObject.SetActive(false);
                        bonus_spr.gameObject.SetActive(false);
                    }
                    else
                    {
                        bonus_label.GetComponent<UILabel>().text = "+" + Shop.coinBonus[index].ToString();
                    }

                    shop_type_label.GetComponent<UILabel>().text = Shop.coinAmount[index].ToString();
                    shop_type_sprite.GetComponent<UISprite>().spriteName = "shop_coin";
                    shop_type_sprite.GetComponent<UISprite>().MakePixelPerfect();

                    item_price_label.GetComponent<UILabel>().text = Shop.coinCondition[index].ToString();
                    item_price_type.GetComponent<UISprite>().spriteName = "card_gem";
                    item_price_type.GetComponent<UISprite>().MakePixelPerfect();

                } break;

            case (int)Shop.TapType.Shop_BingoTicket:
                {
                    if (Shop.ticketBonus[index] == 0)
                    {
                        bonus_label.gameObject.SetActive(false);
                        bonus_spr.gameObject.SetActive(false);
                    }

                    else
                    {
                        bonus_label.GetComponent<UILabel>().text = "+" + Shop.ticketBonus[index].ToString();
                    }

                    shop_type_label.GetComponent<UILabel>().text = Shop.ticketAmount[index].ToString();


                    if (index < 4)
                    {
                        shop_type_sprite.GetComponent<UISprite>().spriteName = "tic_icon";
                    }
                    else
                    {
                        shop_type_sprite.GetComponent<UISprite>().spriteName = "gold_icon";
                    }


                    item_price_label.GetComponent<UILabel>().text = Shop.ticketCondition[index].ToString();
                    item_price_type.GetComponent<UISprite>().spriteName = "card_gem";
                    if (index == 0)
                    {
                        item_price_type.GetComponent<UISprite>().spriteName = "shop_coin";
                    }
                    shop_type_sprite.GetComponent<UISprite>().MakePixelPerfect();
                    item_price_type.GetComponent<UISprite>().MakePixelPerfect();
                } break;
            case (int)Shop.TapType.Shop_Card:
                {
                    bonus_label.gameObject.SetActive(false);
                    bonus_spr.gameObject.SetActive(false);

                    shop_type_label.GetComponent<UILabel>().text = Shop.cardEx[index];
                    shop_type_sprite.GetComponent<UISprite>().spriteName = "card_star";
                    shop_type_sprite.GetComponent<UISprite>().MakePixelPerfect();

                    item_price_label.GetComponent<UILabel>().text = Shop.cardCondition[index].ToString();
                    item_price_type.GetComponent<UISprite>().spriteName = Shop.cardConditionType[index];
                    item_price_type.GetComponent<UISprite>().MakePixelPerfect();

                } break;
        }

        shopItem.transform.parent = gridObject;
        shopItem.GetComponent<UIDragScrollView>().scrollView = scrollView;
        shopItem.transform.localPosition = Vector3.zero;
        shopItem.transform.localScale = Vector3.one;

        return shopItem;
    }



    private void activeTapButton(int tapType)
    {

        for (int i = 0; i < 4; ++i)
        {
            if (tapType == i)
            {
                btn_Taps[tapType].GetComponentInChildren<UISprite>().spriteName = Shop.tapImagePath[tapType] + "_on";

                btn_Taps[tapType].GetComponentInChildren<UIButton>().normalSprite = Shop.tapImagePath[tapType] + "_on";
                btn_Taps[tapType].GetComponentInChildren<UIButton>().pressedSprite = Shop.tapImagePath[tapType] + "_on";
                btn_Taps[tapType].GetComponentInChildren<UIButton>().disabledSprite = Shop.tapImagePath[tapType] + "_on";
            }
            else
            {
                btn_Taps[i].GetComponentInChildren<UISprite>().spriteName = Shop.tapImagePath[i] + "_off";
                btn_Taps[i].GetComponentInChildren<UIButton>().normalSprite = Shop.tapImagePath[i] + "_off";
                btn_Taps[i].GetComponentInChildren<UIButton>().pressedSprite = Shop.tapImagePath[i] + "_off";
                btn_Taps[i].GetComponentInChildren<UIButton>().disabledSprite = Shop.tapImagePath[i] + "_off";
            }
        }
    }

}
