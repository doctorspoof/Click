using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed;
	public float turnSpeed;

	Vector3 currentMousePosition;
	Vector3 lastMousePosition;
	
	SonarManager 	CachedSonarManager;
	float			MoveSonarCounter;
	int				FootstepSonarLocationID;
	
	[SerializeField]	Transform[]		FootstepSonarLocations;
	[SerializeField]	float			TargetMoveSonarTime;
	[SerializeField]	float			FootstepSonarDistance;
	
	void Awake()
	{
		movementSpeed = 5.0f;
		turnSpeed = 150.0f;
	}
	
	void Start()
	{
		CachedSonarManager = GameObject.FindGameObjectWithTag("SonarManager").GetComponent<SonarManager>();	
		FootstepSonarLocationID = 0;
	}

	void FixedUpdate()
	{
		CharacterMovement();
	}

	void CharacterMovement()
	{	
		float MoveCounter = 0.0f;
		
		float VerticalMove = Input.GetAxis("Vertical");
		transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * VerticalMove);
		MoveCounter += Mathf.Abs(VerticalMove);
	
		float HorizontalMove = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * HorizontalMove);
		MoveCounter += Mathf.Abs(HorizontalMove);

		transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Mouse X") * turnSpeed);
		
		if(MoveCounter > 0.005f)
		{
			MoveSonarCounter += Time.deltaTime;
			if(MoveSonarCounter >= TargetMoveSonarTime)
			{
				MoveSonarCounter = 0.0f;
				
				float SonarTime = FootstepSonarDistance / GlobalStaticVars.GlobalSonarSpeed;
				//CachedSonarManager.BeginNewSonarPulse(FootstepSonarLocations[FootstepSonarLocationID].position, SonarTime, FootstepSonarDistance);
				
				++FootstepSonarLocationID;
				if(FootstepSonarLocationID >= FootstepSonarLocations.Length)
				{
					FootstepSonarLocationID = 0;	
				}
			}
		}
		
		CachedSonarManager.UpdatePlayerPosition(transform.position);
	}
}