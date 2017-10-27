using UnityEngine;
using System.Collections;

public class UI_Cell : MonoBehaviour {

    public enum CellIndex
    {
        Cell_00 = 0,
        Cell_01,
        Cell_02,
        Cell_03,
        Cell_04,

        Cell_05,
        Cell_06,
        Cell_07,
        Cell_08,
        Cell_09,

        Cell_10,
        Cell_11,
        Cell_12,
        Cell_13,
        Cell_14,

        Cell_15,
        Cell_16,
        Cell_17,
        Cell_18,
        Cell_19,

        Cell_20,
        Cell_21,
        Cell_22,
        Cell_23,
        Cell_24,
    }

    public CellIndex cellIndex;

    private GameObject playScene;

    

    // Use this for initialization
    void Start()
    {
        playScene = GameObject.Find("PlayScene") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        playScene.GetComponent<PlayScene>().processDaub((int)cellIndex);

    }
}
