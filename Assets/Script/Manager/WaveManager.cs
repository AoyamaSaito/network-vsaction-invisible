using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaveManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Tooltip("Wave�𔭐�������b��")]
    private float _testCount = 4;
    [SerializeField, Tooltip("Panel�̃A�j���[�^�[")]
    private Animator _uiPanel;
    [SerializeField, Tooltip("Wave���������t�H���_�̃p�X")]
    private string _wavePath = "Assets\\Resorces\\Event";
    [SerializeField, Tooltip("Wave��������Data��ۑ�����t�H���_�̃p�X")]
    private string _savePath = "Assets\\Resorces\\Event";

    private List<WaveBase> _events;
    private float _timer = 0;
    private string _message = "���b�Z�[�W";
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
    /// 
    /// </summary>
    /// <param name="photonEvent"></param>
    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        Debug.Log("OnEvent");
        //�C�x���g
        if (photonEvent.Code == 3)
        {
            int i = UnityEngine.Random.Range(0, _events.Count - 1);
            Debug.Log(_events[i].name);
            PlayWave(_events[i]);
        }
    }

    /// <summary>///Wave�𔭐�������֐�/// </summary>///<param name="eventBase">����������Wave</param>
    public void PlayWave(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        // Animator����ObservableStateMachineTrigger�̎Q�Ƃ��擾
        ObservableStateMachineTrigger trigger =
            _uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

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
