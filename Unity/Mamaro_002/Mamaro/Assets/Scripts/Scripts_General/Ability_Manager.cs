using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability_Manager : MonoBehaviour 
{
	// inspector assigned vars
	public AbilitySocket[] sockets = new AbilitySocket[4];
	public Image controlPanel_Parent;
	public Text spareCoreDisplayText;

	// public vars
	public float rotationSpeed;
	public float sliderSpeed;
	public float showTime;

	// private vars
	private int spareCores = 0;
	private int selectedSocket = 0;

	// vars for rotation
	private float startAngle;
	private float currentAngle;
	private float targetAngle;
	private float lerpInc;
	private bool isRotating;
	private string rotDir;
	
	// Use this for initialization
	void Start()
	{
		// set all cores to deactive
		SetSockets(0,0,0,0,0);

		// set each ability sockets disabled and enabled pos
		for(int i = 0; i < sockets.Length; ++i)
		{
			// based off melee socket (in selected position 'up')
			sockets[i].disabledPos = sockets[0].socketImage.transform.localPosition;
			Vector3 temp = sockets[0].socketImage.transform.localPosition;
			sockets[i].enabledPos = new Vector3(temp.x, temp.y + controlPanel_Parent.rectTransform.rect.height, temp.z);
		}
	}

	void Update()
	{
		// update spare core text
		UpdateSparetext();

		//TODO add in gate for if pause here
		// allow user input control
		InputControl();

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

	// allows user input to control rotation and core adding/removing cores
	private void InputControl()
	{
		// vars for rotation
		currentAngle = controlPanel_Parent.transform.rotation.eulerAngles.z;

		// input for which way to rotate
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			// allow change of mind during rotation
			if(isRotating && rotDir == "Right")
			{
				targetAngle -= 90.0f;
			}
			else
				targetAngle = currentAngle - 90.0f;

			rotDir = "Left";
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			// allow change of mind during rotation
			if(isRotating && rotDir == "Left")
			{
				targetAngle += 90.0f;
			}
			else
				targetAngle = currentAngle + 90.0f;
			
			rotDir = "Right";
		}

		LerpLin();

		// apply lerp rotation
		controlPanel_Parent.transform.rotation = Quaternion.Euler(controlPanel_Parent.transform.rotation.eulerAngles.x, controlPanel_Parent.transform.rotation.eulerAngles.y, Mathf.LerpAngle(startAngle, targetAngle, rotationSpeed * lerpInc));
	}

	// a smoothStep interpolation
	private void LerpLin()
	{
		float t = lerpInc;
		t = t*t*t * (t * (6f*t - 15f) + 10f);
		lerpInc = t;
	}
}
