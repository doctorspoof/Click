using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	private float turnSpeed;
		
	void Awake()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Start()
	{
		//Executed in Start so that the parent 'turnSpeed' variable is set first
		turnSpeed = GetComponentInParent<PlayerMovement>().turnSpeed;
	}
	
	void FixedUpdate()
	{	
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + (Time.deltaTime * Input.GetAxis("Mouse Y") * -turnSpeed),
		                                      transform.rotation.eulerAngles.y,
		                                      transform.rotation.eulerAngles.z);
	}
}
