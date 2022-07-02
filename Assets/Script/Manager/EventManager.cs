using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������
using System.IO;
using UnityEditor;
using System;

public class EventManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField, Tooltip("�C�x���g�ő��郁�b�Z�[�W")]
    private string _message = "���b�Z�[�W";
    [SerializeField, Tooltip("Event���������t�H���_�̃p�X")]
    private string _path = "Assets\\Resorces\\Event";
    //[SerializeField]
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
        _events = new List<EventBase>();
        _events = FindEventsAsset(_path);
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
        Debug.Log(_timer);
        _timer += Time.deltaTime;

        if(_timer >= _testCount)
        {
            Raise();
            _timer = 0;
        }
    }

    /// <summary>
    /// �C�x���g���N����
    /// </summary>
    public void Raise()
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
            _events[0].EventStart();
        }
    }
}
