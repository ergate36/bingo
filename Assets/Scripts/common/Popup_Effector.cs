using UnityEngine;
using System.Collections;

public class Popup_Effector : MonoBehaviour {

    public GameObject toUnactiveRoot;
    public GameObject toUnactiveRoot2;

    public GameObject popup_back;

    private BoxCollider[] buttons;

    void Awake()
    {
        if (popup_back)
            popup_back.SetActive(false);
    }

    void Start()
    {
        
    }

    public void activePopup()
    {
        if (popup_back)
            popup_back.SetActive(true);

        if (toUnactiveRoot != null)
        {
            buttons = toUnactiveRoot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }
        if (toUnactiveRoot2 != null)
        {
            buttons = toUnactiveRoot2.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = false;
            }
        }


        transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", gameObject));
    }

    public IEnumerator unactivePopup( float time )
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 0.2f, "y", 0.2f, "z", 0.5f, "easeType", "easeOutBack", "time", 0.4f));
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0.0f, "time", 0.2f, "onupdate", "setalpha", "onupdatetarget", gameObject));

        if (popup_back)
        {
            popup_back.SetActive(false);
        }
        yield return new WaitForSeconds(time);


        if (toUnactiveRoot != null)
        {
            buttons = toUnactiveRoot.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }
        if (toUnactiveRoot2 != null)
        {
            buttons = toUnactiveRoot2.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
        }

        GlobalData.g_global.shop_flag = 1;
        gameObject.SetActive(false);
    }

    private void setalpha(float val)
    {
        if (transform.GetComponent<UIPanel>())
        {
            transform.GetComponent<UIPanel>().alpha = val;
        }
        else {
            transform.GetComponent<UISprite>().alpha = val;
        }
        
        
    }


  
}
