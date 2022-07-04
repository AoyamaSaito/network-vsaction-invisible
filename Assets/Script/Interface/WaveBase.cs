using UnityEngine;

/// <summary>
/// Eventのクラスに継承させる基底クラス
/// </summary>
public abstract class WaveBase : MonoBehaviour
{
    /// <summary>/// イベント開始時に呼ぶ関数/// </summary>
    public virtual void WaveStart()
    {
        //処理を書く
    }
}
