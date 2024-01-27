using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItems : MonoBehaviour
{
    public enum ObjectsType
    {
        Static,
        Dynamic,
        Broken,
        Fragile,
    }

    public float durablity = 100f;
    public int scoreValue = 10;
    private bool isDestoryed = false;

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
        if (durablity <= 0 && !isDestoryed)
        {
            //VFX
            //disable renderer
            //soundFX
            DestoryItem();
        }
    }

    void DestoryItem()
    {
        isDestoryed = true;
        GameManager.Instance.AddScore(scoreValue);
        //sound
        Destroy(gameObject);
        //other destory effects
    }
}
