using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	float DeathDistance;
	public Transform CheckPoint;

	public GameObject AnimationToPlay;
	Camera CachedPlayerCam;
	public GameObject KillVolume;

	GameObject PlayerToDisable;

	public AudioClip DeathSound;
	AINavigation AINavRef;

	public SonarManager CachedSonarManager;

	[SerializeField]	float	AverageRadioSonarRange;
	[SerializeField]	float	RandomRangeOffset;
	[SerializeField]	float	AverageTimeBetweenPulses;
	[SerializeField]	float	RandomTimeOffset;

	public Object EnemyPrefab;

	float 	NextRange;
	float	TargetTime;
	float	PulseCounter;

	Collider[] EnemiesInRadius;

	void Start ()
	{
		DeathDistance = 1.0f;

		CachedPlayerCam = Camera.main;

		PlayerToDisable = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		EnemiesInRadius = Physics.OverlapSphere(transform.position, DeathDistance, 1 << LayerMask.NameToLayer("Enemy"));

		if(EnemiesInRadius.Length > 0)
		{
			Die();
		}

	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "KillVolume")
		{
			transform.position = CheckPoint.position;
		}
	}

	void Die()
	{
		if(AnimationToPlay != null && PlayerToDisable != null)
		{
			//CachedPlayerCam = Camera.main;
			//Camera.main.enabled = false;

			PlayerToDisable.GetComponent<ClickControl>().enabled = false;
			PlayerToDisable.GetComponent<PlayerMovement>().enabled = false;

			AnimationToPlay.GetComponent<Camera>().enabled = true;
			AnimationToPlay.GetComponent<Animation>().Play();

			StartCoroutine(AwaitAnimationComplete());

		}
		//Play death animation then teleport to the checkpoint(trapdoor)
	}

	IEnumerator AwaitAnimationComplete()
	{		
		while(AnimationToPlay.GetComponent<Animation>().isPlaying)
		{
			PulseCounter += Time.deltaTime;
			
			if(PulseCounter >= TargetTime)
			{
				PulseCounter = 0.0f;
				CachedSonarManager.BeginNewSonarPulse(transform.position, NextRange / GlobalStaticVars.GlobalSonarSpeed, NextRange);
				ResetTargetTime();
			}

			yield return 0;
		}

		AnimationToPlay.GetComponent<Camera>().enabled = false;
		CachedPlayerCam.enabled = true;
		PlayerToDisable.GetComponent<ClickControl>().enabled = true;
		PlayerToDisable.GetComponent<PlayerMovement>().enabled = true;

		Application.LoadLevel(1);


		transform.position = CheckPoint.position;
	}

	void ResetTargetTime()
	{
		TargetTime = AverageTimeBetweenPulses + (Random.Range(-RandomTimeOffset, RandomTimeOffset));
		NextRange = AverageRadioSonarRange + (Random.Range(-RandomRangeOffset, RandomRangeOffset));
	}
}
