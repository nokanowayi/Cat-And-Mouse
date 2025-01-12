using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum SpawnModes
{
    Fixed,
    Random
}


public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [Header("出怪设置")]
    [SerializeField] private int enemyCount = 10; //出发敌人数量
    [SerializeField] private float delayBtwWaves; //出怪波次间隔

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns; //出怪间隔


    [Header("Random Delay")]
    [SerializeField] private float minRandomeDelay; //最小出怪间隔
    [SerializeField] private float maxRandomeDelay; //最大出怪间隔

    private float _spawnTimer; //出怪计时器
    private int _spawnedEnemyCount; //已经出怪数量
    public int _enemiesRemaining; //剩余敌人数量

    private ObjectPooler _pooler;
    private WayPoints _waypoints;

    private void Start()
    {
        _enemiesRemaining = enemyCount;
        _pooler = GetComponent<ObjectPooler>();
        _spawnTimer = GetSpawnDelay();
        _waypoints = GetComponent<WayPoints>();
    }



    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_spawnedEnemyCount < enemyCount)
            {
                SpawnEnemy();
                _spawnedEnemyCount++;
            }
        }



    }


    //出怪
    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
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
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRemaining = enemyCount;
        _spawnedEnemyCount = 0;
        _spawnTimer = 0f;
    }
    private void RecordEnemyEndReached()
    {
        _enemiesRemaining--;
        if (_enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }


    private void OnEnable()
    {
        Enemy1.OnEndReached += RecordEnemyEndReached;
    }

    private void OnDisable()
    {
        Enemy1.OnEndReached -= RecordEnemyEndReached;
    }






}
