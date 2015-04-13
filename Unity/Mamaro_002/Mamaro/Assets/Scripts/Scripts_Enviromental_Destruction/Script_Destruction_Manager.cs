using UnityEngine;
using System.Collections;

public class Script_Destruction_Manager : MonoBehaviour {
	public GameObject WallDestruction, buildDestrction;
	public GameObject mamaro;
	public BoxCollider mamaroFistCol;
	public Script_Destruction_Chunks[] chunklets = new Script_Destruction_Chunks[0];
	public int punchCount;
	// Use this for initialization
	void Awake () {
		mamaro = GameObject.FindGameObjectWithTag ("Player");
		mamaroFistCol = mamaro.GetComponentInChildren<BoxCollider> ();
		if (gameObject.tag == "Wall") {
			GameObject WallDestroLoad = Resources.Load ("Envi_Destructables/Kelpi_Wall_Busted") as GameObject;
			WallDestruction = WallDestroLoad;
		} else if (gameObject.tag == "Build") {
			GameObject BuildDestroLoad = Resources.Load ("Envi_Destructables/L_Building_Model_Destro") as GameObject;
			buildDestrction = BuildDestroLoad;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BlowUp(){
		if (gameObject.tag == "Wall") {
			GameObject.Destroy (this.gameObject);
			Instantiate (WallDestruction, new Vector3 (transform.position.x, transform.position.y + 0, transform.position.z), transform.rotation);
		} else if (gameObject.tag == "Build") {
			GameObject.Destroy (this.gameObject);
			Instantiate (buildDestrction, new Vector3 (transform.position.x, transform.position.y + 0, transform.position.z), transform.rotation);
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider == mamaroFistCol) {
			Debug.Log ("MYFACE");
			BlowUp();
			punchCount ++;
			Debug.Log(punchCount.ToString());
		}

	}

	void OnCollisionStay(Collision col)
	{
		if (col.collider == mamaroFistCol) {
			Debug.Log ("Ouch");
		}
	}
}
