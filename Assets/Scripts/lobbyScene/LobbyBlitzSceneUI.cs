using UnityEngine;
using System.Collections;

public class LobbyBlitzSceneUI : MonoBehaviour {
    
    [HideInInspector]
    public GameObject uiRoot;

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
    public Transform btn_test;

    [HideInInspector]
    public Transform tip_label;


    [HideInInspector]
    public Transform countDown;

    [HideInInspector]
    public Transform countDown2;

    [HideInInspector]
    public Transform startcountDown;

    void Awake()
    {
        m_sheetBtn = new Transform[4];

        uiRoot = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobbyBase");
        m_sheetBtn[0] = uiRoot.transform.Find("layer1/sheet01_btn");
        m_sheetBtn[1] = uiRoot.transform.Find("layer1/sheet02_btn");
        m_sheetBtn[2] = uiRoot.transform.Find("layer1/sheet03_btn");
        m_sheetBtn[3] = uiRoot.transform.Find("layer1/sheet04_btn");

        m_waitPopup = uiRoot.transform.Find("popup_wait");
        btn_test = m_waitPopup.transform.Find("btn_test");
        tip_label = m_waitPopup.transform.Find("tip");
        countDown = m_waitPopup.Find("Count");
        countDown2 = m_waitPopup.Find("Count2");

        //m_layerItem = GameObject.Find("lobbyBlitzSceneUI/Camera/Anchor/lobby_layer_Item").transform;

       
    }

    // Use this for initialization
    void Start()
    {
        m_UIWaitTime = uiRoot.transform.Find("popup_wait/ment_wait/countDown");
        
        m_waitPopup.gameObject.SetActive(false);

    }
}
