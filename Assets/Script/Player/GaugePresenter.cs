using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �U���Q�[�W��MVP�p�^�[����Presenter
/// </summary>
public class GaugePresenter : MonoBehaviour
{
    [Tooltip("�Q�[�W�̕\�����Ǘ�����N���X")] 
    AttackGauge _attackGauge;

    [Tooltip("�Q�[�W�̒l���Ǘ�����N���X")]
    IGauge _gauge;

    //private bool _init = false;
    //private GameManager _gameManager;

    private void Start()
    {
        //_init = true;
        //_gameManager = GameManager.Instance;
    }

    private void Update()
    {
        ////������Player�����������̂�҂�
        //if(_init! && GameManager.Instance.MePlayer != null)
        //{
        //    _gauge = GameManager.Instance.MePlayer.GetComponent<IGauge>();
        //    _init = true;
        //}

        if(_gauge != null)
        {
            _attackGauge?.SetValue(_gauge.Ratio);
        } 
    }
}
