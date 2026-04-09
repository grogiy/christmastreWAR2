using UnityEngine;

public class elevatorMove : MonoBehaviour
{
	public float timer;
	public Transform endPos;
	public Transform startPos;
	float dis;
	float speed;
	public bool up;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		dis = transform.position.y - endPos.position.y;
		dis = Mathf.Abs(dis);
		speed = dis / timer;
		up = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (up)

		{
			transform.position = Vector3.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
		}else

		{
			transform.position = Vector3.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
		}
	}
		
}
