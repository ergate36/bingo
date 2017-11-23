using UnityEngine;
using System.Collections;

public class stageJoin_btn : MonoBehaviour
{
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;

    public int stageIndex;
    public bool battleMode = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
	void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            return;
        }

        if (nb_GlobalData.g_global.MainShopActive == true)
        {
            return;
        }

        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/mainBase");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        var sound = this.GetComponent<AudioSource>();
        if (sound != null)
        {
            sound.Play();
        }

        //StartCoroutine("stageSelect");

        //게임리프트 서버 스테이지 접속 정보 가져옴
        int stageId = stageIndex;
        if(battleMode)
        {
            //배틀모드 스테이지id는 일반스테이지+100
            stageId += 100;
        }

        Debug.Log("click stage " + stageId.ToString() + " connect button");

        nbHttp.http.ConnectStage(
            nb_GlobalData.g_global.userSession.SessionKey, stageId);
    }

    //IEnumerator stageSelect()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    Resources.UnloadUnusedAssets();
    //    System.GC.Collect();

    //    Application.LoadLevel("nb_LobbyScene");
    //}
}
