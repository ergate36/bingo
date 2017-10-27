using UnityEngine;
using System.Collections;

public class ShopItemCtrl : MonoBehaviour {

    [HideInInspector]
    public Shop.TapType taptype;
    [HideInInspector]
    public int shopItemIndex;


    ShopCtrl shopCtrl;

    void Awake()
    {
        GameObject popupRoot = GameObject.Find("popup_shop");
        shopCtrl = popupRoot.GetComponent<ShopCtrl>();
    }

    void OnClick()
    {
        shopCtrl.onItemClick(taptype, shopItemIndex);

    }
}