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
        Ball,
    }

    public ObjectsType objectsType;
    GameObject player;

    public float durablity = 100f;
    public int scoreValue = 100;
    public float noiseValue = 20f;
    private bool _isDestoryed = false;
    private RayfireRigid _rayfireRigid;
    private NoiseSystem _noiseSystem;

    public AudioClip glassBroken;
    public AudioClip brokenSound;
    public AudioClip paperSound;
    public AudioClip textureSound;
    private AudioSource playerAudioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _noiseSystem = FindObjectOfType<NoiseSystem>();
        playerAudioSource = player.GetComponent<AudioSource>();

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
            case ObjectsType.Ball:
                //add force
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
        if (_noiseSystem != null)
            _noiseSystem.AddNoise(noiseValue /2f);
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
        
        if (gameObject.CompareTag("GlassObjects"))
        {
            playerAudioSource.PlayOneShot(glassBroken);
        }
        else if (gameObject.CompareTag("Paper"))
        {
            playerAudioSource.PlayOneShot(paperSound);
        }
        else if (gameObject.CompareTag("TextureStuff"))
        {
            playerAudioSource.PlayOneShot(textureSound);
        }
        else
        {
            playerAudioSource.PlayOneShot(brokenSound);
        }
        
        _isDestoryed = true;
        GameManager.Instance.AddScore(scoreValue);

        if(_noiseSystem != null)
            _noiseSystem.AddNoise(noiseValue);

        if (_rayfireRigid != null)
        {
            _rayfireRigid.Demolish();
        }

        
        
        //sound
        // Destroy(gameObject);
        //other destory effects
    }

    public void AddScoreOnly()
    {
        GameManager.Instance.AddScore(scoreValue);
    }
}
