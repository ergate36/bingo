using UnityEngine;
using System.Collections;

public class GoToItemShop_btn : MonoBehaviour {


    public GameObject popupRoot;

    void OnClick()
    {
        GlobalData.g_global.invite_able = false;
        GlobalData.g_global.shopshop = 2;
        popupRoot.SetActive(true);
        popupRoot.GetComponent<ItemShopCtrl>().popupItemShop(true);
    }
}
