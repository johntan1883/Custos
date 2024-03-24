using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //PUBLIC VARIABLE
    public GameObject interactUI;
    public GameObject barkObjectPrefab;
    public Transform GroundCheck;
    public LayerMask GroundObject;
    public float MoveSpeed;
    public float SprintSpeedMultiplier = 2f;
    public float JumpForce;
    public float CheckRadius;
    public bool IsFollowing;
    public bool IsFollowingDog;

    //PRIVATE VARIABLE
    [SerializeField] private AudioClip barkSoundClip;
    [SerializeField] private AudioClip barkSoundForInteractionClip;
    [SerializeField] private GameObject followObjectPrefab;
    [SerializeField] private BlindBoyMovement blindBoyMovement;
    private GameObject currentBarkObject;
    private GameObject boy;
    private Rigidbody2D player_rb;
    private BlindBoyMovement blindBoy;
    private float moveDirection;
    private bool facingRight = true;
    private bool isJumping = false;
    private bool isGrounded;
    private bool isSprinting = false;


    private void Awake()
    {
        player_rb = GetComponent<Rigidbody2D>();
        blindBoy = FindAnyObjectByType<BlindBoyMovement>();

        if (blindBoy != null )
        {
            Debug.LogError("BlindBoyMovement script not found on any GameObject in the scene.");
        }
    }

    private void Start()
    {
        boy = GameObject.FindWithTag("BlindBoy");
    }
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

    //ALL FUNCTION TO USE IN DOG
    private void Move()
    {
        float currentSpeed = isSprinting ? MoveSpeed * SprintSpeedMultiplier : MoveSpeed;
        player_rb.velocity = new Vector2(moveDirection * currentSpeed, player_rb.velocity.y);

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

        //Sprint Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        //Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded && !IsFollowingDog)
        {
            if (!blindBoy.isHoldingDog)
            {
                Debug.Log("Jump");
                isJumping = true;
            }
        }

        //Bark Input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Bark();
        }

        //Bark Input 2 (to notify the boy to interact)
        if (Input.GetKeyDown(KeyCode.R))
        {
            BarkForInteraction();   
        }

    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void Bark()
    {
        //Play bark sound effect
        SoundFXManager.Instance.PlaySoundFXClip(barkSoundClip, transform, 0.3f);
        IsFollowing = !IsFollowing;

        //If there's already a bark object, destroy it
        if (currentBarkObject != null)
        {
            Destroy(currentBarkObject);
        }

        //Spawn a new bark object at the dog's position
        currentBarkObject = Instantiate(barkObjectPrefab, transform.position, Quaternion.identity);

        //Get the target position for the boy to follow (only x value of bark object)
        Vector3 targetPosition = currentBarkObject.transform.position;
        targetPosition.y = boy.transform.position.y;//keep the same y value as the boy

        //Make the boy follow the target position
        boy.GetComponent<BlindBoyMovement>().FollowTarget(targetPosition);

        Debug.Log("Bark!");
    }
    private void BarkForInteraction()
    {
        //Play bark sound effect
        SoundFXManager.Instance.PlaySoundFXClip(barkSoundForInteractionClip, transform,1f);

        //Notify the boy to check surroundings for interaction
        boy.GetComponent<BlindBoyMovement>().CheckSurroundingsForInteraction();
    }
    private void SpawnFollowObject()
    {
        //if (followObject == null)
        //{
        //    // Instantiate the follow object at the dog's position
        //    followObject = Instantiate(followObjectPrefab, transform.position, Quaternion.identity);
        //}
    }
    private void DestroyFollowObject()
    {
        //if(followObject != null)
        //{
        //    Destroy(followObject);
        //}
    }
}
