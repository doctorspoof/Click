using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolPointManager : MonoBehaviour
{
	GameObject[] patrolPoints;
	
	void Awake ()
	{
		patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
	}
	
	void Update ()
	{
		
	}

	public List<Vector3> RequestPatrolPoints(int patrolID)
	{
		List<Vector3> pointGroupArray = new List<Vector3>();

		for(int i = 0; i < patrolPoints.Length; i++)
		{
			if(patrolPoints[i].GetComponent<PatrolPointIDGenerator>().pointGroupID == patrolID)
			{
				pointGroupArray.Add(patrolPoints[i].transform.position);
			}
		}
		return pointGroupArray;
	}
}
