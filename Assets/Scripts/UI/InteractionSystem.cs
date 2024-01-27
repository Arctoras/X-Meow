using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject tipPanel;
    public float InteractionRadius = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Interactable[] interactables = FindObjectsOfType<Interactable>();
        Interactable closest = null;
        float closestDistance = 3f;
        foreach (Interactable interactable in interactables)
        {
            float distance = Vector3.Distance(interactable.transform.position, player.position);
            if (distance < closestDistance)
            {
                closest = interactable;
                closestDistance = distance;
            }
        }

        if (closest != null)
        {
            //Show the interaction tip
            tipPanel.SetActive(true);
            transform.position = UnityEngine.Camera.main.WorldToScreenPoint(closest.transform.position);

            //Interact with the object
            if (Input.GetKeyDown(KeyCode.E))
            {
                closest.Interact();
            }

        }
        else
        {
            tipPanel.SetActive(false);
        }
    }
}
