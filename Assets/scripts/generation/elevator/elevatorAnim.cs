using UnityEngine;

public class elevatorAnim : MonoBehaviour
{
	public Animator animator;
	public BoxCollider doorBox;
	public Transform endPos;
	public Transform elevator;
	bool open = false;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		animator.SetTrigger("default");
	}

	// Update is called once per frame
	void Update()
	{
		if(elevator.position == endPos.position && !open)

		{
			animator.SetTrigger("open");
			open = true;
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		
		{
			animator.SetTrigger("close");
			doorBox.isTrigger = false;
		}
	}
	
}
