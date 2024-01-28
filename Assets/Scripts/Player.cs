using System;
using System.Collections;
using System.Collections.Generic;
using RayFire;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField, Min(0)] float speed;
    [SerializeField, Min(0)] float strafeMultiplier;
    [SerializeField, Min(1)] float sprintMultiplier;

    [SerializeField] float JumpForce = 8f;

    [SerializeField, Min(0)] float headTurnSpeed;
    [SerializeField, Min(0)] float bodyTurnSpeed;
    [SerializeField, Range(0,90)] float headTurnBounds;

    bool canMove = true;
    bool canSmack = true;

    float timeSpentIdle = 0;

    public float forceMagnitude = 10f;

    public GameObject impactPrefab;
    public AudioClip impactSound;

    Rigidbody rb;
    Animator animator;
    private AudioSource _audioSource;
    private NoiseSystem _noiseSystem;

    [SerializeField]UnityEvent OnDestroyItem;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        _noiseSystem = FindObjectOfType<NoiseSystem>();
        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isGameOver)
        {
            animator.speed = 0;
        }
        else if(GameManager.Instance.isGameStarted)
        {
            if (canMove) Move();
            Turn();
            timeSpentIdle += Time.deltaTime;
        }
    }
    void Interact(Collider interactable)
    {
        bool smack = false;
        if (canSmack)
            if (Input.GetButtonDown("Fire1"))
            {
                smack = true;
                StartCoroutine(Smack(true));
            } else if (Input.GetButtonDown("Fire2"))
            {
                smack = true;
                StartCoroutine(Smack(false));
            }
        if (smack)
        {
            ScoreItems scoreItems = interactable.GetComponent<ScoreItems>();
            if (scoreItems != null)
            {
                Vector3 collisionPoint = interactable.ClosestPoint(transform.position);

                GameObject effect = Instantiate(impactPrefab, collisionPoint, Quaternion.identity);
                _audioSource.PlayOneShot(impactSound);
                
                Destroy(effect, 3f);
                switch (scoreItems.objectsType)
                {
                    case ScoreItems.ObjectsType.Dynamic:
                        scoreItems.ApplyDamage(50f);
                        break;
                    case ScoreItems.ObjectsType.Fragile:
                        scoreItems.ApplyDamage(50f);
                        break;
                    case ScoreItems.ObjectsType.Ball:
                        Vector3 relativePostion = interactable.transform.position - transform.position;
                        Vector3 force = relativePostion.normalized * forceMagnitude;
                        
                        interactable.GetComponent<Rigidbody>().AddForce(force);
                        GameManager.Instance.AddScore(scoreItems.scoreValue);
                        if(_noiseSystem != null)
                            _noiseSystem.AddNoise(10f);
                        break;
                    default:
                        break;
                }

                // Trigger other scripts (chatbox etc.)
                OnDestroyItem.Invoke();
            }
        }
        if (smack) timeSpentIdle = 0;
    }
    IEnumerator Smack(bool left)
    {
        canSmack = false;
        canMove = false;
        if (left)
            animator.SetTrigger("Smack_L");
        else
            animator.SetTrigger("Smack_R");
        yield return new WaitForSeconds(0.2f);
        animator.ResetTrigger("Smack_L");
        animator.ResetTrigger("Smack_R");
        canSmack = true;
        canMove = true;
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
            timeSpentIdle = 0;
        }
        if (Input.GetButton("Sprint"))
        {
            displacement *= sprintMultiplier;
            animator.SetInteger("State", 2);
        }
        transform.Translate(new Vector3(displacement.x * strafeMultiplier, 0, displacement.y), Space.Self);

        //laser detect if the player is on the ground
        bool canJump = false;
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
            StartCoroutine(Jump());
            timeSpentIdle = 0;
        }

        if (timeSpentIdle > 5) animator.SetInteger("State", -1);
    }
    private void OnTriggerStay(Collider other)
    {
        Interact(other);
    }

    IEnumerator Jump()
    {
        canMove = false;
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.25f);
        canMove = true;
        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        animator.ResetTrigger("Jump");
    }
}
