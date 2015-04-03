using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability_Manager : MonoBehaviour 
{
	// inspector assigned vars
	public AbilitySocket[] sockets = new AbilitySocket[4];
	public GameObject controlPanel_Parent;
	public Text spareCoreDisplayText;

	// private vars
	private int spareCores = 0;
	
	// Use this for initialization
	void Start()
	{
		// set all cores to deactive
		SetSockets(0,0,0,0,0);

		// set each ability sockets disabled and enabled pos
		for(int i = 0; i < sockets.Length; ++i)
		{
			sockets[i].disabledPos = sockets[i].socketImage.transform.localPosition;
		}
	}

	void Update()
	{
		// update spare core text
		UpdateSparetext();
	}


	// sets each socket to the desired amount
	public void SetSockets(int melee, int speed, int ranged, int shield, int spare)
	{
		sockets[0].SetActiveCores(melee);
		sockets[1].SetActiveCores(speed);
		sockets[2].SetActiveCores(ranged);
		sockets[3].SetActiveCores(shield);
		spareCores = spare;
	}

	// keeps spare core text updated
	private void UpdateSparetext()
	{
		if(spareCores < 10)
			spareCoreDisplayText.text = "0" + spareCores.ToString();
		else
			spareCoreDisplayText.text = spareCores.ToString();
	}
}
