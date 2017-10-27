using UnityEngine;
using System.Collections;

public class Naver_Login_btn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
#if UNITY_ANDROID
        AndroidManager.Instance.callLogin();
#endif
    }
}
