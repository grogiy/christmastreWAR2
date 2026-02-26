using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
  [Header("Movement")]
  public float moveSpeed;
  
  public float groundDrag;
  
  public float jumpForce;
  public float jumpCooldown;
  public float airMultiplier;
  bool readyToJump = true;
  
  public Animator hand;
  
  public hook hook;
	float punchCd;
	public float punchCdTime;
  public float slideForce;
  
  [Header("KeyBinds")]
  public KeyCode jumpKey;
  public KeyCode slideKey;
  public KeyCode punchKey;
  
  [Header("GroundCheck")]
  public float playerHeight;
  public LayerMask whatIsGround;
  public bool grounded;
  bool wasOnGround;
  public bool sliding;
  public Transform orientation;
  public GameObject playerObj;
  
  Vector3 MoveDirection;
  
  Vector3 slideDirection;
  Rigidbody rb;
  
  float Horizontal;
  float Vertical;
  
  void Start()

	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
	}
	void Update()

	{
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
		
		if (grounded && !sliding && !hook.hooking || hook.hooking && grounded)

		{
			rb.linearDamping = groundDrag;
		} else

		{
			rb.linearDamping = 0;
		}
		
		if (grounded)

		{
			wasOnGround = true;
		}
		MyInput();
		
		if(punchCd > 0)

		{
			punchCd -= Time.deltaTime;
		}
		
		SpeedControl();
		
		
	}

	void FixedUpdate()
	{
		MovePlayer();
	}
	private void MyInput()

	{
		Horizontal = Input.GetAxis("Horizontal");
		Vertical = Input.GetAxis("Vertical");
		
		if(Input.GetKey(jumpKey) && readyToJump && grounded)

		{
			readyToJump = false;
			
			Jump();
			
			Invoke(nameof(ResetJump), jumpCooldown);
		}
		
		if(Input.GetKeyDown(slideKey) && wasOnGround && !hook.hooking)

		{
			sliding = true;
			Slide();
		} 
		
		if(Input.GetKeyUp(slideKey))

		{
			wasOnGround = false;
			sliding = false;
			playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, 1f, playerObj.transform.localScale.z);
		}
		
		if(Input.GetKeyDown(punchKey))

		{
			Punch();
		}
	}
	
	private void MovePlayer()

	{
		MoveDirection = orientation.forward * Vertical + orientation.right * Horizontal;
		if (grounded && !sliding)

		{
			rb.AddForce(MoveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
		} else if (!grounded && !sliding)

		{
			rb.AddForce(MoveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
		}
		
	}
	
	private void SpeedControl()

	{
		Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
		
		if(flatVel.magnitude > moveSpeed && !sliding && !hook.hooking && grounded)

		{
			Vector3 limitedVel = flatVel.normalized * moveSpeed;
			rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
		}
	}
	
	private void Jump()

	{
		rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
		
		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}
	
	public void ResetJump()

	{
		readyToJump = true;
	}
	
	public void Slide()

	{
		float vertVel = rb.linearVelocity.y;
		if(vertVel < 0f)

		{
			vertVel = -vertVel;
		}
		playerObj.transform.localScale = new Vector3(playerObj.transform.localScale.x, 0.62395f, playerObj.transform.localScale.z);
		rb.AddForce(orientation.forward * (slideForce + vertVel), ForceMode.Impulse);
	}
	
	public void Punch()

	{
		if(punchCd > 0)return;
		hand.SetTrigger("punch");
		punchCd = punchCdTime;
	}
}