using UnityEngine;
using System.Collections;

public class Click_Test : MonoBehaviour
{
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			GetComponent<Click_Clicker>().Toggle();
	}
	
	void OnGUI()
	{
		//GUI.Label(new Rect(0, 0, 200, 50), "CLICK TO TOGGLE FX");
	}
}
