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
    float _count = 1;

    public override void WaveStart()
    {
        var i = Instantiate(this);
        i.EffectAllInstantiate();
    }

    private void EffectAllInstantiate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject me = players?.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
        PhotonView view = me?.GetPhotonView();
        StartCoroutine(AllFoundCor(view?.GetComponent<PlayerAttack>()));
    }

    IEnumerator AllFoundCor(PlayerAttack playerAttack)
    {
        int count = 0;
        while(count <= 5)
        {
            yield return new WaitForSeconds(0.2f);
            playerAttack?.EffectInstantiate();
            count++;
        }
        Destroy(this.gameObject);
    }
}
