using UnityEngine;
using System.Collections;
public class startClick_btn : MonoBehaviour {

    private Transform progress;
    private GameObject pro;
    private int flag;
    private Transform click;
    private MyCard myCard;
	// Use this for initialization
	void Start () {
        GameObject uiRoot = GameObject.Find("loginSceneUI/Camera/Anchor/Panel") as GameObject;
        click = uiRoot.transform.Find("click");
        flag = 0;
	}
	
	// Update is called once per frame
	/*
    void Update () {
        if(flag == 1){
            pro.GetComponent<UISlider>().value = GlobalData.g_global.progress;
            if (pro.GetComponent<UISlider>().value == 1)
            {
                flag = 0;
                getMyCard();
            }
        }
	}
    */

    void OnClick()
    {
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mNoticeIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mNoticeRequest);
        Vector3 v;
        v = transform.position;
        v.x = -2000f;
        v.y = 1;
        v.z = 0;
        click.transform.localPosition = v;

    }

    private IEnumerator getBand()
    {
        //AndroidManager.Instance.callBandList();
        yield return new WaitForSeconds(0.5f);
       // Application.LoadLevelAsync("MainScene");
    }
}
