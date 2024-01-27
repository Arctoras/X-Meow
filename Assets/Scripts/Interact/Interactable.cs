using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<InteractEvent> InteractEvents = new List<InteractEvent>();

    private int _eventID = 0;

    [Header("Optional Animator")]
    [SerializeField] private Animator m_Animator;


    [Header("Other events that need to be triggered")]
    public UnityEvent OnInteract;

    private NoiseSystem _noiseSystem;
    private AudioSource _audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        _noiseSystem = FindObjectOfType<NoiseSystem>();

        // Note: need to add AudioSource to Player
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//Triggered by InteractionTip.cs
    {
        if (InteractEvents.Count > 0)
        {
            Debug.Log("Interacting with " + name);

            InteractEvent interactEvent = InteractEvents[_eventID];

            // Add score
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(interactEvent.ScoreValue);
            }
            else
            {
                Debug.LogError("No GameManager found!");
            }

            // Play audio
            if (interactEvent.EventAudio!= null) _audioSource.PlayOneShot(interactEvent.EventAudio);

            // Add noise value
            _noiseSystem.AddNoise(interactEvent.NoiseValue);

            // Show dialogue
            StartCoroutine(DelayedDialogue(interactEvent.DialogueText));

            // Trigger animation
            if (m_Animator != null) m_Animator.SetTrigger(interactEvent.AnimationTrigger);

            // Trigger other events
            OnInteract.Invoke();

            // Ready for next event
            _eventID++;
            if (_eventID >= InteractEvents.Count)
            {
                _eventID = InteractEvents.Count - 1;
            }
        }
        else
        {
            Debug.LogError("No InteractEvents found on " + name);
        }
    }

    IEnumerator DelayedDialogue(string text)
    {
        yield return new WaitForSeconds(0);

        // Send dialogue to DialogueSystem
        FindObjectOfType<ChatSystem>().ShowChat(text);
    }
}
