using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���Q�[�W��interface
/// </summary>
interface IGauge
{
    float Ratio { get; }
    void Gauge();
}
