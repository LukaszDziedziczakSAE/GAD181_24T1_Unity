using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CinemachineBrain cinemachineBrain;
    CinemachineVirtualCamera currentCamera;
    [SerializeField] int camPriortyNormal = 10;
    [SerializeField] int camPriortyCurrentActive = 20;

    public event Action BlendComplete;
    bool camBlending;
    float blendFinishTime;


    private void Update()
    {
        if (camBlending && Time.time >= blendFinishTime && !cinemachineBrain.IsBlending)
        {
            camBlending = false;
            BlendComplete?.Invoke();
            BlendComplete = null;
        }

        //if (cinemachineBrain != null && cinemachineBrain.IsBlending) Debug.Log("Is blending");
    }

    public void SwitchTo(CinemachineVirtualCamera nextCamera, float transitionTime = 0)
    {
        if (cinemachineBrain == null)
        {
            Debug.LogWarning("CameraManager is missing CinemachineBrain referance.");
            return;
        }

        if (transitionTime == 0)
        {
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        }
        else
        {
            cinemachineBrain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseIn;
            cinemachineBrain.m_DefaultBlend.m_Time = transitionTime;
            camBlending = true;
            blendFinishTime = Time.time + transitionTime;
        }

        if (currentCamera != null) currentCamera.Priority = camPriortyNormal;

        currentCamera = nextCamera;
        currentCamera.Priority = camPriortyCurrentActive;
    }

    public void OnSceneLoad()
    {
        currentCamera = null;
        cinemachineBrain = FindAnyObjectByType<CinemachineBrain>();
    }
}
