using UnityEngine;
using System.Collections;

public class playScene_Sheet : MonoBehaviour
{

    public enum SheetIndex
    {
        Sheet_00 = 0,
        Sheet_01,
        Sheet_02,
        Sheet_03,
    }

    public SheetIndex sheetIndex;

    private GameObject playBlitzScene;

    

    // Use this for initialization
    void Start()
    {
        playBlitzScene = GameObject.Find("PlayBlitzScene") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {

    }
}
