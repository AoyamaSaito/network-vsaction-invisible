using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork を使うため
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup を使うため
using ExitGames.Client.Photon;  // SendOptions を使うため
using System;

public class WaveManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Tooltip("Waveを発生させる秒数")]
    private float _testCount = 4;
    [SerializeField, Tooltip("Panelのアニメーター")]
    private Animator _uiPanel;
    [SerializeField, Tooltip("WaveData")]
    private WaveData _waveData;

    private float _timer = 0;
    private string _message = "メッセージ";
    private bool _isWave = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        GameManager.Instance.OnEventGameManager += OnWaveEvent;
        _isWave = false;
    }


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
    /// Waveを発生させるイベント
    /// </summary>
    /// <param name="photonEvent"></param>
    private void OnWaveEvent(EventData photonEvent)
    {
        //イベント
        if (photonEvent.Code == 3)
        {
            Debug.Log("OnWave");
            int i = UnityEngine.Random.Range(0, _waveData.Waves.Count);
            Debug.Log(_waveData.Waves[i].name);
            PlayWave(_waveData.Waves[i]);
        }
    }

    /// <summary>///Waveを発生させる関数/// </summary>///<param name="eventBase">発生させるWave</param>
    public void PlayWave(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        // AnimatorからObservableStateMachineTriggerの参照を取得
        ObservableStateMachineTrigger trigger =_uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

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
