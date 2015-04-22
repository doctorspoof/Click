using UnityEngine;
using System.Collections;

public class SoundAlertManager : MonoBehaviour 
{
	
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public void SetTargetForEnemiesInRadius(Vector3 soundSource, float soundRadius)
	{
		Collider[] EnemiesInRadius = Physics.OverlapSphere(soundSource, soundRadius, 1 << LayerMask.NameToLayer("Enemy"));
		if(EnemiesInRadius.Length > 0)
		{
			for(int e = 0; e < EnemiesInRadius.Length; e++)
			{
				EnemiesInRadius[e].GetComponent<AINavigation>().SetEnemySearchingParams(soundSource, soundRadius, gameObject.layer);
			}
		}
	}
}
