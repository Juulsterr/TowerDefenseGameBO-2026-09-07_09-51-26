using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


[System.Serializable]
public class WaveData
{
    public float duration = 10f;
    public int easyEnemies = 5;
    public int hardEnemies = 2;
    public int fastEnemies = 1;
    public int bossEnemy = 1;
    public int bombEnemies = 1;
    public bool isDone = false;
}
public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI WaveText;
    public WaveData[] waves;
    public Button startButton;
    public GameObject easyEnemyPrefab;
    public GameObject hardEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject bossEnemyPrefab;
    public GameObject bombEnemyPrefab;
    public Transform[] wayPoints;

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
            // waves[currentWaveIndex].isDone = true;
            return;
        } 
            
        
        StartCoroutine(RunWave());

    }

    IEnumerator RunWave()
    {
        waveRunning = true;
        startButton.interactable = false;
        WaveData wave = waves[currentWaveIndex];
        int amountToSpawn = wave.easyEnemies + wave.hardEnemies + wave.fastEnemies + wave.bossEnemy + wave.bombEnemies;

        for(int i = 0; i < wave.easyEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(easyEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.easyEnemies);
        }

        for(int i = 0; i < wave.hardEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.hardEnemies);
        }

        for(int i = 0; i < wave.fastEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(fastEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.fastEnemies);
        }

        for(int i = 0; i < wave.bossEnemy; i++)
        {
            amountToSpawn--;
            SpawnEnemy(bossEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.bossEnemy);
        }

        for(int i = 0; i < wave.bombEnemies; i++)
        {
            amountToSpawn--;
            SpawnEnemy(bombEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.bombEnemies);
        }

        {
            wave.isDone = true;
        }
        yield return new WaitForSeconds(wave.duration / 3);
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
        enemy.waypoints = wayPoints;
    }
}
