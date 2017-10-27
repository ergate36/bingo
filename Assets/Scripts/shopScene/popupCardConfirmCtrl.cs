using UnityEngine;
using System.Collections;

public class popupCardConfirmCtrl : MonoBehaviour {

    private Transform btn_shop;
    private Transform btn_cancel;
    private Transform btn_confirm;
    private Transform ment;
    

    void Awake()
    {
        btn_shop = transform.Find("btn_shop");
        btn_cancel = transform.Find("btn_cancel");
        btn_confirm = transform.Find("btn_confirm");
        ment = transform.Find("ment");

        btn_shop.gameObject.SetActive(false);
        btn_cancel.gameObject.SetActive(false);
        btn_confirm.gameObject.SetActive(false);
        ment.gameObject.SetActive(false);
    }

    void Start()
    { 
        
    }

    public void setConfirm()
    {
        int gemCount = GlobalData.g_global.myInfo.gemCount;


        if (gemCount >= 3)
        {
            ment.gameObject.SetActive(true);
            ment.GetComponent<UISprite>().spriteName = "popup_text1";
            btn_confirm.gameObject.SetActive(true);
            btn_cancel.gameObject.SetActive(true);

            btn_shop.gameObject.SetActive(false); 

        }
        else {
            ment.gameObject.SetActive(true);
            ment.GetComponent<UISprite>().spriteName = "popup_text2";
            btn_shop.gameObject.SetActive(true);
            btn_cancel.gameObject.SetActive(true);
            btn_confirm.gameObject.SetActive(false);
        }
    }

}
