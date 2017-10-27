using UnityEngine;
using System.Collections;

public class Sheet_select_btn : MonoBehaviour {
    public int sheetnum;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        for (int i = 1; i < 5; i++ )
        {
            GameObject check = GameObject.Find("mainSceneUI/Camera/Anchor/popup_invite/Sprite"+i);
            check.transform.GetComponent<UISprite>().spriteName = "";
            if(sheetnum == i){
                check.transform.GetComponent<UISprite>().spriteName = "check_big";
            }
        }

        GlobalData.g_global.selectSheet = sheetnum;
    }
}
