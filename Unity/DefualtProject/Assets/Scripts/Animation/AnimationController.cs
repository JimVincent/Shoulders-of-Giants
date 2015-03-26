using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	public Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Active(){
		anim.SetInteger("State",1);
	}

	public void DeActive(){
		anim.SetInteger("State",0);
	}
}
