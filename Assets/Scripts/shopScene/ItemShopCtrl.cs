using UnityEngine;
using System.Collections;


public class ItemShopCtrl : MonoBehaviour {

    private Transform popup_itemShop;
    private Transform popup_confirm;

    [HideInInspector]
    public Shop.Item currentItemType;

    public Transform balloonRight;
    
    
    public UIScrollView sv; // item scrollview

    [HideInInspector]
    public float m_prevPos;

    private bool m_isExplained;

    private Transform soundObj;

    void Awake()
    {
        popup_itemShop = transform.Find("root_panel");
        popup_confirm = transform.Find("confirm_panel");
        soundObj = transform.Find("sound");
    }

    void Start()
    {

        Transform btn_check = popup_confirm.Find("btn_check");
        btn_check.gameObject.SetActive(false);
    }

    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mItemShopIngComplete && GlobalData.g_global.shopshop == 2)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            //Socket_Ctrl.sCtrl.closeSocket();
            GlobalData.g_global.myInfo.ItemCount = GlobalData.g_global.myInfo.ItemCount + Shop.itemItemCount[(int)currentItemType];
            GlobalData.g_global.myInfo.coinAmount = GlobalData.g_global.myInfo.coinAmount - Shop.itemCondition[(int)currentItemType];
            GameObject.Find("item_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.ItemCount.ToString();
            GameObject.Find("gold_label").GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();

            Transform text_ment=popup_confirm.Find("text_ment");
            Transform btn_buy = popup_confirm.Find("btn_buy");
            Transform btn_exit = popup_confirm.Find("btn_exit");
            Transform btn_check = popup_confirm.Find("btn_check");

            text_ment.GetComponent<UILabel>().text = "Buy has been completed.";

            btn_buy.gameObject.SetActive(false);
            btn_exit.gameObject.SetActive(false);
            btn_check.gameObject.SetActive(true);

            soundObj.GetComponent<AudioSource>().PlayOneShot(soundObj.GetComponent<AudioSource>().clip);
        }

        if (m_isExplained == true)
        {
            if (Mathf.Abs(m_prevPos - sv.transform.localPosition.x) > 10.0f)
            {
                balloonRight.gameObject.SetActive(false);
                m_isExplained = false;
            }
        }
    }

    public void showItemExplain(Vector3 pos, Item.ItemType type)
    {
        m_isExplained = true;

        string spritename = Item.itemImageMent[(int)type];

        balloonRight.gameObject.SetActive(true);
        Vector3 destPos = pos;
        
        destPos.y -= 230;
        if (pos.x > 350f)
        {
            destPos.x -= 340f;
            balloonRight.GetComponent<UISprite>().spriteName = "item_explain2";
        }
        else {
            balloonRight.GetComponent<UISprite>().spriteName = "item_explain";
            destPos.x -= 90f;
        }
        
        balloonRight.localPosition = destPos;


        balloonRight.Find("textImage/text_ment").GetComponent<UILabel>().text = spritename;
        balloonRight.Find("textImage").GetComponent<UISprite>().MakePixelPerfect(); 
    }

    public void onSendBuyItem( )
    {
        GlobalData.g_global.shopIndex = (int)currentItemType + 1;
        GlobalData.g_global.shopTapindex = 3;
        GlobalData.g_global.serverFlag = 1;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemShopIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mItemShopRequest);
    }

    public void popupItemShop( bool active )
    {
        popup_itemShop.gameObject.SetActive(false);
        popup_confirm.gameObject.SetActive(false);

        balloonRight.gameObject.SetActive(false);

        m_isExplained = false;
        if (active)
        {
            popup_itemShop.gameObject.SetActive(true);
            popup_itemShop.GetComponent<Popup_Effector>().activePopup();
        }
        
    }

    public void popupConfirm( bool active )
    {
        if (active)
        {
            popup_confirm.gameObject.SetActive(true);
            popup_confirm.GetComponent<Popup_Effector>().activePopup();
            popup_confirm.GetComponent<ItemShopConfirmCtrl>().setConfirmAttrib((int)currentItemType);
        }
        
    }
}
