using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability_Manager : MonoBehaviour 
{
	// static access instance
	public static Ability_Manager inst;
	
	// inspector assigned vars
	public AbilitySocket[] sockets = new AbilitySocket[4];
	public Image controlPanel_Parent;
	public Text spareCoreDisplayText;
	
	// public vars
	public float rotationSpeed = 2.0f;
	public float sliderSpeed;
	public float sliderHangTime = 2.5f;
	
	// private vars
	private int spareCores = 0;
	private int selectedSocket = 0;
	private int previousSocket = 0;
	
	// vars for rotation
	private float currentAngle;
	private float lerpInc;
	private bool isRotating;
	private string rotDir;

	// vars for socket slider
	private bool shownOnce = true;
	private bool showSocket = false;
	private float sHangTimer = 0.0f;
	
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
		SetSockets(0,0,0,0,5);
		sockets[0].oppositeSocket = sockets[2];
		sockets[1].oppositeSocket = sockets[3];
		sockets[2].oppositeSocket = sockets[0];
		sockets[3].oppositeSocket = sockets[1];
	}
	
	void Update()
	{
		// update spare core text
		UpdateSparetext();

		//TODO add in gate for if pause here
		// allow user input control
		InputControl();

		// in position
		if((int)currentAngle == GetSocketAngle(selectedSocket))
		{
			lerpInc = 0.0f;
			isRotating = false;

			// extrude slider for x seconds
			if(showSocket)
			{
				// slide out
				SlideSocket(sockets[selectedSocket].socketImage, sockets[selectedSocket].disabledPos, sockets[selectedSocket].enabledPos);
				
				// slide shut once time limit reached or if rotating
				sHangTimer += Time.deltaTime;
				if(sHangTimer > sliderHangTime)
				{
					SlideSocket(sockets[selectedSocket].socketImage, sockets[selectedSocket].enabledPos, sockets[selectedSocket].disabledPos);
					sHangTimer = 0.0f;
					showSocket = false;
					shownOnce = true;
				}
			}
		}
		else
		{
			currentAngle = Mathf.LerpAngle(GetSocketAngle(previousSocket), GetSocketAngle(selectedSocket), lerpInc);
			isRotating = true;
			controlPanel_Parent.transform.rotation = Quaternion.Euler(controlPanel_Parent.transform.rotation.x, controlPanel_Parent.transform.rotation.y, currentAngle);
			LerpLin();

			// close slider before changing selected socket
			if(showSocket)
			{
				SlideSocket(sockets[previousSocket].socketImage, sockets[previousSocket].enabledPos, sockets[previousSocket].disabledPos);
				sHangTimer = 0.0f;
				//showSocket = false;
				shownOnce = false;
			}
		}
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
		// add '0' in front if less than 10
		if(spareCores < 10)
			spareCoreDisplayText.text = "0" + spareCores.ToString();
		else
			spareCoreDisplayText.text = spareCores.ToString();
	}
	
	//Public Function to Allow Controller Input
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
	
	///Public Function to Select the next Socket to the left
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
	
	/// allows user input to control rotation and core adding/removing cores
	private void InputControl()
	{
		currentAngle = controlPanel_Parent.transform.rotation.eulerAngles.z;
		
		// input for which way to rotate
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			showSocket = true;
			SelectSocketLeft();
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			showSocket = true;
			SelectSocketRight();
		}

		///////////////////////////////////////////////////////////////////

		// input to add and remove cores
		if(!isRotating)
		{
			// add cores to selected
			if(Input.GetKeyDown(KeyCode.UpArrow) && showSocket)
			{
				// check if already full
				if(sockets[selectedSocket].GetCoreCount() < 4)
				{
					// add from spares
					if(spareCores > 0)
					{
						sockets[selectedSocket].AddCore();
						spareCores--;
					}
					else if(sockets[selectedSocket].oppositeSocket.GetCoreCount() > 0)
					{
						// add from opposite
						sockets[selectedSocket].oppositeSocket.RemoveCore();
						sockets[selectedSocket].AddCore();
					}
				}
				sHangTimer = 0.0f;
			}

			if(Input.GetKeyDown(KeyCode.DownArrow) && showSocket)
			{
				// check for empty
				if(sockets[selectedSocket].GetCoreCount() > 0)
				{
					// remove core and reset sHangTimer
					sockets[selectedSocket].RemoveCore();
					spareCores++;
				}
				sHangTimer = 0.0f;
			}

			// show only once per rotation or up input
			if(!shownOnce || Input.GetKeyDown(KeyCode.UpArrow))
			{
				showSocket = true;
				shownOnce = true;
			}
		}
	}
	
	///Returns Angle for Given Socket
	int GetSocketAngle(int socketNum)
	{
		return socketNum * 90; 
	}

	// slides image from a to b with lerp
	private void SlideSocket(Image socket, Vector3 from, Vector3 to)
	{
		// TODO apply a lerp (from, to)
		socket.transform.localPosition = to;
	}

	/// a linear interpolation
	private void LerpLin()
	{
		lerpInc += (Time.deltaTime * rotationSpeed);
	}
}
