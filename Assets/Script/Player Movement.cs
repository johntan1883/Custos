using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public Transform GroundCheck;
    public LayerMask GroundObject;
    public float CheckRadius;
    public bool IsFollowing;
    public bool IsFollowingDog;
    public GameObject interactUI;

    [SerializeField] private AudioClip barkSoundClip;
    [SerializeField] private GameObject followObjectPrefab;
    [SerializeField] private BlindBoyMovement blindBoyMovement;
    private GameObject followObject;
    private Rigidbody2D player_rb;
    private float moveDirection;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isGrounded;
    private bool interactZone = false;
    private BlindBoyMovement blindBoy;

    private void Awake()
    {
        player_rb = GetComponent<Rigidbody2D>();
        blindBoy = FindAnyObjectByType<BlindBoyMovement>();

        if (blindBoy != null )
        {
            Debug.LogError("BlindBoyMovement script not found on any GameObject in the scene.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();
    }

    private void FixedUpdate()
    {
        //Check if grounded
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, GroundObject);
        Move();
    }

    private void Move()
    {
        player_rb.velocity = new Vector2(moveDirection * MoveSpeed, player_rb.velocity.y);

        if (isJumping)
        {
            player_rb.AddForce(new Vector2(0f, JumpForce));
            isJumping = false;
        }
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded && !IsFollowingDog)
        {
            if (!blindBoy.isHoldingDog)
            {
                Debug.Log("Jump");
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Bark();
        }

        //if (Input.GetKeyDown(KeyCode.E) && interactZone)
        //{
        //    HoldBoy();
        //}
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Bark()
    {
        SoundFXManager.Instance.PlaySoundFXClip(barkSoundClip, transform, 0.3f);
        IsFollowing = !IsFollowing;

        if (IsFollowing)
        {
            Debug.Log("Start following!");
            SpawnFollowObject();
        }
        else
        {
            Debug.Log("Stop following!");
            DestroyFollowObject();
        }

        Debug.Log("Bark!");
    }

    private void SpawnFollowObject()
    {
        if (followObject == null)
        {
            // Instantiate the follow object at the dog's position
            followObject = Instantiate(followObjectPrefab, transform.position, Quaternion.identity);

            // Set the followObject for the BlindBoyMovement
            //blindBoyMovement.SetFollowObject(followObject);
        }
    }

    private void DestroyFollowObject()
    {
        if(followObject != null)
        {
            Destroy(followObject);
        }
    }

    private void HoldBoy()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlindBoy"))
        {
            interactUI.SetActive(true);
            interactZone = true;
            Debug.Log("Interact");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlindBoy"))
        {
            interactUI.SetActive(false);
            interactZone = false;
            Debug.Log("Left Interact");
        }
    }
}
