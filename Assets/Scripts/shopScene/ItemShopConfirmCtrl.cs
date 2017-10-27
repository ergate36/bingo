using UnityEngine;
using System.Collections;

public class ItemShopConfirmCtrl : MonoBehaviour {

    private Transform itemImage;
    private Transform ment;
    private Transform btn_buy;
    private Transform move_shop;


    void Awake()
    {
        itemImage = transform.Find("img_shopItem");
        ment = transform.Find("text_ment");
        btn_buy = transform.Find("btn_buy");
        move_shop = transform.Find("move_shop");
        move_shop.gameObject.SetActive(false);
    }

    public void setConfirmAttrib( int index)
    {
        setItemImage(index);
        checkToBuy(index);
    }

    public void setMentBuyComplete()
    {
        ment.GetComponent<UILabel>().text= "purchase complete";
        btn_buy.gameObject.SetActive(false);
    }

    public void setItemImage(int index)
    {
        itemImage.GetComponent<UISprite>().spriteName = Shop.itemImagePath[index];
    }

    private bool checkToBuy(int index)
    {
        int coinAmount = GlobalData.g_global.myInfo.coinAmount;

        if (coinAmount >= Shop.itemCondition[index])
        {
            ment.GetComponent<UILabel>().text = "Do you want to purchase?";
            btn_buy.gameObject.SetActive(true);
            move_shop.gameObject.SetActive(false);
            return true;
        }
        else {
            ment.GetComponent<UILabel>().text = "COIN is low. Would you like go to the SHOP to purchase a COIN?";
            move_shop.gameObject.SetActive(true);
            btn_buy.gameObject.SetActive(false);
        }

        return false;
    }
    

}
