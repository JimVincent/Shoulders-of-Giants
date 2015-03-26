using UnityEngine;
using System.Collections;

public class AnimationKill : MonoBehaviour {
	public Animator animation;
	// Use this for initialization

	void Awake(){
		Debug.Log("awakeshit");
	}
	void Start () {
		Debug.Log ("shit");
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Death(){
		}
}
