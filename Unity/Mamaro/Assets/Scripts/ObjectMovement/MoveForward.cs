﻿using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
	public int speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * speed);
		//transform.Translate(Vector3.up * Time.deltaTime, Space.World);
	}
}
