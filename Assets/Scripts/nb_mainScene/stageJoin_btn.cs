using UnityEngine;
using System.Collections;

public class stageJoin_btn : MonoBehaviour
{
    private GameObject mainSceneUI;
    private BoxCollider[] buttons;
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
        Debug.Log("click stage connect button");
        nbHttp.http.ConnectStage(
            nb_GlobalData.g_global.userSession.SessionKey, 1);
            //nb_GlobalData.g_global.selectStageId);
    }

    //IEnumerator stageSelect()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    Resources.UnloadUnusedAssets();
    //    System.GC.Collect();

    //    Application.LoadLevel("nb_LobbyScene");
    //}
}
