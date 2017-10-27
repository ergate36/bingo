using UnityEngine;
using System.Collections;

public class startGacha : MonoBehaviour {
    public Transform shoppop;
    public Transform gachapop;
    public Animation anim;
    public Transform closepop;
    public GameObject effect;
    public GameObject effect2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GlobalData.g_global.socketState == (int)SocketClass.STATE.mPlayGachaIngComplete)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;

            if (GlobalData.g_global.resultData == 0)
            {
                GlobalData.g_global.myInfo.coinAmount -= 200;
                ItemLayer_Ctrl.ictrl.SetItem();
                StartCoroutine(playGacha());
            }
            else
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.mPlayGachaRequest;
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mPlayGachaRequest);
            }

        }
	}
    void OnClick()
    {
        iTween.ScaleTo(closepop.gameObject, iTween.Hash("x", 0f, "y", 0f, "z", 0f, "easeType", "Linear", "time", 0.2f));

        if (GlobalData.g_global.myInfo.coinAmount < 200)
        {
            shoppop.gameObject.SetActive(true);
            shoppop.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(shoppop.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(shoppop.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", shoppop.gameObject));
        }
        else
        {
            gachapop.gameObject.SetActive(true);
            gachapop.transform.localScale = Vector3.one * 0.7f;
            iTween.ScaleTo(gachapop.gameObject, iTween.Hash("x", 1f, "y", 1f, "z", 1f, "easeType", "easeOutBack", "time", 0.3f));
            iTween.ValueTo(gachapop.gameObject, iTween.Hash("from", 0.4f, "to", 1f, "time", 0.3f, "onupdate", "setalpha", "onupdatetarget", gachapop.gameObject));

            GlobalData.g_global.socketState = (int)SocketClass.STATE.mPlayGachaRequest;
            Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mPlayGachaRequest);

        }
        // popup open
    }
    IEnumerator playGacha()
    {
        GetComponent<AudioSource>().clip = GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.yabawe_loop];
        GetComponent<AudioSource>().Play();
        effect.gameObject.SetActive(true);

        anim.Play("eff_treasurebox_loop_animation");
        yield return new WaitForSeconds(2f);

        gachapop.Find("reward_focus").gameObject.SetActive(true);
        
        gachapop.Find("reward_focus/item/Label").GetComponent<UILabel>().text = "+"+GlobalData.g_global.gachaInfo[0].value.ToString();

        GlobalData.g_global.myInfo.coinAmount += GlobalData.g_global.gachaInfo[0].value;
        ItemLayer_Ctrl.ictrl.SetItem();
        anim.Play("eff_treasurebox_disappear_animation");
        effect.gameObject.SetActive(false);
        effect2.gameObject.SetActive(true);

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = GlobalData.g_global.EffectSound[(int)Sound.EffSoundList.yabawe_end];
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.5f);
        effect2.gameObject.SetActive(false);

        gachapop.Find("reward_focus/btn_confirm").gameObject.SetActive(true);


    }
}
