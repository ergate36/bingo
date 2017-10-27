using UnityEngine;
using System.Collections;

public class close_detail : MonoBehaviour {

    public Transform popup;
    public Transform popup2;

    // Use this for initialization
    void Start()
    {

    }

    void OnClick()
    {
        iTween.ScaleTo(popup.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));
        popup2.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(popup2.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(popup2.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", popup2.gameObject));

    }
}
