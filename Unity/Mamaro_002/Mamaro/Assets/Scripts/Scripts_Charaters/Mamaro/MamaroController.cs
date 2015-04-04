using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class MamaroController : MonoBehaviour {
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
		if (state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
		{
			Vector3 tempDir;

			tempDir = transform.right * state.ThumbSticks.Left.X;
			tempDir += transform.forward * state.ThumbSticks.Left.Y;

			tempDir = tempDir.normalized;

			move.Dodge(tempDir);

		}





		//###################################
		//Set Previous COntroller State
		prevState = state;
		//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
	}
}
