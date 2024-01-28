using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class ClickableBox : MonoBehaviour
{
    private RayfireRigid _rayfireRigid;
    
    void Start()
    {
        _rayfireRigid = GetComponent<RayfireRigid>();
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            _rayfireRigid.Demolish();
            
            GameManager.Instance.BlockClicked();
        }
    }
}
