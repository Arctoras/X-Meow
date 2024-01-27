using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private int _noiseValue = 50;

    [Header("Optional - Show Dialogue")]
    [SerializeField] private GameObject m_DialogueCanvas;

    [Header("Other events that need to be triggered")]
    public UnityEvent OnInteract;

    private NoiseSystem _NoiseSystem;

    public bool HasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_DialogueCanvas != null)
        {
            m_DialogueCanvas.SetActive(false);
        }
        _NoiseSystem = FindObjectOfType<NoiseSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()//Triggered by InteractionTip.cs
    {
        if (!HasInteracted)
        {
            Debug.Log("Interacting with " + name);

            _NoiseSystem.AddNoise(_noiseValue);

            if (m_DialogueCanvas != null)
            {
                m_DialogueCanvas.SetActive(true);
            }

            OnInteract.Invoke();

            HasInteracted = true;
        }
    }
}
