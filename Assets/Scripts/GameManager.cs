using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AudioManager audioManager;
    public CameraManager cameraManager;
    public EnemyBulletPool enemyBulletPool;
    public BossBulletPool bossBulletPool;
    public EnemySpawner enemySpawner;
    public Hud hud;
    public Player player;
    public PlayerBulletsPool playerBulletPool;
    public PlayerMovement playerMovement;
    public UpgradeSpawner upgradeSpawner;
    public Boss boss;

    [SerializeField] private bool godMode;

    [SerializeField] private Light2D globalLight;

    private int numOfWaves = 3;
    private int currWave = 1;

    private float waveTimerDefault = 30f;
    private float waveTimer;
    private float waveTimeIncrease = 15f;

    private bool functionTimerCreated;
    private bool spawnStarted;
    private bool spawningEnemies;

    public bool SpawningEnemies { get => spawningEnemies; set => spawningEnemies = value; }
    public bool GodMode => godMode;
    public bool SpawnStarted { get => spawnStarted; set => spawnStarted = value; }
    public bool FunctionTimerCreated { set => functionTimerCreated = value; }
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            //Destroy(audioManager.gameObject);
            //Destroy(cameraManager.gameObject);
            //Destroy(enemyBulletPool.gameObject);
            //Destroy(enemySpawner.gameObject);
            //Destroy(hud.gameObject);
            //Destroy(player.gameObject);
            //Destroy(playerBulletPool.gameObject);
            //Destroy(playerMovement.gameObject);
            //Destroy(upgradeSpawner.gameObject);
            return;
        }

        FindObjects();

        SceneManager.sceneLoaded += LoadState;

        DontDestroyOnLoad(gameObject);
    }
    public void FindObjects()
    {
        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();
        if (cameraManager == null)
            cameraManager = FindObjectOfType<CameraManager>();
        if (enemyBulletPool == null)
            enemyBulletPool = FindObjectOfType<EnemyBulletPool>();
        if (enemySpawner == null)
            enemySpawner = FindObjectOfType<EnemySpawner>();
        if (hud == null)
            hud = FindObjectOfType<Hud>();
        if (player == null)
            player = FindObjectOfType<Player>();
        if (playerBulletPool == null)
            playerBulletPool = FindObjectOfType<PlayerBulletsPool>();
        if (playerMovement == null)
            playerMovement = FindObjectOfType<PlayerMovement>();
        if (upgradeSpawner == null)
            upgradeSpawner = FindObjectOfType<UpgradeSpawner>();
        if (boss == null)
            boss = FindObjectOfType<Boss>();
        if (bossBulletPool == null)
            bossBulletPool = FindObjectOfType<BossBulletPool>();
        if (globalLight == null)
            globalLight = GameObject.Find("GlobalLight2D").GetComponent<Light2D>();
    }
    public void LoadState (Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
    }
    private void OnLevelWasLoaded(int level)
    {
        switch (level)
        {
            case 0:
                break;
            default:
                GameManager.Instance.FindObjects();
                player.transform.position = GameObject.Find("SpawnPoint").transform.position;
                currWave = 1;
                GameManager.Instance.player.IsAlive = true;
                GameManager.Instance.player.Health = 3;
                GameManager.Instance.hud.DeathMenu.GetComponent<CanvasGroup>().alpha = 0;
                functionTimerCreated = false;
                Enemy.EnemyCount = 0;
                waveTimer = waveTimerDefault;
                ScoreManager.Instance.Score = 0;
                boss.gameObject.SetActive(false);
                spawnStarted = false;
                spawningEnemies = false;
                ScoreManager.Instance.Combo = 0;
                globalLight.color = new Color(176f / 255f, 176f / 255f, 176f / 255f);
                //boss.FireBossBullets.enabled = true;
                player.IsAlive = true;
                AudioManager.Instance.Play("MainTheme");
                AudioManager.Instance.ChangeVolume(0.5f, 5f);
                break;
        }
    }
    private void Start()
    {
        waveTimer = waveTimerDefault;
    }
    private void Update()
    {
        FindObjects();

        waveTimer -= Time.deltaTime;

        if (waveTimer > 0 && Enemy.EnemyCount == 0 && !functionTimerCreated)
        {
            spawningEnemies = true;
            functionTimerCreated = true;
            //FunctionTimer.Create(() => EnemySpawner.Instance.SpawnEnemy(), 2f);
            FunctionTimer.Create(() => GameManager.Instance.enemySpawner.SpawnEnemy(), 2f);
        }

        if (waveTimer <= 0 && Enemy.EnemyCount == 0 && !spawnStarted && !spawningEnemies)
        {
            spawnStarted = true;
            //UpgradeSpawner.Instance.SpawnUpgrades();
            GameManager.Instance.upgradeSpawner.SpawnUpgrades();
        }
    }
    public void NextWave()
    {
        currWave++;
        if (currWave <= numOfWaves)
        {
            GameManager.Instance.enemySpawner.EnemyPrefab.GetComponent<Enemy>().Health++;
            waveTimer = waveTimerDefault;
            waveTimer += waveTimeIncrease * (currWave - 1);
            spawnStarted = false;
        }
        else
        {
            AudioManager.Instance.ChangeVolume(0f, 1f);
            hud.ShowBlackScreen();
        }

    }
    public void ActivateBoss()
    {
        AudioManager.Instance.Play("BossTheme");
        AudioManager.Instance.ChangeVolume(0.5f, 3f);
        AudioManager.Instance.Play("BossRoar");
        boss.gameObject.SetActive(true);
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        globalLight.color = new Color(130f / 255f, 22f / 255f, 22f / 255f);
    }
}
