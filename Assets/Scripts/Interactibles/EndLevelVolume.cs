using UnityEngine;
using System.Collections;

public class EndLevelVolume : MonoBehaviour 
{	
	[SerializeField]	GameObject	MovieManagerToTrigger;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			//Disable player shizz
			other.enabled = false;
			Camera.main.GetComponent<AudioListener>().enabled = false;
			Camera.main.enabled = false;
			
			//Disable enemies
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject obj in enemies)
			{
				obj.SetActive(false);	
			}
			
			MovieManagerToTrigger.GetComponent<MovieManager>().PlayMovie();
		}
	}
}
