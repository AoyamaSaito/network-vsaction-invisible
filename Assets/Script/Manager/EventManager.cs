using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため
using System.IO;
using UnityEditor;
using System;

public class EventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Tooltip("イベントで送るメッセージ")] 
    private string _message = "メッセージ";
    [SerializeField, Tooltip("イベントのフォルダパス")]
    private string _path = "Assets/Script/EventTest.prefabs";
    [SerializeField]
    private List<EventBase> _events;

    private float _timer = 0;
    private float _testCount = 2;

    private void Awake()
    {
        Init();
        //Debug.Log(Array.ForEach(_events, i => i));
    }

    private void Init()
    {
        Debug.Log(_events[0].name);
        //_events = FindEventsAsset(_path);
    }

    //private List<EventBase> FindEventsAsset(string directoryPath)
    //{
    //    List<EventBase> assets = new List<EventBase>();
    //    var fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

    //    foreach (var fileName in fileNames)
    //    {
    //        var asset = AssetDatabase.LoadAssetAtPath<EventBase>(fileName);
    //        if (asset != null)
    //        {
    //            assets.Add(asset);
    //        }
    //    }

    //    return assets;
    //}

    private void Update()
    {
        Debug.Log(_timer);
        _timer += Time.deltaTime;

        if(_timer >= _testCount)
        {
            //Debug.Log("イベント発生");
            Raise();
            _timer = 0;
        }
    }

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
        PhotonNetwork.RaiseEvent(eventCode, _message, raiseEventOptions, sendOptions);
    }

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        Debug.Log("OnEvent");
        //イベント
        if (photonEvent.Code == 3)
        {
            //int n = UnityEngine.Random.Range(0, _events.Count - 1);
            Debug.Log(_message);
            _events[0].EventStart();
        }
    }
}
