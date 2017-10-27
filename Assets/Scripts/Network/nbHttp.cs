using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using Newtonsoft.Json;
using MarigoldModel.Model;
using MarigoldModel.Commands;

public class nbHttp : MonoBehaviour
{
    private static string AWSUrl = "https://v4ldyle7v4.execute-api.ap-northeast-2.amazonaws.com/Prod/";

    public static nbHttp http = null;

    public enum nbHttpState
    {
        Wait = 0,

        //account
        CreateUserSessionStart,
        CreateUserSessionSuccess,
        CreateUserAccountStart,
        CreateUserAccountSuccess,
        ConnectUserAccountStart,
        ConnectUserAccountSuccess,
        ConnectUserAccountFail,
        DisconnectPreviousSessionStart,
        DisconnectPreviousSessionSuccess,
        ConnectSocialStart,
        ConnectSocialSuccess,
        ConnectSocialFail,
        BindSocialStart,
        BindSocialSuccess,

        //stage
        GetStageListStart,
        GetStageListSuccess,
        ConnectStageStart,
        ConnectStageSuccess,
        ConnectStageFail,

        //shop/item
        GetUserPowerUpListStart,
        GetUserPowerUpListSuccess,
        GetPowerListStart,
        GetPowerListSuccess,
        BuyStoreProductStart,
        BuyStoreProductSuccess,

    };

    public static nbHttpState state = nbHttpState.Wait;

    // Use this for initialization
    void Start()
    {
        if (http == null)
        {
            http = GameObject.Find("nb_GlobalObject").GetComponent<nbHttp>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startPost(string controller, string function, Dictionary<string, string> post = null)
    {
        string url = AWSUrl + controller + "/" + function;
        if (post == null)
        {
            post = new Dictionary<string, string>();
            post.Add("", "");
        }

        var res = POST(url, post);
    }

    private WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        if (post != null)
        {
            foreach (KeyValuePair<string, string> post_arg in post)
            {
                form.AddField(post_arg.Key, post_arg.Value);
            }
        }

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
        return www;

    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors 
        if (www.error == null)
        {
            Debug.Log("WWW Result : " + www.text);

            JsonData jsonPlayer = JsonMapper.ToObject(www.text);
            bool isSuccess = jsonPlayer["status"].ToString() == "success";

            if (isSuccess)
            {
                parse(jsonPlayer);
            }
            else
            {
                wwwFail(jsonPlayer);
                //state = nbHttpState.Wait;
            }
        }
        else
        {
            Debug.Log("WWW Error : " + www.error);
        }
    }


    //POST

    public void CreateUserSession()
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("CreateUserSession fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.CreateUserSessionStart;

        string c = "Login";
        string f = "CreateUserSession";

        startPost(c, f);
    }

    public void CreateUserAccount(string session, string name)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("CreateUserAccount fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.CreateUserAccountStart;

        //Debug.Log("CreateUserAccount : " + session + ", " + name);

        string c = "Login";
        string f = "CreateUserAccount";

        Dictionary<string, string> post = new Dictionary<string,string>();
        post.Add("session", session);
        post.Add("name", name);

        startPost(c, f, post);
    }

    public void ConnectUserAccount(string session, long userAccountId, string secret)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("ConnectUserAccount fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.ConnectUserAccountStart;

        //Debug.Log("ConnectUserAccount : " + session + ", " + userAccountId + ", " + secret);

        string c = "Login";
        string f = "ConnectUserAccount";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("userAccountId", userAccountId.ToString());
        post.Add("secret", secret);

        startPost(c, f, post);
    }

    public void DisconnectPreviousSession(string session, long userAccountId, string secret)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("DisconnectPreviousSession fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.DisconnectPreviousSessionStart;

        //Debug.Log("DisconnectPreviousSession : " + session + ", " + userAccountId + ", " + secret);

        string c = "Login";
        string f = "DisconnectPreviousSession";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("userAccountId", userAccountId.ToString());
        post.Add("secret", secret);

        startPost(c, f, post);
    }

    public void ConnectSocial(string session, string socialKey)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("ConnectSocial fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.ConnectSocialStart;

        //Debug.Log("session : " + session + ", " + socialKey + ", " + socialKey);

        string c = "Login";
        string f = "ConnectSocial";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("socialKey", socialKey);

        startPost(c, f, post);
    }

    public void BindSocial(string session, string socialKey)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("BindSocial fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.BindSocialStart;

        //Debug.Log("BindSocial : " + session + ", " + "socialKey, " + socialKey);

        string c = "Login";
        string f = "BindSocial";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("socialKey", socialKey);

        startPost(c, f, post);
    }

    public void GetStageList(string session)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("GetStageList fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.GetStageListStart;
        
        string c = "Stage";
        string f = "GetStageList";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);

        startPost(c, f, post);
    }

    public void ConnectStage(string session, long stageId)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("ConnectStage fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.ConnectStageStart;

        string c = "Stage";
        string f = "ConnectStage";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("stageId", stageId.ToString());

        startPost(c, f, post);
    }

    public void GetPowerUpList()
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("GetPowerUpList fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.GetPowerListStart;

        string c = "Resource";
        string f = "GetPowerUpList";
        
        startPost(c, f);
    }

    public void GetUserPowerUpList(string session)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("GetUserPowerUpList fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.GetUserPowerUpListStart;

        string c = "Player";
        string f = "GetUserPowerUpList";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);

