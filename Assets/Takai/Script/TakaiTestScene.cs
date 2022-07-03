using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    [Header("GameScene��"), Tooltip("GameScene��"), SerializeField] string _gameSceneName; 

    /// <summary>
    /// �Q�[���V�[���ɔ�Ԋ֐�
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Single);
    }
}
