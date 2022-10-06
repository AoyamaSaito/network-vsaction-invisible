using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GUITest : GUIHandle
{
    private Vector3 _start;
    private Vector3 _end;

    private void Start()
    {
        _start = StartPos;
        _end = EndPos;
    }
}
