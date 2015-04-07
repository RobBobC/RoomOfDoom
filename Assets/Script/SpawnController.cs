using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	private List<GameObject> spawnPoints;
	private int wave = 0;
	private int spawnCount = 5;
	public List<GameObject> enemies;
	public float spawnInterval = 1.0f;

	void Start ()
	{
		spawnPoints = new List<GameObject> ();
		GameObject[] array = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		for(int i = 0; i < array.Length; i++)
		{
			spawnPoints.Add(array[i]);
		}
	}

	void Update ()
	{
		switch(wave)
		{
			case 0:
				if(spawnInterval < 0)
				{
					if(spawnCount > 0)
					{
						Instantiate(enemies[0], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(new Vector2(0,0)));
						spawnCount--;
						spawnInterval = 1f;
					}
					else
					{
						wave++;
						spawnCount = 10;
						break;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 1:
				if(spawnInterval < 0)
				{
					if(spawnCount > 0)
					{
						Instantiate(enemies[1], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(new Vector2(0,0)));
						spawnCount--;
						spawnInterval = 1f;
					}
					else
					{
						wave++;
						spawnCount = 10;
						break;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 2:
				wave++;
				break;
		}
	}
}
