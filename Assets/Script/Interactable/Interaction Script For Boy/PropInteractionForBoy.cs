using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInteractionForBoy : MonoBehaviour, IBoyInteractable
{
    [SerializeField] private GameObject keyPrefab;
    private bool spawnedKey = false;

    public void BoyInteract()
    {
        if (!spawnedKey && keyPrefab != null) // Check if key is not already spawned and prefab is assigned
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity); // Spawn key at the current position of the door
            spawnedKey = true;
            Debug.Log("Spawn Key");
        }
    }

    
}
