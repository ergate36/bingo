using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using MarigoldGame.Protocol;
using Newtonsoft.Json;
using MarigoldGame.Common;
using MarigoldGame.Commands;
using MarigoldModel.Model.Enum;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using MarigoldModel.Model;
using MarigoldModel.Commands;


public class nbSocket : MonoBehaviour
{
    const int BUFFER_SIZE = 1024 * 40;

    public TcpClient client = null;
    public static Stream st = null;
    public static byte[] buffer = new byte[BUFFER_SIZE];
    public static int offset = 0;
    public static int Size = BUFFER_SIZE;
    public static int PSize = 0;
    //public static string H = "Json";
    public static byte[] H = { 74, 115, 111, 110 }; //"Json"

    public static string serverIP = "14.63.172.176";
    public static int serverPort = 50111;

    public static nbSocket sCtrl = null;
    public DateTime HeartBeatTime = DateTime.Now;
    public DateTime readTime = DateTime.Now;

    static UdpClient udpClient = new UdpClient();

    private int index;
    // Use this for initialization
    void Start()
    {
        client = new TcpClient();
        if (sCtrl == null)
        {
            sCtrl = GameObject.Find("nb_GlobalObject").GetComponent<nbSocket>();
        }
    }
    
    void Update()
    {
        if (client.Connected == true)
        {
            if ((DateTime.Now.Ticks - HeartBeatTime.Ticks) > (10000000 * 10))
            {
                nbSocket.sCtrl.FrontBeginWrite((int)nb_SocketClass.MsgType.HeartBeatRequest);
            }

            if (ReadCall == true)
            {
                if ((DateTime.Now.Ticks - readTime.Ticks) > (11000000 * 10))
                {
                    ReadCall = false;
                    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.excessTime;
                    //에러 팝업창 발생 로그인 화면으로 이동 처음부터 시작
                }
            }
        }
    }

