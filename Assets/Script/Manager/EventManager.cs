using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������
using System.IO;
using UnityEditor;

public class EventManager : MonoBehaviour
{
    [SerializeField, Tooltip("�C�x���g�ő��郁�b�Z�[�W")] 
    private string m_message = "���b�Z�[�W";
    [SerializeField, Tooltip("�C�x���g�̃t�H���_�p�X")]
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
        PhotonNetwork.RaiseEvent(eventCode, m_message, raiseEventOptions, sendOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        //�C�x���g
        if (photonEvent.Code == 3)
        {
            _events[UnityEngine.Random.Range(0, _events.Length - 1)].EventStart();
        }
    }
}
