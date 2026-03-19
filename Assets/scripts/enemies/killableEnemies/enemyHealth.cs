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
			rb.freezeRotation = false;
			enemy.enabled = false;
			//Destroy(gameObject);
		}
	}
	public void TakeDamage(int damage)

	{
		enemyHp -= damage;
	}
}
