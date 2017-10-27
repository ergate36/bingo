using UnityEngine;
using System.Collections;
public class MailBoxCtrl : MonoBehaviour {

    MailInfo[] MailBoxInfo;
    public Transform gridRoot;
    public UIScrollView sv;
    public UIGrid grid;
    public UIPanel panel;
    private GameObject root2;
    GameObject root;
    GameObject ds;
	// Use this for initialization
    void Awake()
    {

    }
	void Start () {
       
      //  mailling();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void mailling()
    {
        GameObject tex = GameObject.Find("mainSceneUI/Camera/Anchor/popup_mail/Label");
        tex.transform.GetComponent<UILabel>().text = GlobalData.g_global.m_mailInfo.Count+"/30";
        for (int i = 0; i < GlobalData.g_global.m_mailInfo.Count; i++  )
        {
            setMail(GlobalData.g_global.m_mailInfo[i]);
        }

        grid.Reposition();
        sv.ResetPosition();
        panel.Refresh();
    }
    
    public void reFresh(){
        GameObject tex = GameObject.Find("mainSceneUI/Camera/Anchor/popup_mail/Label");
        tex.transform.GetComponent<UILabel>().text = GlobalData.g_global.m_mailInfo.Count + "/30";
        grid.repositionNow = true;
    }
    public void mailClear()
    {
        root2 = GameObject.Find("mainSceneUI/Camera/Anchor/popup_mail/mailList/Grid");
        foreach (Transform mail in root2.transform)
        {
            Destroy(mail.gameObject);
        }
    }

    GameObject setMail(MailInfo mailinfo)
    {
        GameObject mailObject = Instantiate(Resources.Load("common/mail")) as GameObject;
         
        mailObject.GetComponent<MailCtrl>().setMailCtrl(mailinfo);

        Vector3 scale = mailObject.transform.localScale;
        mailObject.transform.parent = gridRoot.transform;
        mailObject.transform.localPosition = Vector3.zero;
        mailObject.transform.localScale = scale;
        mailObject.GetComponent<UIDragScrollView>().scrollView = sv;

        return mailObject;    
    }
}
