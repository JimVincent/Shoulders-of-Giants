using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	public Animator anim;
	public bool active;
	// Use this for initialization
	void Awake () {
		active = false; 
		anim = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Active(){
		anim.SetBool("Active",active);
	}

	public void DeActive(){
		anim.SetInteger("State",0);
	}
}
