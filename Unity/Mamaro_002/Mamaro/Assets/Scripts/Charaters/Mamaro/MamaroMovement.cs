using UnityEngine;
using System.Collections;

public class MamaroMovement : MonoBehaviour {

	public Vector3 moveDir;
	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//moveDir.x = Input.GetAxis ("Horizontal") * speed;
		moveDir = transform.forward * Input.GetAxis ("Vertical") * speed;
		moveDir.y = GetComponent<Rigidbody>().velocity.y;


		transform.Rotate (new Vector3(0,Input.GetAxis ("Horizontal") * 2,0));

		GetComponent<Rigidbody>().velocity = moveDir;
	}
}
