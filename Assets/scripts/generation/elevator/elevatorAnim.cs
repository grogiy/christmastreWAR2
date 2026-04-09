using System.Collections;
using UnityEngine;

public class elevatorAnim : MonoBehaviour
{
	public Animator animator;
	public BoxCollider doorBox;
	public Transform endPos;
	public Transform elevator;
	bool open = false;
	standartLevelGeneration lvlGen;
	GameObject spawn;
	public GameObject outHitbox;
	public GameObject hitbox;
	public GameObject platform;
	public GameObject button;
	public Transform con;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		animator.SetTrigger("default");
		spawn = GameObject.Find("spawnPoint");
		lvlGen = spawn.GetComponentInChildren<standartLevelGeneration>();
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
			StartCoroutine(NextLvl());
		}
	}
	
	IEnumerator NextLvl()

	{
		yield return new WaitForSeconds(1);
		lvlGen.DestroyLvl();
		Destroy(button);
		Destroy(platform);
		yield return new WaitForSeconds(5);
		outHitbox.SetActive(true);
		animator.SetTrigger("open");
		doorBox.isTrigger = true;
		spawn.transform.position = con.position;
		spawn.transform.rotation = con.rotation;
		lvlGen.Generate();
	}
	
}
