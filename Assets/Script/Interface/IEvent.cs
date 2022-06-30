using UnityEngine;

/// <summary>
/// Eventのクラスに継承させるインターフェイス
/// </summary>
public abstract class EventBase : MonoBehaviour
{
    /// <summary>/// イベント開始時に呼ばれる関数/// </summary>
    public virtual void EventStart()
    {
        //処理を書く
    }
}
