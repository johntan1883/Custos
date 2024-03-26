using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionForBoy : MonoBehaviour, IBoyInteractable
{
    //Requirement : Boy holding the key

    [SerializeField] private GameObject doorOpenSprite;
    [SerializeField] private GameObject doorCloseSprite;
    [SerializeField] private GameObject _blindBoy;
    [SerializeField] private AudioClip  openDoorSFX;

    private GameObject key;

    //Boy interact

    private void Awake()
    {
        doorCloseSprite.SetActive(true);
        
        key = GameObject.FindGameObjectWithTag("Key").gameObject;
        if (key == null)
        {
            Debug.Log("Key is not found in the scene");
        }

        _blindBoy = GameObject.FindGameObjectWithTag("BlindBoy").gameObject;
        if (_blindBoy == null)
        {
            Debug.Log("Boy is not found in the scene");
        }
    }

    public void BoyInteract()
    {
        if (_blindBoy.GetComponent<Boy>().IsHoldingKey)
        {
            doorCloseSprite.SetActive(false);
            doorOpenSprite.SetActive(true);

            SoundFXManager.Instance.PlaySoundFXClip(openDoorSFX, transform, 0.2f, "SFX");

            GameObject Key = GameObject.FindGameObjectWithTag("Key");
            Destroy(Key);
        }
    }
}
