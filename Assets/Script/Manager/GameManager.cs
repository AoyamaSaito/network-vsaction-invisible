using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

/// <summary>
/// ゲームを管理するコンポーネント
/// イベントコード 2 を Kill とする
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    //Mono継承しなきゃだめ？
    //「Raise」というOnEventを呼び出す関数を、多数のスクリプト定義する必要が出てきそうなので
    //GameManager内で引数で指定できるようにしておいた方がよさそう

    #region　 Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public event Action<EventData> OnEventGameManager;

    private void Start()
    {
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //mePlayer = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
    }

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        if(OnEventGameManager != null)
        {
            OnEventGameManager(photonEvent);
        }

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

    //GameObject mePlayer;
    //public GameObject MePlayer => mePlayer;
    //public void SetPlayer(GameObject go) { mePlayer = go; }
}