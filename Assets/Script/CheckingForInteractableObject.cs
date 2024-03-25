using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingForInteractableObject : MonoBehaviour
{
    public LayerMask layerToIgnore; // Specify the layer to ignore

    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private float raycastDistance = 10f;

    private void FixedUpdate()
    {
        RotateObject();
        CastRay();
    }

    private void RotateObject()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }

    private void CastRay()
    {
        Vector3 rayDirection = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, raycastDistance, ~layerToIgnore);

        Debug.DrawRay(transform.position, rayDirection * raycastDistance, hit.collider != null ? Color.green : Color.white);

        if (hit.collider != null)
        {
            HandleHitObject(hit.collider.gameObject);
        }
    }

    private void HandleHitObject(GameObject hitObject)
    {
        IBoyInteractable boyInteractable = hitObject.GetComponent<IBoyInteractable>();

        if (boyInteractable != null)
        {
            boyInteractable.BoyInteract();
        }
    }
}
