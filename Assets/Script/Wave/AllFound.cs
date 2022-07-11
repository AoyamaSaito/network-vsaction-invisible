using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �S�v���C���[�̈ʒu�ɃG�t�F�N�g���o��
/// </summary>
public class AllFound : WaveBase
{
    public override void WaveStart()
    {
        Debug.Log("�C�x���g����");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject me = players?.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
        PhotonView view = me.GetPhotonView();
        view.GetComponent<PlayerAttack>().EffectInstantiate();
    }
}
