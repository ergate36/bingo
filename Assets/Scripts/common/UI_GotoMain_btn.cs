using UnityEngine;
using System.Collections;
public class UI_GotoMain_btn : MonoBehaviour {
    
    string sceneName = "";

    void OnClick()
    {
        //GlobalData.g_global.ResetInfo();
        Resources.UnloadUnusedAssets();
        System.GC.Collect(); 

        // 연결 종료
        //StopAllCoroutines();
        //Application.LoadLevel("loadingScene");
        Application.LoadLevel("MainScene");

    }
}
