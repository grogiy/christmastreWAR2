using UnityEngine;

public class brain : MonoBehaviour
{
	public BoxCollider box;
	health plrHP;
	hook hook;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	void OnCollisionEnter(Collision collision)
	{
		
	}
	void OnCollisionExit(Collision collision)
	{
		
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")

		{
			plrHP = other.transform.parent.GetComponent<health>();
			hook = other.transform.parent.GetComponent<hook>();
			plrHP.heal(25f);
			hook.StopHook();
			Destroy(gameObject);
		}
	}	
}
