using System;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator _uiPanel;

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
}
