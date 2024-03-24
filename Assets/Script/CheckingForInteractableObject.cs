using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingForInteractableObject : MonoBehaviour
{
    //In test
    public LayerMask layerToIgnore;//Specify the layer to ignore

    [SerializeField] private float speed = 20f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private float soundVolume = 0.3f;
    RaycastHit2D hit;

    private AudioSource mySource;
    private Vector3 rayDirection; //Direction of the ray

    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the GameObject
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);

        // Calculate the direction of the ray based on the rotation of the GameObject
        rayDirection = transform.right;

        // Cast a ray in the calculated direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance,-layerToIgnore);

        // Draw the ray
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, rayDirection * distance, Color.white);
            Debug.Log("Did hit");

            // Play sound
            mySource.volume = soundVolume;
            mySource.Play();
        }
        else
        {
            Debug.DrawRay(transform.position, rayDirection * distance, Color.black);
            Debug.Log("Did not hit");
        }
    }
}
