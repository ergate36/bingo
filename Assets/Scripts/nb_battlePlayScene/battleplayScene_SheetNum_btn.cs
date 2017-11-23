using UnityEngine;
using System.Collections;

public class battleplayScene_SheetNum_btn : MonoBehaviour
{

    GameObject gameScene;

    public enum SheetNum
    {
        Sheet_01 = 0,
        Sheet_02,
        Sheet_03,
        Sheet_04,
    };

    public SheetNum sheetNo;

    void Start()
    {
        gameScene = GameObject.Find("PlayScene") as GameObject;

        if ((int)sheetNo >= GlobalData.g_global.sheetInfo.activeSheetCount)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnClick()
    {
        
        //AudioClip clip = GetComponent<AudioSource>().clip;
        //audio.PlayOneShot(clip);
        if ((int)sheetNo < GlobalData.g_global.sheetInfo.activeSheetCount)
        {
            gameScene.GetComponent<PlayScene>().activeBingoSheet((int)sheetNo, false);
        }
    }
}
