using UnityEngine;
using System.Collections;

public class UI_ItemConfirmBuy_btn : MonoBehaviour {


    ItemShopCtrl itemshopCtrl;

    void Awake()
    {
        GameObject obj = GameObject.Find("popup_item");
        itemshopCtrl = obj.GetComponent<ItemShopCtrl>();
    }

    void OnClick()
    {
        GlobalData.g_global.shopshop = 2;
        itemshopCtrl.onSendBuyItem();
    }
}
