using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {
    public float spawnInterval = 1.0f;
    public float waveTime = 10f;
    public List<GameObject> enemies;
    public GameObject smokeSpawn;
    public int enemyCount = 0;
    public GameObject chestMessage;
    public int wave = 0;

    enum EnemyType
    {
        Rat,
        Skeleton,
        Imp,
        Demon
    };

    int spawnCount = 5;
	List<GameObject> spawnPoints;
	ChestController chest;
    Vector3 spawnPosition;
    
	void Start ()
	{
		chest = GameObject.FindGameObjectWithTag ("Chest").GetComponent<ChestController>();
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
					waveTime = 15;
                    chest.collectable = true;
                    chestMessage.SetActive(true);
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
                        SpawnEnemy(0);
						spawnCount--;
						spawnInterval = 1f;
                        enemyCount++;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 1:
                if (!chest.collectable)
                {
                    chestMessage.SetActive(false);
                }
				if(waveTime < 0 || (Input.GetKey("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 20;
                    chest.collectable = true;
                    chestMessage.SetActive(true);
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
						SpawnEnemy(spawnCount % 2);
						spawnCount--;
						spawnInterval = 1f;
                        enemyCount++;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
			case 2:
                if (!chest.collectable)
                {
                    chestMessage.SetActive(false);
                }
                if(waveTime < 0 || (Input.GetKey("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 25;
                    chest.collectable = true;
                    chestMessage.SetActive(true);
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
						SpawnEnemy(spawnCount % 3);
						spawnCount--;
						spawnInterval = 1f;
                        enemyCount++;
					}
				}
				else
				{
					spawnInterval -= Time.deltaTime;
					break;
				}
				break;
            case 3:
                if (!chest.collectable)
                {
                    chestMessage.SetActive(false);
                }
				SpawnEnemy(3);
				SpawnEnemy(0);
				SpawnEnemy(0);
				SpawnEnemy(0);
				SpawnEnemy(2);
				SpawnEnemy(2);
				enemyCount = enemyCount + 6;
				wave++;
				break;
			case 4:
				//if(enemyCount == 0)
					//Application.LoadLevel(2);
				break;
		}
	}
    void SpawnEnemy(int type)
    {
        spawnPosition = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].transform.position;
        Instantiate(smokeSpawn, spawnPosition, Quaternion.Euler(new Vector2(0, 0)));
        Instantiate(enemies[type], spawnPosition, Quaternion.Euler(new Vector2(0, 0)));
    }
}
