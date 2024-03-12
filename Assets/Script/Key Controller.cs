using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public bool IsHoldingKey;

    [SerializeField] private GameObject dog;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckHoldingKey();
    }

    public void HoldKey()
    {
        IsHoldingKey = !IsHoldingKey;
    }

    private void CheckHoldingKey()
    {
        if (IsHoldingKey)
        {
            HoldKeyLogic();
            Debug.Log("Holding key!");
        }
        else if (!IsHoldingKey)
        {
            ReleaseKey();
            Debug.Log("Is not holding key!");
        }
    }
    private void HoldKeyLogic()
    {
        // Make the blind boy a child of the dog
        transform.SetParent(dog.transform);

        // Set the local position relative to the dog's position
        transform.localPosition = new Vector3(1.39f, 0.8f, 0f);

        // Disable boy's movement (assuming Rigidbody2D is used)
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void ReleaseKey()
    {
        transform.parent = null;
    }

}
