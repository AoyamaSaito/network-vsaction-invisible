using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveData", order = 1)]
public class WaveData : ScriptableObject
{
    //private static WaveData instance;
    //public static WaveData Instance => instance;
    //private WaveData() { }

    [SerializeField]
    private List<WaveBase> waves;
    public List<WaveBase> Waves
    {
        get => waves;
        set => waves = value;
    }

}
