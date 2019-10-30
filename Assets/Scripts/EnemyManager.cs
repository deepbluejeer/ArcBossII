using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    GameObject enemy;
    int randomenemy;
    int randomposition;
    int timeDiffcult;
    int sortRandom;
    bool waitSpawn;
    public static EnemyManager sharedInstance;
    public bool playerDead;
    int enemiesSpawned;

    // Use this for initialization
    void Start()
    {
        sharedInstance = GetComponent<EnemyManager>();
        enemy = GetComponent<GameObject>();
        timeDiffcult = 3;
        sortRandom = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemiesSpawned);
        
        if (enemiesSpawned > 25)
        {
            sortRandom = 20;
        }

        else if (enemiesSpawned > 15)
        {
            timeDiffcult = 1;
        }

        else if (enemiesSpawned > 5)
        {
            timeDiffcult = 2;
        }

        if (!waitSpawn)
        StartCoroutine(SpawnRandomly());
    }

    IEnumerator SpawnRandomly()
    {
        if (!playerDead)
        {
            waitSpawn = true;
            randomenemy = Random.Range(0, sortRandom);
            randomposition = Random.Range(0, spawnPoints.Length);

            switch (randomenemy)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    enemy = ObjectPooler.sharedInstance.GetPooledObject("Easy");
                    break;

                case 5:
                case 6:
                case 7:
                case 8:
                case 18:
                case 19:
                    enemy = ObjectPooler.sharedInstance.GetPooledObject("Medium");
                    break;

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    enemy = ObjectPooler.sharedInstance.GetPooledObject("Hard");
                    break;
            }

            enemy.transform.position = spawnPoints[randomposition].transform.position;
            enemy.gameObject.SetActive(true);

            yield return new WaitForSeconds(timeDiffcult);
            enemiesSpawned++;
            waitSpawn = false;
        }
    }
}
