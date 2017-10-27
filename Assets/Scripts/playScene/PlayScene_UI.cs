using UnityEngine;
using System.Collections;

public class PlayScene_UI : MonoBehaviour {

    private string playUIPath = "playSceneUI/Camera/Anchor";

    // panels
    private Transform   playUI;

    [HideInInspector]
    public Transform m_debugBoard;
    [HideInInspector]
    public Transform    m_baseBoard;
    private Transform   m_bingoBoard;
    [HideInInspector]
    public Transform   m_sheetBoard;
    private Transform   m_effectLayer01Board;
    [HideInInspector]
    public Transform    m_effectLayer02Board;
    [HideInInspector]
    public Transform m_damageBoard;
    private Transform   m_otherBoard;
    [HideInInspector]
    public Transform   m_targetBoard;
    [HideInInspector]
    public Transform   m_resultBoard;
    
    [HideInInspector]
    public Transform   m_ItemBoard;
    [HideInInspector]
    public Transform    m_daubBoard;
    [HideInInspector]
    public Transform m_otherOut;

    // controls
    [HideInInspector]
    public Transform        m_effectNotice;
    [HideInInspector]
    public Transform        m_ballStart_pos;
    [HideInInspector]
    public Transform[]      m_sheetbuttons;
   
    
    [HideInInspector]
    public Transform        m_item;
    [HideInInspector]
    public Transform[]      m_itemGauge;
    [HideInInspector]
    public Transform        m_itemCooltime;

    // game over
    [HideInInspector]
    public Transform        m_eff_gameover;
    [HideInInspector]
    public Transform        m_talk_gameover;

    [HideInInspector]
    public Transform        m_bingoSheetBG;

    [HideInInspector]
    public Transform        m_badBingoSheetBG;     // badbingo

    [HideInInspector]
    public Transform        m_shieldEffect_pos;
   // [HideInInspector]
   // public Transform        m_frozenEffect_pos;
    [HideInInspector]
    public Transform        m_commonEffect_pos;
    [HideInInspector]
    public Transform        m_blindEffect_pos;
    [HideInInspector]
    public Transform[, ,]   m_otherSheets;
    [HideInInspector]
    public Transform[]      m_cells;
    [HideInInspector]
    public Transform[]      m_targets;
    [HideInInspector]
    public Transform[,]     m_targetSheets;
    [HideInInspector]
    public Transform[,]     m_otherBingoSheets;

    [HideInInspector]
    public Transform[]      m_othersName;
    [HideInInspector]
    public Transform m_gameScore;
    


    // effectSound
    [HideInInspector]
    public Transform        m_sound_attackEffect;
    [HideInInspector]
    public Transform        m_sound_damageEffect;
    [HideInInspector]
    public Transform        m_sound_bingoEffect;
    [HideInInspector]
    public Transform        m_sound_daubEffect;

    [HideInInspector]
    public Transform        m_sound_cleardaubEffect;

    [HideInInspector]
    public Transform        m_talk_attackEffect;

    [HideInInspector]
    public Transform        m_talk_bingoEffect;

    [HideInInspector]
    public Transform         m_talk_daubEffect;


    [HideInInspector]
    public Transform        m_talk_itemReady;

    [HideInInspector]
    public Transform        m_bingoButton;

    // all btn
   
    // game message
    [HideInInspector]
    public Transform m_gameMessage_target;
    [HideInInspector]
    public Transform m_gameMessage_action;

    [HideInInspector]
    public Transform label_bingoCount;

    // buttons
    [HideInInspector]
    public BoxCollider[] buttons;
    [HideInInspector]
    public BoxCollider[] resultButtons;


    // noitemMsg
    [HideInInspector]
    public Transform noItemMsg;


    [HideInInspector]
    public int daubindex;
    [HideInInspector]
    public int daubnumber;


    void Awake()
    {
        playUI = GameObject.Find(playUIPath).transform;

        m_otherOut = playUI.Find("popup_OtherOut");
        m_baseBoard             = playUI.Find("Base_Board");
        m_bingoBoard            = playUI.Find("Bingo_Board");
        m_sheetBoard            = playUI.Find("SheetIndex_Board");
        m_daubBoard             = playUI.Find("Daub_Board");
        m_effectLayer01Board    = playUI.Find("EffectBoard_Layer01");
        m_effectLayer02Board    = playUI.Find("EffectBoard_Layer02");
        m_otherBoard            = playUI.Find("Other_Board");
        m_targetBoard           = playUI.Find("Target_Board");
        m_ItemBoard             = playUI.Find("Item_Board");
        m_resultBoard           = playUI.Find("Result_Board");
        m_debugBoard            = playUI.Find("Debug_Board");
        m_damageBoard = playUI.Find("Damage_Board");
        noItemMsg = m_ItemBoard.Find("noItemMsg");

      
        m_otherSheets   = new Transform[4, 4, 25];
        m_cells         = new Transform[25];
        m_targets       = new Transform[4];
        m_targetSheets  = new Transform[4, 4];
        m_itemGauge     = new Transform[3];
        m_sheetbuttons  = new Transform[4];
        m_targets       = new Transform[4];
        m_otherBingoSheets = new Transform[4, 4];
        m_othersName = new Transform[4];

        m_gameMessage_target = m_baseBoard.Find("gameMessage/target");
        m_gameMessage_action = m_baseBoard.Find("gameMessage/action");
        label_bingoCount = m_baseBoard.Find("label_bingoCount");

        buttons = playUI.GetComponentsInChildren<BoxCollider>();

        Transform resultpopup = playUI.Find("popup_result");
        resultButtons = resultpopup.GetComponentsInChildren<BoxCollider>();

    }

    void Start()
    {
        Init_UIControls();
	}

