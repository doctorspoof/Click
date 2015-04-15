using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed;
	public float turnSpeed;

	Vector3 currentMousePosition;
	Vector3 lastMousePosition;
	
	void Awake()
	{
		movementSpeed = 5.0f;
		turnSpeed = 150.0f;
	}

	void FixedUpdate()
	{
		CharacterMovement();
	}

	void CharacterMovement()
	{				
		if (Input.GetKey(KeyCode.W))
		{
			transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
		}
		
		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(-Vector3.forward * Time.deltaTime * movementSpeed);
		}
		
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-Vector3.right * Time.deltaTime * movementSpeed);
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
		}

		transform.Rotate(Vector3.up, Time.deltaTime * Input.GetAxis("Mouse X") * turnSpeed);
	}
}