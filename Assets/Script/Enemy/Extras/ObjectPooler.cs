using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;//对象池中的对象
    [SerializeField] private int poolSize = 10;

    private List<GameObject> _pool;
    private GameObject _poolContainer;//对象池容器(方便管理刷新不同种类敌人)

    private void Awake()
    {
        _pool = new List<GameObject>();//初始化对象池
        _poolContainer = new GameObject($"Pool - {prefab.name}");//创建一个对象池容器
        CreatePooler();//创建对象池(总的敌人数量)
    }


    //创建对象池
    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInsatnce());
        }
    }

    //创建对象实例
    private GameObject CreateInsatnce() { 
        GameObject newinstance = Instantiate(prefab, transform);
        newinstance.transform.SetParent(_poolContainer.transform);//将对象放入对象池容器
        newinstance.SetActive(false);
        return newinstance;
    }

    //从对象池中获取对象
    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        return CreateInsatnce();
    }

    public static void ReturnToPool(GameObject instance) 
    { 
        instance.SetActive(false);
    }







}
