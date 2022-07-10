using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : ScriptableObject
{
    private static WaveData instance;
    public static WaveData Instance => instance;
    private WaveData() { }

    private List<WaveBase> waves;
    public List<WaveBase> Waves
    {
        get => waves;
        set => waves = value;
    }

}
