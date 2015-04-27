using UnityEngine;
using System.Collections;

public class SoundGeneration : MonoBehaviour
{
	//[HideInInspector]
	private AudioSource audioComponent;
	SonarManager		CachedSonarManager;
	SoundAlertManager	CachedAlertManager;
	
	[SerializeField] float		DesiredCollisionDelay = 1.0f;
	float						CollisionDelay;
	
	// Use this for initialization
	void Awake ()
	{
		audioComponent = GetComponent<AudioSource>();
	}
	
	void Start()
	{
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();
		CachedAlertManager = GameObject.FindGameObjectWithTag("SoundAlertManager").GetComponent<SoundAlertManager>();
		
		CollisionDelay = DesiredCollisionDelay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(CollisionDelay < DesiredCollisionDelay)
		{
			CollisionDelay += Time.deltaTime;
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.contacts.Length > 0 && collision.relativeVelocity.magnitude > 1.5f && CollisionDelay >= DesiredCollisionDelay && audioComponent != null)
		{
			CollisionDelay = 0.0f;
			float SoundDistance = audioComponent.maxDistance;
			CachedSonarManager.BeginNewSonarPulse(collision.contacts[0].point, SoundDistance / GlobalStaticVars.GlobalSonarSpeed, SoundDistance);
			
			audioComponent.Play();
			CachedAlertManager.SetTargetForEnemiesInRadius(transform.position, SoundDistance);
		}
	}

	void OnTriggerEnter(Collider collision)
	{
		if(CollisionDelay >= DesiredCollisionDelay && audioComponent != null)
		{
			CollisionDelay = 0.0f;
			float SoundDistance = audioComponent.maxDistance;
			CachedSonarManager.BeginNewSonarPulse(collision.transform.position, SoundDistance / GlobalStaticVars.GlobalSonarSpeed, SoundDistance);
			
			audioComponent.Play();
			CachedAlertManager.SetTargetForEnemiesInRadius(transform.position, SoundDistance);
		}
	}
}
