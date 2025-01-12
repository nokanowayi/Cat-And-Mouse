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
    [SerializeField] private GameObject testGo; //敌人预制体
    [SerializeField] private int enemyCount = 10; //出发敌人数量

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns; //出怪间隔

    [Header("Random Delay")]
    [SerializeField] private float minRandomeDelay; //最小出怪间隔
    [SerializeField] private float maxRandomeDelay; //最大出怪间隔

    private float _spawnTimer; //出怪计时器
    private int _spawnedEnemyCount; //已经出怪数量

    private ObjectPooler _pooler;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _spawnTimer = GetSpawnDelay();
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


    
    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool();
        //Instantiate(newInstance, transform.position, Quaternion.identity);
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












}
