using UnityEngine;

public class eyeScript : MonoBehaviour
{
	public Rigidbody playerRb;
	//public Transform direction;
	Vector3 direction;
	public Transform plr;
	public float force;
	public bool attract = false;
	float timeStamp;
	public playerMovement playerMovement;
	public Animator animator;
	public CapsuleCollider collide;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
		Invoke(nameof(Attracting), 1.1f);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
    void FixedUpdate()
    {
        if(attract)

		{
			direction = -(plr.position - transform.position).normalized;
			playerRb.linearVelocity += (Time.time / timeStamp) * force * new Vector3(direction.x, direction.y, direction.z);
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
}
