using UnityEngine;
using System.Collections;

public class nb_battlePlayScene_UI : MonoBehaviour
{

    private string playUIPath = "BattlePlaySceneUI/Camera/Anchor";

    // panels
    [HideInInspector]
    public Transform playUI;

    [HideInInspector]
    public Transform m_debugBoard;
    [HideInInspector]
    public Transform    m_baseBoard;
    [HideInInspector]
    public Transform   m_bingoBoard;
    [HideInInspector]
    public Transform m_sheetBoard;
    [HideInInspector]
    public Transform m_effectLayer01Board;
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
    public Transform        m_itemBtn;
    [HideInInspector]
    public Transform      m_itemGauge;
    [HideInInspector]
    public Transform        m_itemCooltime;

    // game over
    //[HideInInspector]
    //public Transform        m_eff_gameover;
    //[HideInInspector]
    //public Transform        m_talk_gameover;

    [HideInInspector]
    public Transform[]        m_bingoSheetBG;

    [HideInInspector]
    public Transform[]        m_badBingoSheetBG;     // badbingo

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
    public Transform[,]      m_cells;
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
    //[HideInInspector]
    //public Transform        m_sound_attackEffect;
    //[HideInInspector]
    //public Transform        m_sound_damageEffect;
    //[HideInInspector]
    //public Transform        m_sound_bingoEffect;
    //[HideInInspector]
    //public Transform        m_sound_daubEffect;

    //[HideInInspector]
    //public Transform        m_sound_cleardaubEffect;

    //[HideInInspector]
    //public Transform        m_talk_attackEffect;

    //[HideInInspector]
    //public Transform        m_talk_bingoEffect;

    //[HideInInspector]
    //public Transform         m_talk_daubEffect;


    //[HideInInspector]
    //public Transform        m_talk_itemReady;

    [HideInInspector]
    public Transform[]        m_bingoButton;

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

    //나온 daub 리스트
    [HideInInspector]
    public Transform[] m_daubBoardNumber;

    //게임종료 팝업
    [HideInInspector]
    public Transform popup_gameEnd;

    //게임종료 이펙트
    [HideInInspector]
    public Transform effect_end;


    void Awake()
    {
        playUI = GameObject.Find(playUIPath).transform;

        m_itemBtn = playUI.Find("item_btn");

        m_otherOut = playUI.Find("popup_OtherOut");
        m_baseBoard = playUI.Find("progress_info");
        m_bingoBoard = playUI.Find("bingo_sheet");
        m_sheetBoard            = playUI.Find("SheetIndex_Board");
        m_daubBoard = playUI.Find("history_info");
        m_effectLayer01Board = playUI.Find("EffectBoard_Layer01");
        m_effectLayer02Board = playUI.Find("EffectBoard_Layer02");
        m_otherBoard = playUI.Find("other_sheets");
        m_targetBoard           = playUI.Find("Target_Board");
        m_ItemBoard             = playUI.Find("Item_Board");
        m_resultBoard           = playUI.Find("Result_Board");
        m_debugBoard            = playUI.Find("Debug_Board");
        m_damageBoard = playUI.Find("Damage_Board");
        //noItemMsg = m_ItemBoard.Find("noItemMsg");

        popup_gameEnd = playUI.Find("popup_GameEnd");
        effect_end = playUI.Find("effect_end");
      
        m_otherSheets   = new Transform[3, 4, 25];
        m_cells         = new Transform[4, 25];
        m_targets       = new Transform[4];
        m_targetSheets  = new Transform[4, 4];
        m_targets       = new Transform[4];
        m_otherBingoSheets = new Transform[4, 4];
        m_othersName = new Transform[4];
        m_bingoButton = new Transform[4];

        m_bingoSheetBG = new Transform[4];
        m_badBingoSheetBG = new Transform[4];

        m_gameMessage_target = m_baseBoard.Find("gameMessage/target");
        m_gameMessage_action = m_baseBoard.Find("gameMessage/action");
        label_bingoCount = m_baseBoard.Find("t_remain_count");

        buttons = playUI.GetComponentsInChildren<BoxCollider>();

        Transform resultpopup = playUI.Find("popup_result");
        resultButtons = resultpopup.GetComponentsInChildren<BoxCollider>();

        m_daubBoardNumber = new Transform[75];
        for (int i = 0; i < 75; ++i)
        {
            m_daubBoardNumber[i] = m_daubBoard.Find("number_group/t_number_" + (i + 1).ToString());
        }

        Init_UIControls();
    }

    void Start()
    {
        //Init_UIControls();
	}

