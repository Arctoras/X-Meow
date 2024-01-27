using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVSystem : MonoBehaviour
{
    public Light cameraLight;
    public Color normalColor = Color.cyan;
    public Color alertColor = Color.yellow;
    public Color detectedColor = Color.red;
    public float alertIncreaseRate = 1f;
    public float alertIncreaseRateWhenDestorying = 2f;
    public float pitchAngle = 30f;

    public float rotationSpeed = 30f;
    public float rotationAngle = 45f;

    private bool isPlayerDestorying = false;
    private float currentAngle = 0f;
    private float direction = 1f;
    private void Start()
    {
        SetCameraColor(normalColor);
    }

    private void Update()
    {
        RotateCamera();
    }

    public void SetCameraColor(Color color)
    {
        if (cameraLight != null)
        {
            cameraLight.color = color;
        }
    }

    private void RotateCamera()
    {
        currentAngle += rotationSpeed * Time.deltaTime * direction;

        if (Mathf.Abs(currentAngle) >= rotationAngle)
        {
            direction *= -1;
        }
        
        transform.localRotation = Quaternion.Euler(pitchAngle, currentAngle, 0f);
    }

    public void PlayerDetected()
    {
        SetCameraColor(detectedColor);
        // GameManager.Instance.IncreaseAlertLevel(alertIncreaseRate);
    }

    public void PlayerAlert()
    {
        SetCameraColor(alertColor);
        // GameManager.Instance.IncreaseAlertLevel(alertIncreaseRate);
    }

    public void ResetCamera()
    {
        SetCameraColor(normalColor);
    }
}
