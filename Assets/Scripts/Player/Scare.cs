using UnityEngine;
using System.Collections;

public class Scare : MonoBehaviour {

	Scare_Manager sManager;
	float scarePercent = 10;

	void Start()
	{
		sManager = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Scare_Manager> ();
	}

	void Update()
	{
		RaycastHit hit;
		Vector3 fwd= transform.TransformDirection(Vector3.forward);

		if (Physics.Raycast(transform.position, fwd, out hit)) 
		{
			if(hit.distance <= 10.0f && hit.collider.gameObject.tag == "Enemy")
			{
				sManager.currentScarePercentage += scarePercent * Time.deltaTime;
			}

			if(hit.distance > 10.0f)
			{
				sManager.currentScarePercentage -= scarePercent * Time.deltaTime;
			}
		}
	}

}
