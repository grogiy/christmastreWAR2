using System;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
   public float sensX;
	public float sensY;

	float xRotation;
	float yRotation;
	float currentX;
	float currentY;
	public float smoothSpeed;
	public Transform Orientation;
	
	public playerMovement player;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		currentX = xRotation;
		currentY = yRotation;
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	void LateUpdate()

	{
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		
		currentX = Mathf.Lerp(currentX, xRotation, smoothSpeed * Time.deltaTime);
		currentY = Mathf.Lerp(currentY, yRotation, smoothSpeed * Time.deltaTime);

		transform.rotation = Quaternion.Euler(currentX, currentY, 0);
		
		
		if(!player.sliding)

		{
			Orientation.rotation = Quaternion.Euler(0, currentY, 0);
		}
	}
	
}
