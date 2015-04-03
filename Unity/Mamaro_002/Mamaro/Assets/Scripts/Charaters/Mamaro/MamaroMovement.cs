using UnityEngine;
using System.Collections;

public class MamaroMovement : MonoBehaviour {

	public Vector3 moveDir;
	public Vector3 rotateEuler;
	public float walkSpeed;
	public float runSpeed;
	public float turnSpeed;
	public float pitchSpeed;

	public float runMaxTime;
	public float runCooldownRate;
	public float timerRun;

	public bool isRun;


	public static MamaroMovement inst;

	void Awake()
	{
		if (inst == null)
		{
			inst = this;
		}
		isRun = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Set Speeds of all
		if (isRun)
		{
			if (timerRun < runMaxTime)
			{
				timerRun += Time.deltaTime;
			}
			moveDir *= runSpeed;
			rotateEuler *= turnSpeed / (runSpeed/walkSpeed);
		}
		else
		{
			if (timerRun > 0)
			{
				timerRun -= runCooldownRate * Time.deltaTime;
			}

			moveDir *= walkSpeed;
			rotateEuler *= turnSpeed;
		}

		//Remove any Roll
		rotateEuler.z = 0;
		//Set Rotation
		transform.Rotate (rotateEuler);

		//Set y velocity to previous so that gravity takes effect
		moveDir.y = GetComponent<Rigidbody>().velocity.y - 2;
		//Set Velocity
		GetComponent<Rigidbody>().velocity = moveDir;
	}
}