    void Init_UIControls()
    {
        m_bingoSheetBG      = m_effectLayer01Board.Find("bingo_blind");
        m_badBingoSheetBG   = m_effectLayer01Board.Find("badbingo_blind");
        m_ballStart_pos     = m_baseBoard.Find("ballStartPos");

        m_sound_attackEffect    = m_effectLayer02Board.Find("effect_attack_sound");
        m_sound_damageEffect    = m_effectLayer02Board.Find("effect_damage_sound");
        m_sound_bingoEffect     = m_effectLayer02Board.Find("effect_bingo_sound");
        m_sound_daubEffect      = m_effectLayer02Board.Find("effect_daub_sound");
        m_sound_cleardaubEffect = m_effectLayer02Board.Find("effect_cleardaub_sound");
        
        m_eff_gameover      = m_effectLayer02Board.Find("effect_gameover");
        m_talk_attackEffect = m_effectLayer02Board.Find("effect_attack_talk");
        m_talk_itemReady    = m_effectLayer02Board.Find("effect_item_ready_talk");
        m_talk_bingoEffect  = m_effectLayer02Board.Find("effect_bingo_talk");
        m_talk_daubEffect   = m_effectLayer02Board.Find("effect_daub_talk");

        m_talk_gameover = m_effectLayer02Board.Find("effect_gameover_talk");
        m_eff_gameover.gameObject.SetActive(false);

        m_otherOut.gameObject.SetActive(false);

        m_gameScore = m_baseBoard.Find("gameScore");

        //othername
        m_othersName[0] = m_baseBoard.Find("users_name/name_00");
        m_othersName[1] = m_baseBoard.Find("users_name/name_01");
        m_othersName[2] = m_baseBoard.Find("users_name/name_02");
        m_othersName[3] = m_baseBoard.Find("users_name/name_03");

        // item gauge
        m_itemGauge[0] = m_ItemBoard.Find("gauge0");
        m_itemGauge[1] = m_ItemBoard.Find("gauge1");
        m_itemGauge[2] = m_ItemBoard.Find("gauge2");
        m_item = m_ItemBoard.Find("item");
        m_itemCooltime = m_ItemBoard.Find("gauge_cooltime");
       
        m_item.gameObject.SetActive(false);
        m_itemCooltime.gameObject.SetActive(false);
        m_itemGauge[0].gameObject.SetActive(true);
        m_itemGauge[1].gameObject.SetActive(false);
        m_itemGauge[2].gameObject.SetActive(false);

        m_badBingoSheetBG.gameObject.SetActive(false);
        m_bingoSheetBG.gameObject.SetActive(false);

        m_resultBoard.gameObject.SetActive(false);
        

        // sheet index button
        for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
        { 
            string sheetPath = "sheet0" + sheetIndex.ToString() +"_btn";
            m_sheetbuttons[sheetIndex] = m_sheetBoard.Find(sheetPath);
            m_sheetbuttons[sheetIndex].GetComponent<UIImageButton>().isEnabled = false;
        }

        // mysheets
        for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
        {
            string cellUI_name = "label/cell_";

            if (cellIndex < 10)
            {
                cellUI_name += "0";
            }

            cellUI_name += cellIndex;
            m_cells[cellIndex] = m_bingoBoard.Find(cellUI_name);
        }

        // other sheets
        for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        {
            string otherPath = "other_0" + otherIndex.ToString();

            for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
            {
                string sheetPath = otherPath + "/sheet_0" + sheetIndex.ToString();

                for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
                {
                    string cellPath = sheetPath;
                    if (cellIndex < 10)
                    {
                        cellPath += "/cell_0";
                    }
                    else
                    {
                        cellPath += "/cell_";
                    }

                    cellPath += cellIndex.ToString();

                    //   Debug.Log(cellPath);
                    m_otherSheets[otherIndex, sheetIndex, cellIndex] = m_otherBoard.Find(cellPath);
                }
            }
        }

        // target
        for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        {
            string targetPath = "target0" + otherIndex.ToString();
            string otherBingoPath = "bingo0" + otherIndex.ToString();

            m_targets[otherIndex] = m_targetBoard.Find(targetPath);
            m_targets[otherIndex].gameObject.SetActive(false);

            for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
            {
                string targetSheetPath = targetPath + "_0" + sheetIndex.ToString();
                m_targetSheets[otherIndex, sheetIndex] = m_targetBoard.Find(targetSheetPath);
                m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(false);

                string bingoSheetPath = otherBingoPath + "_0" + sheetIndex.ToString();
              
                m_otherBingoSheets[otherIndex, sheetIndex] = m_targetBoard.Find(bingoSheetPath);

                if (m_otherBingoSheets[otherIndex, sheetIndex] == null)
                {
                }

                m_otherBingoSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
            }
        }


        // effect
        //m_frozenEffect = m_ItemBoard.FindChild("item_frozen_effect");
        //m_frozenEffect.gameObject.SetActive(false);

        m_bingoButton = m_sheetBoard.Find("bingo_btn");

        // item check
        if (GlobalData.g_global.myInfo.ItemCount <= 0)
        {
            noItemMsg.gameObject.SetActive(true);
        }
        else {
            noItemMsg.gameObject.SetActive(false);
        }

    }

    public void setSheetButton_Bingo(int index)
    {
        m_sheetbuttons[index].GetComponentInChildren<UISprite>().spriteName = "sheetbingo";
        m_sheetbuttons[index].GetComponentInChildren<UISprite>().MakePixelPerfect();
        m_sheetbuttons[index].GetComponent<UIImageButton>().normalSprite = "sheetbingo";
        m_sheetbuttons[index].GetComponent<UIImageButton>().pressedSprite = "sheetbingo";
        

    }

}
