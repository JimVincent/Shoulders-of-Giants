using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed = 15;
	private Vector3 moveDir;
	public bool Driving, isntMoving;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw ("Horizontal") == 0 || Input.GetAxisRaw ("Horizontal") == 0) {
			isntMoving = true;
				} else {
			isntMoving = false;
				}
				if (Driving) {
						moveDir = new Vector3 (0, 0, Input.GetAxisRaw ("Vertical")).normalized;
		} else if(!Driving){
						moveDir = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized;
				}
		}

	void FixedUpdate(){
		rigidbody.MovePosition (rigidbody.position + transform.TransformDirection (moveDir) * moveSpeed * Time.deltaTime);
	}
}
