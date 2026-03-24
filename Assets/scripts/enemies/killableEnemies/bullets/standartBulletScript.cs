using System.Collections;
using UnityEngine;

public class standartBulletScript : MonoBehaviour
{
	public float speed;
	health plrHP;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		StartCoroutine(DestroyAfterTime());
	}
	IEnumerator DestroyAfterTime()

	{
		yield return new WaitForSeconds(50f);
		Destroy(this.gameObject);
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")

		{
			plrHP = other.transform.parent.GetComponent<health>();
			plrHP.TakeDamage(34f);
		}
	}
}
