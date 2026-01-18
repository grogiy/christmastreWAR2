using UnityEngine;

public class likeNextBots : MonoBehaviour
{
	private Camera mainCamera;
	void Start()

	{
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	// Update is called once per frame
	void LateUpdate()
	{
		Vector3 camPos = mainCamera.transform.position;
		
		transform.LookAt(camPos);
		
		transform.Rotate(0f, 180f, 0f);
	}
}
