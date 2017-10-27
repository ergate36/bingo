using UnityEngine;
using System.Collections;

public class playScene_ItemUse_btn : MonoBehaviour
{

    GameObject playScene;

    void Start()
    {
        playScene = GameObject.Find("PlayBlitzScene") as GameObject;
    }

    void OnClick()
    {

        //if ((int)playScene.GetComponent<nb_PlayBlitzScene>().gaugeState != 1)
        {
            return;
        }

        playScene.GetComponent<nb_PlayBlitzScene>().onUseItem();
    }


}
