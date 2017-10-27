using UnityEngine;
using System.Collections;

public class monsterSceneUI : MonoBehaviour {

    private GameObject popup;

    [HideInInspector]
    public Transform monsterUI;
    
    //panel
    [HideInInspector]
    public Transform m_infopopup;
    [HideInInspector]
    public Transform m_sellpopup;

    [HideInInspector]
    public Transform m_inputpanel;
    [HideInInspector]
    public Transform m_layerbase;
    [HideInInspector]
    public Transform m_upgradepanel;
    [HideInInspector]
    public Transform m_errorPopup;
    [HideInInspector]
    public Transform m_upgradeComplete;

    [HideInInspector]
    public Transform soundObj;
    [HideInInspector]
    public Transform soundObj2;
    [HideInInspector]
    public Transform m_mixpanel;
    //button

    [HideInInspector]
    public Transform m_input_btn;

    [HideInInspector]
    public Transform m_cardCount;
    
    [HideInInspector]
    public Transform m_output_btn;
    [HideInInspector]
    public Transform m_sell_btn;
    [HideInInspector]
    public Transform m_mix_btn;

    [HideInInspector]
    public Transform m_upgrade_btn;


    [HideInInspector]
    public Transform m_arrow;


    [HideInInspector]
    public Transform m_sellyes_btn;
    [HideInInspector]
    public Transform m_sellno_btn;
    [HideInInspector]
    public Transform m_moveupgrade_btn;

    // label
    [HideInInspector]
    public Transform m_at1_label;
    [HideInInspector]
    public Transform m_at2_label;
    [HideInInspector]
    public Transform m_at3_label;


    [HideInInspector]
    public Transform m_uat1_label;
    [HideInInspector]
    public Transform m_uat2_label;
    [HideInInspector]
    public Transform m_uat3_label;


    [HideInInspector]
    public Transform m_mat1_label;
    [HideInInspector]
    public Transform m_mat2_label;
    [HideInInspector]
    public Transform m_mat3_label;

    [HideInInspector]
    public Transform m_card_name;
    [HideInInspector]
    public Transform m_card_rank;

    [HideInInspector]
    public Transform m_mixcard_name;
    [HideInInspector]
    public Transform m_mixcard_rank;


    [HideInInspector]
    public Transform m_upgrade_cost;

    [HideInInspector]
    public Transform m_mix_cost;
    [HideInInspector]
    public Transform[] inputindex;

    [HideInInspector]
    public int[] inputindex_cardnum;


    [HideInInspector]
    public Transform[] upgrade_inputindex;


    [HideInInspector]
    public Transform m_input_click;



    [HideInInspector]
    public Transform m_sell_price;

    [HideInInspector]
    public Transform m_sell_yes;
    [HideInInspector]
    public Transform m_sell_no;
    [HideInInspector]
    public Transform m_sell_check;
    [HideInInspector]
    public Transform m_sell_Label;


    [HideInInspector]
    public int[] upgrade_input_flag;
    [HideInInspector]
    public int[] inputindex_flag;
    [HideInInspector]
    public int mix_flag;

    [HideInInspector]
    public Transform m_mix_input;
    [HideInInspector]
    public Transform m_mix_exit;
    [HideInInspector]
    public Transform m_upgrade_exit;

    [HideInInspector]
    public Transform m_info_mix;
    [HideInInspector]
    public Transform m_info_upgrade;
    [HideInInspector]
    public Transform m_upgrade_target;
    [HideInInspector]
    public Transform m_mix_target;
    [HideInInspector]
    public int[] upgradeMe;

    [HideInInspector]
    public int[] upgradeExp;

    [HideInInspector]
    public int mixobject;
    [HideInInspector]
    public int mixobjectId;

    [HideInInspector]
    public int upgradeobject;
    [HideInInspector]
    public int upgradeobjectId;
    [HideInInspector]
    public int expCurrent;

    [HideInInspector]
    public Transform m_upgrade_aLevel;
    [HideInInspector]
    public Transform m_upgrade_bLevel;
    
    [HideInInspector]
    public Transform m_error_message;
    [HideInInspector]
    public Transform m_upgradeComplete_message;

    [HideInInspector]
    public int m_panel_index ;


