using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private bool godMode;

    private int numOfWaves = 3;
    private int currWave = 1;

    private float waveTimerDefault = 30f;
    private float waveTimer;
    private float waveTimeIncrease = 15f;

    private bool functionTimerCreated;
    private bool spawnStarted;

    public bool GodMode => godMode;
    public bool SpawnStarted { get => spawnStarted; set => spawnStarted = value; }
    public bool FunctionTimerCreated { set => functionTimerCreated = value; }
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        waveTimer = waveTimerDefault;
    }
    private void Update()
    {
        waveTimer -= Time.deltaTime;

        if (waveTimer > 0 && Enemy.EnemyCount == 0 && !functionTimerCreated)
        {
            functionTimerCreated = true;
            FunctionTimer.Create(() => EnemySpawner.Instance.SpawnEnemy(), 2f);
        }

        if (waveTimer <= 0 && Enemy.EnemyCount == 0 && !spawnStarted)
        {
            spawnStarted = true;
            UpgradeSpawner.Instance.SpawnUpgrades();
        }
    }
    public void NextWave()
    {
        currWave++;
        if (currWave <= numOfWaves)
        {
            EnemySpawner.Instance.EnemyPrefab.GetComponent<Enemy>().Health++;
            EnemySpawner.Instance.EnemyAmountMax++;
            waveTimer = waveTimerDefault;
            waveTimer += waveTimeIncrease * (currWave - 1);
            spawnStarted = false;
        }
        else
        {
            //transition to boss scene
        }

    }
}
