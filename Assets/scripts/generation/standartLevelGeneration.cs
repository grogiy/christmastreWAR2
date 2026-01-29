using System.Collections.Generic;
using UnityEngine;

public class standartLevelGeneration : MonoBehaviour
{
	public List<GameObject> regularLvlParts;
	public List<GameObject> turningLvlParts;
	public List<GameObject> generatedParts;
	public int howManyParts;
	Transform continuePoint;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		continuePoint = transform;
		Generate();
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	
	private void Generate()
	{
		string lastTurnTag = "";
		for(int madeParts = 0; madeParts < howManyParts; madeParts++)
		{
			bool placeTurn = Random.Range(0f, 1f) > 0.5f;
			if(placeTurn && turningLvlParts.Count > 0)

			{
				
				int nextTurningPart = Random.Range(0,turningLvlParts.Count);
				int attempts = 0;
				int maxAttempts = 100;
				while(turningLvlParts[nextTurningPart].tag == lastTurnTag && attempts < maxAttempts)
				{
					nextTurningPart = Random.Range(0,turningLvlParts.Count);
					attempts++;
				}
				GameObject generatedTurn = Instantiate(turningLvlParts[nextTurningPart], continuePoint.position, continuePoint.rotation);
				Transform nextContinue = generatedTurn.transform.Find("continue");
				if(nextContinue != null)
				{
					continuePoint = nextContinue;
					generatedParts.Add(generatedTurn);
				}
				lastTurnTag = generatedTurn.tag;
				attempts = 0;
			} else
			{
				int nextRegularPart = Random.Range(0, regularLvlParts.Count);
				GameObject generatedPart = Instantiate(regularLvlParts[nextRegularPart], continuePoint.position, continuePoint.rotation);
				Transform nextContinue = generatedPart.transform.Find("continue");
				if(nextContinue != null)
				{
					continuePoint = nextContinue;
					generatedParts.Add(generatedPart);
				}
				lastTurnTag = "";
			}
		}
	}
}
