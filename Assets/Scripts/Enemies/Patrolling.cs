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

	List<Vector3> currentRoute;
	
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
		newTargetSet = false;
	}
	
	public Vector3 Patrol(List<Vector3> enemyRoute)
	{
		StartCoroutine(WaitBeforeNextTarget());
		//currentRoute = enemyRoute;
		print ("Idle - awaiting next target");

		return enemyRoute[target];
	}
		
	public IEnumerator WaitBeforeNextTarget()
	{
		float timer = 0.0f;
		print("timer started");

		while(timer < waitingPeriod)
		{
			//AINav.currentState = AINavigation.enemyState.Idle;
			timer += Time.deltaTime;
			yield return 0;
		}
		print("new target set");
		target++;
		AINav.currentState = AINavigation.enemyState.Patrolling;
		//AINav.patrollingSet = false;
		
		if(target >= PatrolPoints.Count)
		{
			target = 0;
		}
	}
}
