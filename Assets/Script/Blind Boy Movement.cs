using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlindBoyMovement : MonoBehaviour
{
    //Public Variable
    public float fixedZPosition = 5f;
    public bool isHoldingDog = true;
    public bool isHoldingKey = true;

    [SerializeField] private GameObject dog;
    [SerializeField] private float defaultMovingSpeed = 1.5f;
    [SerializeField] private float followingDogSpeed = 15f; // Speed when following dog
    [SerializeField] private float yOffset = 0.5f; // Offset in the y direction
    private PlayerMovement playerMovement;
    private Vector3 targetPosition; // Target position for the boy to move towards.
    

    public void FollowTarget(Vector3 target)
    {
        targetPosition = target;
    }
    private void Start()
    {
        playerMovement = dog.GetComponent<PlayerMovement>();

        targetPosition = transform.position;
    }

    private void Update()
    {
        CheckHoldingDog();

        // Move towards the target position only on the x-axis
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), followingDogSpeed * Time.deltaTime);
    }
    
    public void HoldDog()
    {
        isHoldingDog = !isHoldingDog;
    }
    public void CheckSurroundingsForInteraction()
    {
        //Perform raycast to check for nearby interactable objects
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if ( interactable != null )
            {
                //interactable.BoyInteract();
            }
        }
    }
    private void HoldDogLogic()
    {
        //Find the taget to follow on dog
        Transform target = dog.transform.Find("TargetFollowPos");

        // Set the position to follow
        targetPosition = target.position;

        //// Disable boy's movement (assuming Rigidbody2D is used)
        //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private void ReleaseDog()
    {
        transform.parent = null;
    }
    private void CheckHoldingDog() 
    {
        if (isHoldingDog)
        {
            HoldDogLogic();
            Debug.Log("Holding dog!");
        }
        else if(!isHoldingDog)
        {
            ReleaseDog();
            Debug.Log("Is not holding dog!");
        }
    }
}
