using UnityEngine;
using System.Collections;

public class TEST_Cam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.T))
		{
			Cam_Manager.inst.LerpTo(CamPos.Malfunction);
		}

		if (Input.GetKeyDown (KeyCode.Y))
			Cam_Manager.inst.LerpTo(CamPos.Original);
	}
}
