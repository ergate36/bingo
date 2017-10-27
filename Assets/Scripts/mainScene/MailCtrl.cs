using UnityEngine;
using System.Collections;
public class MailCtrl : MonoBehaviour
{

    Transform item;
    Transform ment;
    Transform time;
    Transform amount;
    MailInfo mailinfo2;
    MailBoxCtrl mailboxctrl;
    private int itemindex;
    private int ticketcount;
    private int jamcount;
    private int goldcount;
    private int itemcount;
    private GameObject root;
    // Use this for initialization
    void Awake()
    {
        item = transform.Find("info_front/item");
        ment = transform.Find("info_front/ment");
        time = transform.Find("info_front/time");
        amount = transform.Find("info_front/amount");
    }
    void Start()
    {
        root = GameObject.Find("mainSceneUI/Camera/Anchor/popup_mail/mailList");
        mailboxctrl = root.GetComponent<MailBoxCtrl>();
    }
    void OnClick()
    {
        getMail();
    }

    void getMail()
    {
        GlobalData.g_global.getMailindex = itemindex;
        GlobalData.g_global.getMailType = 0;
        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailCheckIng;
        Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mMailCheckRequest);
        Destroy(this.gameObject);
        GlobalData.g_global.m_mailInfo.Remove(mailinfo2);
        mailboxctrl.reFresh();
    }

    public void setMailCtrl(MailInfo mailinfo)
    {
        itemindex = mailinfo.mailNo;
        mailinfo2 = mailinfo;
        if (mailinfo.giftIndex == 0)
        {
            item.GetComponent<UISprite>().spriteName = "jam_item";
        }
        else if (mailinfo.giftIndex == 1)
        {
            item.GetComponent<UISprite>().spriteName = "item_coin";
        }
        else if (mailinfo.giftIndex == 2)
        {
            item.GetComponent<UISprite>().spriteName = "item_ticket";
        }
        else if (mailinfo.giftIndex == 3)
        {
            item.GetComponent<UISprite>().spriteName = "item1";
        }
        else if (mailinfo.giftIndex == 4)
        {
            item.GetComponent<UISprite>().spriteName = "item_goldticket";
        }
        item.GetComponent<UISprite>().MakePixelPerfect();

        Vector3 temp;
        temp = item.transform.localScale;
        temp.x = 0.6f;
        temp.y = 0.6f;
        temp.z = 0.6f;

        if (mailinfo.giftIndex==0)
        {
            temp.x = 0.5f;
            temp.y = 0.5f;
            temp.z = 0.5f;
            Vector3 temp2;
            temp2 = item.transform.position;
            temp2.y += 5;
            item.transform.position = temp2;
        }

        item.transform.localScale = temp;

        ment.GetComponent<UILabel>().text = mailinfo.content;
        amount.GetComponent<UILabel>().text = mailinfo.giftCount.ToString();

        time.GetComponent<UILabel>().text = mailinfo.eDate;
    }
}
