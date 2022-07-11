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
    [SerializeField, Tooltip("�^�C�}�[")]
    float _count = 5;

    public override void WaveStart()
    {
        Debug.Log("�C�x���g����");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject me = players?.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
        PhotonView view = me.GetPhotonView();
        StartCoroutine(AllFoundCor(view.GetComponent<PlayerAttack>()));
    }

    IEnumerator AllFoundCor(PlayerAttack playerAttack)
    {
        float timer = 0;
        while(timer <= _count)
        {
            yield return new WaitForSeconds(0.5f);
            playerAttack.EffectInstantiate();
        }
    }
}
