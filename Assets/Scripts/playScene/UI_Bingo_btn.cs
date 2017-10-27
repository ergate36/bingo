using UnityEngine;
using System.Collections;

public class UI_Bingo_btn : MonoBehaviour {

    GameObject playScene;

    void Start()
    {
        playScene = GameObject.Find("PlayScene") as GameObject;
    }

    void OnClick()
    {
        if(GlobalData.g_global.bingoCount == 0 ){
            return;
        }

        playScene.GetComponent<PlayScene>().onSendBingo();

    }

}
