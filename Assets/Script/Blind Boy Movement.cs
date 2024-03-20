using UnityEngine;

public class BlindBoyMovement : MonoBehaviour
{
    public bool isHoldingDog = true;

    [SerializeField] private GameObject dog;
    [SerializeField] private float defaultMovingSpeed = 1.5f;
    [SerializeField] private float followingDogSpeed = 15f; // Speed when following dog
    [SerializeField] private float yOffset = 0.5f; // Offset in the y direction
    [SerializeField] private PlayerMovement playerMovement;

    public bool isFollowing = false;
    private float distance;

    private void Start()
    {
    }
    private void Update()
    {
        
        CheckHoldingDog();
    }

    public void HoldDog()
    {
        isHoldingDog = !isHoldingDog;
    }

    private void HoldDogLogic()
    {
        // Make the blind boy a child of the dog
        transform.SetParent(dog.transform);

        // Set the local position relative to the dog's position
        transform.localPosition = new Vector3(-1.19f, 0.8f, 0f);

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
        else if (!isHoldingDog)
        {
            ReleaseDog();
            Debug.Log("Is not holding dog!");
        }
    }


}
