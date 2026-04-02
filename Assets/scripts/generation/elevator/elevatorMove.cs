using UnityEngine;

public class elevatorMove : MonoBehaviour
{
	public float timer;
	public Transform endPos;
	float dis;
	float speed;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		dis = transform.position.y - endPos.position.y;
		dis = Mathf.Abs(dis);
		speed = dis / timer;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
	}
}
