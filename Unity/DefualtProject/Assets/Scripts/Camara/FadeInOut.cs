using UnityEngine;
using System.Collections;

public class FadeInOut : MonoBehaviour {
	public float fadeSpeed = 1.5f; 

	private bool sceneStarting = true;
	private MeshFilter layerMesh;
	// Use this for initialization
	void Awake () 
	{
		layerMesh = transform.GetComponent<MeshFilter> ();
		//layerMesh.
		//guiTexture.pixelInset = new Rect (0.0f, 0.0f, Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (sceneStarting) 
		{
			StartScene();
		}
	}

	void FadeToClear()
	{
		layerMesh.renderer.material.color = Color.Lerp (layerMesh.renderer.material.color, Color.clear, fadeSpeed * Time.deltaTime);
		//guiTexture.color = Color.Lerp (guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	void FadetoBlack()
	{
		layerMesh.renderer.material.color = Color.Lerp (layerMesh.renderer.material.color, Color.black, fadeSpeed * Time.deltaTime);
		//guiTexture.color = Color.Lerp (guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	void StartScene()
	{
		FadeToClear ();
		//3d Based Fade in out
		if (layerMesh.renderer.material.color.a <= 0.05f) 
		{
			layerMesh.renderer.material.color = Color.clear;
			layerMesh.renderer.enabled = false;
			sceneStarting = false;
		}
		//Gui Based fade in out
		/*
		if (guiTexture.color.a <= 0.05f) 
		{
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			sceneStarting = false;
		}
		*/
	}

	void EndScene()
	{
		layerMesh.renderer.enabled = true;
		//guiTexture.enabled = true;
		FadetoBlack ();

		if (layerMesh.renderer.material.color.a <= 0.05f) 
		{
			Application.LoadLevel(0);
		}

		/*
		if (guiTexture.color.a <= 0.05f) 
		{
			Application.LoadLevel(0);
		}
		*/
	}

	void OnCollisionEnter()
	{
		//EndScene ();
	}
}
