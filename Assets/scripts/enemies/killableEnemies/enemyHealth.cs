using Unity.VisualScripting;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
	public int enemyHp = 3;
	Rigidbody rb;
	public EnemyPatrol enemy;
	bool dead;
	public GameObject brain;
	public Transform headPos;
	public float brainForce;
	Transform plr;
	// Update is called once per frame
	void Start ()

	{
		rb = GetComponent<Rigidbody>();
		plr = GameObject.Find("player").transform;
	}
	void Update()
	{
		if(enemyHp <= 0 && !dead)

		{
			enemy.caps.isTrigger = false;
			rb.freezeRotation = false;
			enemy.enabled = false;
			enemy.dead = true;
			GameObject brainNew = Instantiate(brain, headPos.position, headPos.rotation);
			Rigidbody brainRb = brainNew.GetComponent<Rigidbody>();
			brainRb.AddForce(Vector3.up * brainForce, ForceMode.Impulse);
			dead = true;
			//Destroy(gameObject);
		}
	}
	public void TakeDamage(int damage)

	{
		enemyHp -= damage;
	}
	
}
