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

    /*[SerializeField, Min(0)] float maxJumpForce;
    [SerializeField, Min(0.00001f)] float jumpChargeTime;**/
    [SerializeField] float JumpForce = 8f;

    [SerializeField, Min(0)] float headTurnSpeed;
    [SerializeField, Min(0)] float bodyTurnSpeed;
    [SerializeField, Range(0,90)] float headTurnBounds;


    /*bool canJump = false;*/

    public GameObject impactPrefab;

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
        if (Input.GetButtonDown("Fire1")  || Input.GetButtonDown("Fire2"))
        {
            Debug.Log(interactable.name);
            ScoreItems scoreItems = interactable.GetComponent<ScoreItems>();
            if (scoreItems != null)
            {
                Vector3 collisionPoint = interactable.ClosestPoint(transform.position);

                GameObject effect = Instantiate(impactPrefab, collisionPoint, Quaternion.identity);
                
                Destroy(effect, 3f);
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

        //layser detect if the player is on the ground
        bool canJump = false;
        Debug.DrawRay(transform.position, Vector3.down * 1.1f, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            if (hit.collider.gameObject != null)
            {
                canJump = true;
            }
        }
        else
        {
            canJump = false;
        }

        if(canJump && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Interact(other);
    }
}
