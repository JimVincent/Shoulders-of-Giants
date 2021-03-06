﻿using UnityEngine;
using System.Collections;

public class FusionCore : MonoBehaviour {

	public GameObject ui;

	public RectTransform buttonX;
	public RectTransform buttonY;

	public float intSize = 0.8f;
	public float pickupTime;
	public float previousTime;
	public float timerPickup;

	// Use this for initialization
	void Start () 
	{
		ui.SetActive (false);
		timerPickup = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Reset if Button Released for a single frame.
		if (timerPickup > 0 && timerPickup == previousTime)
		{
			timerPickup = 0;
			buttonX.localScale = Vector3.one * intSize;
			buttonY.localScale = Vector3.one * intSize;
		}

		previousTime = timerPickup;
	}

	public void CollectCore()
	{
		timerPickup += Time.deltaTime / pickupTime;
		buttonY.localScale = Vector3.one * (intSize + timerPickup);

		if (timerPickup > 1f)
		{
			///TODO Increase MamaroStats
			/// 
			/// 
			RemoveObject();

		}
	}

	public void DestroyCore()
	{
		timerPickup += Time.deltaTime / pickupTime;
		buttonX.localScale = Vector3.one * (intSize + timerPickup);

		if (timerPickup > 1f)
		{
			///TODO Decrease Lucys Fear
			/// 
			/// 
			RemoveObject();
			
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "Player")
		{
			MamaroController.inst.fusionCores.Add(this); 
			ui.SetActive(true);
		}

	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			MamaroController.inst.fusionCores.Remove(this); 
			if (MamaroController.inst.fusionCores.Count == 0)
			{
				ui.SetActive(false);
			}
		}
	}

	void RemoveObject()
	{
		MamaroController.inst.fusionCores.Remove(this); 
		buttonX.localScale = Vector3.one * intSize;
		buttonY.localScale = Vector3.one * intSize;
		if (MamaroController.inst.fusionCores.Count == 0)
		{
			ui.SetActive(false);
		}
		Destroy(gameObject);
	}
}
