using UnityEngine;
using System.Collections;

public class AINavigation : MonoBehaviour
{
	public Transform player;
	[HideInInspector]
	public Vector3 target;
	NavMeshAgent agent;
	AudioSource sound;
	float levelOfAttraction;
	bool hasReachedTarget;
	float searchRadius;
	public bool currentlySearching;
	
	Ray searchRay;
	public RaycastHit searchRayHitResult;
	
	// Use this for initialization
	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		sound = GetComponent<AudioSource>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = transform.position;
		levelOfAttraction = 0.0f;
		hasReachedTarget = true;
		searchRadius = -1.0f;
		currentlySearching = false;
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
		//search radius based on the sound 'volume' devided by 4 for increased accuracy
		searchRadius = (soundRadius - (soundRadius / Vector3.Distance(transform.position, soundPosition))) * 0.25f;
		
		CheckIfCanSeeSoundSource(soundPosition, soundRadius, targetLayer);
		
		target = soundPosition + new Vector3(Random.Range(-searchRadius, searchRadius), soundPosition.y , Random.Range(-searchRadius, searchRadius));
		//Debug.Log(string.Format("Attraction: {0} Radius: {1} Target: {2}", levelOfAttraction, searchRadius, target));
	}
	
	public bool HasReachedTarget()
	{
		if(agent.remainingDistance <= 0.5f)
		{
			hasReachedTarget = true;
			levelOfAttraction = 0.0f;
		}
		else
		{
			hasReachedTarget = false;
		}
		//print(hasReachedTarget);
		return hasReachedTarget;
	}
	
	public void AttractionBehaviour()
	{
		//searchRadius = 1 - levelOfAttraction;
		if(levelOfAttraction >= 0.5f)
		{
			agent.speed = 4.0f;
		}
		else if(levelOfAttraction <= 0.5f && levelOfAttraction > 0.0f)
		{
			agent.speed = 2.0f;
		}
		//Due to the way Physics.OverlapSphere works sometimes the distance from enemy to sound source can be < 0, use for very, very, very faint sound detection by enemy
		else if(levelOfAttraction <= 0.0f)
		{
			agent.speed = 0.0f;
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
}
