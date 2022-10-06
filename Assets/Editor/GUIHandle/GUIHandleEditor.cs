using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GUIHandle))]
public class GUIHandleEditor : Editor
{
    void OnSceneGUI()
    {
        var t = target as GUIHandle;
        var start = t.transform.TransformPoint(t.StartPos);
        var end = t.transform.TransformPoint(t.EndPos);
        using (var cc = new EditorGUI.ChangeCheckScope())
        {
            start = Handles.PositionHandle(start, Quaternion.AngleAxis(180, t.transform.up) * t.transform.rotation);
            Handles.Label(start, "Start");
            Handles.Label(end, "End");
            end = Handles.PositionHandle(end, t.transform.rotation);
            if (cc.changed)
            {
                Undo.RecordObject(t, "Move Handles");
                t.StartPos = t.transform.InverseTransformPoint(start);
                t.EndPos = t.transform.InverseTransformPoint(end);
            }
        }
        Handles.color = Color.yellow;
        Handles.DrawDottedLine(start, end, 5);
        Handles.Label(Vector3.Lerp(start, end, 0.5f), "Distance:" + (end - start).magnitude);
    }
}
