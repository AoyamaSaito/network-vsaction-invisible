using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    [Header("GameScene名"), Tooltip("GameScene名"), SerializeField] string _gameSceneName;

    /// <summary>
    /// ゲームシーンに飛ぶ関数
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Single);
    }
}
