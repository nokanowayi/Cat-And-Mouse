using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

    [SerializeField] private Vector3[] points;//存储路线点
    public Vector3[] Points => points;//传递路线点
    

    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;//传递路线点的位置

    void Start()
    {

        _currentPosition = transform.position;
    }

    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.2f);

            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1]);
            }
        }
    }



}
