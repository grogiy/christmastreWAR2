using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
	public Slider hp;
	// Update is called once per frame
	void Update()
	{
		hp.value -= Time.deltaTime * 2f;
	}
	public void heal(float heal)

	{
		hp.value += heal;
	}
	public void TakeDamage(float damage)

	{
		hp.value -= damage;
	}
}

