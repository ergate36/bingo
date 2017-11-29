using UnityEngine;
using System.Collections;
public class nb_BattleLobbyShop : MonoBehaviour
{
    public GameObject lobbyUI;

    private nb_BattleLobbyScene lobby;

    void Awake()
    {
        lobby = lobbyUI.GetComponent<nb_BattleLobbyScene>();
    }
    // Use this for initialization

    private void OnGUI()
    {
    }

    void Start()
    {
        drawStorePowerUpItem();
        drawStorePowerUpItem2();
        lobby.drawPowerUpMoney();
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
            drawStorePowerUpItem2();
            lobby.drawPowerUpMoney();
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

    private void drawStorePowerUpItem2()
    {
        Transform tap5 = gameObject.transform.Find("tap5");
        UILabel count1 = tap5.transform.Find("item_count_1").GetComponent<UILabel>();   //1.single dust
        UILabel count2 = tap5.transform.Find("item_count_2").GetComponent<UILabel>();   //2.fog
        UILabel count3 = tap5.transform.Find("item_count_3").GetComponent<UILabel>();   //3.blind
        UILabel count4 = tap5.transform.Find("item_count_4").GetComponent<UILabel>();   //4.double dust
        UILabel count5 = tap5.transform.Find("item_count_5").GetComponent<UILabel>();   //5.shield
        UILabel count6 = tap5.transform.Find("item_count_6").GetComponent<UILabel>();   //6.mix
        UILabel count7 = tap5.transform.Find("item_count_7").GetComponent<UILabel>();   //7.jamming
        UILabel count8 = tap5.transform.Find("item_count_8").GetComponent<UILabel>();   //8.freezing
        UILabel count9 = tap5.transform.Find("item_count_9").GetComponent<UILabel>();   //9.avoid
        UILabel count10 = tap5.transform.Find("item_count_10").GetComponent<UILabel>(); //10.triple dust

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

        for (int i = 0; i < size; ++i)
        {
            long count = nb_GlobalData.g_global.userPowerUpList[i].Count;
            switch (nb_GlobalData.g_global.userPowerUpList[i].PowerUpId)
            {
                case 18:
                    count1.text = count.ToString();
                    break;
                case 19:
                    count2.text = count.ToString();
                    break;
                case 21:
                    count3.text = count.ToString();
                    break;
                case 22:
                    count4.text = count.ToString();
                    break;
                case 23:
                    count5.text = count.ToString();
                    break;
                case 24:
                    count6.text = count.ToString();
                    break;
                case 25:
                    count7.text = count.ToString();
                    break;
                case 27:
                    count8.text = count.ToString();
                    break;
                case 28:
                    count9.text = count.ToString();
                    break;
                case 29:
                    count10.text = count.ToString();
                    break;
            }
        }
    }
}
