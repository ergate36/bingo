using UnityEngine;
using System.Collections;

public class nb_LobbyBlitzSceneUI : MonoBehaviour {
    
    [HideInInspector]
    public GameObject uiRoot;

    [HideInInspector]
    public GameObject shop;

    [HideInInspector]
    public Transform m_layerBase;
    [HideInInspector]
    public Transform m_layerItem;


    [HideInInspector]
    public Transform m_UIPlayerCount;

    [HideInInspector]
    public Transform[] m_sheetBtn;

    [HideInInspector]
    public Transform m_UIWaitTime;

    [HideInInspector]
    public Transform m_waitPopup;

    [HideInInspector]
    public Transform[] ready;
    [HideInInspector]
    public Transform[] wait;
    [HideInInspector]
    public Transform    mentRetry;
    [HideInInspector]   
    public Transform    mentWait;
    [HideInInspector]
    public Transform    mentStart;
    [HideInInspector]
    public Transform cancel_btn;

    [HideInInspector]
    public Transform countDown;

    [HideInInspector]
    public Transform countDown2;

    [HideInInspector]
    public Transform startcountDown;

    //

    [HideInInspector]
    public Transform progressInfoRoot;

    [HideInInspector]
    public Transform waitRemainRoot;

    [HideInInspector]
    public Transform cardSelectGroup;

    [HideInInspector]
    public Transform waitTextImageSecond;

    [HideInInspector]
    public Transform waitTextImageBingo;

    void Awake()
    {
        m_sheetBtn = new Transform[4];

        uiRoot = GameObject.Find("lobbySceneUI/Camera/Anchor/nb_lobbyBase");
        shop = GameObject.Find("lobbySceneUI/Camera/Anchor/shop");
        m_sheetBtn[0] = uiRoot.transform.Find("layer1/card_selection/sheet01_btn");
        m_sheetBtn[1] = uiRoot.transform.Find("layer1/card_selection/sheet02_btn");
        m_sheetBtn[2] = uiRoot.transform.Find("layer1/card_selection/sheet03_btn");
        m_sheetBtn[3] = uiRoot.transform.Find("layer1/card_selection/sheet04_btn");

        m_waitPopup = uiRoot.transform.Find("popup_wait");
        cancel_btn = m_waitPopup.transform.Find("cancel_btn");


        progressInfoRoot = uiRoot.transform.Find("layer1/progress_info");
        waitRemainRoot = uiRoot.transform.Find("layer1/wait_remain_group");

        countDown = waitRemainRoot.Find("t_remain_count");
        countDown2 = progressInfoRoot.Find("t_remain_count");

        cardSelectGroup = uiRoot.transform.Find("layer1/card_selection");
        
        waitTextImageSecond = waitRemainRoot.Find("i_second_img");
        waitTextImageBingo = waitRemainRoot.Find("i_bingo_img");

    }

    // Use this for initialization
    void Start()
    {
        m_UIWaitTime = uiRoot.transform.Find("popup_wait/ment_wait/countDown");
        
        m_waitPopup.gameObject.SetActive(false);

    }
}
