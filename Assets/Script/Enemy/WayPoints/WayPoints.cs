﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

    [SerializeField] private Vector3[] points;//路线点
    public Vector3[] Points => points;//传递路线点


    private Vector3 _currentPosition;
    public Vector3 CurrentPosition => _currentPosition;//传递路线点位置


    void Start()
    {

        _currentPosition = transform.position;
    }


    //美化路线点
    private void OnDrawGizmos()
    {

        for (int i = 0; i < points.Length; i++)
        {
            //绘制路线点
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.6f);

            //绘制路线点之间的连线
            if (i < points.Length - 1)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1]);
            }
        }
    }


    //为敌人传递路线点的位置
    public Vector3 GetWaypointPosition(int index)
    {
        return Points[index] + CurrentPosition;
    }
}
