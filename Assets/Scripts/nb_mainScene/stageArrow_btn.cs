using UnityEngine;
using System.Collections;
using Spine.Unity;

public class stageArrow_btn : MonoBehaviour
{
    public bool isLeft;

    public nb_MainScene main;


	void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            //Debug.Log("MenuOn Return");
            return;
        }

        if (nb_GlobalData.g_global.MainShopActive == true)
        {
            //Debug.Log("ShopOn Return");
            return;
        }

        if (nb_GlobalData.g_global.MainStageMove == true)
        {
            //Debug.Log("MoveOn Return");
            return;
        }

        nb_GlobalData.g_global.MainStageMove = true;

        if (isLeft)
        {
            //Debug.Log("click Left");
            main.moveStagePrev();
        }
        else
        {
            //Debug.Log("click Right");
            main.moveStageNext();
        }
    }
}
