using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour 
{
	// static instance
	public static Game_Manager inst;

	// static access
	private Mamaro_Attack mAttack;
	private MamaroMovement mMove;
	private Cam_Manager cam;
	private Lucy_Manager Lucy;
	private Ability_Manager abMan;

	// inspector assigned vars
	public float malfunctionLength;


	void Awake()
	{
		// assign static instance
		if (inst = null)
			inst = this;
	}

	void Start()
	{
		// link static vars
		mAttack = Mamaro_Attack.inst;
		mMove = MamaroMovement.inst;
		cam = Cam_Manager.inst;
		Lucy = Lucy_Manager.inst;
		abMan = Ability_Manager.inst;
	}


}
