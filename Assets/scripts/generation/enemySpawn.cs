using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
	EnemyPatrol kes;
	public List<GameObject> enemies;
	Transform pointA;
	Transform pointB;
	Transform spawnPoint;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		GameObject clone;
		pointA = transform.Find("pointA");
		pointB = transform.Find("pointB");
		spawnPoint = transform.Find("spawnPoint");
		GameObject enemyIndex;
		enemyIndex = enemies[Random.Range(0, enemies.Count)];
		clone = Instantiate(enemyIndex, spawnPoint.position, transform.rotation);
		clone.transform.parent = transform;
		kes = clone.GetComponent<EnemyPatrol>();
		kes.pointA = pointA;
		kes.pointB = pointB;
	}
}
