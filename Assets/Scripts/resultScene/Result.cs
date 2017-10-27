using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Result : MonoBehaviour {

	private GameObject popup;
	private Transform popupup;
	private UILabel label;
    private int acquiredTicket;
    private int acquiredGold;

	struct sWinner
	{
		public string nickname;
		public int rank;
	}

	List<sWinner> rankList;
	List<sWinner> m_winnerList;
	private int m_winnerCount = 0;
	
	void Start()
	{
		
		popup = GameObject.Find("resultSceneUI/Camera/Anchor") as GameObject;
		popupup = popup.transform.Find("result_popup");
		popupup.gameObject.SetActive(true);
		m_winnerList = new List<sWinner>();
		m_winnerList.Clear();
		
		GameObject uiRoot = GameObject.Find("resultSceneUI/Camera/Anchor");
		
	}
	void Update()
	{
	}
	
	
}
