﻿using UnityEngine;
using System.Collections;

public class Momentum : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		speed = Random.Range (0.01f, 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody>().AddForce((-transform.up) * speed, ForceMode.Force);
	}
}
