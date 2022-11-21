using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using UniRx;

/// <summary>
/// プレイヤーの攻撃を制御するコンポーネント
/// </summary>
[RequireComponent(typeof(PhotonView))]
public class PlayerAttack : MonoBehaviour
{
    [Header("エフェクト")]
    [SerializeField, Tooltip("攻撃エフェクト")]
    GameObject _attackEffectPrefab;
    [SerializeField, Tooltip("エフェクト単体")]
    GameObject _effectPrefab;
    [SerializeField, Tooltip("死亡時のエフェクト")]
    GameObject _deathPrefab;
    [Header("攻撃関係")]
    [SerializeField, Tooltip("攻撃範囲")]
    Collider2D _attackCollider;
    [SerializeField, Tooltip("攻撃のクールタイム")] 
    float _attackInterval = 5f;
    [SerializeField, Tooltip("攻撃ゲージを管理するクラス")] 
    AttackGauge _attackGauge;

    private PhotonView _view;
    private float _timer;
    /// <summary>攻撃中フラグ</summary>
    bool _isAttack = false;

    private void Start()
    {
        Init();

        GameManager.Instance.CountPlayer++;
    }

    private void Init()
    {
        _view = gameObject.GetPhotonView();

        if (!_view.IsMine) return;
    }

    private void Update()
    {
        if (!_view.IsMine) return;

        Attack();
    }

    private float ratio = 0;
    public void Attack()
    {
        //攻撃ゲージ周り
        ratio = Mathf.Min(1, _timer / _attackInterval);
        _attackGauge?.SetValue(ratio);


        if (_timer <= _attackInterval)
        {
            _timer += Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && _timer >= _attackInterval)
        {
            _timer = 0;
            _isAttack = true;
            AttackEffectInstantiate();
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

    public void EffectInstantiate()
    {
        _view.RPC(nameof(SpawnEffect), RpcTarget.All, null);
    }

    [PunRPC]
    private void SpawnEffect()
    {
        if(_effectPrefab)
        {
            Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        }
    }

    public void AttackEffectInstantiate()
    {
        _view.RPC(nameof(SpawnAttackEffect), RpcTarget.All, null);
    }

    [PunRPC]
    private void SpawnAttackEffect()
    {
        if(_attackEffectPrefab)
        {
            Instantiate(_attackEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    public void DeathEffectInstantiate()
    {
        _view.RPC(nameof(SpawnDeathEffect), RpcTarget.All, null);
    }

    [PunRPC]
    private void SpawnDeathEffect()
    {
        if (_deathPrefab)
        {
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_view.IsMine && collision.tag == "Player" && collision.gameObject.TryGetComponent(out PhotonView otherView))
        {
            print("Attacked");
            // 相手の ActorNumber を取得する
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
