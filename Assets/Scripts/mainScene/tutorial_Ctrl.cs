using UnityEngine;
using System.Collections;

public class tutorial_Ctrl : MonoBehaviour {
    public Transform tutoImage;
    public int tag = 0;
    public GameObject root;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {

        GlobalData.g_global.invite_able = false;


        Transform btn_left = root.transform.Find("left");
        Transform btn_right = root.transform.Find("right");
        Transform btn_exit = root.transform.Find("exit");
        btn_exit.gameObject.SetActive(false);

        if (tag == 1)
        {
            GlobalData.g_global.tutorialPage--;
            tutoImage.GetComponent<UISprite>().spriteName = "guide_" + GlobalData.g_global.tutorialPage;
            // left

        }
        else if (tag == 2)
        {
            GlobalData.g_global.tutorialPage++;
            tutoImage.GetComponent<UISprite>().spriteName = "guide_" + GlobalData.g_global.tutorialPage;
            //right
        }
        else if (tag == 3)
        {
            GlobalData.g_global.invite_able = true;

            SaveTutoData(1);
            GlobalData.g_global.tutorialPage = 1;
            GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
            BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].enabled = true;
            }
            btn_left.gameObject.SetActive(true);
            btn_right.gameObject.SetActive(true);
            tutoImage.GetComponent<UISprite>().spriteName = "guide_" + GlobalData.g_global.tutorialPage;
            root.gameObject.SetActive(false);
        }

        
        if(GlobalData.g_global.tutorialPage ==1){
            btn_left.gameObject.SetActive(false);
        }
        else if (GlobalData.g_global.tutorialPage == 7)
        {
            btn_exit.gameObject.SetActive(true);
            btn_right.gameObject.SetActive(false);
        }
        else
        {
            btn_left.gameObject.SetActive(true);
            btn_right.gameObject.SetActive(true);
        }
        
    }
    void SaveTutoData(int tuto)
    {
        PlayerPrefs.SetInt("Tuto", tuto);
    }
}
