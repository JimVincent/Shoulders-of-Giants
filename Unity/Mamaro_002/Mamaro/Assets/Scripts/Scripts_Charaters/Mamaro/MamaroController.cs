using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class MamaroController : MonoBehaviour {

	public enum ControllerType{Keyboard, Contoller};

	public ControllerType InputDevice;
	//###########################################
	//Required For X Input
	bool playerIndexSet = false;
	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;
	//############################################

	MamaroMovement move;

	void Start()
	{
		move = MamaroMovement.inst;
	}

	// Update is called once per frame
	void Update () 
	{
		switch (InputDevice)
		{
		case ControllerType.Contoller:
			XboxController();
			break;
		case ControllerType.Keyboard:
			KeyboardController();
			break;
		}
	}

	void XboxController()
	{
		//#############################################################
		// Find a PlayerIndex, for a single player game
		// From XInput
		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}
		state = GamePad.GetState(playerIndex);
		//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
		
		
		//LeftStickMovement
		move.moveDir = transform.right * state.ThumbSticks.Left.X / 2;
		move.moveDir += transform.forward * state.ThumbSticks.Left.Y;
		
		//RightStickMovement
		move.rotateEuler.y = state.ThumbSticks.Right.X;
		
		//LeftStick Clicked
		//if moveing and not run for to long
		if (move.moveDir.magnitude > 0 && move.timerRun < move.runMaxTime)
		{
			//LeftStick pressed and was not previously pressed
			if (state.Buttons.LeftStick == ButtonState.Pressed && prevState.Buttons.LeftStick == ButtonState.Released)
			{
				move.isRun = !move.isRun;
			}
		}
		else
		{
			move.isRun = false;
		}
		
		//Dodge Controls
		if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released && move.moveDir.magnitude > 0)
		{
			Vector3 tempDir;
			tempDir = move.moveDir;
			tempDir = tempDir.normalized;
			move.Dodge(tempDir);
		}
		
		
		if (state.DPad.Left == ButtonState.Pressed)
		{
			Ability_Manager.inst.SelectSocketLeft();
		}
		if (state.DPad.Right == ButtonState.Pressed)
		{
			Ability_Manager.inst.SelectSocketRight();
		}
		//###################################
		//Set Previous COntroller State
		prevState = state;
		//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
	}

	void KeyboardController()
	{
		//LeftStickMovement
		move.moveDir = transform.right * Input.GetAxis("Horizontal") / 2;
		move.moveDir += transform.forward * Input.GetAxis("Vertical");
		
		//RightStickMovement
		move.rotateEuler.y = Input.GetAxis ("Mouse X");
		
		//LeftStick Clicked
		//if moveing and not run for to long
		if (move.moveDir.magnitude > 0 && move.timerRun < move.runMaxTime)
		{
			//LeftStick pressed and was not previously pressed
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				move.isRun = !move.isRun;
			}
		}
		else
		{
			move.isRun = false;
		}
		
		//Dodge Controls
		if (Input.GetKeyDown(KeyCode.Space) && move.moveDir.magnitude > 0)
		{
			Vector3 tempDir;
			tempDir = move.moveDir;
			tempDir = tempDir.normalized;
			move.Dodge(tempDir);
		}
		
		
//		if (Input.GetKeyDown(KeyCode.LeftArrow))
//		{
//			Ability_Manager_Chris_try.inst.SelectSocketLeft();
//		}
//		if (Input.GetKeyDown(KeyCode.RightArrow))
//		{
//			Ability_Manager_Chris_try.inst.SelectSocketRight();
//		}
	}
}
