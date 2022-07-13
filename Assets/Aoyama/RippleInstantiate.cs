using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class RippleInstantiate : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Collider2D _circleCollider;

    private void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();
        _circleCollider = transform.GetComponent<Collider2D>();

        Invoke("unenabledTrigger", 0.05f);

        _spriteRenderer.material.SetFloat("_StartTime", Time.time);

        float animationTime = _spriteRenderer.material.GetFloat("_AnimationTime");
        float destroyTime = animationTime;
        destroyTime -= _spriteRenderer.material.GetFloat("_StartWidth") * animationTime;
        destroyTime += _spriteRenderer.material.GetFloat("_Width") * animationTime;
        Destroy(transform.gameObject, destroyTime);
    }
}
