using UnityEngine;
using System.Collections;

public class UI_ConfirmBuy_btn : MonoBehaviour {

    ShopCtrl shopCtrl;

    void Awake()
    { 
        GameObject obj = GameObject.Find("popup_shop");
        shopCtrl = obj.GetComponent<ShopCtrl>();
    }

    void OnClick()
    {
        shopCtrl.onSendBuyItem();
    }
}
