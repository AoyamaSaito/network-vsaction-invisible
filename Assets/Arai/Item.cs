using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviour
{
    PhotonView _view = default;

    void Start()
    {
        _view = gameObject.GetPhotonView();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision.gameObject.name}GetItem!");
    }
}
