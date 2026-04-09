using System.Collections.Generic;
using UnityEngine;

public class standartLevelGeneration : MonoBehaviour
{
	public List<GameObject> regularLvlParts;
	public List<GameObject> turningLvlParts;
	public List<GameObject> generatedParts;
	public List<Bounds> occupiedBounds = new List<Bounds>();
	public int howManyParts;
	public LayerMask partsMask;
	Transform continuePoint;
	public GameObject elevator;
	GameObject prefab;
	public string enemyTag;
	public string brainTag;

	void Start()
	{
		continuePoint = transform;
		Generate();
	}

	void Update() { }

	// Проверка пересечения
	bool CanPlace(Bounds newBounds)
	{
		foreach (Bounds b in occupiedBounds)
		{
			if (b.Intersects(newBounds))
				return false;
		}
		return true;
	}

	public void Generate()
	{
		string lastTurnTag = "";

		for (int madeParts = 0; madeParts < howManyParts; madeParts++)
		{
			bool isLast = madeParts == howManyParts - 1;
			bool placeTurn = Random.value > 0.5f; // решаем: поворот или прямая
			List<GameObject> partsList = placeTurn ? turningLvlParts : regularLvlParts;

			int attempts = 0;
			int maxAttempts = 100;

			while (attempts < maxAttempts)
			{

				if (isLast)
				{
					prefab = elevator;
				}
				else
				{
					prefab = partsList[Random.Range(0, partsList.Count)];
				}
				 

				// защита от одинаковых поворотов подряд
				if (placeTurn && prefab.tag == lastTurnTag)
				{
					attempts++;
					continue;
				}
				 
				// создаём временный объект
				GameObject temp = Instantiate(prefab, continuePoint.position, continuePoint.rotation);

				// берём реальные bounds после спавна
				BoxCollider box = temp.GetComponentInChildren<BoxCollider>();
				if (box == null)
				{
					Debug.LogError("No BoxCollider in prefab: " + prefab.name);
					Destroy(temp);
					attempts++;
					continue;
				}

				Bounds newBounds = box.bounds;

				// проверяем пересечение
				if(isLast || CanPlace(newBounds))
				{
					// всё ок, оставляем объект
					occupiedBounds.Add(newBounds);
					temp.GetComponent<BoxCollider>().enabled = false;

					Transform next = temp.transform.Find("continue");
					if (next != null)
					{
						continuePoint = next;
					}
					else if (!isLast)
					{
						Debug.LogError("У части нет continue: " + temp.name);
					}

					if(!isLast)

					{
						generatedParts.Add(temp);
					}else
					{
						/*for(int i = partsList.Count - 1; i >= 0; i--)

						{
							partsList.RemoveAt(i);
						}*/
					}

					if (placeTurn)
						lastTurnTag = prefab.tag;
					else
						lastTurnTag = "";

					break; // нашли комнату, выходим из while
				}
				else
				{
					// пересеклось, удаляем объект
					Destroy(temp);
					attempts++;
					if(attempts > 60 && generatedParts.Count > 0 && occupiedBounds.Count > 0)

					{
						continuePoint = generatedParts[generatedParts.Count - 2].transform.Find("continue");
						Destroy(generatedParts[generatedParts.Count - 1]);
						generatedParts.RemoveAt(generatedParts.Count - 1);
						occupiedBounds.RemoveAt(occupiedBounds.Count - 1);
						Debug.LogWarning("Не удалось поставить комнату после 60 попыток");
					}
				}
			}

			if (attempts >= maxAttempts)
			{
				Debug.LogWarning("Не удалось поставить комнату после " + maxAttempts + " попыток");
				Debug.Log(madeParts);
				break;
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (Bounds b in occupiedBounds)
			Gizmos.DrawWireCube(b.center, b.size);
	}
	public void DestroyLvl()

	{
		continuePoint = transform;
		for(int i = generatedParts.Count - 1; i >= 0; i--)

		{
			Destroy(generatedParts[i].gameObject);
			generatedParts.RemoveAt(i);
		}
		for(int i = occupiedBounds.Count - 1; i >= 0; i--)

		{
			occupiedBounds.RemoveAt(i);
		}
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		GameObject[] brains = GameObject.FindGameObjectsWithTag(brainTag);
		foreach(GameObject obj in enemies)

		{
			Destroy(obj);
			if(obj == null)

			{
				
				Debug.Log("no tag enemy");
			}
		}
		foreach(GameObject obj in brains)

		{
			Destroy(obj);
			if(obj == null)

			{
				Debug.Log("no tag brain");
			}
		}
	}
}