    public bool StartClient()
    {
        Debug.Log("StartClient : " + client.Connected.ToString());
        if (client.Connected == true)
        {
            try
            {
                //closeSocket();
            }
            catch
            {
            }
        }

        if (client.Connected == false)
        {
            string ipname = "";
            int portname = 0;

            //if (GlobalData.g_global.serverFlag == 1)
            //{   // lobby
            //    ipname = GlobalData.g_global.lobbyip;
            //    portname = GlobalData.g_global.lobbyport;
            //}
            //else if (GlobalData.g_global.serverFlag == 2)
            //{   // 5
            //    //ipname = GlobalData.g_global.itemip[GlobalData.g_global.modeIndex];
            //    //portname = GlobalData.g_global.itemport[GlobalData.g_global.modeIndex];
            //    ipname = itemIP;
            //    portname = 50111;
            //}

            //else if (GlobalData.g_global.serverFlag == 3)
            //{   //100
            //    ipname = bandIP;
            //    portname = bandPort;
            //}
            ipname = serverIP;
            portname = serverPort;

            if (nb_GlobalData.g_global.serverFlag == 1)
            {   // main
                ipname = serverIP;
                portname = serverPort;
            }
            else if (nb_GlobalData.g_global.serverFlag == 2)
            {
                ipname = nb_GlobalData.g_global.gl_ipAddress;
                portname = nb_GlobalData.g_global.gl_port;
            }

            //ipname = nb_GlobalData.g_global.gl_ipAddress;
            //portname = nb_GlobalData.g_global.gl_port;

            //Debug.Log("GlobalData.g_global.serverFlag : " + GlobalData.g_global.serverFlag.ToString());

            try
            {
                IPAddress ip = IPAddress.Parse(ipname);
                IPEndPoint remoteEP = new IPEndPoint(ip, portname);

                client = new TcpClient();

                client.Connect(new IPEndPoint(ip, portname));

                st = client.GetStream();

                FrontBeginRead();

                HeartBeatTime = DateTime.Now;
                Debug.Log("StartClient return true : " + ipname + " " + portname);
                return true;
            }
            catch (Exception e)
            {
                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectionFail;
                Debug.Log("StartClient return false");
                return false;	//접속실패시 예외가 발생하므로 그냥 무시하고 false를 리턴한다.
            }
        }
        Debug.Log("StartClient return true");
        return true;
    }

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
            //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectComplete;
        }
        catch (Exception e)
        {
            Debug.Log("CONNECTION FAIL");
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectionFail;
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
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectionFail;
            Debug.Log("connetion fail");
        }
    }

    public bool ReadCall = false;

    public void FrontReadCallBack(IAsyncResult ar)
    {
        try
        {
            ReadCall = false;

            //string text = BitConverter.ToString(buffer);
            //Debug.Log("read buffer : " + text);

            int EndSize = st.EndRead(ar);
            //Debug.Log("FrontReadCallBack EndSize = " + EndSize.ToString());
            
            if (EndSize <= 0)
            {
                Debug.Log("읽을 메시지가 없음");
                return;
            }

            if (PSize == 0)
            {
                PSize = GetPsize(buffer);
                //Debug.Log("PSize = " + PSize.ToString());
            }

            //Debug.Log("%%%% offset + EndSize : " + offset.ToString() + ", " + EndSize.ToString());
            offset += EndSize;

            if (offset < PSize + 8 || PSize == 0)
            {
                //Debug.Log("아직 덜받아서 재요청");
                FrontBeginRead();
                return;
            }

            using (BinaryReader reader = new BinaryReader(new MemoryStream(buffer, false)))
            {
                //Debug.Log("Read start");

                byte[] header = reader.ReadBytes(4);
                byte[] typeByte = reader.ReadBytes(2);
                byte[] psizeByte = reader.ReadBytes(2);
                Array.Reverse(typeByte, 0, 2);
                Array.Reverse(psizeByte, 0, 2);
                short type = BitConverter.ToInt16(typeByte, 0);
                short size = BitConverter.ToInt16(psizeByte, 0);

                byte[] bodyByte = reader.ReadBytes(size);
                string bodyText = Encoding.UTF8.GetString(bodyByte);



                //Debug.Log("response size : " + size.ToString() + ", bodyText : " + bodyText);

                byte result = 0;

                //if (type >= 1000)
                {
                    //Debug.Log(">> responese : " + type);
                }
                switch (type)
                {
                    case (short)nb_SocketClass.MsgType.HeartBeatResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //Debug.Log("> HeartBeatResponse <");
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.ConnectExitRequest:
                        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mCloseComplete;
                        break;

                    case (short)nb_SocketClass.MsgType.GameLiftConnectResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.GameLiftConnectResponse_End;
                            
                            //Debug.Log("> bodyText " + bodyText);

                            var body = JsonConvert.DeserializeObject<GameLiftConnectResponse>(
                                bodyText);

                            Debug.Log("<> gameliftresponse result : " + body.Result);

                            // test: result 0 //게임리프트 붙었다치자
                            //body.Result = 0;
                            //
                            nb_GlobalData.g_global.gameLiftConnectResponse = body;

                            Debug.Log("(1102) GameLiftConnectResponse Result : " +
                                nb_GlobalData.g_global.gameLiftConnectResponse.Result);

                        }
                        break;

                    //case (short)nb_SocketClass.MsgType.BlitzEnterWaitRoomResponse:
                    //    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    //    {
                    //        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzEnterWaitRoomResponse_End;

                    //        var body = JsonConvert.DeserializeObject<BlitzEnterWaitRoomResponse>(
                    //            bodyText);
                    //        nb_GlobalData.g_global.blitzEnterWaitRoomResponse = body;

                    //        nb_GlobalData.g_global.mWaitingRoomID = body.roomId;

                    //        Debug.Log("(1001) BlitzEnterWaitRoomResponse roomID : " +
                    //            nb_GlobalData.g_global.mWaitingRoomID.ToString());
                    //    }
                    //    break;
                    case (short)nb_SocketClass.MsgType.BlitzWaitRoomStatusAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzWaitRoomStatusAlarm_End;

                            var body = JsonConvert.DeserializeObject<BlitzWaitRoomStatusAlarm>(
                                bodyText);
                            nb_GlobalData.g_global.blitzWaitRoomStatusAlarm = body;

                            //nb_GlobalData.g_global.mWaitingRoomJoinGameRemainSec = body.remainSecond;
                            //nb_GlobalData.g_global.mWaitingRoomEndGameRemainBingo = body.remainBingo;

                            //Debug.Log("(1002) BlitzWaitRoomStatusAlarm waitTime : " +
                            //    nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainSecond.ToString() +
                            //    ", remainBingo : " + nb_GlobalData.g_global.blitzWaitRoomStatusAlarm.RemainBingo.ToString());
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzEnterGameResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            var body = JsonConvert.DeserializeObject<BlitzEnterGameResponse>(
                                bodyText);
                            nb_GlobalData.g_global.blitzEnterGameResponse = body;

                            int joinResult = body.Result;

                            Debug.Log("(1004) BlitzEnterGameResponse joinResult : " +
                                joinResult.ToString());

                            if (joinResult == 0)
                            {
                                //Debug.Log("BlitzEnterGameResponse joinResult == 0");
                                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzEnterGameResponse_End;
                            }

                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzStartGameAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //1005
                            Debug.Log("BlitzStartGameAlarm start");
                            Debug.Log(bodyText);

                            //try
                            //{
                            var trace = new MemoryTraceWriter();
                                var message = JsonConvert.DeserializeObject(bodyText, new JsonSerializerSettings()
                                {
                                    TypeNameHandling = TypeNameHandling.All,
                                    Binder = new MarigoldSerializationBinder(),
                                    TraceWriter = trace 
                                });
                                //Debug.Log(trace);
                            //}
                            //catch (Exception e)
                            //{
                            //    Debug.Log("BlitzStartGameAlarm catch : " + e.ToString());
                            //}

                            //var body = JsonConvert.DeserializeObject<BlitzStartGameAlarm>(
                            //    bodyText);
                                BlitzStartGameAlarm body = (BlitzStartGameAlarm)message;
                            nb_GlobalData.g_global.blitzStartGameAlarm.CardList = body.CardList;
                            nb_GlobalData.g_global.blitzStartGameAlarm.UserNameList = body.UserNameList;
                                                        

                            Debug.Log("BlitzStartGameAlarm DeserializeObject");

                            //빙고판세팅
                            int cardCount = nb_GlobalData.g_global.sheetInfo.activeSheetCount;

                            // 시트 초기화
                            for (int i = 0; i < 25; i++)
                            {
                                nb_GlobalData.g_global.sheetInfo.sheet[0, i] = 0;
                                nb_GlobalData.g_global.sheetInfo.sheet[1, i] = 0;
                                nb_GlobalData.g_global.sheetInfo.sheet[2, i] = 0;
                                nb_GlobalData.g_global.sheetInfo.sheet[3, i] = 0;
                            }
                            nb_GlobalData.g_global.BingoTotalFinishCount = 0;
                            nb_GlobalData.g_global.BingoNumberCount = 0;
                            nb_GlobalData.g_global.sheetInfo.shield = 0;
                            nb_GlobalData.g_global.myShield = 0;
                            nb_GlobalData.g_global.resetMyBlitzRanking();
                            
                            for (int card = 0; card < cardCount; ++card)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    nb_GlobalData.g_global.sheetInfo.bingoSheet[i] = false;
                                }

                                List<Square> cardData = body.CardList[card].SquareList;
                                
                                char[,] tempArray = new char[5, 5];
                                for (int x = 0; x < 5; ++x)
                                {
                                    for(int y = 0; y < 5; ++y)
                                    {
                                        tempArray[x, y] = Convert.ToChar(cardData[(x * 5) + y].Number);
                                    }
                                }
                                
                                // 행렬 뒤집
                                for (int x = 0; x < 5; ++x)
                                {
                                    string text = "";
                                    for (int y = 0; y < 5; ++y)
                                    {
                                        nb_GlobalData.g_global.sheetInfo.
                                            sheet[card, (x * 5) + y] = (int)tempArray[y, x];
                                        text += ((int)tempArray[y, x]).ToString() + ", ";
                                    }
                                    //Debug.Log("Bingo Sheet " + card.ToString() + " : " + text);
                                }
                            }
                            Debug.Log("Sheet Setting Finish");

                            //유저 리스트
                            int userCount = nb_GlobalData.g_global.blitzStartGameAlarm.UserNameList.Count;
                            nb_GlobalData.g_global.BingoRoomUserCount = userCount;
                            nb_GlobalData.g_global.BingoRoomUserNameList = new string[userCount];


                            Debug.Log("@@ BlitzStartGameAlarm userCount : " +
                                userCount.ToString());


                            //int userCount = reader.ReadInt32();
                            //nb_GlobalData.g_global.BingoRoomUserCount = userCount;
                            //nb_GlobalData.g_global.BingoRoomUserNameList = new string[userCount];


                            string temp = "@@ BlitzStartGameAlarm nameList : ";
                            int count = 0;
                            foreach (var user in nb_GlobalData.g_global.blitzStartGameAlarm.UserNameList)
                            {
                                nb_GlobalData.g_global.BingoRoomUserNameList[count] = user;
                                ++count;
                                temp += user + ", ";
                            }
                            //for (int i = 0; i < userCount; ++i)
                            //{
                            //    string userName = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                            //    nb_GlobalData.g_global.BingoRoomUserNameList[i] = userName;
                            //    temp += userName + ", ";
                            //}

                            //Debug.Log("userNameList : " + temp);

                            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzStartGameAlarm_End;
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzBingoNumberAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //빙고 숫자 받음(1006)
                            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzBingoNumberAlarm_End;

                            var body = JsonConvert.DeserializeObject<BlitzCallNumberAlarm>(
                                bodyText);
                            nb_GlobalData.g_global.blitzCallNumberAlarm = body;

                            int bingoNum = body.Number;

                            nb_GlobalData.g_global.BingoNumberCount += 1;
                            nb_GlobalData.g_global.bingoball[nb_GlobalData.g_global.BingoNumberCount] = bingoNum;

                            Debug.Log("@@ mGameBingoNumberIng bingoNum : " +
                                bingoNum.ToString() + "(" + nb_GlobalData.g_global.BingoNumberCount.ToString() + ")");
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzCompleteBingoResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            var body = JsonConvert.DeserializeObject<BlitzCompleteBingoResponse>(
                                bodyText);
                            nb_GlobalData.g_global.blitzCompleteBingoResponse = body;     
                     
                            if(body.Result == 0)
                            {
                                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzCompleteBingoResponse_End;
                            }
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzCompleteBingoAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            var body = JsonConvert.DeserializeObject<BlitzCompleteBingoAlarm>(
                                bodyText);
                            nb_GlobalData.g_global.blitzCompleteBingoAlarm = body;
                            
                            int rank = body.Ranking;                            
                            //string userName = Encoding.UTF8.GetString(reader.ReadBytes(20)).Split(char.MinValue)[0];
                            string userName = body.Name;

                            Debug.Log("@@ BlitzCompleteBingoAlarm userName : " + userName);

                            nb_GlobalData.g_global.BingoTotalFinishCount += 1;
                            nb_GlobalData.g_global.BingoLastFinishUserName = userName;

                            if (nb_GlobalData.g_global.callBingo == false)
                            {
                                //내가 완성했을땐 BlitzCompleteBingoResponse에서 스테이트를 변경
                                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzCompleteBingoAlarm_End;
                            }
                        }
                        break;
                    case (short)nb_SocketClass.MsgType.BlitzEndGameAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //게임 종료
                            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzEndGameAlarm_End;
                        }
                        break;

                    case (short)nb_SocketClass.MsgType.BlitzRobotStateAlarm:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //로봇 상태 알림
                            //Debug.Log("BlitzRobotStateAlarm start");
                            //Debug.Log(bodyText);
                        }
                        break;                     

                    case (short)nb_SocketClass.MsgType.BlitzUsePowerUpResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            var body = JsonConvert.DeserializeObject<BlitzUsePowerUpResponse>(
                                bodyText);
                            //아이템 사용 응답
                            Debug.Log("BlitzUsePowerUpResponse : " + bodyText.ToString());

                            nb_GlobalData.g_global.blitzUsePowerUpResponse = body;

                            //if (body.Result == 0)
                            {
                                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzUsePowerUpResponse_End;
                            }
                        }
                        break;

                    case (short)nb_SocketClass.MsgType.BlitzRefreshPowerUpResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            var body = JsonConvert.DeserializeObject<BlitzRefreshPowerUpResponse>(
                                bodyText);
                            //아이템 재확인 응답
                            Debug.Log("BlitzRefreshPowerUpResponse : " + bodyText.ToString());

                            nb_GlobalData.g_global.blitzRefreshPowerUpResponse = body;

                            //if (body.Result == 0)
                            {
                                nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzRefreshPowerUpResponse_End;
                            }
                        }
                        break;

                    case (short)nb_SocketClass.MsgType.BlitzGambleResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //미니 게임 응답
                        }
                        break;

                    case (short)nb_SocketClass.MsgType.BlitzCheckNumberResponse:
                        using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                        {
                            //var o = JObject.Parse(bodyText);
                            //var c = o["Command"];

                            //ParseCommand(c);

                            //var body = JsonConvert.DeserializeObject<BlitzCheckNumberResponse>(bodyText);

                            var body = JsonConvert.DeserializeObject<BlitzCheckNumberResponse>(
                                bodyText,
                                new JsonSerializerSettings()
                                    {
                                        TypeNameHandling = TypeNameHandling.Objects,
                                        Binder = new MarigoldSerializationBinder(),
                                    });
                            
                            //빙고 숫자 체크 응답
                            Debug.Log("BlitzCheckNumberResponse : " + bodyText.ToString());

                            nb_GlobalData.g_global.blitzCheckNumberResponse = body;

                            //Debug.Log("CheckSquareCommand found");

                            if (nb_GlobalData.g_global.blitzCheckNumberResponse.
                                Command.SubCommandList != null)
                            {
                                IncreasePowerUpGaugeCommand powerGauge = 
                                    nb_GlobalData.g_global.blitzCheckNumberResponse.
                                    Command.SubCommandList[0] as IncreasePowerUpGaugeCommand;

                                Debug.Log("powerGauge : " + powerGauge.PowerUpGauge.ToString());

                                if (nb_GlobalData.g_global.blitzCheckNumberResponse.
                                    Command.SubCommandList[0].SubCommandList != null)
                                {
                                    SelectRandomPowerUpCommand select =
                                        powerGauge.SubCommandList[0] as SelectRandomPowerUpCommand;

                                    if (nb_GlobalData.g_global.selectItemId == 0)
                                    {
                                        //아이템 선택
                                        nb_GlobalData.g_global.selectItemId = (int)select.SelectPowerUpId;
                                    }

                                    Debug.Log("SelectRandomPowerUpCommand found : " + 
                                        select.SelectPowerUpId.ToString());
                                }
                                else
                                {
                                    //Debug.Log("SelectRandomPowerUpCommand null");
                                }
                            }
                            
                            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.BlitzCheckNumberResponse_End;
                            //nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
                        }
                        break;


                        

                }

                nb_GlobalData.g_global.resultData = result;
            }

            //Debug.Log("%%%%% offset : " + offset.ToString());
            //Debug.Log("%%%%% PSize : " + PSize.ToString());
            var new_buffer = new byte[BUFFER_SIZE];
            Array.Copy(buffer, PSize + 8, new_buffer, 0, offset - (PSize + 8));            
            buffer = new_buffer;
            offset = offset - (PSize + 8);
            PSize = 0;
            readTime = DateTime.Now;
            FrontBeginRead();
        }
        catch (Exception e)
        {
            Debug.Log(st.Length.ToString());
            Debug.Log(st.ToString());
            Debug.Log(e.ToString());
        }
    }

    private void ParseCommand(JToken c)
    {
        var type = (CommandType)c["Type"].Value<int>();
        if (type == CommandType.CHECK_SQUARE)
        {
            //  ???
        }
        else if (type == CommandType.INCREASE_POWER_UP_GAUGE)
        {
            // 파워업 게이지가 늘어났을대 할 일
        }

        foreach (var child in c["SubCommandList"].Children())
        {
            ParseCommand(c);
        }
    }
    public void closeSocket()
    {
        if (client.Connected == true)
        {
            FrontBeginWrite((int)nb_SocketClass.STATE.mClose);
            client.Close();
        }
        nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.waitSign;
    }


    public object obj = new object();

    public void FrontBeginWrite(int type)
    {

        if (type != (int)nb_SocketClass.MsgType.HeartBeatRequest)
        {
            readTime = DateTime.Now;
            ReadCall = true;
        }
        if (client == null)
        {
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectionFail;
            return;
        }

        byte[] ReturnByte = null;
        //  Debug.Log(" TYPE : " + type);
        try
        {
            if (type != 97)
            {
                Debug.Log("request SocketClass : " + type);
            }
            switch (type)
            {
                case (short)nb_SocketClass.MsgType.HeartBeatRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((short)nb_SocketClass.MsgType.HeartBeatRequest);
                        writer.Write((short)0);             //사이즈
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();
                        writer.Close();
                    }

                    break;

                case (short)nb_SocketClass.MsgType.ConnectExitRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.ConnectExitRequest);
                        writer.Write((ushort)0);             //사이즈
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.mCloseComplete;
                    //return;
                    break;

                case (short)nb_SocketClass.MsgType.GameLiftConnectRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.GameLiftConnectRequest);

                        string id = nb_GlobalData.g_global.userSession.Id.ToString();
                        var a = new GameLiftConnectRequest
                        {
                            PlayerSessionId = nb_GlobalData.g_global.gl_playerSessionId,
                            SessionKey = nb_GlobalData.g_global.userSession.SessionKey,
                        };

                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);           //바디 사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                //case (short)nb_SocketClass.MsgType.BlitzEnterWaitRoomRequest:
                //    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                //    {
                //        writer.Write(H);             //헤더
                //        writer.Write((ushort)nb_SocketClass.MsgType.BlitzEnterWaitRoomRequest);

                //        var a = new BlitzEnterWaitRoomRequest
                //        {
                //        };

                //        var s = JsonConvert.SerializeObject(a);
                //        byte[] body = new UTF8Encoding().GetBytes(s);

                //        writer.Write((ushort)s.Length);           //바디 사이즈
                //        writer.Write(body);
                //        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                //        writer.Close();
                //    }
                //    break;
                case (short)nb_SocketClass.MsgType.BlitzEnterGameRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.BlitzEnterGameRequest);

                        var a = new BlitzEnterGameRequest
                        {
                            CardCount = nb_GlobalData.g_global.sheetInfo.activeSheetCount,
                        };
                    
                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);             //사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)nb_SocketClass.MsgType.BlitzCompleteBingoRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.BlitzCompleteBingoRequest);

                        var a = new BlitzCompleteBingoRequest
                        {
                            CardIndex = nb_GlobalData.g_global.mSelectBingoButtonIndex,
                        };
                    
                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);             //사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)nb_SocketClass.MsgType.BlitzUsePowerUpRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.BlitzUsePowerUpRequest);

                        Debug.Log("BlitzUsePowerUpRequest : " +
                            nb_GlobalData.g_global.selectItemId.ToString());

                        var a = new BlitzUsePowerUpRequest
                        {
                        };

                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);             //사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)nb_SocketClass.MsgType.BlitzRefreshPowerUpRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.BlitzUsePowerUpRequest);
                        
                        Debug.Log("BlitzRefreshPowerUpRequest");

                        var a = new BlitzRefreshPowerUpRequest
                        {
                        };

                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);             //사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
                case (short)nb_SocketClass.MsgType.BlitzGambleRequest:
                    break;
                case (short)nb_SocketClass.MsgType.BlitzCheckNumberRequest:
                    using (BinaryWriter writer = new BinaryWriter(new MemoryStream()))
                    {
                        writer.Write(H);             //헤더
                        writer.Write((ushort)nb_SocketClass.MsgType.BlitzCheckNumberRequest);

                        Debug.Log("BlitzCheckNumberRequest : " +
                            nb_GlobalData.g_global.CheckNumCardIndex.ToString() + " / " +
                            nb_GlobalData.g_global.CheckNumNumber.ToString());

                        var a = new BlitzCheckNumberRequest
                        {
                            CardIndex = nb_GlobalData.g_global.CheckNumCardIndex,
                            Number = nb_GlobalData.g_global.CheckNumNumber,
                        };

                        var s = JsonConvert.SerializeObject(a);
                        byte[] body = new UTF8Encoding().GetBytes(s);

                        writer.Write((ushort)s.Length);             //사이즈
                        writer.Write(body);
                        ReturnByte = ((MemoryStream)writer.BaseStream).ToArray();

                        writer.Close();
                    }
                    break;
            }

            ChangeBigEndian(ReturnByte);    //메세지 헤더부분 엔디안 변경
            string byteText = BitConverter.ToString(ReturnByte);

            if (type != 97)
            {
                //Debug.Log(type + " ReturnByte.Length " + ReturnByte.Length.ToString() +
                //    " // " + byteText);
            }
            st.Write(ReturnByte, 0, ReturnByte.Length);
            st.Flush();
        }
        catch (Exception e)
        {
            nb_GlobalData.g_global.socketState = (int)nb_SocketClass.STATE.connectionFail;
            Debug.Log(e.ToString());
        }

        HeartBeatTime = DateTime.Now;
    }

    protected int GetBodyLengthFromHeader(byte[] header, int offset, int length)
    {
        if (BitConverter.IsLittleEndian)
            Array.Reverse(header, offset + 6, 2);
        return BitConverter.ToUInt16(header, offset + 6);
    }

    protected void ChangeBigEndian(byte[] header)
    {
        Array.Reverse(header, 4, 2);
        Array.Reverse(header, 6, 2);
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
            byte[] header = reader.ReadBytes(4);
            byte[] type = reader.ReadBytes(2);
            byte[] psize = reader.ReadBytes(2);
            Array.Reverse(type, 0, 2);
            Array.Reverse(psize, 0, 2);
            int typeInt = BitConverter.ToInt16(type, 0);
            int sizeInt = BitConverter.ToInt16(psize, 0);
            //string typeStr = Encoding.UTF8.GetString(type);
            //string sizeStr = Encoding.UTF8.GetString(psize);

            //Debug.Log("getPsize header : " + Encoding.UTF8.GetString(header));
            //Debug.Log("getPsize type : " + typeInt);
            //Debug.Log("getPsize psize : " + sizeInt);

            //int res;
            //int.TryParse(sizeStr, out res);

            //Debug.Log("GetPsize return : " + res.ToString());
            ////return res;

            return sizeInt;
        }
    }





}
