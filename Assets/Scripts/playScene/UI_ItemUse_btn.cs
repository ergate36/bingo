using UnityEngine;
using System.Collections;

public class UI_ItemUse_btn : MonoBehaviour {

    GameObject playScene;

    void Start()
    {
        playScene = GameObject.Find("PlayScene") as GameObject;
    }

    void OnClick()
    {

        if (playScene.GetComponent<PlayScene>().m_onFrozen)
        {
            return;
        }

        if(GlobalData.g_global.ItemUseState == false){
            GlobalData.g_global.ItemUseState = true;

            GlobalData.g_global.useItemCount++;
            GlobalData.g_global.myInfo.ItemCount--;
            playScene.GetComponent<PlayScene>().onUseItem();
        }        
    }


}
