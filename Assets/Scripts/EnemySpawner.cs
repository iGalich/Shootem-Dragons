using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject enemyPrefab;

    private int randomAmount;
    private int enemyAmountMax = 2;

    public int EnemyAmountMax { get => enemyAmountMax; set => enemyAmountMax = value; }
    public GameObject EnemyPrefab { get => enemyPrefab; set => enemyPrefab = value; }
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        enemyPrefab.GetComponent<Enemy>().Health = enemyPrefab.GetComponent<Enemy>().MinHealth;
    }
    public void SpawnEnemy()
    {
        randomAmount = Random.Range(1, enemyAmountMax + 1);
        bool[] checks = new bool[spawnPoints.Length];
        Enemy.EnemyCount = randomAmount;
        for (; randomAmount > 0; randomAmount--)
        {
            var r = Random.Range(1, spawnPoints.Length);
            if (checks[r])
            {
                randomAmount++;
                continue;
            }
            checks[r] = true;

            Instantiate(enemyPrefab, spawnPoints[r].transform.position, Quaternion.identity);
        }
    }
}
