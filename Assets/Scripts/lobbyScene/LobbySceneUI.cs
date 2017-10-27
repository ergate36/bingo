using UnityEngine;
using System.Collections;

public class LobbySceneUI : MonoBehaviour {
    
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
    public Transform startcountDown;

    void Awake()
    {
        m_sheetBtn = new Transform[4];

        uiRoot = GameObject.Find("lobbySceneUI/Camera/Anchor/lobbyBase");
        m_sheetBtn[0] = uiRoot.transform.Find("layer1/sheet01_btn");
        m_sheetBtn[1] = uiRoot.transform.Find("layer1/sheet02_btn");
        m_sheetBtn[2] = uiRoot.transform.Find("layer1/sheet03_btn");
        m_sheetBtn[3] = uiRoot.transform.Find("layer1/sheet04_btn");

        m_waitPopup = uiRoot.transform.Find("popup_wait");
        btn_test = m_waitPopup.transform.Find("btn_test");
        tip_label = m_waitPopup.transform.Find("tip");
        countDown = m_waitPopup.Find("Count");

        m_layerItem = GameObject.Find("lobbySceneUI/Camera/Anchor/lobby_layer_Item").transform;

       // ready = new Transform[4];
       // ready[0] = m_waitPopup.FindChild("ready_01");
      //  ready[1] = m_waitPopup.FindChild("ready_02");
      //  ready[2] = m_waitPopup.FindChild("ready_03");
      //  ready[3] = m_waitPopup.FindChild("ready_04");

      //  wait = new Transform[4];
     //   wait[0] = m_waitPopup.FindChild("wait_01");
    //    wait[1] = m_waitPopup.FindChild("wait_02");
   //     wait[2] = m_waitPopup.FindChild("wait_03");
  //      wait[3] = m_waitPopup.FindChild("wait_04");

       // mentRetry = m_waitPopup.FindChild("ment_retry");
      //  mentWait = m_waitPopup.FindChild("ment_wait");
      //  mentStart = m_waitPopup.FindChild("ment_start");
      //  startcountDown = m_waitPopup.FindChild("ment_start/countDown");
       // mentStart.gameObject.SetActive(false);

       
    }
    /*
    void initPopup()
    {
        Debug.Log("==============initpopup=============");
      
        mentRetry.gameObject.SetActive(false);
        mentWait.gameObject.SetActive(true);

        for (int i = 0; i < 4; ++i)
        {
            ready[i].gameObject.SetActive(false);
            wait[i].gameObject.SetActive(true);
        }
    }
    */
    // Use this for initialization
    void Start()
    {
        m_UIWaitTime = uiRoot.transform.Find("popup_wait/ment_wait/countDown");
        
        m_waitPopup.gameObject.SetActive(false);

    }
}
