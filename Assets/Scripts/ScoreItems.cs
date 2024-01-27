using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class ScoreItems : MonoBehaviour
{
    public enum ObjectsType
    {
        Static,
        Dynamic,
        Fragile,
        Interactive,
    }

    public ObjectsType objectsType;

    public float durablity = 100f;
    public int scoreValue = 10;
    private bool _isDestoryed = false;
    private RayfireRigid _rayfireRigid;

    private void Start()
    {
        switch (objectsType)
        {
            case ObjectsType.Static:
                //Unbeatable
                break;
            case ObjectsType.Dynamic:
                //can be pushed
                //drop on floor to be destoried
                break;
            case ObjectsType.Interactive:
                //maybe animation?
                break;
            case ObjectsType.Fragile:
                //broken
                _rayfireRigid = GetComponent<RayfireRigid>();
                break;
        }
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     CheckForDestruction();
        // }
    }

    public void ApplyDamage(float damage)
    {
        durablity -= damage;
        CheckForDestruction();
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > 5)
        {
            durablity -= other.relativeVelocity.magnitude;
            CheckForDestruction();
        }
    }

    void CheckForDestruction()
    {
        if (durablity <= 0 && !_isDestoryed)
        {
            //VFX
            //disable renderer
            //soundFX
            DestoryItem();
        }
    }

    void DestoryItem()
    {
        _isDestoryed = true;
        GameManager.Instance.AddScore(scoreValue);
        if (_rayfireRigid != null)
        {
            _rayfireRigid.Demolish();
        }
        
        //sound
        // Destroy(gameObject);
        //other destory effects
    }
}
