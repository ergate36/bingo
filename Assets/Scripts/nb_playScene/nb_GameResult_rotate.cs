using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nb_GameResult_rotate : MonoBehaviour 
{
    public float rot;

	void Update ()
    {
        this.transform.Rotate(Vector3.forward * Time.deltaTime * rot);
	}
}
