using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offset;
    
    void Update()
    {
        if (objectToFollow != null)
        {
            transform.position = UnityEngine.Camera.main.WorldToScreenPoint(objectToFollow.position + offset);
            // Vector3 screenPoint = UnityEngine.Camera.main.WorldToScreenPoint(objectToFollow.position + offset);
            // if (screenPoint.z > 0) 
            // {
            //     transform.position = screenPoint;
            // }
        }
    }
}
