using UnityEngine;
using System.Collections;
public class reSelect_btn : MonoBehaviour {
	
	// Use this for initialization

    Reward_Ctrl rewardCtrl;

    void Awake()
    {
        rewardCtrl =  transform.parent.GetComponent<Reward_Ctrl>();
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnClick()
    {
        if (!rewardCtrl.bMustSelect )
        {

            //rewardCtrl.onGetReward(2);
            StartCoroutine(rewardCtrl.setMonsterToRetry());
        }
        
	}
	
}
