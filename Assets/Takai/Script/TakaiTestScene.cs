using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    /// <summary>
    /// �Q�[���V�[���ɔ�Ԋ֐�
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }
}
