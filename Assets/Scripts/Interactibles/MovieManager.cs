using UnityEngine;
using System.Collections;

public class MovieManager : MonoBehaviour 
{
	[SerializeField]	GameObject	RelatedCamera;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void PlayMovie()
	{
		RelatedCamera.transform.GetChild(0).GetComponent<Light>().enabled = true;
		RelatedCamera.GetComponent<Camera>().enabled = true;
		RelatedCamera.GetComponent<AudioListener>().enabled = true;
		
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
		GetComponent<AudioSource>().Play();
	}
}
