using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{
	Animator animComponent;
	NavMeshAgent navAgent;

	float initialRotation;

	void Start()
	{
		animComponent = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();

		initialRotation = transform.rotation.eulerAngles.y;
	}

	void FixedUpdate()
	{
		float v = Input.GetAxis("Vertical");

		float currentRotation = transform.rotation.eulerAngles.y;

		float rotationDifference = currentRotation - initialRotation;

		animComponent.SetFloat("Speed", navAgent.speed);

		//set to dot product between current rot and target rot
		animComponent.SetFloat("Direction", v);

		//transform.Rotate(Vector3.up, initialRotation + -v);
	}
}
