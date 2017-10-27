using UnityEngine;
using System.Collections;

public class Update_btn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        Application.OpenURL(GlobalData.g_global.updateUrl);
        //Application.OpenURL("market://details?q=pname:com.mycopmany.myapp/");

    }
}
