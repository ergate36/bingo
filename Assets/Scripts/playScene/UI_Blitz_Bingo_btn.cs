﻿using UnityEngine;
using System.Collections;

public class UI_Blitz_Bingo_btn : MonoBehaviour {

    public enum SheetIndex
    {
        Sheet_00 = 0,
        Sheet_01,
        Sheet_02,
        Sheet_03,
    }

    public SheetIndex sheetIndex;

    GameObject playBlitzScene;


    void Start()
    {
        playBlitzScene = GameObject.Find("PlayBlitzScene") as GameObject;
    }

    void OnClick()
    {
        //if(GlobalData.g_global.bingoCount == 0 ){
        //    return;
        //}

        nb_GlobalData.g_global.mSelectBingoButtonIndex = (int)sheetIndex;
        playBlitzScene.GetComponent<PlayBlitzScene>().onSendBingo((int)sheetIndex);

        Debug.Log("select sheet count : " + ((int)sheetIndex).ToString());

    }

}
