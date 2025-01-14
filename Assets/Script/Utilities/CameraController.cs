using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
       instance = this; 
    }

    public Vector2 targetPosition;
    public int targetSize;

    public void TurnCameraPositon(Vector3 targetPos)
    {
        targetPosition = targetPos;
        transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
    }

    public void TurnCameraSize(int size)
    {
        targetSize = size;
        Camera.main.orthographicSize = targetSize;
    }

    public void TurnBackCameraPositon()
    {
        transform.position = new Vector3(0,0,-10);
    }
}