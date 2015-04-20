using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ClickState
{
	NoClick = 0,
	FirstFrameDown = 1,
	ClickHeld = 2,
	FirstFrameUp = 3
}

public class ClickControl : MonoBehaviour 
{
	[SerializeField]	AudioClip	ClickSound;
	[SerializeField]  	float  	MaxSonarDistance;
	[SerializeField]	float	MaxChargeTime;
	[SerializeField]	float	MinChargeTime;
	
	SonarManager 	CachedSonarManager;
	float			CurrentChargeTime;
	bool			bIsChargingSonar;
	
	bool			bLastFrameClickWasDown;


	// Use this for initialization
	void Start () 
	{
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		// Set up our vars to make it more accessible
		float ClickInput = Input.GetAxisRaw("Fire1");
		
		ClickState CurrentClickState = ClickState.NoClick;
		
		if(ClickInput > 0 && !bLastFrameClickWasDown)
		{
			CurrentClickState = ClickState.FirstFrameDown;
		}
		else if(ClickInput < 1 && bLastFrameClickWasDown)
		{
			CurrentClickState = ClickState.FirstFrameUp;	
		}
		else if(ClickInput > 0 && bLastFrameClickWasDown)
		{
			CurrentClickState = ClickState.ClickHeld;	
		}
		else if(ClickInput < 1 && !bLastFrameClickWasDown)
		{
			CurrentClickState = ClickState.NoClick;	
		}

		// Handle the actual event now
		switch(CurrentClickState)
		{
			case ClickState.FirstFrameUp:
			{
				bIsChargingSonar = false;
				
				if(CurrentChargeTime > 0.0f)
				{
					if(CurrentChargeTime < MinChargeTime)
					{
						CurrentChargeTime = MinChargeTime;
					}
				
					float ChargePercent = CurrentChargeTime / MaxChargeTime;
					float SonarDistance = ChargePercent * MaxSonarDistance;
					float SonarTime = SonarDistance / GlobalStaticVars.GlobalSonarSpeed;
					CachedSonarManager.BeginNewSonarPulse(transform.position + (Vector3.down * transform.localScale.y), SonarTime, SonarDistance);
					
					CurrentChargeTime = 0.0f;
				
					this.GetComponent<AudioSource>().PlayOneShot(ClickSound, ChargePercent);
				}
				break;
			}
			case ClickState.FirstFrameDown:
			{
				bIsChargingSonar = true;
				break;	
			}
			case ClickState.ClickHeld:
			{
				CurrentChargeTime += Time.deltaTime;
				
				if(CurrentChargeTime > MaxChargeTime)
				{
					CurrentChargeTime = MaxChargeTime;	
				}
				break;
			}
			default:
			{
				break;
			}
		}
		
		// Set up our vars for next frame's input
		bLastFrameClickWasDown = ClickInput > 0;
	}
}
