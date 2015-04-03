using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Represents a single ability socket which contains
 * four cores that could be either active or deactive (represented by
 * two sprite images)
 * 
 * Can change the states of the cores via public add and remove functions
 * Contains getter func for active core count
 * 
*/

[System.Serializable]
public class AbilitySocket
{
	// inspector edited vars
	public string abilityName;
	public Image socketImage;
	public Image[] coresImages = new Image[4];
	public Sprite activeSprite, deactiveSprite;
	
	// inspector hidden vars
	[HideInInspector]
	public Vector3 disabledPos;
	[HideInInspector]
	public Vector3 enabledPos;

	// private vars
	private int activeCores = 0;

	// changes the next deactive core to active if valid
	public void AddCores()
	{
		// check capacity
		if(activeCores < coresImages.Length)
		{
			activeCores++;
			coresImages[activeCores-1].sprite = activeSprite;
			// TODO Play foley add sound
		}
		else
		{
			// already at max capacity
			// TODO Play foley full sound
		}
	}

	// changes the last active core to deactive if valid
	public void RemoveCores()
	{
		// check capacity
		if(activeCores > 0)
		{
			activeCores--;
			coresImages[activeCores-1].sprite = activeSprite;
			// TODO Play foley remove sound
		}
		else
		{
			// already empty
			// TODO Play foley empty sound
		}
	}

	// sets scoket to the desired amount of active cores
	public void SetActiveCores(int amount)
	{
		// make sure the amount is valid
		if (amount > coresImages.Length)
			Debug.LogError("Tried to assign " + amount + " cores to " + abilityName + " when max capacity is " + coresImages.Length);
		else
		{
			// set active and deactive sprites
			for(int i = 0; i < coresImages.Length; ++i)
			{
				if(i < amount)
					coresImages[i].sprite = activeSprite;
				else
					coresImages[i].sprite = deactiveSprite;
			}
			
			// ammend core count
			activeCores = amount;
		}
	}

	// returns the amount of active cores
	public int GetCoreCount()
	{
		return activeCores;
	}
}
