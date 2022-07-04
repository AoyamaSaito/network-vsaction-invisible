using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Animator _uiPanel;

    public void PlayAnimation()
    {
        _uiPanel.SetTrigger("Play");
    }
}
