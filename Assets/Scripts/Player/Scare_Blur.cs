using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Scare_Blur : MonoBehaviour {

	private Scare_Manager sManager;
	private MotionBlur mBlur;

	void Start()
	{
		//mBlur = Camera.main.gameObject.GetComponent<MotionBlur> ();
		//sManager = Camera.main.gameObject.GetComponent<Scare_Manager> ();
		mBlur = GameObject.FindWithTag("MainCamera").GetComponent<MotionBlur> ();
		sManager = GameObject.FindWithTag("MainCamera").GetComponent<Scare_Manager> ();
		mBlur.enabled = false;
	}

	void Update()
	{
		if( sManager.currentScarePercentage >= 20)
		{
			mBlur.enabled = true;
			mBlur.blurAmount = 0.1f;
		}
		if( sManager.currentScarePercentage >= 40)
		{
			mBlur.enabled = true;
			mBlur.blurAmount = 0.4f;
		}
		if( sManager.currentScarePercentage >= 60)
		{
			mBlur.enabled = true;
			mBlur.blurAmount = 0.75f;
		}
		if( sManager.currentScarePercentage >= 80)
		{
			mBlur.enabled = true;
			mBlur.blurAmount = 0.99f;
		}
	}
}
