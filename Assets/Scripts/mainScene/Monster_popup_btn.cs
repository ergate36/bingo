using UnityEngine;
using System.Collections;

public class Monster_popup_btn : MonoBehaviour
{

    private GameObject popup;
    private Transform popupup;
    private Transform popupCtrl;
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
    private GameObject root;
    private CardListCtrl cardListctrl;
    private CardListCtrl2 cardListctrl2;

    public string SceneName;
    private MyCard myCard;
    private int flag = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMonsterCardListIngComplete && GlobalData.g_global.currentScene == 2 && SceneName == "mainScene")
            {

                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
                GlobalData.g_global.myCardList.Sort((temp1, temp2) => temp1.cardRank.CompareTo(temp2.cardRank));

                GlobalData.g_global.myCardList.Reverse();
                makeList();
                flag = 0;
            }
            else if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mMonsterCardListIngComplete && SceneName == "lobbyScene")
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
                GlobalData.g_global.myCardList.Sort((temp1, temp2) => temp1.cardRank.CompareTo(temp2.cardRank));

                GlobalData.g_global.myCardList.Reverse();
                makeList2();
                flag = 0;
            }
        }
    }
    void OnClick()
    {
        GlobalData.g_global.invite_able = false;

        SaveCardCountData(GlobalData.g_global.cardCount);
        if (SceneName == "mainScene")
        {
            getMyCard();
        }
        else
        {
            getMyCard2();
        }
        flag = 1;
    }
    void SaveCardCountData(int cardCount)
    {
        PlayerPrefs.SetInt("cardCount", cardCount);
    }
    private void getMyCard()
    {
        GlobalData.g_global.currentScene = 2;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardListIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardListRequest);
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_monster");
        popupup.gameObject.SetActive(true);
        GlobalData.g_global.afterCardCount = GlobalData.g_global.cardCount;
    }

    private void makeList()
    {
        GlobalData.g_global.selectedCardNumber = 0;
        GlobalData.g_global.selectedCardNum = 0;
        GlobalData.g_global.selectedCardId = 0;
        //GlobalData.g_global.selectedCardId = GlobalData.g_global.myCardList[0].cardId;
        GlobalData.g_global.monsterFlag = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;


        popupup.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));

        popupCtrl = popupup.transform.Find("infopopup_panel");
        popupCtrl.gameObject.SetActive(false);

        popupCtrl = popupup.transform.Find("sellpopup_panel");
        popupCtrl.gameObject.SetActive(false);
        popupCtrl = popupup.transform.Find("upgrade_panel");
        popupCtrl.gameObject.SetActive(false);

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        root = GameObject.Find("mainSceneUI/Camera/Anchor/popup_monster/card_List");
        cardListctrl = root.GetComponent<CardListCtrl>();
        cardListctrl.cardListClear();

        cardListctrl.cardList(2);

    }


    private void getMyCard2()
    {
        GlobalData.g_global.currentScene = 2;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardListIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMonsterCardListRequest);
        popup = GameObject.Find("lobbySceneUI/Camera/Anchor") as GameObject;
        popupup = popup.transform.Find("popup_monster");
        popupup.gameObject.SetActive(true);
        GlobalData.g_global.afterCardCount = GlobalData.g_global.cardCount;
    }

    private void makeList2()
    {
        GlobalData.g_global.selectedCardNumber = 0;
        GlobalData.g_global.selectedCardNum = 0;
        GlobalData.g_global.selectedCardId = 0;
        //GlobalData.g_global.selectedCardId = GlobalData.g_global.myCardList[0].cardId;
        GlobalData.g_global.monsterFlag = 0;
        popup = GameObject.Find("lobbySceneUI/Camera/Anchor") as GameObject;


        popupup.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));

        popupCtrl = popupup.transform.Find("infopopup_panel");
        popupCtrl.gameObject.SetActive(false);
        popupCtrl = popupup.transform.Find("mix_panel");
        popupCtrl.gameObject.SetActive(false);
        popupCtrl = popupup.transform.Find("sellpopup_panel");
        popupCtrl.gameObject.SetActive(false);
        popupCtrl = popupup.transform.Find("upgrade_panel");
        popupCtrl.gameObject.SetActive(false);

        mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
        mainSceneUI = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        root = GameObject.Find("lobbySceneUI/Camera/Anchor/popup_monster/card_List");
        cardListctrl2 = root.GetComponent<CardListCtrl2>();
        cardListctrl2.cardListClear();

        cardListctrl2.cardList(2);

    }


}
