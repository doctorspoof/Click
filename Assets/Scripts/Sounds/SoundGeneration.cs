using UnityEngine;
using System.Collections;

public class SoundGeneration : MonoBehaviour
{
	//[HideInInspector]
	private AudioSource audioComponent;
	
	// Use this for initialization
	void Awake ()
	{
		audioComponent = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetMouseButtonDown(0) && transform.tag == "Player")
		{
			Click();
		}
	}
	
	void SetTargetForEnemiesInRadius(Vector3 soundSource, float soundRadius)
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
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.contacts.Length > 0)
		{
			//soundPosition = collision.contacts[0].point;
			audioComponent.Play();
			SetTargetForEnemiesInRadius(transform.position, GetComponent<AudioSource>().maxDistance);
		}
	}

	public void Click()
	{
		audioComponent.Play();
		SetTargetForEnemiesInRadius(transform.position, gameObject.GetComponent<AudioSource>().maxDistance);

	}
}
