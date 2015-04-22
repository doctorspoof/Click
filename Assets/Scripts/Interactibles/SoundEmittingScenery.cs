using UnityEngine;
using System.Collections;

public class SoundEmittingScenery : MonoBehaviour 
{
	[SerializeField]	Transform 	SoundEmitLocation;
	[SerializeField]	AudioClip	SoundClipToEmit;
	[SerializeField]	float		AverageRepeatTime;
	[SerializeField]	float		RepeatTimeRandomOffset;
	[SerializeField]	float		EmitSonarDistance;
	
	float 			TargetTime;
	float 			CurrentTime;
	AudioSource		CachedAudioSource;
	SonarManager	CachedSonarManager;
	
	void Start () 
	{
		CachedAudioSource = GetComponent<AudioSource>();
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();
		CachedAudioSource.clip = SoundClipToEmit;
		ResetTargetTime();
	}
	
	void Update () 
	{
		CurrentTime += Time.deltaTime;
		
		if(CurrentTime >= TargetTime)
		{
			CachedAudioSource.Play();
			CachedSonarManager.BeginNewSonarPulse(SoundEmitLocation.position, EmitSonarDistance / GlobalStaticVars.GlobalSonarSpeed, EmitSonarDistance);
			
			ResetTargetTime();
		}
	}
	
	void ResetTargetTime()
	{
		CurrentTime = 0.0f;
		TargetTime = AverageRepeatTime + Random.Range(-RepeatTimeRandomOffset, RepeatTimeRandomOffset);	
	}
}
