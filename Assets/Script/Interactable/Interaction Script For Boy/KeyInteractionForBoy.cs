using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractionForBoy : MonoBehaviour,IBoyInteractable
{
    [SerializeField] private Transform holdingPosition;
    [SerializeField] private GameObject _interactionObject;
    [SerializeField] private GameObject boyObject;

    private bool boyPickedUpKey;

    private void Start()
    {
        if (boyObject == null)
        {
            boyObject = GameObject.FindGameObjectWithTag("BlindBoy");
        }

        if (holdingPosition == null)
        {
            holdingPosition = GameObject.FindGameObjectWithTag("BoyHoldingPosition").transform;
        }
    }

    public void BoyInteract()
    {
        if (boyPickedUpKey)
        {
            BoyDropKey();
        }
        else
        {
            BoyGrabKey();
            if (gameObject.GetComponent<GameEnder>() != null)
            {
                gameObject.GetComponent<GameEnder>().PickedUpEndKey = true;
            }
        }
    }

    private void BoyGrabKey()
    {
        // Ensure the player object is set as the parent of the Interaction object
        _interactionObject.transform.parent = boyObject.transform;

        // Ensure the Key object is set as the parent of the Interaction object
        gameObject.transform.parent = _interactionObject.transform;

        // Move the Interaction object (which now includes the Key) to the holding position
        _interactionObject.transform.position = holdingPosition.position;

        //
        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = true;

        boyPickedUpKey = true;

        boyObject.GetComponent<Boy>().IsHoldingKey = boyPickedUpKey;

        Debug.Log("BOY PICK UP KEY");
    }

    private void BoyDropKey()
    {
        // Remove the Interaction object from being a child of the player
        _interactionObject.transform.parent = null;

        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = false;

        boyPickedUpKey = false;

        boyObject.GetComponent<Boy>().IsHoldingKey = boyPickedUpKey;

        Debug.Log("BOY DROP KEY");
    }
}
