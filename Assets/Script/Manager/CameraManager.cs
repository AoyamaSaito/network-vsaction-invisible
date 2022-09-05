using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineBrain _camBrain;
    [SerializeField] 
    private CinemachineVirtualCamera[] _vCam;
    [Header("Test")]
    [SerializeField]
    private bool _shakeTest = false;

    private CinemachineVirtualCamera _currentVCam;

    private void Start()
    {
        _currentVCam = (CinemachineVirtualCamera)_camBrain.ActiveVirtualCamera;
    }

    private void FixedUpdate()
    {
        if(_shakeTest)
        {
            ShakeCamera();
            _shakeTest = false;
        }
    }

    public void ShakeCamera()
    {
        _currentVCam.TryGetComponent(out CinemachineImpulseSource imp);
        imp.GenerateImpulse();
    }

    public void ChangeCamara(CinemachineVirtualCamera vcam)
    {
        _currentVCam = vcam;
    }
}
