using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] float speed;
    [SerializeField, Min(0)] float strafeMultiplier;
    [SerializeField, Min(1)] float sprintMultiplier;

    [SerializeField, Min(0)] float maxJumpForce;
    [SerializeField, Min(0.00001f)] float jumpChargeTime;

    [SerializeField, Min(0)] float headTurnSpeed;
    [SerializeField, Min(0)] float bodyTurnSpeed;
    [SerializeField, Range(0,90)] float headTurnBounds;

    bool canJump = false;
    float jumpForce = 0;

    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Smack();
    }
    void Interact(Collider interactable)
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Debug.Log(interactable.name);
            ScoreItems scoreItems = interactable.GetComponent<ScoreItems>();
            if (scoreItems != null)
            {
                switch (scoreItems.objectsType)
                {
                    case ScoreItems.ObjectsType.Dynamic:
                        scoreItems.ApplyDamage(50f);
                        //sound
                        //vfx
                        break;
                    case ScoreItems.ObjectsType.Fragile:
                        scoreItems.ApplyDamage(50f);
                        //sfx
                        //vfx
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void Smack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.Play("Smack1");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play("Smack2");
        }
    }

    void Turn()
    {
        float rotation = Input.GetAxis("Mouse X") * bodyTurnSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
        
    void Move()
    {
        animator.SetInteger("State", 0);
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector2 displacement = direction * speed * Time.deltaTime;
        if(displacement.magnitude > 0)
        {
            animator.SetInteger("State", 1);
        }
        if (Input.GetButton("Sprint"))
        {
            displacement *= sprintMultiplier;
            animator.SetInteger("State", 2);
        }
        transform.Translate(new Vector3(displacement.x * strafeMultiplier, 0, displacement.y), Space.Self);
        if(canJump && Input.GetButton("Jump"))
        {
            jumpForce = Mathf.Min(maxJumpForce, jumpForce + (maxJumpForce * ( Time.deltaTime / jumpChargeTime)));
            animator.SetInteger("State", 3);
        }
        if(canJump && Input.GetButtonUp("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumpForce = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Interact(other);
    }

    private void OnCollisionStay(Collision collision)
    {
        canJump = true;
    }

    void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }
}
