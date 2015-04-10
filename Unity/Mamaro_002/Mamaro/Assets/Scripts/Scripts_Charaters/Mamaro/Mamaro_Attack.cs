using UnityEngine;
using System.Collections;

public class Mamaro_Attack : MonoBehaviour 
{
	// static instance
	public static Mamaro_Attack inst;

	//inspector assigned vars
	public int maxAttack, maxRangedCoolDown;
	[Range(15.0f, 30.0f)]
	public float chargeRate = 10.0f;
	public Collider fistCollider;

	// private vars
	public bool isAttacking = false;
	public float punchCharge = 0.0f, rangedCharge = 0.0f;


	//Animation Variables
	Animator anim;


	// Use this for initialization
	void Awake() 
	{
		// assign static instance
		if (inst == null)
			inst = this;

		anim = GetComponentInChildren<Animator>();
		fistCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO apply gate for if pause here
		PunchAttack ();
		RangedAttack();
		ChargePunch();
		ChargeRanged();

		//check to turn off fist collider
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("CHR_Mamaro_Anim_Idle"))
		{
			fistCollider.enabled = false;
		}

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

		if (Input.GetKeyDown(KeyCode.Q))
		{
			//Set Animation to start
			anim.SetBool("Bool_RangedCharge", true);
		}
		else if (Input.GetKeyUp(KeyCode.Q))
		{
			//Set Animation to start
			anim.SetTrigger("Trig_RangedAttack");
			anim.SetBool("Bool_RangedCharge", false);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			//Set Animation to start
			anim.SetBool("Bool_MeeleCharge", true);
		}
		else if (Input.GetKeyUp(KeyCode.E))
		{
			//Set Animation to start
			anim.SetTrigger("Trig_MeeleAttack");
			anim.SetBool("Bool_MeeleCharge", false);

			// turn on fist collider
			fistCollider.enabled = true;
		}


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
			if(rangedCharge > 0.0f)
			{
				rangedCharge -= Time.deltaTime * (chargeRate / 2);
				if(rangedCharge < 0.0f)
					rangedCharge = 0.0f;
			}
		}
	}
}
