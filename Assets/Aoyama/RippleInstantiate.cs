using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class RippleInstantiate : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = transform.GetComponent<SpriteRenderer>();

        Invoke("unenabledTrigger", 0.05f);

        _spriteRenderer.material.SetFloat("_StartTime", Time.time);

        float animationTime = _spriteRenderer.material.GetFloat("_AnimationTime");
        float destroyTime = animationTime;
        destroyTime -= _spriteRenderer.material.GetFloat("_StartWidth") * animationTime;
        destroyTime += _spriteRenderer.material.GetFloat("_Width") * animationTime;
        Destroy(transform.gameObject, destroyTime);
    }

    private void OnEnable()
    {
        
    }
}
