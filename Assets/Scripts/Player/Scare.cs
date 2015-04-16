using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

	Scare_Manager sManager;

	void Start()
	{
		sManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Scare_Manager> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter () {
	
	}
}
