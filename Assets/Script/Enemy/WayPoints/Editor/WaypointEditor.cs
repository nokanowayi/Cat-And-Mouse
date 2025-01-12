using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WayPoints))]
public class WaypointEditor : Editor
{
    WayPoints WayPoints => target as WayPoints;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        for (int i = 0; i < WayPoints.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //创建控制点的位置
            Vector3 currentWaypointPoint = WayPoints.Points[i] + WayPoints.CurrentPosition;
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, 0.1f, new Vector3(0.2f, 0.2f, 0.2f), Handles.SphereHandleCap);

            //创建控制点的顺序显示
            GUIStyle textstyle = new GUIStyle();
            textstyle.fontSize = 15;
            textstyle.fontStyle = FontStyle.Bold;
            textstyle.normal.textColor = Color.yellow;
            Vector3 textALLigment = Vector3.down * 0.1f + Vector3.right * 0.1f;
            Handles.Label(WayPoints.CurrentPosition + WayPoints.Points[i]+textALLigment, $"{i+1}", textstyle);

            EditorGUI.EndChangeCheck();


            //如果控制点发生变化，记录变化并且更新控制点的位置
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(WayPoints, "Change Waypoint Position");
                WayPoints.Points[i] = newWaypointPoint - WayPoints.CurrentPosition;
            }
        }
    }
}
