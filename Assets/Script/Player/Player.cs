using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.5f;

    [Header("Bark to follow")]
    private GameObject objectToFollow;
    [SerializeField] private GameObject objectToFollowPrefab;

    [Header("Turn Check")]
    [SerializeField] private GameObject lLeg;
    [SerializeField] private GameObject rLeg;

    [Header("Ground Chcek")]
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Sound")]
    [SerializeField] private AudioClip barkSoundClip;

    [HideInInspector] public bool IsFacingRight;

    [Header("Game Over")]
    public GameObject gameOverCanvas;

    private Rigidbody2D rb;
    private Collider2D coll;
    private BoyInteraction boyInteraction; // Reference to BoyInteraction script 
    private Boy boy;

    private float moveInput;

    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;

    private RaycastHit2D groundHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        StartDirectionCheck();

        // Find and store reference to BoyInteraction script
        boyInteraction = FindFirstObjectByType<BoyInteraction>();
        if (boyInteraction == null)
        {
            Debug.LogError("BoyInteraction script not found.");
        }

        // Find and store reference to Boy script
        boy = FindFirstObjectByType<Boy>();
        if (boy == null)
        {
            Debug.LogError("Boy script not found.");
        }

    }

    private void Update()
    {
        Move();
        Jump();
        Bark();

        // Move the boy towards the target object
        boy.MoveToTarget();
    }

    #region Movement Functions

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        if (moveInput > 0 || moveInput < 0)
        {
            TurnCheck();
        }

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        // If the boy is holding the dog, prevent the player from jumping
        if (boyInteraction != null && boyInteraction.isHoldingDog)
        {
            return;
        }

        //button was just pushed
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {

            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            /*For future when we have the animation*/
            //anim.SetTrigger("jump");
        }

        //button is being held
        if (UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }

            else if(jumpTimeCounter == 0)
            {
                isFalling = true;
                isJumping = false;
            }

            else
            {
                isJumping = false;
            }
        }

        //button was released this frame
        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
            isFalling = true;
        }

        if (!isJumping && CheckForLand())
        {
            /*For future when we have the animation*/
            //anim.SetTrigger("land");
        }

        DrawGroundCheck();
    }

    private void Bark()
    {
        if (UserInput.instance.controls.Interact.BarkToFollow.WasPressedThisFrame())
        {
            SoundFXManager.Instance.PlaySoundFXClip(barkSoundClip, transform, 0.3f, "PlayerSound");

            //If there's already a bark object, destroy it
            if (objectToFollow != null)
            {
                Destroy(objectToFollow);
            }

            //Spawn a new bark object at the dog's position
            objectToFollow = Instantiate(objectToFollowPrefab, transform.position, Quaternion.identity);

            Debug.Log("Bark!");

            // Move the boy towards the new object to follow
            boy.SetTargetTransform(objectToFollow.transform);
        }
    }

    #endregion

    #region Turn Check

    private void StartDirectionCheck()
    {
        if (rLeg.transform.position.x > lLeg.transform.position.x)
        {
            IsFacingRight = true;
        }

        else
        {
            IsFacingRight = false;
        }
    }

    private void TurnCheck()
    {
        if (UserInput.instance.moveInput.x > 0 && !IsFacingRight)
        {
            Turn();
        }

        else if (UserInput.instance.moveInput.x < 0 && IsFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }

        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

    #endregion

    #region Ground/Landed Check

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if (groundHit.collider != null)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private bool CheckForLand()
    {
        if (isFalling)
        {
            if (IsGrounded())
            {
                //Player has landed
                isFalling = false;

                return true;
            }

            else
            {
                return false;
            }
        }

        else
        {
            return false;
        }
    }

    #endregion

    #region Debug Functions

    private void DrawGroundCheck()
    {
        Color rayColor;

        if (IsGrounded())
        {
            rayColor = Color.green;
        }

        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extraHeight), Vector2.right * (coll.bounds.extents.x + 2), rayColor);
    }

    #endregion

    private void OnDestroy()
    {
        EnableObject();
    }

    private void EnableObject()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
}
