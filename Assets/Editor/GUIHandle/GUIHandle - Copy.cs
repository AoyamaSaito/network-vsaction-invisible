using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUIHandle : MonoBehaviour 
{
    [Header("���W�w��")]
    [SerializeField]
    public Vector3 StartPos;
    [SerializeField]
    public Vector3 EndPos = new Vector3(0, 1, 0);

}

