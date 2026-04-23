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
	float animSpeed = 0.3f;
	bool stay = false;
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
	public float jumpForce;
	public playerMovement plrMove;
	public bool jump;
	public bool dead;
	float posX;
	float posZ;
	float r;
	public CapsuleCollider caps;
	float airTime = 1;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		if(GetComponent<Animator>() != null)

		{
			anim = GetComponent<Animator>();
		}
		currentPoint = pointB;
		Player = GameObject.Find("player").transform;
		plrMove = Player.GetComponent<playerMovement>();
	}

	void FixedUpdate()
	{
		transform.localScale =new Vector3(1, 1, 1);
		grounded = Physics.Raycast(transform.position, Vector3.down,
		playerHeight * 0.5f + 0.2f, whatIsGround);
		plrDis = Vector3.Distance(transform.position, Player.position) <= 10f;
		if(stay)
		{
			float Angles = Mathf.SmoothDampAngle(transform.eulerAngles.y, 180, ref r, animSpeed);
			transform.rotation = Quaternion.Euler(transform.rotation.x, Angles, transform.rotation.z);
		}
		if(jump)
			{
				rb.position = new Vector3(posX, transform.position.y, posZ);
				rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
				airTime -= Time.deltaTime;
				if(grounded && airTime <= 0f)
				{
					jump = false;
					Debug.Log("jump = " + jump);
					caps.isTrigger = false;
				}
				if(rb.linearVelocity.y > jumpForce)

				{
					rb.linearVelocity = new Vector3(0f, jumpForce, 0f);	
				}
			}
		if (!grounded) return;
		{
			if(!plrDis)
			{
				orientation.LookAt(currentPoint.position);

				Vector3 moveDir = orientation.forward * speed;
				rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
				

				if (Vector3.Distance(transform.position, pointB.position) < 0.5f &&
					currentPoint == pointB)
				{
					speed = 0;
					currentPoint = pointA;
					StartCoroutine(stayOnPlace());
					stay = true;
				}

				if (Vector3.Distance(transform.position, pointA.position) < 0.5f &&
					currentPoint == pointA)
				{
					StartCoroutine(stayOnPlace());
					stay = true;
					speed = 0;
					currentPoint = pointB;
				}
			}else

			{
				if(!jump)
				orientation.LookAt(Player.position);
				Shoot();
			}
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
	/*private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.CompareTag("Player") && plrMove.groundSlide && !jump && !dead)

		{
			airTime = 1f;
			jump = true;
			posX = transform.position.x;
			posZ = transform.position.z;
			rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
			Rigidbody plrRb = Player.GetComponent<Rigidbody>();
			plrRb.linearVelocity = new Vector3(plrRb.linearVelocity.x, 0f, plrRb.linearVelocity.z);
			Debug.Log("jump = " + jump);
			caps.isTrigger = true;
		}
	}*/
	IEnumerator stayOnPlace()

	{
		yield return new WaitForSeconds(5);
		stay = false;
		speed = 4;
	}
	}