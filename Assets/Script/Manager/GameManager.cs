using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/// <summary>
/// ゲームを管理するコンポーネント
/// イベントコード 2 を Kill とする
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == 2)
        {
            int killedPlayerActorNumber = (int)photonEvent.CustomData;
            print($"Player {photonEvent.Sender} killed Player {killedPlayerActorNumber}");
            
            // やられたのが自分だったら自分を消す
            if (killedPlayerActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject me = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
                PhotonView view = me.GetPhotonView();
                PhotonNetwork.Destroy(view);
            }
        }
    }
}