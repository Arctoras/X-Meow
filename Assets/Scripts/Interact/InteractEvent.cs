using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractEvent", menuName = "InteractEvent", order = 1)]
public class InteractEvent : ScriptableObject
{
    public int ScoreValue = 0;
    public float NoiseValue = 50;
    public Sprite FaceImage;
    public float DialogueDelay = 0f;
    public string AnimationTrigger = "Interact";
    public AudioClip EventAudio;
}
