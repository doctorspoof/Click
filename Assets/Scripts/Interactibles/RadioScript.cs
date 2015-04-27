using UnityEngine;
using System.Collections;

public class RadioScript : MonoBehaviour 
{
	[SerializeField]	float	AverageRadioSonarRange;
	[SerializeField]	float	RandomRangeOffset;
	[SerializeField]	float	AverageTimeBetweenPulses;
	[SerializeField]	float	RandomTimeOffset;
	
	float 	NextRange;
	float	TargetTime;
	float	PulseCounter;

	public AudioClip radioStatic;
	bool staticSet;
	
	SonarManager CachedSonarManager;
	
	// Use this for initialization
	void Start () 
	{
		ResetTargetTime();
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();

		staticSet = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		PulseCounter += Time.deltaTime;
			
		if(PulseCounter >= TargetTime)
		{
			PulseCounter = 0.0f;
			CachedSonarManager.BeginNewSonarPulse(transform.position, NextRange / GlobalStaticVars.GlobalSonarSpeed, NextRange);
			ResetTargetTime();
		}

		if(!GetComponent<AudioSource>().isPlaying && !staticSet)
		{
			GetComponent<AudioSource>().clip = radioStatic;
			staticSet = true;
		}

		if(!GetComponent<AudioSource>().isPlaying)
		{
			GetComponent<AudioSource>().Play();
		}
	}
	
	void ResetTargetTime()
	{
		TargetTime = AverageTimeBetweenPulses + (Random.Range(-RandomTimeOffset, RandomTimeOffset));
		NextRange = AverageRadioSonarRange + (Random.Range(-RandomRangeOffset, RandomRangeOffset));
	}
}
