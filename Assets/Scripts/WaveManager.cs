using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


[System.Serializable]
public class WaveData
{
    [SerializeField]
    private float duration = 10f;
    public float Duration 
    { 
        get { return duration; } 
        set { duration = value; }
    } // Publieke getter en setter voor de duur van de golf
    [SerializeField]
    private int easyEnemies = 5;
    public int EasyEnemies
    {
        get { return easyEnemies; }
        set { easyEnemies = value; }
    }
    [SerializeField]
    private int hardEnemies = 2;
    public int HardEnemies
    {
        get { return hardEnemies; }
        set { hardEnemies = value; }
    }
    [SerializeField]
    private int fastEnemies = 1;
    public int FastEnemies
    {
        get { return fastEnemies; }
        set { fastEnemies = value; }
    }
    [SerializeField]
    private int bossEnemy = 1;
    public int BossEnemy
    {
        get { return bossEnemy; }
        set { bossEnemy = value; }
    }
    [SerializeField]
    private int bombEnemies = 1;
    public int BombEnemies
    {
        get { return bombEnemies; }
        set { bombEnemies = value; }
    }
    [SerializeField]
    private bool isDone = false;
    public bool IsDone
    {
        get { return isDone; }
        set { isDone = value; }
    } // Publieke getter en setter voor de status van de golf (afgerond of niet)
}
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI WaveText;
    public TextMeshProUGUI waveText
    {
        get { return WaveText; }
        set { WaveText = value; }
    } // Publieke getter en setter voor het tekstveld dat de huidige golf toont
    [SerializeField]
    private WaveData[] waves;
    public WaveData[] Waves
    {
        get { return waves; }
        set { waves = value; }
    } // Publieke getter en setter voor de golven
    [SerializeField]
    private Button startButton;
    public Button StartButton
    {
        get { return startButton; }
        set { startButton = value; }
    } // Publieke getter en setter voor de startknop van de golf
    [SerializeField]
    private GameObject easyEnemyPrefab;
    public GameObject EasyEnemyPrefab
    {
        get { return easyEnemyPrefab; }
        set { easyEnemyPrefab = value; }
    } // Publieke getter en setter voor het prefab van de makkelijke vijand
    [SerializeField]
    private GameObject hardEnemyPrefab;
    public GameObject HardEnemyPrefab
    {
        get { return hardEnemyPrefab; }
        set { hardEnemyPrefab = value; }
    } // Publieke getter en setter voor het prefab van de moeilijke vijand
    [SerializeField]
    private GameObject fastEnemyPrefab;
    public GameObject FastEnemyPrefab
    {
        get { return fastEnemyPrefab; }
        set { fastEnemyPrefab = value; }
    } // Publieke getter en setter voor het prefab van de snelle vijand
    [SerializeField]
    private GameObject bossEnemyPrefab;
    public GameObject BossEnemyPrefab
    {
        get { return bossEnemyPrefab; }
        set { bossEnemyPrefab = value; }
    } // Publieke getter en setter voor het prefab van de baasvijand
    [SerializeField]
    private GameObject bombEnemyPrefab;
    public GameObject BombEnemyPrefab
    {
        get { return bombEnemyPrefab; }
        set { bombEnemyPrefab = value; }
    } // Publieke getter en setter voor het prefab van de bomvijand
    [SerializeField]
    private Transform[] wayPoints;
    public Transform[] WayPoints
    {
        get { return wayPoints; }
        set { wayPoints = value; }
    } // Publieke getter en setter voor de waypoints van de vijanden

    private int currentWaveIndex = 0;
    private bool waveRunning = false;
       void Start()
    {
        startButton.onClick.AddListener(StartWave);
    }
    public void StartWave()
    {
        if (waveRunning) return;
        if(currentWaveIndex >= waves.Length)
        {
            // waves[currentWaveIndex].IsDone = true;
            return;
        } 
            
        
        StartCoroutine(RunWave());

    }

    IEnumerator RunWave()
    {
        waveRunning = true;
        startButton.interactable = false;
        WaveData wave = waves[currentWaveIndex];
        int amountToSpawn = wave.EasyEnemies + wave.HardEnemies + wave.FastEnemies + wave.BossEnemy + wave.BombEnemies;

        for(int i = 0; i < wave.EasyEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(easyEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.EasyEnemies);
        }

        for(int i = 0; i < wave.HardEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.HardEnemies);
        }

        for(int i = 0; i < wave.FastEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(fastEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.FastEnemies);
        }

        for(int i = 0; i < wave.BossEnemy; i++)
        {
            amountToSpawn--;
            SpawnEnemy(bossEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.BossEnemy);
        }

        for(int i = 0; i < wave.BombEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(bombEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.BombEnemies);
        }

        {
            wave.IsDone = true;
        }
        yield return new WaitForSeconds(wave.Duration / 3);
        waveRunning = false;
        startButton.interactable = true;
        currentWaveIndex++;
        WaveText.text = (currentWaveIndex + 1).ToString();
    }

    // private void Update()
    // {
    //     Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
    //     if(waveRunning == true && enemies.Length == 0)
    //     {
    //         waveRunning = false;
    //         startButton.interactable = true;
    //         currentWaveIndex++;
    //     }
    // }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.Waypoints = wayPoints;
    }
}
