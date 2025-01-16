using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum SpawnModes
{
    Fixed,
    Random
}

[System.Serializable]
public class WaveConfig
{
    public int enemyCount; // 每小波的敌人数量
    public float delayBtwWaves; // 每一小波的间隔
}

public class Spawner : MonoBehaviour
{
    [Header("出怪设置")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    //[SerializeField] private int enemyCount = 10; //每一小波次出发敌人数量
    //[SerializeField] private float delayBtwWaves; //出怪小波次间隔
    [SerializeField] private WaveConfig[] waveConfigs; // 所有大波次的配置

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns; //每一小波的出怪间隔


    [Header("Random Delay")]
    [SerializeField] private float minRandomeDelay; //最小出怪间隔
    [SerializeField] private float maxRandomeDelay; //最大出怪间隔

    [Header("所有波次")]
    [SerializeField] private ObjectPooler enemyWave1;
    [SerializeField] private ObjectPooler enemyWave2;
    [SerializeField] private ObjectPooler enemyWave3;
    public int _currentWave = 0;
    //public int delayBtwAllWaves = 1; //所有波次间隔
    public int CurrentWaveLeavedEnemies;//当前波次剩余敌人数量

    private float _spawnTimer; //出怪计时器
    private int _spawnedEnemyCount; //已经出怪数量
    public int _enemiesRemaining; //剩余敌人数量
    [Header("游戏设置")]
    [SerializeField] private int maxEnemies = 50; // 最大敌人数量

    private WayPoints _waypoints;



    private ObjectPooler GetPooler()
    {
        if (_currentWave ==0)
        {
            return enemyWave1;
        }
        else if (_currentWave == 1)
        {
            
            return enemyWave2;
        }
        else if (_currentWave >= 2)
        {
            
            return enemyWave3;
        }
        return null;
    }

    private ObjectPooler GetWave()
    {
        if (_currentWave <= 1)
        {
            return enemyWave2;
        }
        else if (_currentWave == 2)
        {
            return enemyWave3;
        }
        else if (_currentWave >= 3)
        {
            return enemyWave3;
        }
        return null;
    }



    private void Start()
    {
        CurrentWaveLeavedEnemies = GetPooler().AllenemyCount;
        _enemiesRemaining = waveConfigs[_currentWave].enemyCount;
        _spawnTimer = GetSpawnDelay();
        _waypoints = GetComponent<WayPoints>();
    }



    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)  
        {
            _spawnTimer = GetSpawnDelay();
            if (_spawnedEnemyCount < waveConfigs[_currentWave].enemyCount)
            {
                SpawnEnemy();
                _spawnedEnemyCount++;
                CheckEnemyCount();
            }
        }
        

    }

    private void CheckEnemyCount()
    {
        if (maxEnemies==0)
        {
            Time.timeScale = 0;
        }
    }

    //出怪
    private void SpawnEnemy()
    {
        
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy1 enemy1 = newInstance.GetComponent<Enemy1>();
        enemy1.WayPoints = _waypoints;
        enemy1.ResetEnemy();

        enemy1.transform.localPosition = transform.position;

        newInstance.SetActive(true);
    }


    


    //根据出怪模式获取出怪间隔
    private float GetSpawnDelay()
    {
        float delay = 0f;
        switch (spawnMode)
        {
            case SpawnModes.Fixed:
                delay = delayBtwSpawns;
                break;
            case SpawnModes.Random:
                delay = GetRandomDelay();
                break;
        }
        return delay;
    }
    //随机出怪间隔
    private float GetRandomDelay()
    {
        return Random.Range(minRandomeDelay, maxRandomeDelay);//随机出怪间隔
    }



    //开始下一波出怪
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveConfigs[_currentWave].delayBtwWaves);
        if (CurrentWaveLeavedEnemies == 0)
        {
            _currentWave++;
            if (_currentWave < waveConfigs.Length)
            {
                CurrentWaveLeavedEnemies = GetWave().AllenemyCount;
                _enemiesRemaining = waveConfigs[_currentWave].enemyCount;
                _spawnedEnemyCount = 0;
                _spawnTimer = 0f;

            }

        }
        
    }
    private void RecordEnemyEndReached()
    {
        _enemiesRemaining--;
        CurrentWaveLeavedEnemies--;
        maxEnemies--;
        if (_enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }


    private void OnEnable()
    {
        Enemy1.OnEndReached += RecordEnemyEndReached;
        Enemy1.OnEnemyDeath += RecordEnemyEndReached;
    }

    private void OnDisable()
    {
        Enemy1.OnEndReached -= RecordEnemyEndReached;
        Enemy1.OnEnemyDeath -= RecordEnemyEndReached;
    }






}
