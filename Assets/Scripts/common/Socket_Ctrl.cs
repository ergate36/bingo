using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
public class Socket_Ctrl : MonoBehaviour
{
    public TcpClient client = null;
    public static Stream st = null;
    public static byte[] buffer = new byte[1024 * 10];
    public static int offset = 0;
    public static int Size = 1024 * 10;
    public static int PSize = 0;
    public static ushort H = 0xffff;
    //211.172.241.136  외부 빌드 IP
    //1.223.21.91  테스트 IP


    public static string lobbyIP = "14.63.172.176";
    public static string itemIP = "14.63.172.176";
    public static string bandIP = "14.63.172.176";
    public static int lobbyPort = 50111;
    public static int itemPort = 50112;
    public static int bandPort = 50113;
    public static RankInfo rankList;
    public static MailInfo mailList;

    public static Socket_Ctrl sCtrl = null;
    public DateTime HeartBeatTime = DateTime.Now;
    public DateTime readTime = DateTime.Now;

    private int index;
    // Use this for initialization
    void Start()
    {
        client = new TcpClient();
        if (sCtrl == null)
        {
            sCtrl = GameObject.Find("GlobalObject").GetComponent<Socket_Ctrl>();
        }
    }
    
    void Update()
    {
        if (client.Connected == true)
        {
            if ((DateTime.Now.Ticks - HeartBeatTime.Ticks) > (10000000 * 10))
            {
                Socket_Ctrl.sCtrl.FrontBeginWrite((int)SocketClass.MsgType.mHeartBeat);
            }

            if (ReadCall == true)
            {
                if ((DateTime.Now.Ticks - readTime.Ticks) > (11000000 * 10))
                {
                    ReadCall = false;
                    GlobalData.g_global.socketState = (int)SocketClass.STATE.excessTime;
                    //에러 팝업창 발생 로그인 화면으로 이동 처음부터 시작
                }
            }
        }
    }

    public bool StartClient()
    {
        if (client.Connected == true)
        {
            try
            {
                closeSocket();
            }
            catch
            {
            }
        }

        if (client.Connected == false)
        {
            string ipname = "";
            int portname = 0;

            if (GlobalData.g_global.serverFlag == 1)
            {   // lobby
                ipname = GlobalData.g_global.lobbyip;
                portname = GlobalData.g_global.lobbyport;
            }
            else if (GlobalData.g_global.serverFlag == 2)
            {   // 5
                ipname = GlobalData.g_global.itemip[GlobalData.g_global.modeIndex];
                portname = GlobalData.g_global.itemport[GlobalData.g_global.modeIndex];
            }

            else if (GlobalData.g_global.serverFlag == 3)
            {   //100
                ipname = bandIP;
                portname = bandPort;
            }

            try
            {
                IPAddress ip = IPAddress.Parse(ipname);
                IPEndPoint remoteEP = new IPEndPoint(ip, portname);

                client = new TcpClient();

                client.Connect(new IPEndPoint(ip, portname));

                st = client.GetStream();

                FrontBeginRead();

                HeartBeatTime = DateTime.Now;
                return true;
            }
            catch (Exception e)
            {
                GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
                return false;	//접속실패시 예외가 발생하므로 그냥 무시하고 false를 리턴한다.
            }
        }
        return true;
    }

    /*
    public bool NewStartClient()
    {

        if (client.Connected == false)
        {
            string ipname = "";
            int portname = 0;

            if (GlobalData.g_global.serverFlag == 1)
            {   // lobby
                ipname = GlobalData.g_global.lobbyip;
                portname = GlobalData.g_global.lobbyport;
            }
            else if (GlobalData.g_global.serverFlag == 2)
            {   // 5
                Debug.Log("IP" +GlobalData.g_global.itemip[GlobalData.g_global.modeIndex]);
                Debug.Log("PORT" + GlobalData.g_global.itemport[GlobalData.g_global.modeIndex]);

                ipname = GlobalData.g_global.itemip[GlobalData.g_global.modeIndex];
                portname = GlobalData.g_global.itemport[GlobalData.g_global.modeIndex];
            }
            else if (GlobalData.g_global.serverFlag == 3)
            {   //100
                ipname = bandIP;
                portname = bandPort;
            }

            try
            {
                IPAddress ip = IPAddress.Parse(ipname);
                IPEndPoint remoteEP = new IPEndPoint(ip, portname);

                client = new TcpClient();

                client.BeginConnect(ip, portname, new AsyncCallback(EndConnectCallBack),client);

                return true;
            }
            catch (Exception e)
            {
                Debug.Log("CONNECTION FAIL");
                GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
                return false;	//접속실패시 예외가 발생하므로 그냥 무시하고 false를 리턴한다.
            }
        }
        return true;
    }
    */
    public void EndConnectCallBack(IAsyncResult ar)
    {
        try
        {
            Debug.Log("end callback");

            TcpClient t = (TcpClient)ar.AsyncState;
            t.EndConnect(ar);
            st = t.GetStream();

            FrontBeginRead();
            HeartBeatTime = DateTime.Now;
            GlobalData.g_global.socketState = (int)SocketClass.STATE.connectComplete;
        }
        catch (Exception e)
        {
            Debug.Log("CONNECTION FAIL");
            GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
        }
    }

    public void FrontBeginRead()
    {
        try
        {
            st.BeginRead(buffer, offset, Size - offset, new AsyncCallback(FrontReadCallBack), st);
        }
        catch (Exception e)
        {
            Debug.Log("CONNECTION FAIL READ @@@");
            GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
            Debug.Log("connetion fail");
        }
    }

    public bool ReadCall = false;

