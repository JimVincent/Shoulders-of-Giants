using UnityEngine;
using System.Collections;

public class Character_Shoot : MonoBehaviour {
	public GameObject bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("4")) {
			 Instantiate(bullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 5), transform.rotation);
				}
	}
}
