using UnityEngine;
using System.Collections;

public class Notice_Ctrl : MonoBehaviour {

    public int flag = 0;
    GameObject loginObj;
    public int value = 0;
	// Use this for initialization
	void Start () {
        loginObj = GameObject.Find("LoginScene");
        if(flag == 2){
            transform.GetComponent<UISprite>().spriteName = "";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnClick()
    {
        // 이미지 링크 이동
        if(flag ==1){
  //          Debug.Log("MOVE URL");
//            Debug.Log(GlobalData.g_global.urlCount);

            //Debug.Log(GlobalData.g_global.mUrlInfo[GlobalData.g_global.urlCount].move_url);

            Application.OpenURL(GlobalData.g_global.mUrlInfo[GlobalData.g_global.urlCount].move_url);
        }
        // 다시 보지않음 
        else if (flag == 2)
        {
            if (value == 0)
            {
                value = 1;
                SaveNoticeViewData(GlobalData.g_global.mUrlInfo[GlobalData.g_global.urlCount].notice_Num);
                transform.GetComponent<UISprite>().spriteName = "check";
            }
            else
            {
                value = 0;
                SaveNoticeViewData2(GlobalData.g_global.mUrlInfo[GlobalData.g_global.urlCount].notice_Num);
                transform.GetComponent<UISprite>().spriteName = "";
            }
        }
        // 확인 
        else if (flag == 3)
        {
            GlobalData.g_global.urlCount++;
            GameObject root = GameObject.Find("loginSceneUI/Camera/Anchor/Panel") as GameObject;
            Transform tex = root.transform.Find("url/Urltexture");
            root.transform.Find("url/check").GetComponent<UISprite>().spriteName = "";

            StartCoroutine(loginObj.GetComponent<LoginScene>().GetWebImage(GlobalData.g_global.urlCount));

            //SaveNoticeViewData2(GlobalData.g_global.mUrlInfo[GlobalData.g_global.urlCount].notice_Num);
            
            //tex.gameObject.SetActive(false);
        }
    }

    void SaveNoticeViewData(int noticeViewNum)
    {
        PlayerPrefs.SetInt("noticeView" + noticeViewNum, 1);
    }
    void SaveNoticeViewData2(int noticeViewNum)
    {
        PlayerPrefs.SetInt("noticeView" + noticeViewNum, 0);
    }
}
