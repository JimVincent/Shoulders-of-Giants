using UnityEngine;
using System.Collections;

public class Rotate3 : MonoBehaviour {
	public float target = 270.0F;
	public float speed = 45.0F;
	public bool isOpening;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
	if(isOpening){
		float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.z, target, speed * Time.deltaTime);
		transform.eulerAngles = new Vector3 (0, 0, angle);
	}
	}
}
	
