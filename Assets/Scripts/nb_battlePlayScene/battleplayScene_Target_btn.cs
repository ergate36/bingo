using UnityEngine;
using System.Collections;

public class battleplayScene_Target_btn : MonoBehaviour
{

    public enum Target_ID
    { 
        Target_Sheet00 = 0,
        Target_Sheet00_00,
        Target_Sheet00_01,
        Target_Sheet00_02,
        Target_Sheet00_03,

        Target_Sheet01,
        Target_Sheet01_00,
        Target_Sheet01_01,
        Target_Sheet01_02,
        Target_Sheet01_03,

        Target_Sheet02,
        Target_Sheet02_00,
        Target_Sheet02_01,
        Target_Sheet02_02,
        Target_Sheet02_03,


        Target_Sheet03,
        Target_Sheet03_00,
        Target_Sheet03_01,
        Target_Sheet03_02,
        Target_Sheet03_03,
    }

    private GameObject playScene;
    public Target_ID target;

    void Start()
    {
        playScene = GameObject.Find("PlayScene") as GameObject;
    }

    void OnClick()
    {

        int otherIndex = (int)target / 5;
        int sheetIndex = (int)target % 5;

        GlobalData.g_global.target_sheetindex = sheetIndex;
        GlobalData.g_global.ItemUseState = false;
        playScene.GetComponent<PlayScene>().setItemTarget(otherIndex, sheetIndex);


    }
}
