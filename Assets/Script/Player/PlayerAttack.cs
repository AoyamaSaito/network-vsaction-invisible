using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// プレイヤーの攻撃を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class PlayerAttack : MonoBehaviour
{
    /// <summary>攻撃エフェクト（他のクライアントのみに表示する）</summary>
    [SerializeField] GameObject _attackEffectPrefab;
    /// <summary>攻撃範囲</summary>
    [SerializeField] Collider2D _attackCollider;
    /// <summary>攻撃のクールタイム</summary>
    [SerializeField] float _attackInterval = 5f;
    PhotonView _view;
    float _timer;
    /// <summary>攻撃中フラグ</summary>
    bool _isAttack = false;

    private void Start()
    {
        _view = gameObject.GetPhotonView();
    }

    private void Update()
    {
        if (!_view.IsMine) return;

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && _timer <= 0)
        {
            _timer = _attackInterval;
            _isAttack = true;
            // 他のクライアントだけにエフェクトを表示する
            _view.RPC(nameof(SpawnAttackEffect), RpcTarget.Others, null);
        }
    }

    private void FixedUpdate()
    {
        // 攻撃中なら攻撃を有効にし、次の物理演算時に無効にする
        if (_attackCollider.gameObject.activeSelf)
        {
            _attackCollider.gameObject.SetActive(false);
        }

        if (_isAttack)
        {
            _attackCollider.gameObject.SetActive(true);
            _isAttack = false;
        }
    }

    [PunRPC]
    private void SpawnAttackEffect()
    {
        Instantiate(_attackEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_view.IsMine && collision.tag == "Player")
        {
            print("Attacked");
            // 相手の ActorNumber を取得する
            PhotonView otherView = collision.gameObject.GetPhotonView();
            int otherActorNumber = otherView.OwnerActorNr;
            print($"Attacked {otherActorNumber}");
            // 相手を倒したことを通知する(Kill のイベントコードは2)
            RaiseEventOptions target = new RaiseEventOptions();
            target.Receivers = ReceiverGroup.All;
            SendOptions sendOptions = new SendOptions();
            PhotonNetwork.RaiseEvent(2, otherActorNumber, target, sendOptions);
        }
    }
}