    public void FrontReadCallBack(IAsyncResult ar)
    {
        try
        {
            ReadCall = false;

            int EndSize = st.EndRead(ar);

            if (EndSize <= 0)
            {
                Debug.Log("읽을 메시지가 없음");
                return;
            }

            if (PSize == 0)
            {
                PSize = GetPsize(buffer);
            }

            if (offset + EndSize < PSize)
            {
                Debug.Log("아직 덜받아서 재요청");
                offset = offset + EndSize;
                FrontBeginRead();
                return;
            }

            using (BinaryReader reader = new BinaryReader(new MemoryStream(buffer, false)))
            {
                short header = reader.ReadInt16();
                short size = reader.ReadInt16();
                short type = reader.ReadInt16();

                byte result = 0;

                int id;
                int nGems;
                int nCoin;
                int nTickets;
                int nGoldTickets;
                int itemCount;
                int mailCount;
                int myBlockinfo;
                int waitTime;
                int ticketCount;
                int last_rank;
                int last_bestscore;
                int reward_gem;
                int reward_coin;
                int weeklyBestScore;
                int iscore;
                int bscore;
                int ranking;
                int bcount;
                string tempnick;
                int[] cType = new int[3];
                int[] value = new int[3];
                int[] selected = new int[3];
                int card_Key;
                int mon_id;
                int level;
                int exp;
                int used;
                int userid;
                int Send_userID;
                int Datasize;

                if (type >= 1000)
                {
                    Debug.Log(">> responese : " + type);
                }
                switch (type)
                {
                    case (short)SocketClass.MsgType.mServerInfoResponse:

                        GlobalData.g_global.lobbyip = Encoding.UTF8.GetString(reader.ReadBytes(100)).Split(char.MinValue)[0];
                        GlobalData.g_global.lobbyport = reader.ReadInt32();

                        for (int i = 0; i < 4; i++)
                        {
                            GlobalData.g_global.itemip[i] = Encoding.UTF8.GetString(reader.ReadBytes(100)).Split(char.MinValue)[0];
                            GlobalData.g_global.itemport[i] = reader.ReadInt32();
                        }

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerInfoComplete;
                        break;
                    case (short)SocketClass.MsgType.mHeartBeat:
                        break;
                    case (short)SocketClass.MsgType.mServerTestResponse:
                        result = reader.ReadByte();
                        if (result == 1)
                        {
                            GlobalData.g_global.servertesting = true;
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mServerTestIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mNoticeResponse:

                        bcount = reader.ReadByte();
                        int num;
                        urlInfo t_urlinfo = new urlInfo();

                        GlobalData.g_global.mUrlInfo.Clear();

                        for (int i = 0; i < bcount; i++)
                        {
                            num = reader.ReadInt32();
                            t_urlinfo.notice_Num = num;
                            t_urlinfo.img_url = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];
                            t_urlinfo.move_url = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];

                            GlobalData.g_global.mUrlInfo.Add(t_urlinfo);
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mNoticeIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mLoginResponse:
                        result = reader.ReadByte();
                        GlobalData.g_global.loginState = result;

                        string url = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];
                        string message = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];
                        id = reader.ReadInt32();
                        string tempnic = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];

                        GlobalData.g_global.updateUrl = url;
                        GlobalData.g_global.testMessage = message;
                        GlobalData.g_global.myInfo.userID = id;
                        GlobalData.g_global.myInfo.nickName = tempnic;

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mLoginIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMyGameInfoResponse:
                        nGems = reader.ReadInt32();
                        nCoin = reader.ReadInt32();
                        nTickets = reader.ReadInt32();
                        nGoldTickets = reader.ReadInt32();
                        itemCount = reader.ReadInt32();
                        mailCount = reader.ReadInt32();
                        myBlockinfo = reader.ReadInt32();
                        int win = reader.ReadInt32();
                        int lose = reader.ReadInt32();

                        GlobalData.g_global.myInfo.week_bestScore= reader.ReadInt32();
                        Debug.Log("BEST SCORE : " + GlobalData.g_global.myInfo.week_bestScore);
                        
                        GlobalData.g_global.myInfo.gemCount = nGems;
                        GlobalData.g_global.myInfo.coinAmount = nCoin;
                        GlobalData.g_global.myInfo.ticketCount = nTickets;
                        GlobalData.g_global.myInfo.goldTicket = nGoldTickets;
                        GlobalData.g_global.myInfo.ItemCount = itemCount;
                        GlobalData.g_global.myInfo.win = win;
                        GlobalData.g_global.myInfo.lose = lose;


                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMyGameInfoIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mBandkeySetResponse:
                        result = reader.ReadByte();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mBandkeySetIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mBandListResponse:
                        result = reader.ReadByte();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mBandListIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mTicketChargeResponse:
                        waitTime = reader.ReadInt32();
                        ticketCount = reader.ReadInt32();

                        GlobalData.g_global.myInfo.waitTime = waitTime;

                        GlobalData.g_global.myInfo.ticketCount += ticketCount;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketChargeIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mTicketSendResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketSendIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mTicketSettingResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mTicketSettingIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mItemShopResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemShopIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mBandRankingResponse:
                        result = reader.ReadByte();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mBandRankingIngComplete;
                        break;


                    case (short)SocketClass.MsgType.mWorldRankingResponse:

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mWorldRankingIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mCoinRankingResponse:

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mCoinRankingIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMailGetResponse:
                        int mCount = reader.ReadByte();
                        GlobalData.g_global.m_mailInfo.Clear();
                        for (int i = 0; i < mCount; i++)
                        {
                            mailList = new MailInfo();
                            mailList.mailNo = reader.ReadInt32();
                            mailList.sender = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                            mailList.giftIndex = reader.ReadInt32();
                            mailList.giftCount = reader.ReadInt32();
                            mailList.eDate = Encoding.UTF8.GetString(reader.ReadBytes(30)).Split(char.MinValue)[0];
                            mailList.content = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];
                            GlobalData.g_global.m_mailInfo.Add(mailList);
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailGetIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mMailCheckResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMailCheckIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mDropResponse:


                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mDropIngComplete;


                        break;
                    case (short)SocketClass.MsgType.mMonsterGachaResponse:
                        result = reader.ReadByte();

                        if (result == 1)
                        {

                        }

                        mon_id = reader.ReadInt32();
                        GlobalData.g_global.monsterGachaId = mon_id;
                        card_Key = reader.ReadInt32();
                        GlobalData.g_global.monsterGachaKey = card_Key;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterGachaIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mPlayGachaResponse:
                        result = reader.ReadByte();
                        for (int i = 0; i < 3; i++)
                        {
                            GlobalData.g_global.gachaInfo[i].type = reader.ReadInt32();
                            GlobalData.g_global.gachaInfo[i].value = reader.ReadInt32();
                            GlobalData.g_global.gachaInfo[i].selected = reader.ReadInt32();
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mPlayGachaIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMonsterCardListResponse:
                        GlobalData.g_global.myCardList.Clear();

                        result = reader.ReadByte();
                        int monsterCount = reader.ReadInt32();
                        MyCard myCard = new MyCard();
                        for (int i = 0; i < monsterCount; i++)
                        {
                            card_Key = reader.ReadInt32();
                            mon_id = reader.ReadInt32();
                            level = reader.ReadInt32();
                            exp = reader.ReadInt32();
                            used = reader.ReadInt32();

                            myCard.cardused = used;
                            myCard.cardId = mon_id;
                            myCard.cardlevel = level;
                            myCard.cardno = card_Key;
                            myCard.cardexp = exp;
                            myCard.cardRank = XmlCtrl.x_xml.MCxml[mon_id - 1].Rank;
                            myCard.cardRare = XmlCtrl.x_xml.MCxml[mon_id - 1].Rare;
                            myCard.AT1 = XmlCtrl.x_xml.MCxml[mon_id - 1].Ability1;
                            myCard.AT2 = XmlCtrl.x_xml.MCxml[mon_id - 1].Ability2;
                            myCard.AT3 = XmlCtrl.x_xml.MCxml[mon_id - 1].Ability3;
                            myCard.ATv1 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability1value;
                            myCard.ATv2 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability2value;
                            myCard.ATv3 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability3value;

                            // myCard.ATv1 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability1value + ((level - 1) * XmlCtrl.x_xml.MCxml[mon_id - 1].AT2Add);
                            // myCard.ATv2 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability2value + ((level - 1) * XmlCtrl.x_xml.MCxml[mon_id - 1].AT2Add);
                            // myCard.ATv3 = XmlCtrl.x_xml.MCxml[mon_id - 1].Lv1ability3value + ((level - 1) * XmlCtrl.x_xml.MCxml[mon_id - 1].AT3Add);

                            GlobalData.g_global.myCardList.Add(myCard);
                        }

                        GlobalData.g_global.cardCount = GlobalData.g_global.myCardList.Count;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardListIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMonsterCardUseResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardUseIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMonsterCardMixResponse:

                        result = reader.ReadByte();
                        mon_id = reader.ReadInt32();
                        level = reader.ReadInt32();
                        exp = reader.ReadInt32();

                        GlobalData.g_global.mixResultId = mon_id;

                        GlobalData.g_global.m_setInfo.card_exp = 0;
                        GlobalData.g_global.m_setInfo.card_id = 0;
                        GlobalData.g_global.m_setInfo.card_level = 0;
                        GlobalData.g_global.m_setInfo.card_num = 0;
                        GlobalData.g_global.m_setInfo.card_rank = 0;
                        GlobalData.g_global.m_setInfo.sell_money = 0;


                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMonsterCardMixIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mEventListResponse:
                        bcount = reader.ReadInt32();
                        for (int i = 0; i < bcount; i++)
                        {
                            GlobalData.g_global.m_eventData[i].productID = reader.ReadInt32();
                            GlobalData.g_global.m_eventData[i].sell_Count = reader.ReadInt32();
                            GlobalData.g_global.m_eventData[i].total_Count = reader.ReadInt32();
                            GlobalData.g_global.m_eventData[i].img_url = Encoding.UTF8.GetString(reader.ReadBytes(200)).Split(char.MinValue)[0];
                            GlobalData.g_global.m_eventData[i].name = Encoding.UTF8.GetString(reader.ReadBytes(100)).Split(char.MinValue)[0];

                            byte[] bingo = new byte[25];
                            bingo = reader.ReadBytes(25);
                            for (int k = 0; k < 25; k++)
                            {
                                GlobalData.g_global.m_eventData[i].bingo[k] = bingo[k];
                            }

                            GlobalData.g_global.m_eventData[i].bingoInfo = reader.ReadByte();
                        }



                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventListIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mEventGachaResponse:
                        result = reader.ReadByte();
                        if (result == 0)
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventGachaIngComplete;
                        }
                        else if (result == 1)
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventGachaEnd;
                        }
                        else if (result == 2)
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventGachaRequest;
                        }
                        else if (result == 3)
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventGachaFail;
                        }
                        break;
                    case (short)SocketClass.MsgType.mEventMyInfoResponse:

                        //GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventMyInfoIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mGameServerJoinResponse:
                        result = reader.ReadByte();
                        GlobalData.g_global.sheetInfo.userID = reader.ReadInt32();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameServerJoinIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMatchResponse:

                        waitTime = reader.ReadInt32();
                        bcount = reader.ReadInt32();
                        GlobalData.g_global.tourPlayer_count = bcount;
                        GlobalData.g_global.lobby_waitTime = waitTime;

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mMatchSuccessNoticeResponse:

                        bcount = reader.ReadByte();
                        GlobalData.g_global.tourPlayer_count = bcount;
                        int tempnum = 0;

                        int cnt = 0;
                        for (int i = 0; i < bcount; i++)
                        {
                            tempnum = reader.ReadInt32();
                            tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                            byte card_id = reader.ReadByte();
                            int betting_index = reader.ReadInt32();
                            byte sheet_count = reader.ReadByte();

                            if ((int)GlobalData.g_global.sheetInfo.userID != tempnum)
                            {
                                GlobalData.g_global.otherSheetInfo[cnt].userID = tempnum;
                                GlobalData.g_global.otherSheetInfo[cnt].nickname = tempnick;
                                GlobalData.g_global.otherSheetInfo[cnt].monsterId = card_id;
                                GlobalData.g_global.otherSheetInfo[cnt].betting_index = betting_index;
                                GlobalData.g_global.otherSheetInfo[cnt].activeSheetCount = sheet_count;
                                GlobalData.g_global.otherBingoS[cnt].userId = tempnum;

                                GlobalData.g_global.otherBingoS[cnt].bingo[0] = false;
                                GlobalData.g_global.otherBingoS[cnt].bingo[1] = false;
                                GlobalData.g_global.otherBingoS[cnt].bingo[2] = false;
                                GlobalData.g_global.otherBingoS[cnt].bingo[3] = false;
                                cnt++;
                            }

                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchSuccessNoticeComplete;
                        break;
                    case (short)SocketClass.MsgType.mMatchCancelNoticeResponse:
                        userid = reader.ReadInt32();
                        GlobalData.g_global.tourPlayer_count--;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchCancelNoticeResponse;
                        break;
                    case (short)SocketClass.MsgType.mGameStartResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameStartIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mGameStartNoticeResponse:

                        for (int i = 0; i < 50; i++)
                        {
                            GlobalData.g_global.bingoball[i] = reader.ReadByte();
                        }

                        bcount = reader.ReadByte();

                        for (int i = 0; i < bcount; i++)
                        {
                            int tempnum5 = reader.ReadInt32();
                            tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                            Datasize = reader.ReadInt16();

                            byte[] otherSheet1 = new byte[25];
                            otherSheet1 = reader.ReadBytes(25);

                            byte[] otherSheet2 = new byte[25];
                            otherSheet2 = reader.ReadBytes(25);

                            byte[] otherSheet3 = new byte[25];
                            otherSheet3 = reader.ReadBytes(25);

                            byte[] otherSheet4 = new byte[25];
                            otherSheet4 = reader.ReadBytes(25);

                            for (int cnt1 = 0; cnt1 < 4; cnt1++)
                            {
                                if (GlobalData.g_global.otherSheetInfo[cnt1].userID == tempnum5)
                                {
                                    for (int k = 0; k < 25; k++)
                                    {
                                        GlobalData.g_global.otherSheetInfo[cnt1].sheet[0, k] = (int)otherSheet1[k];
                                        GlobalData.g_global.otherSheetInfo[cnt1].sheet[1, k] = (int)otherSheet2[k];
                                        GlobalData.g_global.otherSheetInfo[cnt1].sheet[2, k] = (int)otherSheet3[k];
                                        GlobalData.g_global.otherSheetInfo[cnt1].sheet[3, k] = (int)otherSheet4[k];
                                    }
                                    break;
                                }
                            }

                        }

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameStartNoticeIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mReadycompleteResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mReadycompleteIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mReadycompleteNoticeResponse:
                        GlobalData.g_global.bingoCount = reader.ReadInt32();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mReadycompleteNoticeIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mGameMessageResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameMessageIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mGameMessageNoticeResponse:

                        Send_userID = reader.ReadInt32();
                        Datasize = reader.ReadInt16();

                        byte[] tempchar = new byte[20];
                        tempchar = reader.ReadBytes(20);

                        byte[] otherSheet13 = new byte[25];
                        otherSheet13 = reader.ReadBytes(25);

                        byte[] otherSheet23 = new byte[25];
                        otherSheet23 = reader.ReadBytes(25);

                        byte[] otherSheet33 = new byte[25];
                        otherSheet33 = reader.ReadBytes(25);

                        byte[] otherSheet43 = new byte[25];
                        otherSheet43 = reader.ReadBytes(25);

                        byte[] bingoState = new byte[4];
                        bingoState = reader.ReadBytes(4);
                        byte shield = reader.ReadByte();

                        for (int i = 0; i < 4; i++)
                        {
                            if (GlobalData.g_global.otherSheetInfo[i].userID == Send_userID)
                            {
                                GlobalData.g_global.otherSheetInfo[i].bingoSheet[0] = Convert.ToBoolean(bingoState[0]);
                                GlobalData.g_global.otherSheetInfo[i].bingoSheet[1] = Convert.ToBoolean(bingoState[1]);
                                GlobalData.g_global.otherSheetInfo[i].bingoSheet[2] = Convert.ToBoolean(bingoState[2]);
                                GlobalData.g_global.otherSheetInfo[i].bingoSheet[3] = Convert.ToBoolean(bingoState[3]);
                                GlobalData.g_global.otherSheetInfo[i].shield = shield;

                                for (int k = 0; k < 25; k++)
                                {
                                    GlobalData.g_global.otherSheetInfo[i].sheet[0, k] = (int)otherSheet13[k];
                                    GlobalData.g_global.otherSheetInfo[i].sheet[1, k] = (int)otherSheet23[k];
                                    GlobalData.g_global.otherSheetInfo[i].sheet[2, k] = (int)otherSheet33[k];
                                    GlobalData.g_global.otherSheetInfo[i].sheet[3, k] = (int)otherSheet43[k];
                                }
                                break;
                            }
                        }

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameMessageNoticeIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mItemMessageResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemMessageIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mItemMessageNoticeResponse:

                        GlobalData.g_global.attack_id = reader.ReadInt32();
                        GlobalData.g_global.attack_index = reader.ReadInt32();
                        GlobalData.g_global.attack_sheetindex = reader.ReadInt32();
                        GlobalData.g_global.attacker_sheetindex = reader.ReadInt32();

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mItemMessageNoticeIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mBingoResultResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mBingoResultIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mGameResultNoticeResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mGameResultNoticeIngComplete;
                        break;

                    case (short)SocketClass.MsgType.mOtherOutResponse:
                        userid = reader.ReadInt32();
                        GlobalData.g_global.bingoCount = reader.ReadInt32();
                        GlobalData.g_global.outer_id = userid;
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mOtherOutIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mMatchFailResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mMatchFailIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mClose:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mCloseComplete;
                        break;
                    case (short)SocketClass.MsgType.mBingoRankResponse:
                        // 빙고 랭킹 
                        tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];

                        if (GlobalData.g_global.myInfo.nickName == tempnick)
                        {

                            if (GlobalData.g_global.myRankFlag == false)
                            {
                                GlobalData.g_global.myrank = GlobalData.g_global.rankCount;
                                GlobalData.g_global.myRankFlag = true;
                            }
                        }

                        GlobalData.g_global.m_winnerList[GlobalData.g_global.rankCount].nickname = tempnick;
                        GlobalData.g_global.m_winnerList[GlobalData.g_global.rankCount].rank = GlobalData.g_global.rankCount;
                        GlobalData.g_global.m_winnerList[GlobalData.g_global.rankCount].monster_id = reader.ReadInt32();


                        GlobalData.g_global.game_over = reader.ReadByte();

                        GlobalData.g_global.rankCount++;
                        GlobalData.g_global.bingoCount = Math.Max(0, GlobalData.g_global.bingoCount - 1);

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mBingoRankIngComplete;
                        break;
                    case (short)SocketClass.MsgType.mScoreUpdateResponse:
                        GlobalData.g_global.myInfo.coinAmount += GlobalData.g_global.myInfo.game_gold;
                        GlobalData.g_global.myInfo.ticketCount += GlobalData.g_global.myInfo.game_ticket;
                        break;
                    case (short)SocketClass.MsgType.mEventServerJoinResponse:
                        GlobalData.g_global.event_join = reader.ReadByte();
                        GlobalData.g_global.event_period = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mEventServerJoinComplete;
                        break;
                    case (short)SocketClass.MsgType.mDailyInfoResponse:
                        for (int i = 0; i < 7; i++)
                        {
                            GlobalData.g_global.dailyInfo[i] = reader.ReadByte();

                            if (GlobalData.g_global.dailyInfo[i] == 0 && GlobalData.g_global.dailyIndex == 0)
                            {
                                GlobalData.g_global.dailyIndex = i + 1;
                            }
                        }

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyInfoComplete;
                        break;
                    case (short)SocketClass.MsgType.mDailyCheckResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mDailyCheckComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendConnectInfoResponse:

                        GlobalData.g_global.friend_count = reader.ReadInt16();

                        for (int i = 0; i < GlobalData.g_global.friend_count; i++)
                        {
                            friendData fd = new friendData();

                            fd = GlobalData.g_global.myFriendList[i];

                            fd.FriendId = reader.ReadInt32();
                            fd.flag = reader.ReadByte();

                            GlobalData.g_global.myFriendList[i] = fd;
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendConnectInfoComplete;
                        break;



                    case (short)SocketClass.MsgType.mFriendInviteResponse:
                        GlobalData.g_global.Invite_result = reader.ReadByte();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendInviteAgreeResponse:

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteAgreeComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendInviteAgreeNoticeResponse:
                        GlobalData.g_global.invite_name = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                        GlobalData.g_global.selectFriendId = reader.ReadInt32();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteAgreeNoticeComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendInviteCancelResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteCancelComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendInviteCancelNoticeResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInviteCancelNoticeComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendInfoResponse:

                        GlobalData.g_global.f_Info.score_rank = reader.ReadInt32();
                        GlobalData.g_global.f_Info.coin_rank = reader.ReadInt32();
                        GlobalData.g_global.f_Info.score = reader.ReadInt32();
                        GlobalData.g_global.f_Info.coin = reader.ReadInt32();

                        for (int i = 0; i < 4; i++)
                        {
                            GlobalData.g_global.f_Info.slot[i] = reader.ReadByte();
                        }

                        GlobalData.g_global.f_Info.battle_rank = reader.ReadInt32();
                        GlobalData.g_global.f_Info.win = reader.ReadInt32();
                        GlobalData.g_global.f_Info.lose = reader.ReadInt32();

                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendInfoComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendSuccessNoticeResponse:
                        GlobalData.g_global.friendRoom = reader.ReadInt32();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendSuccessNoticeComplete;
                        break;
                    case (short)SocketClass.MsgType.mFriendPlayResponse:
                        GlobalData.g_global.sheetInfo.userID = reader.ReadInt32();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mFriendPlayComplete;
                        break;
                    case (short)SocketClass.MsgType.mCouponResponse:
                        GlobalData.g_global.couponResult = reader.ReadByte();
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mCouponComplete;
                        break;
                    case (short)SocketClass.MsgType.mPushinfoResponse:
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mPushinfoComplete;
                        break;
                    case (short)SocketClass.MsgType.mRankingResponse:
                        int value1 = reader.ReadInt32();
                        int value2 = reader.ReadInt32();
                        int ranking1 = reader.ReadInt32();
                        int bcount1 = reader.ReadByte();
                        switch (GlobalData.g_global.rank_state)
                        {
                            case 0:
                                GlobalData.g_global.r_rankInfo.Clear();

                                GlobalData.g_global.myInfo.week_bestScore = value1;
                                Debug.Log("=========================");
                                RankInfo myRankList2 = new RankInfo();
                                myRankList2.nickname = GlobalData.g_global.myInfo.nickName;
                                myRankList2.weekly_best = GlobalData.g_global.myInfo.week_bestScore;
                                myRankList2.rank = ranking1;
                                myRankList2.userKey = GlobalData.g_global.myInfo.userKey;
                                GlobalData.g_global.r_rankInfo.Add(myRankList2);

                                if (bcount1 != 0)
                                {
                                    for (int i = 0; i < bcount1; i++)
                                    {
                                        rankList = new RankInfo();

                                        rankList.userKey = Encoding.UTF8.GetString(reader.ReadBytes(30)).Split(char.MinValue)[0];
                                        
                                        tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20));
                                        rankList.nickname = tempnick;
                                        rankList.weekly_best = reader.ReadInt32();
                                        reader.ReadInt32();
                                        rankList.rank = reader.ReadInt32();

                                        GlobalData.g_global.r_rankInfo.Add(rankList);
                                    }
                                }

                                break;
                            case 1:

                                GlobalData.g_global.c_rankInfo.Clear();

                                GlobalData.g_global.myInfo.week_bestCoin = value1;

                                CoinRankInfo myRankList3 = new CoinRankInfo();
                                myRankList3.nickname = GlobalData.g_global.myInfo.nickName;
                                myRankList3.weekly_best = GlobalData.g_global.myInfo.week_bestCoin;
                                myRankList3.rank = ranking1;
                                myRankList3.userKey = GlobalData.g_global.myInfo.userKey;

                                GlobalData.g_global.c_rankInfo.Add(myRankList3);

                                if (bcount1 != 0)
                                {
                                    for (int i = 0; i < bcount1; i++)
                                    {
                                        CoinRankInfo c_rankList = new CoinRankInfo();

                                        c_rankList.userKey = Encoding.UTF8.GetString(reader.ReadBytes(30)).Split(char.MinValue)[0];
                                        tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20));
                                        c_rankList.nickname = tempnick;
                                        c_rankList.weekly_best = reader.ReadInt32();
                                        reader.ReadInt32();
                                        c_rankList.rank = reader.ReadInt32();

                                        GlobalData.g_global.c_rankInfo.Add(c_rankList);
                                    }
                                }

                                break;
                            case 2:

                                GlobalData.g_global.b_rankInfo.Clear();

                                GlobalData.g_global.myInfo.win = value1;
                                GlobalData.g_global.myInfo.lose = value2;

                                BattleRankInfo myRankList4 = new BattleRankInfo();
                                myRankList4.nickname = GlobalData.g_global.myInfo.nickName;
                                myRankList4.win = GlobalData.g_global.myInfo.win;
                                myRankList4.lose = GlobalData.g_global.myInfo.lose;
                                myRankList4.rank = ranking1;
                                myRankList4.userKey = GlobalData.g_global.myInfo.userKey;

                                GlobalData.g_global.b_rankInfo.Add(myRankList4);


                                if (bcount1 != 0)
                                {
                                    for (int i = 0; i < bcount1; i++)
                                    {
                                        BattleRankInfo b_rankList = new BattleRankInfo();

                                        b_rankList.userKey = Encoding.UTF8.GetString(reader.ReadBytes(30)).Split(char.MinValue)[0];
                                        tempnick = Encoding.UTF8.GetString(reader.ReadBytes(20));
                                        b_rankList.nickname = tempnick;
                                        b_rankList.win = reader.ReadInt32();
                                        b_rankList.lose = reader.ReadInt32();
                                        b_rankList.rank = reader.ReadInt32();

                                        GlobalData.g_global.b_rankInfo.Add(b_rankList);
                                    }
                                }


                                break;
                        }
                        GlobalData.g_global.socketState = (int)SocketClass.STATE.mRankingComplete;
                        break;

                    //170804:추가프로토콜 콜백
                    case (short)SocketClass.BingoMsg.mWaitingRoomJoinResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            GlobalData.g_global.mWaitingRoomID = reader.ReadInt32();
                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mWaitingRoomJoinComplete;

                            Debug.Log("(1001) mWaitingRoomJoinResponse roomID : " +
                                GlobalData.g_global.mWaitingRoomID.ToString());
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mWaitingRoomJoinIng:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            GlobalData.g_global.mWaitingRoomJoinGameRemainSec = reader.ReadInt32();
                            GlobalData.g_global.mWaitingRoomEndGameRemainBingo = reader.ReadInt32();

                            //Debug.Log("(1002) mWaitingRoomJoinIng waitTime : " +
                            //    GlobalData.g_global.mWaitingRoomJoinGameRemainSec.ToString());
                            //Debug.Log("(1002) mWaitingRoomEndGameRemainBingo : " +
                            //    GlobalData.g_global.mWaitingRoomEndGameRemainBingo.ToString());
                            Debug.Log("(1002) mWaitingRoomJoinIng waitTime");
                            //Debug.Log("(1002) mWaitingRoomEndGameRemainBingo");
                            //

                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mWaitingRoomJoinIng;
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameJoinResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            int joinResult = reader.ReadInt32();

                            Debug.Log("(1004) mGameJoinResponse joinResult : " +
                                joinResult.ToString());

                            if (joinResult == 0)
                            {
                                GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameJoinIngComplete;
                            }

                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameStartResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //빙고판세팅
                            int cardCount = reader.ReadInt32();
                            GlobalData.g_global.sheetInfo.activeSheetCount = cardCount;
                            
                            Debug.Log("@@ mGameStartResponse cardCount : " +
                                cardCount.ToString());

                            // 시트 초기화
                            for (int i = 0; i < 25; i++)
                            {
                                GlobalData.g_global.sheetInfo.sheet[0, i] = 0;
                                GlobalData.g_global.sheetInfo.sheet[1, i] = 0;
                                GlobalData.g_global.sheetInfo.sheet[2, i] = 0;
                                GlobalData.g_global.sheetInfo.sheet[3, i] = 0;
                            }
                            GlobalData.g_global.BingoNumberCount = 0;
                            GlobalData.g_global.sheetInfo.shield = 0;
                            GlobalData.g_global.myShield = 0;

                            for (int card = 0; card < cardCount; ++card)
                            {
                                char[] numbers = new char[25];
                                numbers = reader.ReadChars(25);
                                for (int i = 0; i < 4; i++)
                                {
                                    GlobalData.g_global.sheetInfo.bingoSheet[i] = false;
                                }

                                char[,] tempArrayA = new char[5, 5];
                                for (int x = 0; x < 5; ++x)
                                {
                                    for(int y = 0; y < 5; ++y)
                                    {
                                        tempArrayA[x, y] = numbers[(x * 5) + y];
                                    }
                                }

                                //행렬 뒤집어서 생성
                                for (int x = 0; x < 5; ++x)
                                {
                                    for (int y = 0; y < 5; ++y)
                                    {
                                        GlobalData.g_global.sheetInfo.
                                            sheet[card, (x * 5) + y] = (int)tempArrayA[y, x];
                                    }
                                }
                            }

                            //유저 리스트
                            int userCount = reader.ReadInt32();
                            GlobalData.g_global.BingoRoomUserCount = userCount;
                            GlobalData.g_global.BingoRoomUserNameList = new string[userCount];

                            Debug.Log("@@ mGameStartResponse userCount : " +
                                userCount.ToString());

                            string temp = "@@ mGameStartResponse nameList : ";
                            for (int i = 0; i < userCount; ++i)
                            {
                                string userName = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                                GlobalData.g_global.BingoRoomUserNameList[i] = userName;
                                temp += userName + ", ";
                            }

                            Debug.Log(temp);

                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameStartIngComplete;
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameBingoNumberIng:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //빙고 숫자 받음
                            int bingoNum = reader.ReadInt32();

                            GlobalData.g_global.BingoNumberCount += 1;
                            GlobalData.g_global.bingoball[GlobalData.g_global.BingoNumberCount] = bingoNum;
                            
                            //Debug.Log("@@ mGameBingoNumberIng bingoNum : " +
                            //    bingoNum.ToString() + "(" + GlobalData.g_global.BingoNumberCount.ToString() + ")");

                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameBingoNumberIngComplete;
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameBingoFinishResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameBingoFinishComplete;
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameBingoFinishOtherResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            int rank = reader.ReadInt32();
                            string userName = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];

                            //Debug.Log("@@ mGameBingoFinishOtherResponse rank : " + rank.ToString());
                            Debug.Log("@@ mGameBingoFinishOtherResponse userName : " + userName);

                            GlobalData.g_global.BingoTotalFinishCount += 1;
                            GlobalData.g_global.BingoLastFinishUserName = userName;                            

                            GlobalData.g_global.socketState = (int)SocketClass.BingoState.mGameBingoFinishOtherResponse;
                        }
                        break;
                    case (short)SocketClass.BingoMsg.mGameCloseResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            GlobalData.g_global.socketState = (int)SocketClass.BingoMsg.mGameCloseResponse;
                        }
                        break;

                    case (short)SocketClass.BingoMsg.mGameRobotStatusAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //int rank = reader.ReadInt32();
                            Debug.Log("> mGameRobotStatusAlarm");
                        }
                        break;

                }

                GlobalData.g_global.resultData = result;
            }

            buffer = new byte[1024 * 10];
            offset = 0;
            PSize = 0;
            readTime = DateTime.Now;
            FrontBeginRead();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    public void closeSocket()
    {
        if (client.Connected == true)
        {
            FrontBeginWrite((int)SocketClass.STATE.mClose);
            client.Close();
        }
        GlobalData.g_global.socketState = (int)SocketClass.STATE.waitSign;
    }


    public object obj = new object();

    public void FrontBeginWrite(int type)
    {

        if (type != (int)SocketClass.MsgType.mHeartBeat)
        {
            readTime = DateTime.Now;
            ReadCall = true;
        }
        if (client == null)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
            return;
        }

        byte[] ReturnByte = null;
        //  Debug.Log(" TYPE : " + type);
        try
        {
            if (type >= 1000)
            {
                Debug.Log("request SocketClass : " + type);
            }
            switch (type)
            {
                case (short)SocketClass.MsgType.mServerInfoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mServerInfoRequest);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mHeartBeat:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mHeartBeat);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mServerTestRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)7);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mServerTestRequest);
                        writer.Write((byte)3);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }
                    break;

                case (short)SocketClass.MsgType.mNoticeRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)7);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mNoticeRequest);
                        writer.Write((byte)3);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }
                    break;

                case (short)SocketClass.MsgType.mLoginRequest:

                    byte[] userKey = new byte[30];
                    byte[] tempkey = new byte[30];
                    tempkey = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.userKey);

                    Array.Copy(tempkey, userKey, tempkey.Length);

                    byte[] bufNickName = new byte[20];
                    byte[] tempnick = new byte[20];

                    tempnick = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);

                    Array.Copy(tempnick, bufNickName, tempnick.Length);

                    byte[] device = new byte[100];
                    byte[] tempdevice = new byte[100];
                    tempdevice = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);
                    Array.Copy(tempdevice, device, tempdevice.Length);

                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Flush();
                        writer.Write(H);             //헤더
                        writer.Write((short)158);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mLoginRequest);
                        writer.Write(userKey);
                        writer.Write(bufNickName);
                        writer.Write((byte)116);   // version
                        writer.Write((byte)5);
                        writer.Write(device);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mMyGameInfoRequest:

                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMyGameInfoRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mBandkeySetRequest:
                    byte[] bufbandkey = new byte[100];
                    byte[] tempband = new byte[100];

                    tempband = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.bandKey);
                    Array.Copy(tempband, bufbandkey, tempband.Length);

                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)111);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mBandkeySetRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((byte)1);
                        writer.Write(bufbandkey);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mBandListRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)7);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mBandListRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;

                case (short)SocketClass.MsgType.mTicketChargeRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mTicketChargeRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mTicketSendRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mTicketSendRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;

                case (short)SocketClass.MsgType.mTicketSettingRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mTicketSettingRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;

                case (short)SocketClass.MsgType.mItemShopRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)19);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mItemShopRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.shopTapindex);
                        writer.Write((int)GlobalData.g_global.shopIndex);
                        writer.Write((byte)5);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;


                case (short)SocketClass.MsgType.mBandRankingRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mBandRankingRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;

                case (short)SocketClass.MsgType.mWorldRankingRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mWorldRankingRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }
                    break;

                case (short)SocketClass.MsgType.mCoinRankingRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mCoinRankingRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mMailGetRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMailGetRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mMailCheckRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)15);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMailCheckRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((byte)GlobalData.g_global.getMailType);
                        writer.Write((int)GlobalData.g_global.getMailindex);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mDropRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {

                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mDropRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mMonsterGachaRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)18);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMonsterGachaRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.shopTapindex);
                        writer.Write((int)GlobalData.g_global.shopIndex);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mPlayGachaRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        GlobalData.g_global.myrank++;
                        writer.Write(H);             //헤더
                        writer.Write((short)18);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mPlayGachaRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.myrank);
                        writer.Write((int)GlobalData.g_global.gameType);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mMonsterCardListRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMonsterCardListRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);


                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mMonsterCardUseRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)26);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMonsterCardUseRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.m_setInfo.stat);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_num);
                        writer.Write((int)GlobalData.g_global.m_setInfo.sell_money);
                        writer.Write((int)GlobalData.g_global.m_setInfo.slot);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mMonsterCardMixRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        int headerSize = 42;
                        writer.Write(H);             //헤더
                        writer.Write((short)headerSize);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMonsterCardMixRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.m_setInfo.stat);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_num);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_id);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_level);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_exp);
                        writer.Write((int)GlobalData.g_global.m_setInfo.card_rank);
                        writer.Write((int)GlobalData.g_global.m_setInfo.sell_money);
                        writer.Write((int)GlobalData.g_global.m_material.Count);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mEventListRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mEventListRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mEventGachaRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {

                        byte[] b_sheet = new byte[25];

                        for (int i = 0; i < 25; i++)
                        {
                            b_sheet[i] = Convert.ToByte(GlobalData.g_global.m_eventData[GlobalData.g_global.event_btnindex].bingo[i]);
                        }
                        writer.Write(H);             //헤더
                        writer.Write((short)44);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mEventGachaRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.event_index);
                        writer.Write(b_sheet);
                        writer.Write((byte)GlobalData.g_global.m_eventData[GlobalData.g_global.event_btnindex].bingoInfo);
                        writer.Write((int)2);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mEventMyInfoRequest:

                    //////////////////////////////////////141215

                    byte[] bufNickName4 = new byte[20];
                    byte[] tempbuf = new byte[20];

                    tempbuf = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);
                    Array.Copy(tempbuf, bufNickName4, tempbuf.Length);

                    byte[] bufNickName5 = new byte[20];
                    byte[] tempbuf2 = new byte[20];

                    tempbuf2 = Encoding.UTF8.GetBytes(GlobalData.g_global.event_phoneNumber);
                    Array.Copy(tempbuf2, bufNickName5, tempbuf2.Length);

                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)55);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mEventMyInfoRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);             //
                        writer.Write(bufNickName4);             //닉네임
                        writer.Write((int)GlobalData.g_global.event_index);
                        writer.Write(bufNickName5);             //폰넘버
                        writer.Write((byte)3);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mGameServerJoinRequest:

                    byte[] bufNickName2 = new byte[20];
                    byte[] tempnick2 = new byte[20];

                    tempnick2 = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);
                    Array.Copy(tempnick2, bufNickName2, tempnick2.Length);
                    byte gametype = (byte)(GlobalData.g_global.gameType - 1);

                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)31);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mGameServerJoinRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write(bufNickName2);
                        writer.Write(gametype);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mMatchRequest:
                    byte gametype2 = (byte)(GlobalData.g_global.gameType - 1);
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)13);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMatchRequest);
                        writer.Write(gametype2);             //game 번호
                        writer.Write((byte)GlobalData.g_global.myPlayCard);
                        writer.Write((byte)GlobalData.g_global.selectSheet);
                        writer.Write(GlobalData.g_global.betting_index);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)SocketClass.MsgType.mMatchCancelRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mMatchCancelRequest);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mGameStartRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        byte[] sheet11 = new byte[25];
                        byte[] sheet12 = new byte[25];
                        byte[] sheet13 = new byte[25];
                        byte[] sheet14 = new byte[25];
                        byte tempsheet1 = 0;
                        byte tempsheet2 = 0;
                        byte tempsheet3 = 0;
                        byte tempsheet4 = 0;

                        for (int i = 0; i < 25; i++)
                        {
                            tempsheet1 = (byte)GlobalData.g_global.sheetInfo.sheet[0, i];
                            sheet11[i] = tempsheet1;

                            tempsheet2 = (byte)GlobalData.g_global.sheetInfo.sheet[1, i];
                            sheet12[i] = tempsheet2;

                            tempsheet3 = (byte)GlobalData.g_global.sheetInfo.sheet[2, i];
                            sheet13[i] = tempsheet3;

                            tempsheet4 = (byte)GlobalData.g_global.sheetInfo.sheet[3, i];
                            sheet14[i] = tempsheet4;
                        }


                        writer.Write(H);             //헤더
                        writer.Write((short)108);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mGameStartRequest);
                        writer.Write((short)100);
                        writer.Write(sheet11);
                        writer.Write(sheet12);
                        writer.Write(sheet13);
                        writer.Write(sheet14);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mReadycompleteRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mReadycompleteRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mGameMessageRequest:

                    byte[] sheet1 = new byte[25];
                    byte[] sheet2 = new byte[25];
                    byte[] sheet3 = new byte[25];
                    byte[] sheet4 = new byte[25];
                    byte shield;

                    byte[] bufNickName3 = new byte[20];
                    byte[] tempnick4 = new byte[20];

                    tempnick4 = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);
                    Array.Copy(tempnick4, bufNickName3, tempnick4.Length);


                    for (int i = 0; i < 25; i++)
                    {

                        sheet1[i] = Convert.ToByte(GlobalData.g_global.sheetInfo.sheet[0, i]);
                        sheet2[i] = Convert.ToByte(GlobalData.g_global.sheetInfo.sheet[1, i]);
                        sheet3[i] = Convert.ToByte(GlobalData.g_global.sheetInfo.sheet[2, i]);
                        sheet4[i] = Convert.ToByte(GlobalData.g_global.sheetInfo.sheet[3, i]);
                    }

                    byte[] bingoState = new byte[4];

                    for (int i = 0; i < 4; i++)
                    {
                        bingoState[i] = Convert.ToByte(GlobalData.g_global.sheetInfo.bingoSheet[i]);
                    }
                    shield = (byte)GlobalData.g_global.myShield;
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)133);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mGameMessageRequest);
                        writer.Write((short)125);
                        writer.Write(bufNickName3);
                        writer.Write(sheet1);
                        writer.Write(sheet2);
                        writer.Write(sheet3);
                        writer.Write(sheet4);
                        writer.Write(bingoState);
                        writer.Write(shield);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mItemMessageRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)22);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mItemMessageRequest);
                        writer.Write((int)GlobalData.g_global.target_id);
                        writer.Write((int)GlobalData.g_global.target_index);
                        writer.Write((int)GlobalData.g_global.target_sheetindex);
                        writer.Write((int)GlobalData.g_global.targeter_sheetindex);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();

                    }

                    break;
                case (short)SocketClass.MsgType.mBingoResultRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mBingoResultRequest);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;
                case (short)SocketClass.MsgType.mClose:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mClose);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    GlobalData.g_global.socketState = (int)SocketClass.STATE.mCloseComplete;
                    break;
                case (short)SocketClass.MsgType.mScoreUpdateRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)26);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mScoreUpdateRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.GameScore);
                        writer.Write((int)GlobalData.g_global.myInfo.game_gold);
                        writer.Write((int)GlobalData.g_global.myInfo.game_ticket);
                        writer.Write((int)GlobalData.g_global.myInfo.game_goldticket);
                        writer.Write((int)GlobalData.g_global.useItemCount);             //메시지 번
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();

                    }
                    GlobalData.g_global.socketState = (int)SocketClass.STATE.mScoreUpdateIngComplete;
                    break;
                case (short)SocketClass.MsgType.mEventServerJoinRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mEventServerJoinRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }

                    //st.BeginWrite(ReturnByte, 0, ReturnByte.Length, new AsyncCallback(WriteCallBack), st);
                    break;
                case (short)SocketClass.MsgType.mDailyInfoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mDailyInfoRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }

                    //st.BeginWrite(ReturnByte, 0, ReturnByte.Length, new AsyncCallback(WriteCallBack), st);
                    break;
                case (short)SocketClass.MsgType.mDailyCheckRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        GlobalData.g_global.dailyIndex -= 1;
                        writer.Write(H);             //헤더
                        writer.Write((short)14);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mDailyCheckRequest);
                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((int)GlobalData.g_global.dailyIndex);



                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    //st.BeginWrite(ReturnByte, 0, ReturnByte.Length, new AsyncCallback(WriteCallBack), st);
                    break;






                case (short)SocketClass.MsgType.mFriendConnectInfoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        int Hsize = 8;

                        Hsize = Hsize + (30 * GlobalData.g_global.myFriendList.Count);

                        if (GlobalData.g_global.myFriendList.Count >= 30)
                        {
                            Hsize = Hsize + (30 * 30);
                        }


                        writer.Write(H);             //헤더
                        writer.Write((short)Hsize);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendConnectInfoRequest);
                        writer.Write((short)GlobalData.g_global.myFriendList.Count);             //친구수량

                        for (int i = 0; i < GlobalData.g_global.myFriendList.Count; i++)
                        {
                            byte[] userKey3 = new byte[30];
                            byte[] tempuserKey3 = new byte[30];

                            tempuserKey3 = Encoding.UTF8.GetBytes(GlobalData.g_global.myFriendList[i].FriendKey);
                            Array.Copy(tempuserKey3, userKey3, tempuserKey3.Length);
                            writer.Write(userKey3);
                        }

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mFriendInviteRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)12);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendInviteRequest);
                        writer.Write((int)GlobalData.g_global.selectFriendId);
                        writer.Write((byte)GlobalData.g_global.selectSheet);
                        writer.Write((byte)GlobalData.g_global.myPlayCard);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mFriendInviteAgreeRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)13);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendInviteAgreeRequest);
                        writer.Write((byte)GlobalData.g_global.selectInvite);
                        writer.Write((byte)GlobalData.g_global.selectSheet);
                        writer.Write((byte)GlobalData.g_global.myPlayCard);
                        writer.Write((int)GlobalData.g_global.selectFriendId);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mFriendInviteCancelRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendInviteCancelRequest);
                        writer.Write((int)GlobalData.g_global.selectFriendId);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mFriendInfoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)36);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendInfoRequest);

                        byte[] userKey4 = new byte[30];
                        byte[] tempuserKey4 = new byte[30];

                        tempuserKey4 = Encoding.UTF8.GetBytes(GlobalData.g_global.selectFriendKey);
                        Array.Copy(tempuserKey4, userKey4, tempuserKey4.Length);

                        writer.Write(userKey4);             //메시지 번
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mFriendPlayRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)36);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mFriendPlayRequest);

                        byte[] username4 = new byte[20];
                        byte[] tempusername4 = new byte[20];

                        tempusername4 = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.nickName);
                        Array.Copy(tempusername4, username4, tempusername4.Length);

                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write(username4);
                        writer.Write((int)GlobalData.g_global.friendRoom);
                        writer.Write((byte)GlobalData.g_global.myPlayCard);
                        writer.Write((byte)GlobalData.g_global.selectSheet);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mCouponRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)30);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mCouponRequest);

                        byte[] coupon = new byte[20];
                        byte[] tempcoupon = new byte[20];

                        tempcoupon = Encoding.UTF8.GetBytes(GlobalData.g_global.couponNum);
                        Array.Copy(tempcoupon, coupon, tempcoupon.Length);

                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write(coupon);

                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mPushinfoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)211);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mPushinfoRequest);

                        byte[] device_id = new byte[200];
                        byte[] tempdevice2 = new byte[200];

                        tempdevice2 = Encoding.UTF8.GetBytes(GlobalData.g_global.myInfo.deviceId);
                        Array.Copy(tempdevice2, device_id, tempdevice2.Length);

                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((byte)5);
                        writer.Write(device_id);


                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.MsgType.mRankingRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)11);             //사이즈
                        writer.Write((short)SocketClass.MsgType.mRankingRequest);

                        writer.Write((int)GlobalData.g_global.myInfo.userID);
                        writer.Write((byte)GlobalData.g_global.rank_state);


                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;

                //170804:추가프로토콜 리퀘스트
                case (short)SocketClass.BingoMsg.mWaitingRoomJoinRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)6);             //사이즈
                        writer.Write((short)SocketClass.BingoMsg.mWaitingRoomJoinRequest);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.BingoMsg.mGameJoinRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.BingoMsg.mGameJoinRequest);
                        writer.Write((int)GlobalData.g_global.sheetInfo.activeSheetCount);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)SocketClass.BingoMsg.mGameBingoFinishRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)10);             //사이즈
                        writer.Write((short)SocketClass.BingoMsg.mGameBingoFinishRequest);
                        writer.Write((int)GlobalData.g_global.mSelectBingoButtonIndex);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;


            }

            st.Write(ReturnByte, 0, ReturnByte.Length);
            st.Flush();
        }
        catch (Exception e)
        {
            GlobalData.g_global.socketState = (int)SocketClass.STATE.connectionFail;
            Debug.Log(e.ToString());
        }

        HeartBeatTime = DateTime.Now;
    }

    public void WriteCallBack(IAsyncResult ar)
    {
        st.EndWrite(ar);
        st.Flush();
    }

    public int GetPsize(byte[] buffer)
    {
        using (BinaryReader reader = new BinaryReader(new MemoryStream(buffer, false)))
        {
            short header = reader.ReadInt16();
            short psize = reader.ReadInt16();
            short type = reader.ReadInt16();
            return psize;
        }
    }





}