	// Use this for initialization
	void Start () {
        upgradeobject = 0;
        mixobject = 0;
        mixobjectId = 0;
        m_panel_index = 0;
        popup = GameObject.Find("mainSceneUI/Camera/Anchor") as GameObject;
        
        monsterUI = popup.transform.Find("popup_monster");

        soundObj = popup.transform.Find("sound");
        soundObj2 = popup.transform.Find("sound_bell");

        m_mixpanel = monsterUI.Find("mix_panel");
        m_infopopup = monsterUI.Find("infopopup_panel");
        m_sellpopup = monsterUI.Find("sellpopup_panel");
        m_inputpanel = monsterUI.Find("input_panel");
        m_layerbase = monsterUI.Find("layer_base");
        m_upgradepanel = monsterUI.Find("upgrade_panel");
        m_errorPopup = monsterUI.Find("errorpopup_panel");
        m_upgradeComplete = monsterUI.Find("upgradeComplete_panel");


        m_mix_btn = m_mixpanel.Find("btn_mix");
        m_sellyes_btn = m_sellpopup.Find("yes_btn");
        m_sellno_btn = m_sellpopup.Find("no_btn");

        m_cardCount = m_layerbase.Find("card_count");

        m_input_btn = m_infopopup.Find("btn_input");
        m_output_btn = m_infopopup.Find("btn_output");
        m_sell_btn = m_infopopup.Find("btn_sell");

        m_info_mix = m_infopopup.Find("mix_btn");
        m_info_upgrade = m_infopopup.Find("upgrade_btn");
        m_arrow = m_upgradepanel.Find("arrow");
        m_upgrade_btn = m_upgradepanel.Find("btn_upgrade");

        m_upgrade_target = m_upgradepanel.Find("upgrade_target");
        m_upgrade_aLevel = m_upgradepanel.Find("after_level");
        m_upgrade_bLevel = m_upgradepanel.Find("before_level");


        m_upgrade_exit = m_upgradepanel.Find("btn_exit");

        m_upgrade_cost = m_upgrade_btn.Find("cost");
        m_mix_cost = m_mix_btn.Find("cost");

        m_at1_label = m_infopopup.Find("at1_label");
        m_at2_label = m_infopopup.Find("at2_label");
        m_at3_label = m_infopopup.Find("at3_label");

        m_uat1_label = m_upgradepanel.Find("at1_label");
        m_uat2_label = m_upgradepanel.Find("at2_label");
        m_uat3_label = m_upgradepanel.Find("at3_label");

        m_mat1_label = m_mixpanel.Find("at1_label");
        m_mat2_label = m_mixpanel.Find("at2_label");
        m_mat3_label = m_mixpanel.Find("at3_label");


        m_card_rank = m_infopopup.Find("star");
        m_card_name = m_infopopup.Find("name");


        m_mixcard_rank = m_mixpanel.Find("star");
        m_mixcard_name = m_mixpanel.Find("name");


        m_sell_price = m_sellpopup.Find("price");
        m_sell_yes = m_sellpopup.Find("yes_btn");
        m_sell_no = m_sellpopup.Find("no_btn");
        m_sell_check = m_sellpopup.Find("check_btn");
        m_sell_Label = m_sellpopup.Find("Label");
        m_error_message = m_errorPopup.transform.Find("message");
        m_upgradeComplete_message = m_upgradeComplete.transform.Find("message");

        m_errorPopup.gameObject.SetActive(false);

        m_sell_check.gameObject.SetActive(false);
        m_upgradeComplete.gameObject.SetActive(false);
        m_sell_btn.gameObject.SetActive(true);
        m_input_btn.gameObject.SetActive(true);
        m_upgradepanel.gameObject.SetActive(false);
        m_output_btn.gameObject.SetActive(false);
        m_infopopup.gameObject.SetActive(false);
        inputindex = new Transform[4];
        upgrade_inputindex = new Transform[10];
        m_input_click = m_inputpanel.Find("input_click");
        upgrade_input_flag = new int[10];
        inputindex_flag = new int[4];
        upgradeMe = new int[10];
        inputindex_cardnum = new int[4];
        for (int i = 0; i < 10; i++)
        {
            upgradeMe[i] = 0;
        }

        expCurrent = 0;
        upgradeExp = new int[10];
        for (int i = 0; i < 10; i++)
        {
            upgradeExp[i] = 0;
        }


        mix_flag = 0;

        for (int i = 0; i < 4; i ++ )
        {
            inputindex_cardnum[i] = 0;
            
            
        }

        StopCoroutine("clickSet2");
        StopCoroutine("clickSet");

        m_input_click.gameObject.SetActive(false);

        for (int i = 0; i < 10; i ++ )
        {
            upgrade_inputindex[i] = m_upgradepanel.Find("input_"+i);
            upgrade_input_flag[i] = 0;
        }
        for (int i = 0; i < 4; i++)
        {
            inputindex[i] = m_inputpanel.Find("input_" + (i + 1));
            inputindex[i].GetComponent<BoxCollider>().enabled = true;
    
            inputindex_flag[i] = 0;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void callAni()
    {
        StartCoroutine(ani_ctrl());
    }
    private IEnumerator ani_ctrl()
    {
        GameObject mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_monster/card_List");
        BoxCollider[] buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }
        mainSceneUI = GameObject.Find("mainSceneUI/Camera/Anchor/popup_monster/upgrade_panel");
        buttons = mainSceneUI.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < buttons.Length; ++i)
        {
            buttons[i].enabled = false;
        }

        GameObject daubItemObject = Instantiate(Resources.Load("effects/yun/eff_card_02")) as GameObject;

        daubItemObject.transform.parent = m_upgrade_target;
        daubItemObject.transform.localPosition = Vector3.zero;
        daubItemObject.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(1.5f);
        Destroy(daubItemObject);

        m_upgradeComplete.gameObject.SetActive(true);
        m_upgradeComplete.transform.localScale = Vector3.one * 0.7f;
        iTween.ScaleTo(m_upgradeComplete.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
        iTween.ValueTo(m_upgradeComplete.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", m_upgradeComplete.gameObject));

        m_upgradeComplete_message.GetComponent<UILabel>().text = "upgrade has been completed.";

        soundObj2 = monsterUI.Find("sound_bell");
        soundObj2.GetComponent<AudioSource>().PlayOneShot(soundObj2.GetComponent<AudioSource>().clip);

        GlobalData.g_global.selectedCardNumber = 0;

       // m_upgradeinput_btn.gameObject.SetActive(true);
       // m_upgradeoutput_btn.gameObject.SetActive(false);

        

        GlobalData.g_global.closeType = 20;


    }
}
