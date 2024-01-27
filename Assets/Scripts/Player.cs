using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] float speed;
    [SerializeField, Min(0)] float strafeMultiplier;
    [SerializeField, Min(1)] float sprintMultiplier;
    [SerializeField, Min(0)] float jumpForce;

    [SerializeField, Min(0)] float headTurnSpeed;
    [SerializeField, Min(0)] float bodyTurnSpeed;
    [SerializeField, Range(0,90)] float headTurnBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector2 displacement = direction * speed * Time.deltaTime;
        if (Input.GetButton("Sprint"))
        {
            displacement *= sprintMultiplier;
        }
        transform.position += new Vector3(displacement.x * strafeMultiplier, 0, displacement.y);
    }
}
