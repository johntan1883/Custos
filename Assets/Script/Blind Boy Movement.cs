using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlindBoyMovement : MonoBehaviour
{
    [SerializeField] private GameObject dog;
    [SerializeField] private float defaultMovingSpeed = 1.5f;
    [SerializeField] private float followingDogSpeed = 15f; // Speed when following dog
    [SerializeField] private float yOffset = 0.5f; // Offset in the y direction
    public float fixedZPosition = 5f;

    private PlayerMovement playerMovement;
    private Vector3 targetPosition; // Target position for the boy to move towards.
    public bool isHoldingDog = true;

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

    private void HoldDogLogic()
    {
        // Make the blind boy a child of the dog
        //transform.SetParent(dog.transform);

        Transform target = dog.transform.Find("TargetFollowPos");

        //Debug.Log(target);


        // Set the local position relative to the dog's position
        targetPosition = dog.transform.position;

        // Disable boy's movement (assuming Rigidbody2D is used)
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
