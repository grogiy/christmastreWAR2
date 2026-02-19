using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	private Rigidbody rb;
	private Animator anim;

	public Transform pointA;
	public Transform pointB;
	private Transform currentPoint;

	public float speed = 10f;
	public bool grounded;
	public LayerMask whatIsGround;
	public float playerHeight = 2f;
	public Transform orientation;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		currentPoint = pointB;
		anim.SetBool("isRunning", true);
	}

	void FixedUpdate()
	{
		grounded = Physics.Raycast(transform.position, Vector3.down,
			playerHeight * 0.5f + 0.2f, whatIsGround);

		if (!grounded) return;

		orientation.LookAt(currentPoint.position);

		Vector3 moveDir = orientation.forward * speed;
		rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

		if (Vector3.Distance(transform.position, pointB.position) < 0.5f &&
			currentPoint == pointB)
		{
			currentPoint = pointA;
		}

		if (Vector3.Distance(transform.position, pointA.position) < 0.5f &&
			currentPoint == pointA)
		{
			currentPoint = pointB;
		}
	}
	
}