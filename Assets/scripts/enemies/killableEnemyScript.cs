using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
	private Rigidbody rb;
	private Animator anim;

	public Transform pointA;
	public Transform pointB;

	private Transform currentPoint;

	public float speed = 2f;
	bool grounded;
	public LayerMask whatIsGround;
	public float playerHeight;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		currentPoint = pointB;
		anim.SetBool("isRunning", true);
	}

	void Update()
	{
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
		
		// Двигаемся к текущей точке
		if(grounded)

		{
				if (currentPoint == pointB)
			{
				rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, rb.linearVelocity.z);
			}
			else
			{
				rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, rb.linearVelocity.z);
			}

			// Проверяем, достигли ли точки B
			if (Vector3.Distance(transform.position, pointB.position) < 0.5f 
				&& currentPoint == pointB)
			{
				currentPoint = pointA;
			}

			// Проверяем, достигли ли точки A
			if (Vector3.Distance(transform.position, pointA.position) < 0.5f 
				&& currentPoint == pointA)
			{
				currentPoint = pointB;
			}
			if(rb.linearVelocity.z > 0.5f || rb.linearVelocity.z < -0.5f)

			{
				float b = transform.position.x - 5f;
			float a = transform.position.x + 5f;
			pointB.position = new Vector3(b, transform.position.y, transform.position.z);
			pointA.position = new Vector3(a, transform.position.y, transform.position.z);
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(other.tag == "Player")

		{
			float b = transform.position.x - 5f;
			float a = transform.position.x + 5f;
			pointB.position = new Vector3(b, transform.position.y, transform.position.z);
			pointA.position = new Vector3(a, transform.position.y, transform.position.z);
		}
	}
}