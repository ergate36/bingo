using UnityEngine;
using System.Collections;

public class Reward_Confirm_btn : MonoBehaviour {

    public Reward_Ctrl rewardCtrl;

    void OnClick()
    {
        rewardCtrl.setRetry();
      //StartCoroutine(  transform.parent.GetComponent<Popup_Effector>().unactivePopup(0.1f) );
    }
}
