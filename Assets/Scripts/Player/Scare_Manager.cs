using UnityEngine;
using System.Collections;

public class Scare_Manager : MonoBehaviour
{
	public float currentScarePercentage = 20.0f;
	public float maxScarePercentage = 100.0f;
	public float minScarePercentage = 0.0f;

	void Update()
	{	
		if(currentScarePercentage <= 100) 
		{
			currentScarePercentage -= Time.deltaTime;
		}
		if (currentScarePercentage >= 100) 
		{
			currentScarePercentage = maxScarePercentage;
		}
		if (currentScarePercentage <= 0) 
		{
			currentScarePercentage = minScarePercentage;
		}
	}
}