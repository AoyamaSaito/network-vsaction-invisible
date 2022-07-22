using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 攻撃ゲージのMVPパターンのView
/// </summary>
public class AttackGauge : MonoBehaviour
{
    [SerializeField] Image _image;
    
    public void SetValue(float value)
    {
        if(value == 1)
        {
            _image.enabled = false;
        }
        else
        {
            _image.enabled = true;
        }

        if(_image != null)
        {
            _image.fillAmount = value;
        }
    }
}
