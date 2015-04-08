using UnityEngine;
using System.Collections;

public class Script_Enemy_Ranged : MonoBehaviour 
{

	//static access vars
	Mamaro_Manager mamaroM;	// access to player position, etc.

	// inspector assigned vars
	public float moveSpeed;
	public float sprintSpeed;
	public float engagementRadius;
	public float keptDistance;
	public int lowHealthThreshold;

	// private vars
	public int health;
	public EnemyState state = EnemyState.Standby;
	public bool alert = false;
	public float distFrom;

	// test

	// Use this for initialization
	void Start () 
	{
		mamaroM = Mamaro_Manager.inst;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		distFrom = Vector3.Distance(mamaroM.transform.position, this.transform.position);

		switch (state) 
		{
		// player has not yet been within engagment range
		case EnemyState.Standby:


			// check if mamaro is within engagment range
			if(!alert && distFrom < engagementRadius)
				state = EnemyState.Offensive;

			// wait for mamaro to stop malfunctioning
			if(alert && !mamaroM.isMalfunctioning)
			{
				// check which state to revert to
				if(distFrom < engagementRadius)
				{
					// check health level
					if(health <= lowHealthThreshold)
						state = EnemyState.Defensive;
					else
						state = EnemyState.Offensive;
				}
				else
					state = EnemyState.Stalking;
			}


			break;

		// player is within engagment range. Enemy health is sufficient
		case EnemyState.Offensive:

			break;

		// player is within engagement range. Enemy is low on health
		case EnemyState.Defensive:

			break;

		case EnemyState.Stalking:

			break;

		// error catch
		default:
			Debug.LogError("Switch statement fell through. Please revise.");
			break;
		}
	}

	// draw inspector gizoms
	void OnDrawGizmos() 
	{
		// engagement radius
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, engagementRadius);

		// only show if in range
		if(distFrom < engagementRadius)
		{
			// keptDistance radius
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(mamaroM.transform.position, keptDistance);
		}

	}

	// returns a valid new position within the given range
	private Vector3 GetNewPos(float range)
	{
		bool foundPath = false;
		Vector3 tempV;
		float breakTimer = 0.0f;

		// only return a clear path position
		while(!foundPath)
		{
			// break for constant loop defence
			breakTimer += Time.deltaTime;
			if(breakTimer > 2.0f)
			{
				Debug.LogError("While loop was stuck in constant loop. Check you logic!");
				return this.transform.position;
			}

			// pick a pos within the specefied range
			tempV = this.transform.position + Random.insideUnitSphere * (engagementRadius / 2);

			// within engagement range but out of keptDistance range
			float tempDist = Vector3.Distance(mamaroM.transform.position, tempV);
			if(tempDist > keptDistance && tempDist < engagementRadius)
			{
				// check if point A and B have an obstacle in the way and less than desired angle
				RaycastHit hit;
				if(!Physics.Linecast(this.transform.position, tempV, out hit))
				{
					foundPath = true;
					return new Vector3(tempV.x, transform.position.y, tempV.z);
				}
			}
		}

		return this.transform.position;
	}

}
