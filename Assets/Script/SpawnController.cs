using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	private List<GameObject> spawnPoints;
	private int wave = 0;
	private int spawnCount = 5;
	public List<GameObject> enemies;
	public float spawnInterval = 1.0f;
	public float waveTime = 10f;

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
				if(waveTime < 0 || (Input.GetKeyDown("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 30;
					break;
				}
				else
				{
					waveTime -= Time.deltaTime;
				}
				if(spawnInterval < 0)
				{
					if(spawnCount > 0)
					{
						Instantiate(enemies[0], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(new Vector2(0,0)));
						spawnCount--;
						spawnInterval = 1f;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 1:
				if(waveTime < 0 || (Input.GetKey("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 30;
					break;
				}
				else
				{
					waveTime -= Time.deltaTime;
				}
				if(spawnInterval < 0)
				{
					if(spawnCount > 0)
					{
						Instantiate(enemies[spawnCount % 2], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(new Vector2(0,0)));
						spawnCount--;
						spawnInterval = 1f;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 2:
				if(spawnInterval < 0)
				{
					if(spawnCount > 0)
					{
						Instantiate(enemies[spawnCount % 3], spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position, Quaternion.Euler(new Vector2(0,0)));
						spawnCount--;
						spawnInterval = 1f;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			//case 3:
				//break;
		}
	}
}
