using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Track : MonoBehaviour
{
	public GameObject[] obstacles;
	public Vector2 numberOfObstacles;
	
	public GameObject coin;
	public Vector2 numberOfCoins;

	public GameObject magnet;
	public Vector2 numberOfmagnet;

	public GameObject speedUp;
	public Vector2 numberOfspeedUp;

	public GameObject AsiCard;
	public Vector2 numberOfAsiCard;


	public List<GameObject> newObstacles;
	public List<GameObject> newCoins;
	public List<GameObject> newMagnets;
	public List<GameObject> newSpeedUps;
	public List<GameObject> newAsiCards;

	// Use this for initialization
	void Start()
	{

		int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
		int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);
		int newnumberOfmagnet = (int)Random.Range(numberOfmagnet.x, numberOfmagnet.y);
		int newnumberOfspeedUp= (int)Random.Range(numberOfspeedUp.x, numberOfspeedUp.y);
		int newNumberOfAsiCard = (int) Random.Range(numberOfAsiCard.x, numberOfAsiCard.y);

		for (int i = 0; i < newNumberOfObstacles; i++)
		{
			newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
			newObstacles[i].SetActive(false);
		}

		for (int i = 0; i < newNumberOfCoins; i++)
		{
			newCoins.Add(Instantiate(coin, transform));
			newCoins[i].SetActive(false);
		}

		for (int i = 0; i < newnumberOfmagnet; i++)
		{
			newMagnets.Add(Instantiate(magnet, transform));
			newMagnets[i].SetActive(false);
		}

		for (int i = 0; i < newnumberOfspeedUp; i++)
		{
			newSpeedUps.Add(Instantiate(speedUp, transform));
			newSpeedUps[i].SetActive(false);
		}
		
		for (int i = 0; i < newNumberOfAsiCard; i++)
		{
			newAsiCards.Add(Instantiate(AsiCard, transform));
			newAsiCards[i].SetActive(false);
		}


		PositionateObstacles();
		PositionateCoins();
		PositionateMagnets();
		PositionateSpeedUps();
		PositionateAsiCards(); 
	}

	void PositionateObstacles()
	{
		for (int i = 0; i < newObstacles.Count; i++)
		{
			float posZMin = (116f / newObstacles.Count) + (116f / newObstacles.Count) * i;
			float posZMax = (116f / newObstacles.Count) + (116f / newObstacles.Count) * i + 1;
			newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            newObstacles[i].SetActive(true);
            if (newObstacles[i].GetComponent<ChangeLane>() != null)
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
        }
    }

	void PositionateCoins()
	{
		float minZPos = 10f;
		for (int i = 0; i < newCoins.Count; i++)
		{
			float maxZPos = minZPos + 5f;
			float randomZPos = Random.Range(minZPos, maxZPos);
			newCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y+1, randomZPos);
			newCoins[i].SetActive(true);
			newCoins[i].GetComponent<ChangeLane>().PositionLane();
			minZPos = randomZPos + 1;
		}
	}

	void PositionateMagnets()
	{
			float minZPos = 50f;
            float maxZPos = minZPos + 5f;
            float randomZPos = Random.Range(minZPos, maxZPos);
            newMagnets[0].transform.localPosition = new Vector3(transform.position.x, transform.position.y + 1, randomZPos);
            newMagnets[0].SetActive(true);
            if (newMagnets[0].GetComponent<ChangeLane>() != null)
                newMagnets[0].GetComponent<ChangeLane>().PositionLane();
    }

	void PositionateSpeedUps() 
	{
		float minZPos = 100f;
		float maxZPos = minZPos + 5f;
		float randomZPos = Random.Range(minZPos, maxZPos);
		newSpeedUps[0].transform.localPosition = new Vector3(transform.position.x, transform.position.y + 1, randomZPos);
		newSpeedUps[0].SetActive(true);
		if (newSpeedUps[0].GetComponent<ChangeLane>() != null)
			newSpeedUps[0].GetComponent<ChangeLane>().PositionLane();
	}

	void PositionateAsiCards()
	{
		float minZPos = 100f;
		float maxZPos = minZPos + 5f;
		float randomZPos = Random.Range(minZPos, maxZPos);
		newAsiCards[0].transform.localPosition = new Vector3(transform.position.x, transform.position.y + 1, randomZPos);
		newAsiCards[0].SetActive(true);
		if (newAsiCards[0].GetComponent<ChangeLane>() != null)
			newAsiCards[0].GetComponent<ChangeLane>().PositionLane();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			transform.position = new Vector3(0, 0, transform.position.z + 116f * 2);
			PositionateObstacles();
			PositionateCoins();
			PositionateMagnets();
			PositionateSpeedUps();
			PositionateAsiCards();
		}
	}

}
