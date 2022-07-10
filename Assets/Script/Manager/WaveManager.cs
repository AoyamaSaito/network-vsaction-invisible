using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaveManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Tooltip("Waveを発生させる秒数")]
    private float _testCount = 4;
    [SerializeField, Tooltip("Panelのアニメーター")]
    private Animator _uiPanel;
    [SerializeField, Tooltip("Waveが入ったフォルダのパス")]
    private string _wavePath = "Assets\\Resorces\\Event";
    [SerializeField, Tooltip("Waveが入ったDataを保存するフォルダのパス")]
    private string _savePath = "Assets\\Resorces\\Event";

    private List<WaveBase> _events;
    private float _timer = 0;
    private string _message = "メッセージ";
    private bool _isWave = false;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _isWave = false;

        _events = new List<WaveBase>();
#if UNITY_EDITOR
        _events = FindEventsAsset(_wavePath);
#endif
    }

#if UNITY_EDITOR
    private List<WaveBase> FindEventsAsset(string directoryPath)
    {
        var obj = ScriptableObject.CreateInstance<WaveData>();
        //List<WaveBase> assets = new List<WaveBase>();
        var fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

        foreach (var fileName in fileNames)
        {
            var asset = AssetDatabase.LoadAssetAtPath<WaveBase>(fileName);
            if (asset != null)
            {
                obj.Waves.Add(asset);
            }
        }

        return obj.Waves;
    }
#endif

    private void Update()
    {
        TestWaveTimer();
    }

    private void TestWaveTimer()
    {
        if (_isWave == false)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _testCount)
        {
            EventRaise();
            _isWave = true;
            _timer = 0;
        }
    }

    /// <summary>
    /// イベントを起こす
    /// </summary>
    public void EventRaise()
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="photonEvent"></param>
    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        Debug.Log("OnEvent");
        //イベント
        if (photonEvent.Code == 3)
        {
            int i = UnityEngine.Random.Range(0, _events.Count - 1);
            Debug.Log(_events[i].name);
            PlayWave(_events[i]);
        }
    }

    /// <summary>///Waveを発生させる関数/// </summary>///<param name="eventBase">発生させるWave</param>
    public void PlayWave(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        // AnimatorからObservableStateMachineTriggerの参照を取得
        ObservableStateMachineTrigger trigger =
            _uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

        // Stateの終了イベント
        IDisposable exitState = trigger
            .OnStateExitAsObservable()
            .Subscribe(onStateInfo =>
            {
                AnimatorStateInfo info = onStateInfo.StateInfo;

                if (info.IsName("Base Layer.PanelAnimation"))
                {
                    eventBase.WaveStart();
                    _isWave = false;
                }
            }).AddTo(this);
    }
}
