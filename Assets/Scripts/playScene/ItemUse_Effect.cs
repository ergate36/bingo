using UnityEngine;
using System.Collections;

public class ItemUse_Effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SetItemUse();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetItemUse()
    { 
        Vector3 rot = Vector3.zero;
        rot.z = 90.0f;
        iTween.RotateTo(gameObject, iTween.Hash("rotation", rot, "easetype", iTween.EaseType.easeInQuart, "time", 0.3f, "OnComplete", "onCompleteHit"));
    }

    void onCompleteHit()
    {
        iTween.RotateTo(gameObject, iTween.Hash("rotation", Vector3.zero, "easetype", iTween.EaseType.easeInQuart, "time", 0.5f, "OnComplete", "onCompleteDone"));
    }

    void onCompleteDone()
    {
        Destroy(gameObject);
    }
}
