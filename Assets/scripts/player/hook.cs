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
	
	public Transform hookObj;
	private Vector3 currentHookingPoint;
	
	public playerMovement plr;

	public float animTime;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		lr.SetPosition(1, hookTip.position);
	}
	//IEnumerator GroundCheck()

	//{
		//yield return new WaitForSeconds(1);
		//if(plr.grounded)
		
		//{
			//StopHook();
		//}
	//}
	// Update is called once per frame
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
		
		if(hooking)

		{
			hookTip.LookAt(hookPoint);
		}
		
		
	}

	void FixedUpdate()
	{
		if(forcing)
		{ 
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
		
		if(Physics.Raycast(cam.position, cam.forward, out hit, maxHookDistance, hookAble))

		{
			//StartCoroutine(GroundCheck());
			hookPoint = hit.point;
			
			forcing = true;
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
		
		rb.AddForce(hookTip.forward * hookForce * Time.deltaTime, ForceMode.VelocityChange);
		
	}
	private void StopHook()

	{
		hooking = false;
		
		hookCdTimer = hookCd;
		
		forcing = false;
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
