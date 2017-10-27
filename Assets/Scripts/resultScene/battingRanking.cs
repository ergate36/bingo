using UnityEngine;
using System.Collections;

public class battingRanking : MonoBehaviour {
    public Transform root;
    private Transform label;
    private Transform coin;
    public Transform lose;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator initRanking()
    {
        root.gameObject.SetActive(true);
        root.Find("bg/gotoResult_btn").GetComponent<BoxCollider>().enabled = false;
        
        if (GlobalData.g_global.m_winnerList[0].nickname != GlobalData.g_global.myInfo.nickName)
        {
            GlobalData.g_global.win = 1;
            lose.gameObject.SetActive(true);
        }

        Transform soundObj = transform.Find("sound2");
        soundObj.GetComponent<AudioSource>().PlayOneShot(soundObj.GetComponent<AudioSource>().clip);

        for (int i = 0; i < GlobalData.g_global.tourPlayer_count; i++ )
        {
            if(GlobalData.g_global.otherSheetInfo[i].nickname ==GlobalData.g_global.m_winnerList[0].nickname){
                
                if (GlobalData.g_global.otherSheetInfo[i].betting_index == 0)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 500*GlobalData.g_global.tourPlayer_count;
                }
                if (GlobalData.g_global.otherSheetInfo[i].betting_index == 1)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 1000 * GlobalData.g_global.tourPlayer_count;
                }
                if (GlobalData.g_global.otherSheetInfo[i].betting_index == 2)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 1500 * GlobalData.g_global.tourPlayer_count;
                }
                break;
            }
            else if (GlobalData.g_global.m_winnerList[0].nickname == GlobalData.g_global.myInfo.nickName)
            {
                if (GlobalData.g_global.betting_index == 0)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 500 * GlobalData.g_global.tourPlayer_count;
                }
                if (GlobalData.g_global.betting_index == 1)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 1000 * GlobalData.g_global.tourPlayer_count;
                }
                if (GlobalData.g_global.betting_index == 2)
                {
                    GlobalData.g_global.m_winnerList[0].coin = 1500 * GlobalData.g_global.tourPlayer_count;
                }
                break;
            }
        }
        coin = root.Find("bg/Label");
        label = root.Find("bg/card_name");
        coin.GetComponent<UILabel>().text = "+"+GlobalData.g_global.m_winnerList[0].coin;
        label.GetComponent<UILabel>().text = GlobalData.g_global.m_winnerList[0].nickname;

        yield return new WaitForSeconds(1f);
        root.Find("bg/gotoResult_btn").GetComponent<BoxCollider>().enabled = true;
    }
}
