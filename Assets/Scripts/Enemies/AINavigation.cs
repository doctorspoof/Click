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
	
	Ray searchRay;
	public RaycastHit searchRayHitResult;
	
	Patrolling PatrolRef;
	PatrolPointManager PatrolManager;
	
	public float backToRoamingDelay;
	bool goBack;

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

		backToRoamingDelay = 15.0f;
		goBack = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		AttractionBehaviour();
		HasReachedTarget();
		agent.SetDestination(target);
		
		if(!hasReachedTarget)
		{
			if(!sound.isPlaying)
			{
				sound.Play();
			}
		}
	}
		
	public void SetEnemySearchingParams(Vector3 soundPosition, float soundRadius, int targetLayer)
	{
		levelOfAttraction = 1 - ((Vector3.Distance(transform.position, soundPosition)) / soundRadius);
		searchRadius = soundRadius - (soundRadius / Vector3.Distance(transform.position, soundPosition));
		
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
				for(int i = 0; i < transform.childCount; i++)
				{
					localPatrolPath.Add(GMlocalPatrolPath[i].transform.position);
				}
				arePointsCached = true;
				levelOfAttraction = 0.4f;
				currentlySearching = false;
			}
			hasReachedTarget = true;
		}
		else
		{
			hasReachedTarget = false;
		}
		return hasReachedTarget;
	}
	
	public void AttractionBehaviour()
	{
		//searchRadius = 1 - levelOfAttraction;
		if(levelOfAttraction >= 0.5f)
		{
			agent.speed = 4.0f;

		}
		else if(levelOfAttraction <= 0.5f && levelOfAttraction > 0.0f && arePointsCached)
		{
			//cache local points positions
			agent.speed = 2.0f;
			target = PatrolRef.Patrol(localPatrolPath);
			StartCoroutine(WaitBeforeGoingBackToOriginalPosition());
			goBack = false;
		}
		//Due to the way Physics.OverlapSphere works sometimes the distance from enemy to sound source can be < 0, use for very, very, very faint sound detection by enemy
		else if(levelOfAttraction <= 0.0f && goBack)
		{
			//Go back to patrolling if no sound is heard
			target = PatrolRef.Patrol(patrolPath);
		}
	}
	
	public bool CheckIfCanSeeSoundSource(Vector3 soundsSource, float seachRadius, int targetLayer)
	{
		searchRay.origin = transform.position;
		searchRay.direction = soundsSource - transform.position;
		
		Debug.DrawLine(searchRay.origin, soundsSource, Color.red, 10.0f);
		
		if(Physics.Raycast(searchRay, out searchRayHitResult, seachRadius, 1 << targetLayer))
		{
			//print("Found you, Mofo!!!");
			return true;
		}
		else
		{
			//print("Where are you!?!");
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
		levelOfAttraction = 0.0f;
		goBack = true;
		arePointsCached = false;
	}
}
