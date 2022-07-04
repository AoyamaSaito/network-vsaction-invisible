using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData
{
    private static WaveData instance;
    public static WaveData Instance => instance;
    private WaveData() { }

    private static List<WaveBase> waves;
    public static List<WaveBase> Waves => waves;
}
