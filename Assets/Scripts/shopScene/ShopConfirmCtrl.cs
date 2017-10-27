using UnityEngine;
using System.Collections;

public class ShopConfirmCtrl : MonoBehaviour {

    [HideInInspector]
    public Shop.TapType tapTaype;
    [HideInInspector]
    public int itemIndex;

    private Transform btn_buy;

    Transform itemImg;
    Transform ment;

    void Awake()
    {
        itemImg = transform.Find("img_shopItem");
        ment = transform.Find("text_ment");
        btn_buy = transform.Find("btn_buy");

    }


	// Use this for initialization
	void Start () {
	    
	}

    public void setMentBuyComplete()
    {
        ment.GetComponent<UILabel>().text= "purchase complete";
        btn_buy.gameObject.SetActive(false);
    }

    public void setConfirmAttrib( Shop.TapType type, int index )
    {
        tapTaype = type;
        itemIndex = index;

        setImage();
        setMent();
    }

    private void setMent()
    {
        int result = getConditionToBuy();   
 
        if( result < 0 )
        {
            ment.GetComponent<UILabel>().text = "Do you want to purchase?";
            btn_buy.gameObject.SetActive(true);
        }
        else if( result == (int)Shop.ShopType.Shop_Gem )
        {
            ment.GetComponent<UILabel>().text = "jem is low. Would you like go to the SHOP to purchase a jem?";
               // btn_buy.gameObject.SetActive(false);
        }
        else if( result == (int)Shop.ShopType.Shop_Gold )
        {
            ment.GetComponent<UILabel>().text = "COIN is low. Would you like go to the SHOP to purchase a COIN?";
               // btn_buy.gameObject.SetActive(false);
        }
    }

    private int getConditionToBuy()
    {
        switch (tapTaype)
        {
            case Shop.TapType.Shop_Gem:
                {               

                }break;
            case Shop.TapType.Shop_Gold:
                {
                    int gemAmount = GlobalData.g_global.myInfo.gemCount;

                    if (gemAmount < Shop.coinCondition[itemIndex])
                    {
                        return (int)Shop.TapType.Shop_Gem;
                    }
                    
                } break;

            case Shop.TapType.Shop_BingoTicket:
                {
                    int coinAmount = GlobalData.g_global.myInfo.coinAmount;
                    int myGem = GlobalData.g_global.myInfo.gemCount;
                    if (itemIndex == 0)
                    {
                        if (coinAmount < Shop.ticketCondition[itemIndex])
                        {
                            return (int)Shop.TapType.Shop_Gold;
                        }
                    }
                    else
                    {
                        if (myGem < Shop.ticketCondition[itemIndex])
                        {
                            return (int)Shop.TapType.Shop_Gem;
                        }
                    }
                }break;
            case Shop.TapType.Shop_Card:
                {
                    if (itemIndex == 0)
                    {
                        int coinAmount = GlobalData.g_global.myInfo.coinAmount;

                        if (coinAmount < Shop.cardCondition[itemIndex])
                        {
                            return (int)Shop.TapType.Shop_Gold;
                        }

                    }
                    else
                    {
                        int coinAmount = GlobalData.g_global.myInfo.gemCount;

                        if (coinAmount < Shop.cardCondition[itemIndex])
                        {
                            return (int)Shop.TapType.Shop_Gem;
                        }
                    }
                    
                } break;
        }

        return -1;
    }

    private void setImage()
    {
        switch (tapTaype)
        {
            case Shop.TapType.Shop_Gem:
                {
                    itemImg.GetComponent<UISprite>().spriteName = Shop.gemImagePath[itemIndex];
                }break;
            case Shop.TapType.Shop_Gold:
                {
                    itemImg.GetComponent<UISprite>().spriteName = Shop.coinImagePath[itemIndex];    
                } break;
            case Shop.TapType.Shop_BingoTicket:
                {
                    itemImg.GetComponent<UISprite>().spriteName = Shop.ticketImagePath[itemIndex];
                } break;
            case Shop.TapType.Shop_Card:
                {
                    itemImg.GetComponent<UISprite>().spriteName = Shop.cardImagePath[itemIndex];
                } break;
        }
        itemImg.GetComponent<UISprite>().MakePixelPerfect();
    }


}
