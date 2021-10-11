using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public static UpgradeSpawner Instance { get; private set; }

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private GameObject[] upgradePrefabs;

    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void SpawnUpgrades()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(upgradePrefabs[i], spawnPoints[i].transform.position, Quaternion.identity);
        }
    }
    public void DestroyUpgrades()
    {
        Upgrade[] upgrades = (Upgrade[])FindObjectsOfType(typeof(Upgrade));

        for (int i = 0; i < upgrades.Length; i++)
        {
            Destroy(upgrades[i].gameObject);
        }

        GameManager.Instance.NextWave();
    }
}
