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
	public float waitingPeriod;

	AINavigation AINav;
	NavMeshAgent navAgent;
	
	void Start()
	{
		PatrolPoints = GameObject.FindGameObjectWithTag("PatrolPointManager").GetComponent<PatrolPointManager>().RequestPatrolPoints(PatrollingPointsID);
		AINav = GetComponent<AINavigation>();
		navAgent = GetComponent<NavMeshAgent>();
	
		waitingPeriod = 2.0f;

		newTargetSet = false;
		target = 0;
	}

	void Update()
	{
		
	}
	
	public Vector3 Patrol(List<Vector3> enemyRoute)
	{
		if(enemyRoute.Count > 0)
		{
			if(AINav.hasReachedTarget)
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

			return enemyRoute[target];
		}
		//Don't patrol when there is no patrol points
		return transform.position;
	}
		
	IEnumerator WaitBeforeNextTarget()
	{
		float timer = 0.0f;
		navAgent.speed = 0.0f;
		
		while(timer < waitingPeriod)
		{
			timer += Time.deltaTime;
			yield return 0;
		}
		target++;
		navAgent.speed = 1.0f;
		
		if(target >= PatrolPoints.Count)
		{
			target = 0;
		}
	}
}
