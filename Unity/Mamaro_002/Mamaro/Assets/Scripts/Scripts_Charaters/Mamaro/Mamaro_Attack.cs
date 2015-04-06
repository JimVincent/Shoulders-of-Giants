using UnityEngine;
using System.Collections;

public class Mamaro_Attack : MonoBehaviour 
{
	//inspector assigned vars
	public int maxAttack, maxRangedCoolDown;
	[Range(15.0f, 30.0f)]
	public float chargeRate = 10.0f;

	// private vars
	public bool isAttacking = false;
	public float punchCharge = 0.0f, rangedCharge = 0.0f;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO apply gate for if pause here
		PunchAttack ();
		RangedAttack();
		ChargePunch();
		ChargeRanged();
	}

	// applies punch attack sequence
	private void PunchAttack()
	{

	}

	// applies ranged attack sequence
	private void RangedAttack()
	{
	
	}

	// adds punch charge from 0 to 100 in respects to time held
	private void ChargePunch()
	{
		// receive held input
		if(Input.GetKey(KeyCode.E))
		{
			if(punchCharge < 100.0f)
			{
				punchCharge += Time.deltaTime * chargeRate;

				// limit to max of 100
				if(punchCharge > 100.0f)
					punchCharge = 100.0f;
			}
		}
		else
		{
			//punchCharge = 0.0f;	// button let go looses charge ??????
			// or
			// reduce at half charge rate until empty
			if(punchCharge > 0.0f)
			{
				punchCharge -= Time.deltaTime * (chargeRate / 2); 

				// don't drop below 0.0f
				if(punchCharge < 0.0f)
					punchCharge = 0.0f;
			}
		}
	}

	// adds ranged charge from 0 to 100 in respects to time held
	private void ChargeRanged()
	{
		// receive held input
		if(Input.GetKey(KeyCode.Q))
		{
			if(rangedCharge < 100.0f)
			{
				rangedCharge += Time.deltaTime * chargeRate;
				
				// limit to max of 100
				if(rangedCharge > 100.0f)
					rangedCharge = 100.0f;
			}
		}
		else
		{
			//rangedCharge = 0.0f;	// button let go looses charge ??????
			// or
			// reduce at half charge rate until empty
			if(rangedCharge > 0.0f)
			{
				rangedCharge -= Time.deltaTime * (chargeRate / 2);
				if(rangedCharge < 0.0f)
					rangedCharge = 0.0f;
			}
		}
	}
}
