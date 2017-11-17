using UnityEngine;
using System.Collections;
public class nb_MainShop : MonoBehaviour
{
    public GameObject mainUI;

    private nb_MainScene main;

    void Awake()
    {
        main = mainUI.GetComponent<nb_MainScene>();
    }
    // Use this for initialization

    private void OnGUI()
    {
    }

    void Start()
    {
        drawStorePowerUpItem();
        //main.drawPowerUpMoney();
    }



    void Update()
    {
        //http
        if (nbHttp.state == nbHttp.nbHttpState.BuyStoreProductSuccess)
        {
            //상점 구매 완료
            nbHttp.state = nbHttp.nbHttpState.Wait;

            //아이템 갱신
            nbHttp.http.GetUserPowerUpList(
                nb_GlobalData.g_global.userSession.SessionKey);
        }
        else if (nbHttp.state == nbHttp.nbHttpState.GetUserPowerUpListSuccess)
        {
            //아이템 갱신 함
            nbHttp.state = nbHttp.nbHttpState.Wait;

            drawStorePowerUpItem();
            main.drawPowerUpMoney();
        }
    }
    
    private void drawStorePowerUpItem()
    {
        Transform tap4 = gameObject.transform.Find("tap4");
        UILabel count1 = tap4.transform.Find("item_count_1").GetComponent<UILabel>(); //1.single daub
        UILabel count2 = tap4.transform.Find("item_count_2").GetComponent<UILabel>(); //2.coin reward
        UILabel count3 = tap4.transform.Find("item_count_3").GetComponent<UILabel>(); //3.chest
        UILabel count4 = tap4.transform.Find("item_count_4").GetComponent<UILabel>(); //4.double daubs
        UILabel count5 = tap4.transform.Find("item_count_5").GetComponent<UILabel>(); //5.double exp
        UILabel count6 = tap4.transform.Find("item_count_6").GetComponent<UILabel>(); //6.double reward
        UILabel count7 = tap4.transform.Find("item_count_7").GetComponent<UILabel>(); //7.bomb
        UILabel count8 = tap4.transform.Find("item_count_8").GetComponent<UILabel>(); //8.instant win
        UILabel count9 = tap4.transform.Find("item_count_9").GetComponent<UILabel>(); //9.booster
        UILabel count10 = tap4.transform.Find("item_count_10").GetComponent<UILabel>(); //10.triple daubs

        count1.text = "0";
        count2.text = "0";
        count3.text = "0";
        count4.text = "0";
        count5.text = "0";
        count6.text = "0";
        count7.text = "0";
        count8.text = "0";
        count9.text = "0";
        count10.text = "0";

        int size = nb_GlobalData.g_global.userPowerUpList.Count;

        for(int i = 0; i < size; ++i)
        {
            long count = nb_GlobalData.g_global.userPowerUpList[i].Count;
            switch (nb_GlobalData.g_global.userPowerUpList[i].PowerUpId)
            {
                case 5:
                    count1.text = count.ToString();
                    break;
                case 6:
                    count2.text = count.ToString();
                    break;
                case 3:
                    count3.text = count.ToString();
                    break;
                case 8:
                    count4.text = count.ToString();
                    break;
                case 9:
                    count5.text = count.ToString();
                    break;
                case 11:
                    count6.text = count.ToString();
                    break;
                case 1:
                    count7.text = count.ToString();
                    break;
                case 13:
                    count8.text = count.ToString();
                    break;
                case 15:
                    count9.text = count.ToString();
                    break;
                case 17:
                    count10.text = count.ToString();
                    break;
            }
        }
    }
}
