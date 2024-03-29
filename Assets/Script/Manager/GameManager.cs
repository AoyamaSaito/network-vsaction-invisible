using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;
using UnityEngine.UI;

/// <summary>
/// ゲームを管理するコンポーネント
/// イベントコード 2 を Kill とする
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    //「Raise」というOnEventを呼び出す関数を、多数のスクリプト定義する必要が出てきそうなので
    //GameManager内で引数で指定できるようにしておいた方がよさそう

    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private CameraManager _cameraManager;
    [SerializeField]
    private Text _playerText;
    [SerializeField] 
    private bool _deathTest = false;

    #region　 Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public event Action<EventData> OnEventGameManager;

    public event Action OnPlayerDeath;

    public int CountPlayer = 0;

    private void Start()
    {
        CountPlayer = 0;
    }

    private void Update()
    {
        if(_deathTest)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Array.ForEach(players, p => p.GetComponent<PlayerController>().PlayerView.Show());
            GameObject me = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
            me.GetComponent<PlayerController>().Death();

            _uiManager?.PlayerDefeat();

            _deathTest = false;
        }

        _playerText.text = "残り：" + $"{CountPlayer}人";
    }

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        if(OnEventGameManager != null)
        {
            OnEventGameManager(photonEvent);
        }

        if (photonEvent.Code == 2)
        {
            KillPlayer(photonEvent);
        }
    }

    private void KillPlayer(EventData photonEvent)
    {
        GameObject[] players;
        int length = 0;
        int killedPlayerActorNumber = (int)photonEvent.CustomData;
        print($"Player {photonEvent.Sender} killed Player {killedPlayerActorNumber}");

        _cameraManager.ShakeCamera();
        players = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = PhotonNetwork.CountOfPlayersInRooms;
        GameManager.Instance.CountPlayer--;
        if (_playerText != null)
        {
            _playerText.text = "残り：" + $"{ playerCount - 1}人";
        }

        // やられたのが自分だったら自分を消す、周りのプレイヤーを表示する
        if (killedPlayerActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            Array.ForEach(players, p => p.GetComponent<PlayerController>().PlayerView.Show());
            GameObject me = players.Where(x => x.GetPhotonView().IsMine).FirstOrDefault();
            me.GetComponent<PlayerController>().Death();

            _uiManager?.PlayerDefeat();
        }

        if (playerCount <= 2)
        {
            Finish(GameObject.FindGameObjectWithTag("Player").GetPhotonView().OwnerActorNr);
        }
    }

    private void Finish(int playerNumber)
    {
        _uiManager?.Finish(playerNumber);
    }
}