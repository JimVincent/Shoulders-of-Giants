using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioSource Ambience;
	public Transform VolumeSlider;
	public float volume;
	// Use this for initialization
	void Start () {
		VolumeSlider = GameObject.FindGameObjectWithTag ("VOLUMESLIDER").GetComponent<Transform>();
		Ambience = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		volume = VolumeSlider.localPosition.x;
		AudioListener.volume = volume;
		if (Input.GetKeyDown (KeyCode.F1)) {
				}
	}
}
