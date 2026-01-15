using UnityEngine;

public class likeNextBots : MonoBehaviour
{
	public Camera mainCamera;
	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 camPos = mainCamera.transform.position;
		
		transform.LookAt(camPos);
		
		transform.Rotate(0f, 180f, 0f);
	}
}
