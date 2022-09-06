using UnityEngine;
using Photon.Pun;

/// <summary>
/// プレイヤーの動きを制御するコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(PhotonView), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField] 
    private float _jumpSpeed = 5f;

    private PhotonView _view;
    private Rigidbody2D _rb;

    private void Start()
    {
        _view = gameObject.GetPhotonView();
        _rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 _velo;
    private bool _isGrounded = false;
    private void Update()
    {
        if (!_view.IsMine)
        {
            return;
        }

        Move(Input.GetAxisRaw("Horizontal"));

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        _rb.velocity = _velo;
    }

    private void Move(float input)
    {
        _velo = _rb.velocity;
        _velo.x = _speed * input;
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _velo.y = _jumpSpeed;
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }
}