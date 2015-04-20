using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SonarManager : MonoBehaviour 
{
	List<GameObject> 	SonarUpdatableSceneObjects;
	Material			CachedMaterialType;
	
	float[]		 		DesiredSonarTimes;
	
	[SerializeField]	int			UniqueSonarPulses;
	
	float[]				CachedSonarTimes;
	
	void Start () 
	{
		SonarUpdatableSceneObjects = new List<GameObject>();
		CachedMaterialType = GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().sharedMaterial;
		
		GameObject[] Objects = GameObject.FindObjectsOfType<GameObject>() as GameObject[];
		for(int i = 0; i < Objects.Length; i++)
		{
			if(Objects[i] != this.gameObject && Objects[i].GetComponent<Renderer>() != null && Objects[i].GetComponent<Renderer>().sharedMaterial == CachedMaterialType)
			{
				SonarUpdatableSceneObjects.Add(Objects[i]);
			}
		}
		
		DesiredSonarTimes = new float[UniqueSonarPulses];
		CachedSonarTimes = new float[UniqueSonarPulses];
		
		for(int i = 0; i < UniqueSonarPulses; i++)
		{
			DesiredSonarTimes[i] = 0.0f;
			CachedSonarTimes[i] = 1.0f;
		}
	}
	
	void Update () 
	{
		for(int i = 0; i < UniqueSonarPulses; i++)
		{
			if(DesiredSonarTimes[i] != 0.0f)
			{
				float TimeIncrement = Time.deltaTime * (1.0f / DesiredSonarTimes[i]);
				foreach(GameObject obj in SonarUpdatableSceneObjects)
				{
					Material ObjMat = obj.GetComponent<Renderer>().material;
					string idString = (i + 1).ToString();
					if(ObjMat.GetFloat("_SonarTime" + idString) < 1.0f)
					{
						ObjMat.SetFloat("_SonarTime" + idString, ObjMat.GetFloat("_SonarTime" + idString) + TimeIncrement);
					}
				}
				
				CachedSonarTimes[i] += TimeIncrement;
			}
		}
	}
	
	public void BeginNewSonarPulse(Vector3 WorldPos, float SonarTimeLength, float SonarDistance)
	{
		float HighestSonarTime = 0.0f;
		int LowestSonarID = -1;
		
		for(int i = 0; i < UniqueSonarPulses; i++)
		{
			// We want the 'highest' time, so get the largest value
			if(CachedSonarTimes[i] > HighestSonarTime || LowestSonarID == -1)
			{
				// Alternatively, if the time value is 1.0 or higher it's already finished and we can safely use it; no point in looking any further
				if(CachedSonarTimes[i] >= 1.0f)
				{
					LowestSonarID = i;
					break;
				}
				
				HighestSonarTime = CachedSonarTimes[i];
				LowestSonarID = i;
			}
		}
		
		
		// Access id with lowestsonarid
		string idString = (LowestSonarID + 1).ToString();
		foreach(GameObject obj in SonarUpdatableSceneObjects)
		{
			Material ObjMat = obj.GetComponent<Renderer>().material;
			ObjMat.SetVector("_SonarWorldPos" + idString, WorldPos);
			ObjMat.SetFloat("_SonarDistance" + idString, SonarDistance);
			ObjMat.SetFloat("_SonarTime" + idString, 0.0f);
			CachedSonarTimes[LowestSonarID] = 0.0f;
		}
		
		
		// Now cache the required time in our local list
		DesiredSonarTimes[LowestSonarID] = SonarTimeLength;
	}
	
	public void UpdatePlayerPosition(Vector3 PlayerPos)
	{
		foreach(GameObject obj in SonarUpdatableSceneObjects)
		{
			Material ObjMat = obj.GetComponent<Renderer>().material;
			ObjMat.SetVector("_PlayerPosition", PlayerPos);
		}
	}
}
