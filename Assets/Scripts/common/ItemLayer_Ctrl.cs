using UnityEngine;
using System.Collections;

public class ItemLayer_Ctrl : MonoBehaviour
{
    private Transform m_goldLabel;
    private Transform m_gemLabel;
    private Transform m_itemLabel;
    private Transform m_bingoTicketLabel;
    private Transform m_goldTicketLabel;
    public static ItemLayer_Ctrl ictrl= null;
    void Awake()
    { 
        m_goldLabel = transform.Find("gold_label");
        m_gemLabel = transform.Find("gem_label");
        m_itemLabel = transform.Find("item_label");
        m_bingoTicketLabel = transform.Find("bingoticket_label");
     //   m_goldTicketLabel = transform.FindChild("goldticket_label");    
    }

	// Use this for initialization
	void Start () {
        if (ictrl == null)
        {
            ictrl = gameObject.GetComponent<ItemLayer_Ctrl>();
        }
        SetItem();
	}
    public void SetItem()
    {
        m_goldLabel.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.coinAmount.ToString();
        m_gemLabel.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.gemCount.ToString();
        m_itemLabel.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.ItemCount.ToString();
        m_bingoTicketLabel.GetComponent<UILabel>().text = GlobalData.g_global.myInfo.ticketCount + " /20";
    }
}
