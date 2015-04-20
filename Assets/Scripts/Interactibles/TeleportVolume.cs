using UnityEngine;
using System.Collections;

public class TeleportVolume : MonoBehaviour 
{
	[SerializeField]	Transform		TargetLocation;
	[SerializeField]	GameObject		PlayerToDisable;
	[SerializeField]	GameObject		AnimationToPlay;
	[SerializeField]	GameObject[]	ObjectsToDisableOnTeleport;
	
	Camera CachedPlayerCam;
	
	void OnTriggerEnter(Collider other)
	{
		// Make sure it's the player that hit us
		if(other.tag == "Player")
		{
			if(AnimationToPlay != null && PlayerToDisable != null)
			{
				CachedPlayerCam = Camera.main;
				Camera.main.enabled = false;
				PlayerToDisable.GetComponent<ClickControl>().enabled = false;
				PlayerToDisable.GetComponent<PlayerMovement>().enabled = false;
				
				AnimationToPlay.GetComponent<Camera>().enabled = true;
				AnimationToPlay.GetComponent<Animation>().Play();
				
				foreach(GameObject obj in ObjectsToDisableOnTeleport)
				{
					obj.SetActive(false);	
				}
				
				StartCoroutine(AwaitAnimationComplete());
			}
			
			other.transform.position = TargetLocation.position;
			other.transform.rotation = TargetLocation.rotation;
		}
	}
	
	IEnumerator AwaitAnimationComplete()
	{
		float timer = 0.0f;
		bool bHasPulsed = false;
		
		while(AnimationToPlay.GetComponent<Animation>().isPlaying)
		{
			timer += Time.deltaTime;
			
			if(timer > 0.4f && !bHasPulsed)
			{
				bHasPulsed = true;
				GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>().BeginNewSonarPulse(TargetLocation.transform.position, 12.5f / GlobalStaticVars.GlobalSonarSpeed, 12.5f);
			}
			yield return 0;
		}
		
		AnimationToPlay.GetComponent<Camera>().enabled = false;
		
		CachedPlayerCam.enabled = true;
		PlayerToDisable.GetComponent<ClickControl>().enabled = true;
		PlayerToDisable.GetComponent<PlayerMovement>().enabled = true;
	}
}
