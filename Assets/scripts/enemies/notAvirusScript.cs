using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class notAvirusScript : MonoBehaviour
{
	public bool follow;
	GameObject plr;
	Rigidbody rb;
	float speed;
	public Transform virus; 
	Vector3 target;
	public float distance;
	public float keepDis;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		plr = GameObject.Find("player");
		rb = plr.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		distance = (plr.transform.position - virus.transform.position).magnitude;
		
		if(follow)


		{
			target = plr.transform.position;
			speed = (distance * 3f) - keepDis;
			//virus.transform.parent = plr.transform;
		}
		virus.position = Vector3.MoveTowards(virus.position, target, speed * Time.deltaTime);	
	}
}
