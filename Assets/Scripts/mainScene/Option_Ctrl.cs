using UnityEngine;
using System.Collections;

public class Option_Ctrl : MonoBehaviour {

    public int tag;
    public GameObject popup;
    private Transform popupup;
    private AudioSource[] audios;
    private BoxCollider[] buttons;

    private BoxCollider[] box;
    
    public GameObject mainSceneUI;
    public GameObject mainSceneUI2;

    private Transform boxroot;
    private Transform soundOn;
    private Transform soundOff;
	// Use this for initialization
	void Start () {
	
    }
	
	// Update is called once per frame
	void Update () {
	
        if( GlobalData.g_global.socketState == (int)SocketClass.STATE.mDropIngComplete){
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
           // PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteAll();
#if UNITY_ANDROID
            AndroidManager.Instance.callDisconnect();
#endif
        }
	}
    void OnClick()
    {
        boxroot = popup.transform.Find("popup_option/popup_base");
        GlobalData.g_global.invite_able = false;

        if (tag == 1)
        {
            //logout
            popupup = popup.transform.Find("popup_option/popup_logout");
            popupup.gameObject.SetActive(true);
            popupup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
        
            boxroot = popup.transform.Find("popup_option/popup_base");
            box = boxroot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < box.Length; ++i)
            {
                box[i].enabled = false;
            }
        }
        else if(tag == 2)
        {
            //disconnection
            popupup = popup.transform.Find("popup_option/popup_disconnect");
            popupup.gameObject.SetActive(true);
            popupup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));
            
            box = boxroot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < box.Length; ++i)
            {
                box[i].enabled = false;
            }
        }
        else if(tag ==3){
#if UNITY_ANDROID
            AndroidManager.Instance.callLogout();
#endif
        }
        else if (tag == 4)
        {
            PlayerPrefs.DeleteAll();

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mDropRequest;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mDropRequest);
        }
        else if(tag == 5)
        {
            //sound on
            soundOn = boxroot.transform.Find("soundon/Background");
            soundOn.GetComponent<UISprite>().spriteName = "check";
            soundOff = boxroot.transform.Find("soundoff/Background");
            soundOff.GetComponent<UISprite>().spriteName = "";
            GlobalData.g_global.soundFlg = 0;
            PlayerPrefs.SetInt("soundFlg", 0);
        }
        else if (tag == 6)
        {
            //sound off
            soundOn = boxroot.transform.Find("soundon/Background");
            soundOn.GetComponent<UISprite>().spriteName = ""; 
            soundOff = boxroot.transform.Find("soundoff/Background");
            soundOff.GetComponent<UISprite>().spriteName = "check";
            GlobalData.g_global.soundFlg = 1;
            PlayerPrefs.SetInt("soundFlg", 1);
        }

        else if (tag == 9)
        {
            //popup disconnect close
            popupup = popup.transform.Find("popup_option/popup_disconnect");
         //   popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            boxroot = popup.transform.Find("popup_option/popup_base");
            box = boxroot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < box.Length; ++i)
            {
                box[i].enabled = true;
            }
        }
        else if (tag == 10)
        {
            //popup logout close
            popupup = popup.transform.Find("popup_option/popup_logout");
            //popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            boxroot = popup.transform.Find("popup_option/popup_base");
            box = boxroot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < box.Length; ++i)
            {
                box[i].enabled = true;
            }
        }
        else if(tag == 11){
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
            if (mainSceneUI2 != null)
            {
                buttons = mainSceneUI2.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].enabled = false;
                }

            }

            // root  popup  open
            popupup = popup.transform.Find("popup_option");
            popupup.gameObject.SetActive(true);
            popupup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));

            popupup = popup.transform.Find("popup_option/popup_disconnect");
            popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            popupup = popup.transform.Find("popup_option/popup_logout");
            popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

            popupup = popup.transform.Find("popup_option/popup_coupon");
            popupup.gameObject.SetActive(false);
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));


            if (PlayerPrefs.GetInt("soundFlg") == 0)
            {
                soundOn = boxroot.transform.Find("soundon/Background");
                soundOn.GetComponent<UISprite>().spriteName = "check";
                soundOff = boxroot.transform.Find("soundoff/Background");
                soundOff.GetComponent<UISprite>().spriteName = "";
            }
            else
            {
                soundOn = boxroot.transform.Find("soundon/Background");
                soundOn.GetComponent<UISprite>().spriteName = "";
                soundOff = boxroot.transform.Find("soundoff/Background");
                soundOff.GetComponent<UISprite>().spriteName = "check";
            }
        }
        else if(tag ==12){
            Transform input_coupon;
            input_coupon = transform.parent.Find("Input");
            GlobalData.g_global.couponNum = input_coupon.GetComponent<UIInput>().value;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.mCouponRequest;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mCouponRequest);
        }
        else if (tag == 13)
        {
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            
            popupup = popup.transform.Find("popup_option/popup_coupon");
            
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
            popupup.gameObject.SetActive(false);
        }
        else
        {
            buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
            buttons = mainSceneUI2.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            popupup = popup.transform.Find("popup_option/popup_coupon");
            popupup.gameObject.SetActive(true);
            popupup.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(popupup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(popupup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popupup.gameObject));

            //쿠폰
        }
    }
}
