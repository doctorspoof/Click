using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickControl : MonoBehaviour 
{
	//List<GameObject> ObjectsToUpdate;
	SonarManager CachedSonarManager;
	
	[SerializeField]  	float  	MaxSonarDistance;
	[SerializeField]	float	MaxChargeTime;
	
	[SerializeField] float	CurrentChargeTime;
	[SerializeField] bool	bIsChargingSonar;
	
	// Use this for initialization
	void Start () 
	{
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(Input.GetMouseButtonDown(0))
		{
			bIsChargingSonar = true;	
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			bIsChargingSonar = false;
			
			if(CurrentChargeTime > 0.0f)
			{
				// Do a pulse here
				float SonarDistance = (CurrentChargeTime / MaxChargeTime) * MaxSonarDistance;
				float SonarTime = SonarDistance / GlobalStaticVars.GlobalSonarSpeed;
				CachedSonarManager.BeginNewSonarPulse(transform.position + (Vector3.down * transform.localScale.y), SonarTime, SonarDistance);
				
				CurrentChargeTime = 0.0f;
			}
		}
		
		if(bIsChargingSonar && Input.GetMouseButton(0))
		{
			CurrentChargeTime += Time.deltaTime;
			
			if(CurrentChargeTime > MaxChargeTime)
			{
				CurrentChargeTime = MaxChargeTime;	
			}
		}
	}
}
