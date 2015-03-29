using UnityEngine;
using System.Collections;

public class Bullet_Collision : MonoBehaviour {
	public float speed = 2000f;
	// Use this for initialization
	void Awake () {
		transform.rigidbody.velocity = transform.forward * speed * Time.deltaTime;
		Destroy(this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "WALL") {
			Destroy(col.gameObject);
			Destroy(this.gameObject);
				}

		if (col.gameObject.tag == "PILLAR") {
			AnimationController colAnim = col.gameObject.GetComponent<AnimationController>();
			colAnim.active = true;
			colAnim.Active();
			Destroy(this.gameObject);
		}
	}
}
