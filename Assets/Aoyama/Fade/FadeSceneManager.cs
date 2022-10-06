using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeSceneManager : MonoBehaviour
{
    [SerializeField]
    private bool _isStartFade = true;
    [SerializeField]
    private float _fadeTime = 1.5f;
    [Header("���ȎQ��")]
    [SerializeField]
    private Animator _fadeAnimator;
    [SerializeField]
    private UnityEvent _sceneChangeEvent;
    

    private void Start()
    {
        if(_isStartFade )
        {
            _fadeAnimator!.Play("FadeOut");
        }
    }

    /// <summary>
    /// Fade���Ȃ���SceneChange�ł���֐�
    /// </summary>
    /// <param name="sceneName">�ړ�����V�[��</param>
    /// <param name="isStartFade">�ړ��������FadeOut���邩�ǂ���</param>
    public void SceneChange(string sceneName)
    {
        StartCoroutine(SceneChangeCor(sceneName));
    }

    IEnumerator SceneChangeCor(string sceneName, bool isStartFade = false)
    {
        _isStartFade = isStartFade;
        _fadeAnimator!.SetTrigger("FadeIn");
        yield return new WaitForSeconds(_fadeTime);
        if(_sceneChangeEvent != null)
        {
            _sceneChangeEvent.Invoke();
        }
        SceneManager.LoadScene(sceneName);
    }
}
