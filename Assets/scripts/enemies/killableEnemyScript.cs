using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random; 

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
	public Transform Player;
	bool plrDis;
	public GameObject bullet;
	public float bulletSpeed;
	float gunCd;
	public float maxGunCd;
	

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		currentPoint = pointB;
		//anim.SetBool("isRunning", true);
		Player = GameObject.Find("player").transform;
	}

	void FixedUpdate()
	{
		grounded = Physics.Raycast(transform.position, Vector3.down,
		playerHeight * 0.5f + 0.2f, whatIsGround);
		plrDis = Vector3.Distance(transform.position, Player.position) <= 10f;

	if (!grounded) return;
		
		if(!plrDis)
		{
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
		}else

		{
			orientation.LookAt(Player.position);
			//Vector3 Direction = new Vector3.LookAt(Player.position);
			//Instantiate(bullet, orientation.position, orientation.rotation);
			Shoot();
		}
	}
	private void Shoot()

	{
		gunCd -= Time.deltaTime;
		if(gunCd <= 0f)

		{
			bullet = Instantiate(bullet, orientation.position, orientation.rotation);
			Vector3 shootDir = (Player.transform.position - orientation.position).normalized;
			bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-5f, 5f), Vector3.up) * shootDir * bulletSpeed;
			gunCd = maxGunCd;
		}
	}
	
}