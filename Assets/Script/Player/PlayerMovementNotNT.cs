using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの動きを制御するコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMovementNotNT : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _entryTime;

    [SerializeField,Tooltip("Quick match")] GameObject _quickMatch;
    [SerializeField,Tooltip("TakaiTestScene")] TakaiTestScene _scene;
    Rigidbody2D _rb;
    [SerializeField] Image _image;
    bool _isGrounded = false;
    bool _isEnterded = false;
    float ratio;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = _rb.velocity;
        velocity.x = _speed * h;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = _jumpSpeed;
            _isGrounded = false;
        }
        _rb.velocity = velocity;

        if (_isEnterded)
        {
            ratio += Time.deltaTime / _entryTime;
            _image.fillAmount = ratio;

            if(_image.fillAmount >= 1)
            {
                _scene.OnClickEnterGame();
            }
        }
        else
        {
            ratio = 0;
            _image.fillAmount = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_quickMatch)
        {
            _isEnterded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isEnterded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }
}