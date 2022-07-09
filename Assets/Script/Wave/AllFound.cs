using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 全プレイヤーの位置にエフェクトを出す
/// </summary>
public class AllFound : WaveBase
{
    public override void WaveStart()
    {
        Debug.Log("イベント発生");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject me = players?.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
        PhotonView view = me.GetPhotonView();
        view.GetComponent<PlayerAttack>().EffectInstantiate();
    }
}
