using UnityEngine;
using System.Collections;

public class optionpopup : MonoBehaviour
{
    public Transform successPopup;
    public Transform failPopup;
    public Transform failPopup_label;

    public Transform couponPopup;

    public Transform main;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mCouponComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
            Debug.Log("GLOBAL : " + GlobalData.g_global.couponResult);

            BoxCollider[] buttons = main.GetComponentsInChildren<BoxCollider>();

            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }



            switch (GlobalData.g_global.couponResult)
            {
                case 0:
                    buttons = successPopup.GetComponentsInChildren<BoxCollider>();

                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    successPopup.gameObject.SetActive(true);
                    successPopup.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(successPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(successPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", successPopup.gameObject));
                    break;
                case 1:
                    buttons = failPopup.GetComponentsInChildren<BoxCollider>();

                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }
                    failPopup.gameObject.SetActive(true);
                    failPopup.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(failPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(failPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", failPopup.gameObject));

                    failPopup_label.GetComponent<UILabel>().text = "Invalid coupon code.Please try again.";
                    break;
                case 2:
                    buttons = failPopup.GetComponentsInChildren<BoxCollider>();

                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    failPopup.gameObject.SetActive(true);
                    failPopup.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(failPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(failPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", failPopup.gameObject));
                    failPopup_label.GetComponent<UILabel>().text = "Already  registered  or  no longer valid  in  the  last  coupon  code.";
                    break;

                case 3:
                    buttons = failPopup.GetComponentsInChildren<BoxCollider>();

                    for (int i = 0; i < buttons.Length; ++i)
                    {
                        buttons[i].enabled = true;
                    }

                    failPopup.gameObject.SetActive(true);
                    failPopup.transform.localScale = Vector3.one * 0.7f;
                    iTween.ScaleTo(failPopup.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
                    iTween.ValueTo(failPopup.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", failPopup.gameObject));
                    failPopup_label.GetComponent<UILabel>().text = "Already  registered  or  no longer valid  in  the  last  coupon  code.";

                    break;
            }
        }
    }
}
