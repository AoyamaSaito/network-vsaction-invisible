using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    /// <summary>
    /// ƒQ[ƒ€ƒV[ƒ“‚É”ò‚ÔŠÖ”
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }
}
