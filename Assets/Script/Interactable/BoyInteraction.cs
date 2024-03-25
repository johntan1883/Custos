using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyInteraction : InteractableBase
{
    [SerializeField] private Transform boyHoldingPosition;
    [SerializeField] private GameObject _interactionObject;
    private Boy boy; // Reference to the Boy script


    public bool isHoldingDog;

    private void Start()
    {
        //Find and store reference to the Boy script
        boy = _interactionObject.GetComponent<Boy>();
        if (boy == null)
        {
            Debug.LogError("Boy script not found.");
        }
    }

    public override void Interact()
    {
        if (!isHoldingDog)
        {
            BoyHoldDog();
        }
        else
        {
            BoyLetGoDog();
        }
    }

    private void BoyHoldDog()
    {
        // Set the parent of the boy to the dog
        _interactionObject.transform.parent = _player.transform;

        // Move the boy to the holding position
        _interactionObject.transform.position = boyHoldingPosition.position;

        // Set the flag indicating that the boy is held by the dog
        isHoldingDog = true;

        // Set the target transform for the boy to follow the dog
        boy.SetTargetTransform(_player.transform);

        // Set the indicator's parent to the boy
        _interactableIndicatorIcon.transform.parent = _interactionObject.transform;

        // Ensure the indicator is active
        _interactableIndicatorIcon.SetActive(true);

        // Update the indicator's position to match the boy's position
        _interactableIndicatorIcon.transform.localPosition = Vector3.zero;

        Debug.Log("Boy picked up by dog");
    }

    private void BoyLetGoDog()
    {
        // Set the parent of the boy to null (no longer held by the dog)
        _interactionObject.transform.parent = null;

        // Set the flag indicating that the boy is not held by the dog
        isHoldingDog = false;

        // Clear the target transform (stop following the dog)
        boy.ClearTargetTransform();

        // Reset the indicator's parent
        _interactableIndicatorIcon.transform.parent = null;

        // Set the indicator inactive
        _interactableIndicatorIcon.SetActive(false);

        Debug.Log("Boy put down by dog");
    }
}
