using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため
using System.IO;
using UnityEditor;

public class EventManager : MonoBehaviour
{
    [SerializeField, Tooltip("イベントで送るメッセージ")] 
    private string m_message = "メッセージ";
    [SerializeField, Tooltip("イベントのフォルダパス")]
    private string _path = "Assets/Script/Event";

    private float _timer = 0;
    private float _testCount = 4;
    private EventBase[] _events;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        FindEventsAsset(_path);
    }

    private List<EventBase> FindEventsAsset(string directoryPath)
    {
        List<EventBase> assets = new List<EventBase>();
        var fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

        foreach (var fileName in fileNames)
        {
            var asset = AssetDatabase.LoadAssetAtPath<EventBase>(fileName);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _testCount)
        {
            Raise();
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
