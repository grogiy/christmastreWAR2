using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
	public float sensX;
	public float sensY;
	public float smoothSpeed;

	float xRotation;
	float yRotation;
	float currentX;
	float currentY;

	public Transform Orientation;
	public playerMovement player;

	bool firstFrame = false;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		// Получаем реальные углы камеры на старте
		Vector3 initialRot = transform.eulerAngles;
		xRotation = initialRot.x;
		yRotation = initialRot.y;

		// На старте current = target
		currentX = xRotation;
		currentY = yRotation;

		// Жёстко фиксируем камеру первый кадр
		transform.rotation = Quaternion.Euler(currentX, currentY, 0f);
	}

	void LateUpdate()
	{
		float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

		if (Mathf.Abs(mouseY) < 0.001f) mouseY = 0f;

		
		if(!firstFrame)
		{
			mouseX = 0f;
			mouseY = 0f;
			StartCoroutine(waitASec());	
		}else

		{
			xRotation -= mouseY;
			yRotation += mouseX;
		}
		xRotation = Mathf.Clamp(xRotation, -80f, 80f);

		// Первый кадр — сразу ставим rotation без Lerp
		
		currentX = Mathf.Lerp(currentX, xRotation, smoothSpeed * Time.deltaTime);
		currentY = Mathf.Lerp(currentY, yRotation, smoothSpeed * Time.deltaTime);
		

		transform.rotation = Quaternion.Euler(currentX, currentY, 0f);

		if (!player.sliding)
			Orientation.rotation = Quaternion.Euler(0f, currentY, 0f);
	}
	
	IEnumerator waitASec()

	{
		yield return new WaitForSeconds(0.1f);
		firstFrame = true;
	}
}