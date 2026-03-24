using System.Collections;
using UnityEngine;

public class hook : MonoBehaviour
{
	public AudioSource tongue;
	public Transform cam;
	public Transform hookTip;
	public LayerMask hookAble;
	public float maxHookDistance;
	public float hookDelayTime;
	private Vector3 hookPoint;
	public float hookCd;
	public float hookCdTimer;
	public KeyCode hookKey;
	public bool hooking;
	public LineRenderer lr;
	public Rigidbody rb;
	public float hookForce;
	private bool forcing;
	bool enemy;
	bool brain;
	BoxCollider brainColl;
	Rigidbody brainRb;
	Transform enemyPos;
	Transform target;
	
	public Transform hookObj;
	private Vector3 currentHookingPoint;
	
	public playerMovement plr;

	public float animTime;
	Vector3 direction;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		lr.SetPosition(1, hookTip.position);
	}
	void Update()
	{
		if(Input.GetKeyDown(hookKey))

		{
			StartHook();
		}
		if(Input.GetKeyUp(hookKey))

		{
			StopHook();
		}
		
		if(hookCdTimer > 0)

		{
			hookCdTimer -= Time.deltaTime;
		}
		if(enemy || brain)

		{
			hookPoint = enemyPos.position;
		}
		if(brain)

		{
			enemyPos.position = Vector3.MoveTowards(enemyPos.position, target.position, hookForce * Time.deltaTime * 15f);
		}
	}

	void FixedUpdate()
	{
		if(forcing)
		{ 
			plr.moveSpeed = 7f;
			direction = -(hookTip.position - hookPoint).normalized;
			ExecuteHook();
		}
		
	}

	private void LateUpdate()
	{
		DrawRope();
	}

	private void StartHook()

	{
		if(hookCdTimer > 0) return;
		
		hooking = true;
		
		RaycastHit hit;
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
		
		if(Physics.Raycast(/*cam.position, cam.forward + Vector3.up * 0.1f*/ray, out hit, maxHookDistance, hookAble))

		{
			if(hit.transform.tag == "enemy")

			{
				enemyPos = hit.transform;
				enemy = true;
				forcing = true;
			} else

			{
				hookPoint = hit.point;
			
				forcing = true;
				Debug.Log(hit.transform.tag);
			}
			if(hit.transform.tag == "brain")

			{
				brain = true;
				enemyPos = hit.transform;
				target = transform;
				forcing = false;
				brainColl = hit.transform.GetComponent<BoxCollider>();
				brainRb = hit.transform.GetComponent<Rigidbody>();
				brainRb.linearVelocity = new Vector3(0, 0, 0);
				brainRb.useGravity = false;
				brainColl.isTrigger = true;
			}
			//rb.linearVelocity = new Vector3(0, 0, 0);
		} else

		{
			hookPoint = cam.position + cam.forward * maxHookDistance;
			Invoke(nameof(StopHook), hookDelayTime);
		}
		tongue.pitch = Random.Range(0.5f, 3f);
		tongue.Play();
		currentHookingPoint = hookTip.position;
	}
	
	private void ExecuteHook()

	{
		
		rb.linearVelocity += (Time.time / Time.time) * hookForce * new Vector3(direction.x, direction.y, direction.z);
		
	}
	public void StopHook()

	{
		hooking = false;
		
		hookCdTimer = hookCd;
		
		forcing = false;
		
		target = null;
		
		plr.moveSpeed = 10f;
		enemy = false;
		brain = false;
		if(brainColl != null && brainRb != null)

		{
			brainColl.isTrigger = false;
			brainRb.useGravity = true;
			brainColl = null;
			brainRb = null;
		}
	}

	public Vector3 GetHookPoint()

	{
		return hookPoint;
	}
	
	public void DrawRope()

	{
		if(!hooking)

		{
			lr.enabled = false;
			
			lr.SetPosition(1, hookTip.position);
			return;
		}
		currentHookingPoint = Vector3.Lerp(currentHookingPoint, GetHookPoint(), Time.deltaTime * 20);
		lr.enabled = true;
		
		lr.SetPosition(0, hookTip.position);
		
		
		lr.SetPosition(1, currentHookingPoint);
	}
	
}
