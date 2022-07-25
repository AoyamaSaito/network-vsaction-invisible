using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TakaiTestScene : MonoBehaviour
{
    [Header("GameSceneñº"), Tooltip("GameSceneñº"), SerializeField] string _gameSceneName; 

    /// <summary>
    /// ÉQÅ[ÉÄÉVÅ[ÉìÇ…îÚÇ‘ä÷êî
    /// </summary>
    public void OnClickEnterGame()
    {
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnClickEnterGame();
    }
}
