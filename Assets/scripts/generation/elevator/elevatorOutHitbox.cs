using System.Collections;
using UnityEngine;

public class elevatorOutHitbox : MonoBehaviour
{
   public Animator animator;
	public BoxCollider doorBox;
	public Transform startPos;
	public Transform elevator;
	public GameObject allElevator;
	public elevatorMove elev;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if(elevator.position == startPos.position)

		{
			Destroy(allElevator);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		
		{
			animator.SetTrigger("close");
			doorBox.isTrigger = false;
			StartCoroutine(DectroyElevator());
			Debug.Log("elevator is moving down");
		}
	}
	
	IEnumerator DectroyElevator()

	{
		yield return new WaitForSeconds(3);
		elev.up = false;
	}
	
}
