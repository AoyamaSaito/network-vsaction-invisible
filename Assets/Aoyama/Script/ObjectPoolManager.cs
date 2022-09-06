using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

public class ObjectPoolManager : MonoBehaviour, IPunPrefabPool
{
    [SerializeField] 
    private GameObject _prefab;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    public void Start()
    {
        PhotonNetwork.PrefabPool = this;
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        if (_pool?.Count > 0)
        {
            GameObject go = _pool.Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);

            return go;
        }

        return Instantiate(_prefab, position, rotation);
    }

    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);

        _pool.Enqueue(gameObject);
    }
}
