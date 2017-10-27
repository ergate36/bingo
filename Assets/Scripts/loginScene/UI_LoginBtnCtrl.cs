using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class UI_LoginBtnCtrl : MonoBehaviour {

    Transform obj_InputID;
    private GameObject popup;
    private Transform popupInputNick;
    private Transform popupNickcheck;
    void Start()
    {
        obj_InputID = transform.parent.Find("Input_ID");

    }

    void OnClick()
    {
        popup = GameObject.Find("loginSceneUI/Camera/Anchor");
        popupInputNick = popup.transform.Find("Panel/popup_nick");

        popupNickcheck = popup.transform.Find("popup_nickCheck");

        //AndroidManager.Instance.callLogin();
        string nickname = obj_InputID.GetComponent<UIInput>().value;
        //GlobalData.g_global.myInfo.nickName = nickname;
        //nickname = "가나다";
        GameObject loginObj = GameObject.Find("LoginScene");
        if (IsVaildStr(nickname))
        {
            loginObj.GetComponent<LoginScene>().loginStart(nickname);
        }
        else
        {
            popupInputNick.gameObject.SetActive(false);
            popupNickcheck.gameObject.SetActive(true);
        }
    }
    
    public bool IsVaildStr(string strText)
    {
        if(strText ==""){
            return false;
        }
        string Pattern = @"^[a-zA-Z0-9가-힣]*$";
        return Regex.IsMatch(strText, Pattern);
    }


    public static bool CheckEnglishNumber(string letter)
    {
        char[] charArr = letter.ToCharArray();

        bool isCheck = true;

        for (int i = 0; i < charArr.Length; i++ )
        {
            if ((charArr[i] < '0' || charArr[i] > '9') && (charArr[i] < 'a' || charArr[i] > 'z') && (charArr[i] < 'A' || charArr[i] > 'Z'))
            {
                isCheck = false;
                return isCheck;
            }
        }
        return isCheck;
        
        /*
        bool IsCheck = true;
        Regex engRegex = new Regex(@"[a-zA-Z]");
        Boolean ismatch = engRegex.IsMatch(letter);
        Regex numRegex = new Regex(@"[0-9]");
        Boolean ismatchNum = numRegex.IsMatch(letter);

        Debug.Log("ENG : " + ismatch);
        Debug.Log("NUM : " + ismatchNum);

        if (!ismatch && !ismatchNum)
        {
            IsCheck = false;
        }

        return IsCheck;
        */
    }
}
