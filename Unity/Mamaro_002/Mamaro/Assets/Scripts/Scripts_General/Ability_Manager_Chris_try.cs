using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability_Manager_Chris_try : MonoBehaviour 
{
	public static Ability_Manager_Chris_try inst;

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
	public int selectedSocket = 0;
	public int previousSocket = 0;

	// vars for rotation
	public float currentAngle;
	public float lerpInc;
	public bool isRotating;
	public string rotDir;

	void Awake()
	{
		if (inst == null)
		{
			inst = this;
		}
	}

	// Use this for initialization
	void Start()
	{
		// set all cores to deactive
		//SetSockets(0,0,0,0,0);

		// set each ability sockets disabled and enabled pos
		/*
		for(int i = 0; i < sockets.Length; ++i)
		{
			// based off melee socket (in selected position 'up')
			sockets[i].disabledPos = sockets[0].socketImage.transform.localPosition;
			Vector3 temp = sockets[0].socketImage.transform.localPosition;
			sockets[i].enabledPos = new Vector3(temp.x, temp.y + controlPanel_Parent.rectTransform.rect.height, temp.z);
		}
		*/
	}

	void Update()
	{
		// update spare core text
		//UpdateSparetext();

		// apply lerp rotation
		if((int)currentAngle == (int)GetFromToAngles().y)
		{
			lerpInc = 0.0f;
			isRotating = false;
		}
		else
		{
			currentAngle = Mathf.LerpAngle(GetFromToAngles().x, GetFromToAngles().y, lerpInc);
			isRotating = true;
			controlPanel_Parent.transform.rotation = Quaternion.Euler(controlPanel_Parent.transform.rotation.x, controlPanel_Parent.transform.rotation.y, currentAngle);
			LerpLin();
		}



		//TODO add in gate for if pause here
		// allow user input control for testing
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

	public void SelectSocketLeft()
	{
		// allow change of mind during rotation
		if(isRotating)
		{
			// only allow a change of direction during rotation
			if(rotDir == "Right")
			{
				previousSocket = selectedSocket;
				selectedSocket -= 1;
				if (selectedSocket < 0)
				{
					selectedSocket = 3;
				}
				lerpInc = 1 - lerpInc;
				rotDir = "Left";
			}
		}
		else
		{
			previousSocket = selectedSocket;
			selectedSocket -= 1;
			if (selectedSocket < 0)
			{
				selectedSocket = 3;
			}
			lerpInc = 0;
			rotDir = "Left";
		}
	}

	public void SelectSocketRight()
	{
		// allow change of mind during rotation
		if(isRotating)
		{
			// only allow a change of direction during rotation
			if(rotDir == "Left")
			{
				previousSocket = selectedSocket;
				selectedSocket += 1;
				if (selectedSocket > 3)
				{
					selectedSocket = 0;
				}
				
				lerpInc = 1 - lerpInc;
				rotDir = "Right";
			}
		}
		else
		{
			previousSocket = selectedSocket;
			selectedSocket += 1;
			if (selectedSocket > 3)
			{
				selectedSocket = 0;
			}
			
			lerpInc = 0;
			rotDir = "Right";
		}
		
		
	}

	// allows user input to control rotation and core adding/removing cores
	private void InputControl()
	{
		currentAngle = controlPanel_Parent.transform.rotation.eulerAngles.z;

		// input for which way to rotate
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			SelectSocketLeft();
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			SelectSocketRight();
		}
	}

	Vector2 GetFromToAngles()
	{
		Vector2 temp = new Vector2(previousSocket * 90, selectedSocket * 90);
		return temp; 
	}


	// a linear interpolation
	private void LerpLin()
	{
		lerpInc += (Time.deltaTime * rotationSpeed);
	}
}
