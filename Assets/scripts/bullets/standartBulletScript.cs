using System.Collections;
using UnityEngine;

public class standartBulletScript : MonoBehaviour
{
	public float speed;
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
}
