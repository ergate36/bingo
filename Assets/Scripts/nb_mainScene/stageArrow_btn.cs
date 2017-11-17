using UnityEngine;
using System.Collections;
using Spine.Unity;

public class stageArrow_btn : MonoBehaviour
{
    public bool isLeft;

    // Use this for initialization

	void OnClick()
    {
        if (nb_GlobalData.g_global.MainMenuActive == true)
        {
            return;
        }

        if (nb_GlobalData.g_global.WorldStageSpineRefresh == false)
        {
            if (isLeft)
            {
                if (nb_GlobalData.g_global.SelectWorldStage > 1)
                {
                    nb_GlobalData.g_global.WorldStageSpineRefresh = true;
                    nb_GlobalData.g_global.WorldStageSpineAnimation =
                        "move" + nb_GlobalData.g_global.SelectWorldStage.ToString();

                    nb_GlobalData.g_global.SelectWorldStage -= 1;
                }
            }
            else
            {
                if (nb_GlobalData.g_global.SelectWorldStage < 2)    //스테이지가 아직 2개 밖에 없음
                {
                    nb_GlobalData.g_global.WorldStageSpineRefresh = true;
                    nb_GlobalData.g_global.WorldStageSpineAnimation =
                        "move" + nb_GlobalData.g_global.SelectWorldStage.ToString();

                    nb_GlobalData.g_global.SelectWorldStage += 1;
                }
            }
        }
    }
}