    void Init_UIControls()
    {
        m_bingoSheetBG[0]   = m_effectLayer01Board.Find("bingo_blind_0");
        m_bingoSheetBG[1]   = m_effectLayer01Board.Find("bingo_blind_1");
        m_bingoSheetBG[2]   = m_effectLayer01Board.Find("bingo_blind_2");
        m_bingoSheetBG[3]   = m_effectLayer01Board.Find("bingo_blind_3");
        m_badBingoSheetBG[0] = m_effectLayer01Board.Find("badbingo_blind_0");
        m_badBingoSheetBG[1] = m_effectLayer01Board.Find("badbingo_blind_1");
        m_badBingoSheetBG[2] = m_effectLayer01Board.Find("badbingo_blind_2");
        m_badBingoSheetBG[3] = m_effectLayer01Board.Find("badbingo_blind_3");
        m_ballStart_pos     = m_baseBoard.Find("ballStartPos");

        //m_sound_attackEffect    = m_effectLayer02Board.Find("effect_attack_sound");
        //m_sound_damageEffect    = m_effectLayer02Board.Find("effect_damage_sound");
        //m_sound_bingoEffect     = m_effectLayer02Board.Find("effect_bingo_sound");
        //m_sound_daubEffect      = m_effectLayer02Board.Find("effect_daub_sound");
        //m_sound_cleardaubEffect = m_effectLayer02Board.Find("effect_cleardaub_sound");
        
        //m_eff_gameover      = m_effectLayer02Board.Find("effect_gameover");
        //m_talk_attackEffect = m_effectLayer02Board.Find("effect_attack_talk");
        //m_talk_itemReady    = m_effectLayer02Board.Find("effect_item_ready_talk");
        //m_talk_bingoEffect  = m_effectLayer02Board.Find("effect_bingo_talk");
        //m_talk_daubEffect   = m_effectLayer02Board.Find("effect_daub_talk");

        //m_talk_gameover = m_effectLayer02Board.Find("effect_gameover_talk");
        //m_eff_gameover.gameObject.SetActive(false);

        m_otherOut.gameObject.SetActive(false);

        m_gameScore = m_baseBoard.Find("gameScore");

        //othername
        m_othersName[0] = m_baseBoard.Find("users_name/name_00");
        m_othersName[1] = m_baseBoard.Find("users_name/name_01");
        m_othersName[2] = m_baseBoard.Find("users_name/name_02");
        m_othersName[3] = m_baseBoard.Find("users_name/name_03");

        // item gauge
        m_itemGauge = m_itemBtn.Find("i_gauge");

        m_itemGauge.GetComponent<UISprite>().fillAmount = 0;
       
        //m_item.gameObject.SetActive(false);
        //m_itemCooltime.gameObject.SetActive(false);
        //m_itemGauge[0].gameObject.SetActive(true);
        //m_itemGauge[1].gameObject.SetActive(false);
        //m_itemGauge[2].gameObject.SetActive(false);

        m_badBingoSheetBG[0].gameObject.SetActive(false);
        m_badBingoSheetBG[1].gameObject.SetActive(false);
        m_badBingoSheetBG[2].gameObject.SetActive(false);
        m_badBingoSheetBG[3].gameObject.SetActive(false);
        m_bingoSheetBG[0].gameObject.SetActive(false);
        m_bingoSheetBG[1].gameObject.SetActive(false);
        m_bingoSheetBG[2].gameObject.SetActive(false);
        m_bingoSheetBG[3].gameObject.SetActive(false);

        m_resultBoard.gameObject.SetActive(false);
        
        
        // mysheets
        Debug.Log("sheet + cell ui setting start");
        for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
        {
            for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
            {
                string cellUI_name = "sheet_" + sheetIndex + "/cell_";

                if (cellIndex < 10)
                {
                    cellUI_name += "0";
                }

                cellUI_name += cellIndex;
                //Debug.Log("cellUI_name : " + cellUI_name);
                m_cells[sheetIndex, cellIndex] = m_bingoBoard.Find(cellUI_name);
            }
        }

        // other sheets
        for (int otherIndex = 0; otherIndex < 3; ++otherIndex)
        {
            string otherPath = "player_sheets_" + (otherIndex + 1).ToString();

            for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
            {
                string sheetPath = otherPath + "/sheet" + (sheetIndex + 1).ToString();

                for (int cellIndex = 0; cellIndex < 25; ++cellIndex)
                {
                    string cellPath = sheetPath + "/other_sheet";
                    if (cellIndex < 10)
                    {
                        cellPath += "/other_daub_0";
                    }
                    else
                    {
                        cellPath += "/other_daub_";
                    }

                    cellPath += cellIndex.ToString();

                    //   Debug.Log(cellPath);
                    m_otherSheets[otherIndex, sheetIndex, cellIndex] = m_otherBoard.Find(cellPath);
                }
            }
        }

        // target
        //for (int otherIndex = 0; otherIndex < 4; ++otherIndex)
        //{
        //    string targetPath = "target0" + otherIndex.ToString();
        //    string otherBingoPath = "bingo0" + otherIndex.ToString();

        //    m_targets[otherIndex] = m_targetBoard.Find(targetPath);
        //    m_targets[otherIndex].gameObject.SetActive(false);

        //    for (int sheetIndex = 0; sheetIndex < 4; ++sheetIndex)
        //    {
        //        string targetSheetPath = targetPath + "_0" + sheetIndex.ToString();
        //        m_targetSheets[otherIndex, sheetIndex] = m_targetBoard.Find(targetSheetPath);
        //        m_targetSheets[otherIndex, sheetIndex].gameObject.SetActive(false);

        //        string bingoSheetPath = otherBingoPath + "_0" + sheetIndex.ToString();
              
        //        m_otherBingoSheets[otherIndex, sheetIndex] = m_targetBoard.Find(bingoSheetPath);

        //        if (m_otherBingoSheets[otherIndex, sheetIndex] == null)
        //        {
        //        }

        //        m_otherBingoSheets[otherIndex, sheetIndex].gameObject.SetActive(false);
        //    }
        //}


        // effect
        //m_frozenEffect = m_ItemBoard.FindChild("item_frozen_effect");
        //m_frozenEffect.gameObject.SetActive(false);

        m_bingoButton[0] = m_bingoBoard.Find("sheet_0").transform.Find("bingo_btn");
        m_bingoButton[1] = m_bingoBoard.Find("sheet_1").transform.Find("bingo_btn");
        m_bingoButton[2] = m_bingoBoard.Find("sheet_2").transform.Find("bingo_btn");
        m_bingoButton[3] = m_bingoBoard.Find("sheet_3").transform.Find("bingo_btn");

        // item check
        //if (nb_GlobalData.g_global.myInfo.ItemCount <= 0)
        //{
        //    noItemMsg.gameObject.SetActive(true);
        //}
        //else {
        //    noItemMsg.gameObject.SetActive(false);
        //}

    }

}
