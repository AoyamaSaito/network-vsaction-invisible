using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため

public class EventManager : MonoBehaviour
{
    [SerializeField, Tooltip("イベントで送るメッセージ")] 
    private string m_message;
    [SerializeField, Tooltip("発生させるイベント")] 
    private IEvent[] _events;

    /// <summary>
    /// イベントを起こす
    /// </summary>
    public void Raise()
    {
        //イベントとして送るものを作る
        byte eventCode = 3; // イベントコード 0~199 まで指定できる。200 以上はシステムで使われているので使えない。
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,  // 全体に送る 他に MasterClient, Others が指定できる
        };  // イベントの起こし方
        SendOptions sendOptions = new SendOptions(); // オプションだが、特に何も指定しない

        // イベントを起こす
        PhotonNetwork.RaiseEvent(eventCode, m_message, raiseEventOptions, sendOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        //イベント
        if (photonEvent.Code == 3)
        {
            _events[UnityEngine.Random.Range(0, _events.Length - 1)].EventStart();
            
        }
    }
}
