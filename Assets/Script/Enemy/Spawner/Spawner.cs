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

    [SerializeField] private GameObject testGo; //敌人预制体
    [SerializeField] private int enemyCount = 10; //出发敌人数量

    [SerializeField] private float delayBtwSpawns; //出怪间隔
    [SerializeField] private float minRandomeDelay; //最小出怪间隔
    [SerializeField] private float maxRandomeDelay; //最大出怪间隔

    private float _spawnTimer; //出怪计时器
    private int _spawnedEnemyCount; //已经出怪数量

    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetRandomDelay();
            if (_spawnedEnemyCount < enemyCount)
            {
                SpawnEnemy();
                _spawnedEnemyCount++;
            }
        }



    }


    
    private void SpawnEnemy()
    {
        Instantiate(testGo, transform.position, Quaternion.identity);
    }

    private float GetRandomDelay()
    {
        return Random.Range(minRandomeDelay, maxRandomeDelay);//随机出怪间隔
    }












}
