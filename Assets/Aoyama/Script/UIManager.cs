using System;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Text _defeatText;
    [SerializeField]
    private Text _finishText;
    [SerializeField] 
    private Animator _uiPanel;

    private void Start()
    {
        _defeatText.enabled = false;
    }

    public void PlayEvent(WaveBase eventBase)
    {
        _uiPanel.SetTrigger("Play");

        ObservableStateMachineTrigger trigger =
            _uiPanel.GetBehaviour<ObservableStateMachineTrigger>();

        IDisposable exitState = trigger
            .OnStateExitAsObservable()
            .Subscribe(onStateInfo =>
            {
                AnimatorStateInfo info = onStateInfo.StateInfo;

                if (info.IsName("Base Layer.PanelAnimation"))
                {
                    eventBase.WaveStart();
                }
            }).AddTo(this);
    }

    public void PlayerDefeat()
    {
        _defeatText.enabled = true;
    }

    public void Finish(int playerNumber)
    {
        _finishText.enabled = true;
        _finishText.text = "Player" + playerNumber + " WIN";
    }
}
