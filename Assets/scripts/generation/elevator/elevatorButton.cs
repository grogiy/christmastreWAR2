using UnityEngine;

public class elevatorButton : MonoBehaviour
{
	public KeyCode button;
	public GameObject elevator;
	// Update is called once per frame
	void Update()
	{
		
	}
	 void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")

		{
			if(Input.GetKey(button))

			{
				elevator.SetActive(true);
				Debug.Log("elevator working");
			}
		}
	}
	
}
