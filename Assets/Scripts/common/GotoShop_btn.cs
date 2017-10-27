using UnityEngine;
using System.Collections;

public class GotoShop_btn : MonoBehaviour {

    public GameObject popupRoot;
    public GameObject popupupRoot;
    public GameObject popupupRoot_back;
    public GameObject btnctrl;
    public GameObject closeitem;
    public GameObject itembtn;

    private BoxCollider[] buttons;

    public Shop.TapType taptype;

    void Start()
    {

    }

    void OnClick()
    {
        GlobalData.g_global.shopshop = 1;
        GlobalData.g_global.invite_able = false;
        if (itembtn != null)
        {
            buttons = itembtn.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }
        if(popupupRoot != null){
            popupupRoot.SetActive(false);
        }
        if (popupupRoot_back != null)
        {
            popupupRoot_back.SetActive(false);
        }
        if(btnctrl != null){
            buttons = btnctrl.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
        

        popupRoot.SetActive(true);
        popupRoot.GetComponent<ShopCtrl>().popupShop(true, (int)taptype);

        if(closeitem !=null){

            closeitem.gameObject.SetActive(false);
        }
    }
}
