using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject DialogueCanvas;

    [Header("Other events that need to be triggered")]
    public UnityEvent OnInteract;

    public bool HasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        DialogueCanvas.SetActive(false);
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
            DialogueCanvas.SetActive(true);
            OnInteract.Invoke();

            HasInteracted = true;
        }
    }
}
