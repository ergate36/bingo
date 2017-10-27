using UnityEngine;
using System.Collections;

public class UI_TabButton : MonoBehaviour {

    public Shop.TapType tabType;

    private ShopCtrl shopCtrl;

    void Awake()
    { 
        GameObject shopRoot =GameObject.Find("popup_shop");
        shopCtrl = shopRoot.GetComponent<ShopCtrl>();
    }

    void OnClick()
    {
        shopCtrl.InitShop((int)tabType);
    }
}
