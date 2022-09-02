using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    PlayerAttack _playerAttack;
    [SerializeField] 
    PlayerMovement _playerMovement;
    [SerializeField]
    PlayerView _playerView;

    public PlayerAttack PlayerAttack => _playerAttack;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerView PlayerView => _playerView;

    public void Death()
    {
        _playerView.Hide();
        _playerAttack.DeathEffectInstantiate();
        PhotonView view = gameObject.GetPhotonView();
        PhotonNetwork.Destroy(view);
    }
}
