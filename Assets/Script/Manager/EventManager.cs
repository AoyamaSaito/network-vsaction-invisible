using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   // PhotonNetwork ���g������
using Photon.Realtime;  // RaiseEventOptions/ReceiverGroup ���g������
using ExitGames.Client.Photon;  // SendOptions ���g������

public class EventManager : MonoBehaviour
{
    [SerializeField, Tooltip("�C�x���g�ő��郁�b�Z�[�W")] 
    private string m_message;
    [SerializeField, Tooltip("����������C�x���g")] 
    private IEvent[] _events;

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