        startPost(c, f, post); 
    }

    public void BuyStoreProduct(string session, long storeProductId, string receipt)
    {
        if (nbHttp.state != nbHttpState.Wait)
        {
            Debug.Log("BuyStoreProduct fail, state : " + nbHttp.state.ToString());
            return;
        }

        state = nbHttpState.BuyStoreProductStart;

        string c = "Store";
        string f = "BuyStoreProduct";

        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("session", session);
        post.Add("storeProductId", storeProductId.ToString());
        post.Add("receipt", receipt);

        startPost(c, f, post);
    }




    // parse

    private void parse(JsonData data)
    {
        switch (state)
        {
            case nbHttpState.Wait:
                break;
            case nbHttpState.CreateUserSessionStart:
                {
                    state = nbHttpState.CreateUserSessionSuccess;
                    nb_GlobalData.g_global.userSession =
                        JsonConvert.DeserializeObject<UserSession>(data["userSession"].ToJson());
                }
                break;
            case nbHttpState.CreateUserSessionSuccess:
                break;
            case nbHttpState.CreateUserAccountStart:
                {
                    state = nbHttpState.CreateUserAccountSuccess;
                    nb_GlobalData.g_global.userAccount =
                        JsonConvert.DeserializeObject<UserAccount>(data["userAccount"].ToJson());
                }
                break;
            case nbHttpState.CreateUserAccountSuccess:
                break;
            case nbHttpState.ConnectUserAccountStart:
                {
                    state = nbHttpState.ConnectUserAccountSuccess;
                    nb_GlobalData.g_global.userAccount =
                        JsonConvert.DeserializeObject<UserAccount>(data["userAccount"].ToJson());
                }
                break;
            case nbHttpState.ConnectUserAccountSuccess:
                break;
            case nbHttpState.DisconnectPreviousSessionStart:
                {
                    state = nbHttpState.DisconnectPreviousSessionSuccess;
                }
                break;
            case nbHttpState.ConnectSocialStart:
                {
                    state = nbHttpState.ConnectSocialSuccess;
                    nb_GlobalData.g_global.userAccount =
                        JsonConvert.DeserializeObject<UserAccount>(data["userAccount"].ToJson());
                }
                break;
            case nbHttpState.BindSocialStart:
                {
                    state = nbHttpState.BindSocialSuccess;
                }
                break;

        //stage
            case nbHttpState.GetStageListStart:
                {
                    state = nbHttpState.GetStageListSuccess;
                    nb_GlobalData.g_global.stageList.Clear();

                    int count = data["stageList"].Count;
                    for (int i = 0; i < count; ++i)
                    {
                        nb_GlobalData.g_global.stageList.Add(
                            JsonConvert.DeserializeObject<Stage>(
                            data["stageList"][i].ToJson())
                            );
                    }
                }
                break;
            case nbHttpState.GetStageListSuccess:
                break;
            case nbHttpState.ConnectStageStart:
                {
                    state = nbHttpState.ConnectStageSuccess;
                    nb_GlobalData.g_global.gl_playerSessionId = data["playerSessionId"].ToString();
                    nb_GlobalData.g_global.gl_ipAddress = data["ipAddress"].ToString();
                    nb_GlobalData.g_global.gl_port = (int)data["port"];
                }
                break;
            case nbHttpState.ConnectStageSuccess:
                break;

        //item, store
            case nbHttpState.GetUserPowerUpListStart:
                {
                    state = nbHttpState.GetUserPowerUpListSuccess;
                    nb_GlobalData.g_global.userPowerUpList.Clear();

                    int count = data["userPowerUpList"].Count;
                    for (int i = 0; i < count; ++i)
                    {
                        nb_GlobalData.g_global.userPowerUpList.Add(
                            JsonConvert.DeserializeObject<UserPowerUp>(
                            data["userPowerUpList"][i].ToJson())
                            );
                    }
                }
                break;
            case nbHttpState.GetUserPowerUpListSuccess:
                break;
            case nbHttpState.GetPowerListStart:
                break;
            case nbHttpState.GetPowerListSuccess:
                break;
            case nbHttpState.BuyStoreProductStart:
                {
                    state = nbHttpState.BuyStoreProductSuccess;

                    Command cmd = JsonConvert.DeserializeObject<Command>(
                        data["command"].ToJson());

                    int count = cmd.SubCommandList.Count;
                    Debug.Log("subCommand count : " + count.ToString());

                    //for (int i = 0; i < count; ++i)
                    //{
                    //    nb_GlobalData.g_global.UserPowerUpList.Find
                    //    cmd.SubCommandList[i].AssetId
                    //}

                    //    Debug.Log("subCommand : " + cmd.SubCommandList[0].AssetId.ToString());


                    //nb_GlobalData.g_global.userAccount =
                    //    JsonConvert.DeserializeObject<UserAccount>(data["userAccount"].ToJson());

                }
                break;
            case nbHttpState.BuyStoreProductSuccess:
                break;
        }
    }

    private void wwwFail(JsonData data)
    {
        switch (state)
        {
            case nbHttpState.Wait:
                break;
            case nbHttpState.CreateUserSessionStart:
                break;
            case nbHttpState.CreateUserSessionSuccess:
                break;
            case nbHttpState.CreateUserAccountStart:
                break;
            case nbHttpState.ConnectUserAccountStart:
                {
                    state = nbHttpState.ConnectUserAccountFail;
                }
                break;
            case nbHttpState.ConnectSocialStart:
                {
                    //소셜 로그인 실패 - 등록 안된 소셜키
                    state = nbHttpState.ConnectSocialFail;
                }
                break;
            case nbHttpState.ConnectStageStart:
                {
                    //스테이지 접속 실패
                    state = nbHttpState.ConnectStageFail;
                }
                break;
        }
    }
}
