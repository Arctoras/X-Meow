using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private CCTVSystem _cctvSystem;

    private void Start()
    {
        _cctvSystem = GetComponentInParent<CCTVSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cctvSystem.PlayerDetected();
        }
        else if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            _cctvSystem.PlayerAlert();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cctvSystem.ResetCamera();
        }
    }
}
