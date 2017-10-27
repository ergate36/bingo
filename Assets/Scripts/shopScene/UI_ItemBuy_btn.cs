using UnityEngine;
using System.Collections;

public class UI_ItemBuy_btn : MonoBehaviour {

    public Shop.Item itemType;
    
    private ItemShopCtrl itemshopCtrl;
    private Transform popup_confirm;
    void Awake()
    {
        

    }

    void OnClick()
    {


        GameObject popup_item = GameObject.Find("popup_item");
        itemshopCtrl = popup_item.GetComponent<ItemShopCtrl>();
        popup_confirm = popup_item.transform.Find("confirm_panel");

        Transform text_ment = popup_confirm.Find("text_ment");
        Transform btn_buy = popup_confirm.Find("btn_buy");
        Transform btn_exit = popup_confirm.Find("btn_exit");
        Transform btn_check = popup_confirm.Find("btn_check");

        text_ment.GetComponent<UILabel>().text = "구입하시겠습니까?";

        btn_buy.gameObject.SetActive(true);
        btn_exit.gameObject.SetActive(true);
        btn_check.gameObject.SetActive(false);

        itemshopCtrl.currentItemType = itemType;
        itemshopCtrl.popupConfirm(true);
    }
}
