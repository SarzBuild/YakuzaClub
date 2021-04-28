using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Sc_CameraRelated : MonoBehaviour
{
    private static Sc_CameraRelated _cameraInstance;
    public static Sc_CameraRelated CameraInstance
    {
        get
        { 
            return _cameraInstance;
        }
        
    }

    [SerializeField] private Image blackFade;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float shakeTimer;

    private void Awake()
    {
        if (_cameraInstance != null && _cameraInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _cameraInstance = this;
        }
        
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineBasicMultiChannelPerlin channelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //blackFade = GameObject.FindWithTag("Crossfade").GetComponentInChildren<Image>();
        blackFade.canvasRenderer.SetAlpha(1f);
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        channelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    public void FadeOut(float duration, bool ignoreTimeScale)
    {
        Color fixedColor = blackFade.color;
        fixedColor.a = 1;
        blackFade.color = fixedColor;
        
        blackFade.CrossFadeAlpha(0f,0f,true);
        blackFade.CrossFadeAlpha(1f, duration, ignoreTimeScale);
    }

    public void FadeIn(float duration, bool ignoreTimeScale)
    {
        Color fixedColor = blackFade.color;
        fixedColor.a = 1;
        blackFade.color = fixedColor;
        
        blackFade.CrossFadeAlpha(1f,0f,true);
        blackFade.CrossFadeAlpha(0f, duration, ignoreTimeScale);
    }
    

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin channelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                channelPerlin.m_AmplitudeGain = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            FadeOut(1f, false);
        }
    }
}
