using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStart : MonoBehaviour
{
    [Tooltip("�^�C�g��Anim")]Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnClickStart()
    {
        _animator.SetBool("Start", true);
    }
}
