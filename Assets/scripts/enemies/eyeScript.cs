using System.Collections;
using UnityEngine;

public class eyeScript : MonoBehaviour
{
	Rigidbody playerRb;
	//public Transform direction;
	Vector3 direction;
	Transform plr;
	public float force;
	public bool attract = false;
	float timeStamp;
	playerMovement playerMovement;
	public Animator animator;
	public CapsuleCollider collide;
	public GameObject newEye;
	public GameObject oldEye;
	Transform orientation;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
		Invoke(nameof(Attracting), 1.1f);
		Invoke(nameof(Stopping), 6.1f);
		plr = GameObject.Find("player").transform;
		playerRb = plr.GetComponent<Rigidbody>();
		playerMovement = plr.GetComponent<playerMovement>();
		orientation = GameObject.Find("playerOrientation").transform;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	void FixedUpdate()
	{
		if(attract)

		{
			float elapsed = Time.time - timeStamp;
			elapsed = Mathf.Max(elapsed, 0.01f);
			direction = -(plr.position - transform.position).normalized;
			playerRb.linearVelocity += elapsed * force * new Vector3(direction.x, direction.y, direction.z);
			playerMovement.moveSpeed = 7;
			//playerRb.AddForce(direction.forward.normalized * force * Time.deltaTime, ForceMode.Impulse);
		}else

		{
			playerRb.linearVelocity = playerRb.linearVelocity;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")

		{
			attract = true;
			timeStamp = Time.time;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		
		{
			attract = false;
			playerMovement.moveSpeed = 14;
		}
	}

	private void Attracting()

	{
		collide.enabled = true;
		animator.SetTrigger("eyeAttack");
	}
	
	private void Stopping()

	{
		collide.enabled = false;
		attract = false;
		animator.SetTrigger("eyeStop");
		StartCoroutine(spawnNew());
		playerMovement.moveSpeed = 14;
	}
	IEnumerator spawnNew()

	{
		Vector3 distance = new Vector3(Random.Range(orientation.position.x + 5f, orientation.position.x + -5f), Random.Range(orientation.position.y + 5f, orientation.position.y + -5f), Random.Range(orientation.position.z + 5f, orientation.position.z + 10f));
		yield return new WaitForSeconds(1.1f);
		Destroy(oldEye);
		Vector3 spawnPos = distance;
		GameObject clone = Instantiate(newEye, spawnPos, oldEye.transform.rotation);
		eyeScript es = clone.GetComponentInChildren<eyeScript>();
		if(es != null) es.enabled = true;
		likeNextBots lnb = clone.GetComponentInChildren<likeNextBots>();
		if (lnb != null) lnb.enabled = true;
		Animator anim = clone.GetComponentInChildren<Animator>();
		if (anim != null) anim.enabled = true;
		clone.name = "eye";	
		oldEye = clone;
	}
}
