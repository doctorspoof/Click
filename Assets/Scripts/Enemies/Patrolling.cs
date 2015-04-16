using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrolling : MonoBehaviour {
	
	List<Vector3> roamingPoints;
	GameObject[] roamingPointsRef;
	
	int target;
	
	void Start()
	{
		roamingPoints = new List<Vector3>();
		
		roamingPointsRef = GameObject.FindGameObjectsWithTag("RoamingPoint");
		
		for(int i = 0; i < roamingPointsRef.Length; i++)
		{
			roamingPoints.Add(roamingPointsRef[i].transform.position);
		}
		
		target = 0;
	}

	void Update()
	{
	
	}
	
	public Vector3 Patrol(NavMeshAgent navAgent)
	{
		if(HasReachedTarget(roamingPoints[target]))
		{
			StartCoroutine(WaitBeforeNextTarget());
			//target++;
		}
		
		if(target >= roamingPointsRef.Length)
		{
			target = 0;
		}
		
		//print (target);
		//print (HasReachedTarget(roamingPoints[target]));
		
		return roamingPoints[target];	
	}
	
	bool HasReachedTarget(Vector3 currentTarget)
	{
		if(Vector3.Distance(transform.position, currentTarget) < 1.0f)
		{
			return true;
		}
		else
		{
			return false;
		}	
	}
	
	IEnumerator WaitBeforeNextTarget()
	{
		float timer = 0.0f;
		
		while(timer < 1.0f)
		{
			timer += Time.deltaTime;
			yield return 0;
		}
		
		print (target);
		target++;
	}
}
