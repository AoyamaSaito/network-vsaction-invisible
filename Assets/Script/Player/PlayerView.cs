using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PhotonView _view;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _view = gameObject.GetPhotonView();
        _sprite = GetComponent<SpriteRenderer>();

        // ���̃v���C���[����͌����Ȃ�����
        if (!_view.IsMine)
        {
            Hide();
        }
    }

    public void Hide()
    {
        _sprite.enabled = false;
    }

    public void Show()
    {
        _sprite.enabled = true;
    }
}
