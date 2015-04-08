using UnityEngine;
using System.Collections;

public class Lucy_Manager : MonoBehaviour {

	public enum LucyState{Idle, Repair, Scared, Frightened, Tapping};
	public static Lucy_Manager inst;

	LucyState state;
	Mamaro_Manager mamaro;
	GameObject lucyTapping;

	public int fear;
	public int fearMax;
	public int fearScaredLevel;
	public int fearFrightenedLevel;

	void Awake()
	{
		if (inst == null)
		{
			inst = this;
		}
	}
	// Use this for initialization
	void Start () 
	{
		mamaro = Mamaro_Manager.inst;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mamaro.isMalfunctioning)
		{
			ChangeState(LucyState.Tapping);
		}
		switch (state)
		{
		case LucyState.Idle:
			Idle ();
			break;
		case LucyState.Repair:
			Repair();
			break;
		case LucyState.Scared:
			Scared();
			break;
		case LucyState.Frightened:
			Frightened ();
			break;
		case LucyState.Tapping:
			Tapping ();
			break;
		}
	}

	public void OnChangeFear(FearType fearType)
	{
		fear += fearType;

		if (fear > fearFrightenedLevel)
		{
			ChangeState(LucyState.Frightened);
		}
		else if(fear > fearScaredLevel)
		{
			ChangeState(LucyState.Scared);
		}
		else 
		{
			ChangeState(LucyState.Idle);
		}
	}


	void Idle()
	{
		if (fear > fearScaredLevel)
		{
			ChangeState(LucyState.Scared);
		}
		else if (mamaro.health < mamaro.maxHealth)
		{
			ChangeState(LucyState.Repair);
		}
	}

	void Repair()
	{
		if (fear > fearScaredLevel)
		{
			ChangeState(LucyState.Scared);
		}
		else if (mamaro.health == mamaro.maxHealth)
		{
			ChangeState(LucyState.Idle);
		}
	}

	void Scared()
	{
		if (fear > fearFrightenedLevel)
		{
			ChangeState(LucyState.Frightened);
		}
		else if (fear < fearScaredLevel)
		{
			ChangeState(LucyState.Idle);
		}
	}

	void Frightened()
	{
		if (fear < fearFrightenedLevel)
		{
			ChangeState(LucyState.Scared);
		}
	}

	void Tapping()
	{
		if (!mamaro.isMalfunctioning)
		{
			ChangeState(LucyState.Idle);
		}
	}

	void ChangeState(LucyState ls)
	{
		switch (ls)
		{
		case LucyState.Idle:
			break;
		case LucyState.Repair:;
			break;
		case LucyState.Scared:
			break;
		case LucyState.Frightened:
			break;
		case LucyState.Tapping:
			break;
		}
		state = ls;
	}
}


































