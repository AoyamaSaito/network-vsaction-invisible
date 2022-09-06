using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃ゲージのMVPパターンのPresenter
/// </summary>
public class GaugePresenter : MonoBehaviour
{
    [Tooltip("ゲージの表示を管理するクラス")] 
    AttackGauge _attackGauge;

    [Tooltip("ゲージの値を管理するクラス")]
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
        ////自分のPlayerが生成されるのを待つ
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
