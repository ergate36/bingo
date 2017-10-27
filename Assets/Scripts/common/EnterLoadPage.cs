using UnityEngine;
using System.Collections;
public class EnterLoadPage : MonoBehaviour
{
    AsyncOperation async;
    public Camera cam;

    public IEnumerator StartLoad(string strSceneName)
    {

        AsyncOperation async = Application.LoadLevelAsync(strSceneName);

        DontDestroyOnLoad(this.gameObject);

        while (async.isDone ==false)
        {
            yield return null;
        }

        Destroy(this.gameObject);
    }

    void Start()
    {
        StartCoroutine("StartLoad", "MainScene");
    }


    //로딩 페이지에서 연속으로 애니메이션 만들때 Update 함수 내에서 만들면 됩니다.
    void Update()
    {
        
    }
}
