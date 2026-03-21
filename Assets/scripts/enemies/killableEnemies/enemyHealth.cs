using Unity.VisualScripting;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
	public int enemyHp = 3;
	Rigidbody rb;
	public EnemyPatrol enemy;
	// Update is called once per frame
	void Start ()

	{
		rb = GetComponent<Rigidbody>();
	}
	void Update()
	{
		if(enemyHp <= 0)

		{
			enemy.caps.isTrigger = false;
			rb.freezeRotation = false;
			enemy.enabled = false;
			enemy.dead = true;
			//Destroy(gameObject);
		}
	}
	public void TakeDamage(int damage)

	{
		enemyHp -= damage;
	}
}
