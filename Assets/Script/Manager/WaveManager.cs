using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������
using System.IO;
using UnityEditor;
using System;

public class WaveManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Tooltip("Wave�𔭐�������b��")] 
    private float _testCount = 4;
    [SerializeField, Tooltip("Panel�̃A�j���[�^�[")]
    private Animator _uiPanel;
    [SerializeField, Tooltip("Event���������t�H���_�̃p�X")]
    private string _path = "Assets\\Resorces\\Event";
    //[SerializeField]
    private List<WaveBase> _events;

    private float _timer = 0;
    private string _message = "���b�Z�[�W";

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _events = new List<WaveBase>();
        _events = FindEventsAsset(_path);
    }

    private List<WaveBase> FindEventsAsset(string directoryPath)
    {
        List<WaveBase> assets = new List<WaveBase>();
        var fileNames = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

        foreach (var fileName in fileNames)
        {
            var asset = AssetDatabase.LoadAssetAtPath<WaveBase>(fileName);
            if (asset != null)
            {
                assets.Add(asset);
            }
        }

        return assets;
    }

    private void Update()
    {
        Debug.Log(_timer);
        _timer += Time.deltaTime;

        if(_timer >= _testCount)
        {
            EventRaise();
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

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        Debug.Log("OnEvent");
        //�C�x���g
        if (photonEvent.Code == 3)
        {
            Debug.Log(_message);
            int i = UnityEngine.Random.Range(0, _events.Count - 1);
            PlayWave(_events[i]);
        }
    }

    public void PlayWave(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        ObservableStateMachineTrigger trigger =
            _uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

        IDisposable exitState = trigger
            .OnStateExitAsObservable()
            .Subscribe(onStateInfo =>
            {
                AnimatorStateInfo info = onStateInfo.StateInfo;

                if (info.IsName("Base Layer.PanelAnimation"))
                {
                    eventBase.WaveStart();
                }
            }).AddTo(this);
    }
}
