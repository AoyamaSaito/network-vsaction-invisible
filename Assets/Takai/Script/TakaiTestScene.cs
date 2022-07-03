using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    [Header("GameScene–¼"), Tooltip("GameScene–¼"), SerializeField] string _gameSceneName; 

    /// <summary>
    /// ƒQ[ƒ€ƒV[ƒ“‚É”ò‚ÔŠÖ”
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Single);
    }
}
