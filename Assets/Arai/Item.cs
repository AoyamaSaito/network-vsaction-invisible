using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class Item : MonoBehaviour
{
    PhotonView _view = default;

    void Start()
    {
        _view = gameObject.GetPhotonView();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_view.IsMine && collision.gameObject.TryGetComponent<PhotonView>(out PhotonView otherView))
        {
            int otherActorNumber = otherView.OwnerActorNr;
            print($"GetItem {otherActorNumber}");
            RaiseEventOptions target = new RaiseEventOptions();
            target.Receivers = ReceiverGroup.All;
            SendOptions sendOptions = new SendOptions();
            PhotonNetwork.RaiseEvent(3, otherActorNumber, target, sendOptions);
        }
    }

    public void DestroyItem()
    {
        _view.RPC(nameof(Action),RpcTarget.All,RpcTarget.All);
    }

    [PunRPC]
    void Action()
    {
        Debug.Log("ƒAƒCƒeƒ€‚ªŽæ‚ç‚ê‚½");
        PhotonNetwork.Destroy(_view);
    }
}
