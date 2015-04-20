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
		bool bHasMoved = false;
		
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
			bHasMoved = true;
		}
		
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(-Vector3.forward * Time.deltaTime * movementSpeed);
			bHasMoved = true;
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-Vector3.right * Time.deltaTime * movementSpeed);
			bHasMoved = true;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
			bHasMoved = true;
		}

		transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Mouse X") * turnSpeed);
		
		if(bHasMoved)
		{
			MoveSonarCounter += Time.deltaTime;
			if(MoveSonarCounter >= TargetMoveSonarTime)
			{
				MoveSonarCounter = 0.0f;
				
				CachedSonarManager.BeginNewSonarPulse(FootstepSonarLocations[FootstepSonarLocationID].position, 2.0f, 2.5f);
				
				++FootstepSonarLocationID;
				if(FootstepSonarLocationID >= FootstepSonarLocations.Length)
				{
					FootstepSonarLocationID = 0;	
				}
			}
		}
	}
}