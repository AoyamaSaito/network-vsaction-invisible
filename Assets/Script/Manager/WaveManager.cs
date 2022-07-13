using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������
using System;

public class WaveManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Tooltip("Wave�𔭐�������b��")]
    private float _testCount = 4;
    [SerializeField, Tooltip("Panel�̃A�j���[�^�[")]
    private Animator _uiPanel;
    [SerializeField, Tooltip("WaveData")]
    private WaveData _waveData;

    private float _timer = 0;
    private string _message = "���b�Z�[�W";
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
    /// �C�x���g���N����
    /// </summary>
    public void EventRaise()
    {
        //�C�x���g�Ƃ��đ�����̂����
        byte eventCode = 3; // �C�x���g�R�[�h 0~199 �܂Ŏw��ł���B200 �ȏ�̓V�X�e���Ŏg���Ă���̂Ŏg���Ȃ��B
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,  // �S�̂ɑ��� ���� MasterClient, Others ���w��ł���
        };  // �C�x���g�̋N������
        SendOptions sendOptions = new SendOptions(); // �I�v�V���������A���ɉ����w�肵�Ȃ�

        // �C�x���g���N����
        PhotonNetwork.RaiseEvent(eventCode, _message, raiseEventOptions, sendOptions);
    }

    /// <summary>
    /// Wave�𔭐�������C�x���g
    /// </summary>
    /// <param name="photonEvent"></param>
    private void OnWaveEvent(EventData photonEvent)
    {
        //�C�x���g
        if (photonEvent.Code == 3)
        {
            Debug.Log("OnWave");
            int i = UnityEngine.Random.Range(0, _waveData.Waves.Count);
            Debug.Log(_waveData.Waves[i].name);
            PlayWave(_waveData.Waves[i]);
        }
    }

    /// <summary>///Wave�𔭐�������֐�/// </summary>///<param name="eventBase">����������Wave</param>
    public void PlayWave(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        // Animator����ObservableStateMachineTrigger�̎Q�Ƃ��擾
        ObservableStateMachineTrigger trigger =_uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

        // State�̏I���C�x���g
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
