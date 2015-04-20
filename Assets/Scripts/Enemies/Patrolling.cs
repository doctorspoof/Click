using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Patrolling : MonoBehaviour
{
	
	List<Vector3> PatrolPoints;
	Vector3[] localPatrolPoints;
	bool newTargetSet;
	
	int target;

	public int PatrollingPointsID;
	public int initialPatrollingPointsID;
	
	void Start()
	{
		PatrolPoints = GameObject.FindGameObjectWithTag("PatrolPointManager").GetComponent<PatrolPointManager>().RequestPatrolPoints(PatrollingPointsID);

		print(PatrolPoints.Count);
		newTargetSet = false;
		target = 0;

		initialPatrollingPointsID = PatrollingPointsID;
	}

	void Update()
	{
		//print(PatrolPoints.Count);
	}

	public List<Vector3> GetPatrolRoute()
	{
		if(PatrollingPointsID == -1)
		{
			List<Vector3> localPoints = new List<Vector3>();
			for(int i = 0; i < transform.childCount; i++)
			{
				if(transform.GetChild(i).tag == "PatrolPoint")
				{
					//chachehek this
					localPoints.Add(new Vector3(transform.GetChild(i).position.x, transform.GetChild(i).position.y, transform.GetChild(i).position.z));
				}
			}
			return localPoints;
		}
		else
		{
			return PatrolPoints;
		}
	}
	
	public Vector3 Patrol(NavMeshAgent navAgent)
	{
		if(PatrolPoints.Count > 0)
		{
			if(HasReachedTarget(GetPatrolRoute()[target]))
			{
				if(!newTargetSet)
				{
					StartCoroutine(WaitBeforeNextTarget());
					newTargetSet = true;
				}
			}
			else
			{
				newTargetSet = false;
			}
			//print (target);
			//print (HasReachedTarget(roamingPoints[target]));
			
			return GetPatrolRoute()[target];
		}
		//Don't roam when there is no roaming points
		return transform.position;
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
		target++;
		
		if(target >= PatrolPoints.Count)
		{
			target = 0;
		}
	}
}
