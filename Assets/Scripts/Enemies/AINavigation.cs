using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AINavigation : MonoBehaviour
{
	[HideInInspector]
	public Transform player;
	[HideInInspector]
	List<Vector3> patrolPath;
	[HideInInspector]
	List<GameObject> GMlocalPatrolPath;
	List<Vector3> localPatrolPath;
	bool arePointsCached;
	[HideInInspector]
	public Vector3 target;
	NavMeshAgent agent;
	AudioSource sound;
	public float levelOfAttraction;
	public bool hasReachedTarget;
	float searchRadius;
	public bool currentlySearching;
	public AudioClip[] EnemySounds;

	public SonarManager CachedSonarManager;
	
	Ray searchRay;
	public RaycastHit searchRayHitResult;
	
	Patrolling PatrolRef;
	PatrolPointManager PatrolManager;
	
	public float backToRoamingDelay;
	bool goBack;

	public enum enemyState
	{
		Idle = 0,
		Patrolling = 1,
		Chasing = 2,
		Alert = 4
		//Kill = 3
	};

	public bool idleSet;
	public bool patrollingSet;
	public bool chasingSet;
	public bool alertSet;

	bool fuckingdoit;

	public enemyState currentState;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		sound = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = transform.position;
		levelOfAttraction = 0.0f;
		hasReachedTarget = true;
		searchRadius = -1.0f;
		currentlySearching = false;

		arePointsCached = false;



		PatrolRef = GetComponent<Patrolling>();
		PatrolManager = GameObject.FindGameObjectWithTag("PatrolPointManager").GetComponent<PatrolPointManager>();

		patrolPath = PatrolManager.RequestPatrolPoints(PatrolRef.PatrollingPointsID);

		GMlocalPatrolPath = new List<GameObject>();
		localPatrolPath = new List<Vector3>();

		//Cache local patrol points
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).tag == "PatrolPoint")
			{
				GMlocalPatrolPath.Add(transform.GetChild(i).gameObject);
			}
		}

		backToRoamingDelay = 5.0f;
		goBack = true;

		idleSet = false;
		patrollingSet = false;
		chasingSet = false;
		alertSet = false;
	}

	public void Nable()
	{
		print ("lol");
		currentState = enemyState.Idle;
		target = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//print(currentState);
		//print(agent.remainingDistance);
		SetStates();
		EnemyBehaviour();
		HasReachedTarget();
		agent.SetDestination(target);
		
		if(!hasReachedTarget)
		{
			if(!sound.isPlaying)
			{
				//sound.Play();
			}
		}

		//print (searchRadius);

	}
		
	public void SetEnemySearchingParams(Vector3 soundPosition, float soundRadius, int targetLayer)
	{
		levelOfAttraction = 1 - ((Vector3.Distance(transform.position, soundPosition)) / soundRadius);
		searchRadius = 0.5f;//soundRadius - (soundRadius / Vector3.Distance(transform.position, soundPosition * 0.05f));
		
		CheckIfCanSeeSoundSource(soundPosition, soundRadius, targetLayer);
		
		target = (soundPosition + new Vector3(Random.Range(-searchRadius, searchRadius), soundPosition.y , Random.Range(-searchRadius, searchRadius)));
		currentlySearching = true;
		//Debug.Log(string.Format("Attraction: {0} Radius: {1} Target: {2}", levelOfAttraction, searchRadius, target));
	}
	
	public bool HasReachedTarget()
	{
		if(agent.remainingDistance <= 1.0f)
		{
			//if points are not already cached, cache them and patrol
			if(!arePointsCached && currentlySearching)
			{
				localPatrolPath.Clear();
				for(int i = 0; i < 3; i++)
				{
					localPatrolPath.Add(GMlocalPatrolPath[i].transform.position);
				}
				arePointsCached = true;
				//currentState = enemyState.Alert;
				currentlySearching = false;
			}
			hasReachedTarget = true;
		}
		else
		{
			hasReachedTarget = false;
			//currentState = enemyState.Patrolling;
			//patrollingSet = false;
		}
		return hasReachedTarget;
	}
	
	public void SetStates()
	{
		//searchRadius = 1 - levelOfAttraction;
		if(levelOfAttraction >= 0.1f && hasReachedTarget)// && arePointsCached)
		{
			//cache local points positions
			currentState = enemyState.Alert;
			//print ("Alertings");
			//goBack = false;
		}
		else if (levelOfAttraction >= 0.1f && !hasReachedTarget)
		{
			currentState = enemyState.Chasing;
			//print ("Chasings");
		}
		//Due to the way Physics.OverlapSphere works sometimes the distance from enemy to sound source can be < 0, use for very, very, very faint sound detection by enemy
		else if(hasReachedTarget)
		{
			currentState = enemyState.Idle;
			//print ("Idlings");
		}
		/*else if(levelOfAttraction < 0.1f && hasReachedTarget && currentState == enemyState.Patrolling)
		{

		}*/
	}

	public void EnemyBehaviour()
	{
		switch(currentState)
		{
			case enemyState.Alert:
				if(!alertSet)
				{
					agent.speed = 1.5f;
					target = PatrolRef.Patrol(localPatrolPath);
					StartCoroutine(WaitBeforeGoingBackToOriginalPosition());
					
					idleSet = false;
					patrollingSet = false;
					chasingSet = false;
					
					Debug.LogWarning("Im Alerted");
					alertSet = true;
				}
				break;
			case enemyState.Idle:
				if(!idleSet)
				{
					agent.speed = 0.0f;
					GetComponent<AudioSource>().PlayOneShot(EnemySounds[0], 1.0f);
					CachedSonarManager.BeginNewSonarPulse(transform.position, 4.44f / GlobalStaticVars.GlobalSonarSpeed, 4.44f);
					target = PatrolRef.Patrol(patrolPath);
					
				print ("Im Idle");
					patrollingSet = false;
					chasingSet = false;
					alertSet = false;

					idleSet = true;
				}
				break;
			case enemyState.Patrolling:
				if(!patrollingSet)
				{
					agent.speed = 1.0f;

					idleSet = false;
					chasingSet = false;
					alertSet = false;
					
				print ("Im Patrolling");
					patrollingSet = true;
				}
				break;
			case enemyState.Chasing:
				if(!chasingSet)
				{
					agent.speed = 2.5f;
					GetComponent<AudioSource>().PlayOneShot(EnemySounds[1], 1.0f);
					CachedSonarManager.BeginNewSonarPulse(transform.position, 7.0f / GlobalStaticVars.GlobalSonarSpeed, 7.0f);

					idleSet = false;
					patrollingSet = false;
					alertSet = false;

				print ("Im Chasing");
					chasingSet = true;
				}
				break;
		}
	}
	
	public bool CheckIfCanSeeSoundSource(Vector3 soundsSource, float seachRadius, int targetLayer)
	{
		searchRay.origin = transform.position;
		searchRay.direction = soundsSource - transform.position;
		
		Debug.DrawLine(searchRay.origin, soundsSource, Color.red, 10.0f);
		
		if(Physics.Raycast(searchRay, out searchRayHitResult, seachRadius, 1 << targetLayer))
		{
			print("Found you, Mofo!!!");
			return true;
		}
		else
		{
			print("Where are you!?!");
			return false;
		}		
	}
	
	IEnumerator WaitBeforeGoingBackToOriginalPosition()
	{
		float timer = 0.0f;
		while(timer < backToRoamingDelay)
		{
			timer += Time.deltaTime;
			yield return 0;
		}
		//go back to original patrolling points
		currentState = enemyState.Patrolling;

		//levelOfAttraction = 0.0f;
		//idleSet = false;
		arePointsCached = false;
	}
}
