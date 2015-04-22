using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {
	
	public List<GameObject> enemies;
	public GameObject smokeSpawn;
	public float spawnInterval = 1.0f;
	public float waveTime = 10f;
	public int enemyCount = 0;

	private List<GameObject> spawnPoints;
	private int wave = 0;
	private int spawnCount = 5;
	private ChestController chest;
	private bool opened;
    private Vector3 spawnPosition;

    private enum EnemyType {
        Rat,
        Skeleton,
        Imp,
		Demon
    };

	// Use this for initialization
	void Start ()
	{
		chest = GameObject.FindGameObjectWithTag ("Chest").GetComponent<ChestController>();
		opened = true;
		spawnPoints = new List<GameObject> ();
		GameObject[] array = GameObject.FindGameObjectsWithTag ("SpawnPoint");
		for(int i = 0; i < array.Length; i++)
		{
			spawnPoints.Add(array[i]);
		}
	}

	// Update is called once per frame
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
				if(opened)
				{
					chest.CloseChest();
					opened = false;
				}
				chest.collectable = true;
				if(waveTime < 0 || (Input.GetKey("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 10;
					opened = true;
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
				if(opened)
				{
					chest.CloseChest();
					opened = false;
				}
				chest.collectable = true;
				if(waveTime < 0 || (Input.GetKey("space") && spawnCount == 0))
				{
					wave++;
					spawnCount = 10;
					waveTime = 10;
					opened = true;
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
				if(opened)
				{
					chest.CloseChest();
					opened = false;
				}
				chest.collectable = true;
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
				if(enemyCount == 0)
					Application.LoadLevel(2);
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
