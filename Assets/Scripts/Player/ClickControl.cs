using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickControl : MonoBehaviour 
{
	List<GameObject> ObjectsToUpdate;
	
	// Use this for initialization
	void Start () 
	{
		ObjectsToUpdate = new List<GameObject>();
		Material OurCachedMaterial = this.GetComponent<Renderer>().sharedMaterial;
		
		GameObject[] Objects = GameObject.FindObjectsOfType<GameObject>() as GameObject[];
		for(int i = 0; i < Objects.Length; i++)
		{
			if(Objects[i] != this.gameObject && Objects[i].GetComponent<Renderer>() != null && Objects[i].GetComponent<Renderer>().sharedMaterial == OurCachedMaterial)
			{
				ObjectsToUpdate.Add(Objects[i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject obj in ObjectsToUpdate)
		{
			Material ObjMat = obj.GetComponent<Renderer>().material;
			ObjMat.SetFloat("_SonarTime", ObjMat.GetFloat("_SonarTime") + (Time.deltaTime * 0.005f));
		}
		
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			//GetComponent<Click_Clicker>().Toggle();
			
			Vector3 CurrPosition = transform.position;
			
			foreach(GameObject obj in ObjectsToUpdate)
			{
				Material ObjMat = obj.GetComponent<Renderer>().material;
				ObjMat.SetVector("_WorldPosition", new Vector4(CurrPosition.x, CurrPosition.y, CurrPosition.z, 1.0f));
				ObjMat.SetFloat("_SonarTime", 0.0f);
				ObjMat.SetFloat("_SonarIntensity", 100.0f);
			}
		}
	}
}
