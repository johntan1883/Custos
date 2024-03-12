using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_range : MonoBehaviour
{
    public GameObject interactUI;
    public GameObject interactForBoy;
    public GameObject Player;

    private bool interactZone = false;
    private bool isEKeyDown = false;

    private void Update()
    {
        if (interactZone && Input.GetKeyDown(KeyCode.E))
        {
            isEKeyDown = true;
            interactForBoy.SetActive(true);
        }

        if (isEKeyDown && Input.GetKeyUp(KeyCode.E))
        {
            isEKeyDown = false;
            interactForBoy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactUI.SetActive(true);
            interactZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactUI.SetActive(false);
            interactZone = false;
            if (isEKeyDown)
            {
                isEKeyDown = false;
                interactForBoy.SetActive(false);
            }
        }
    }
}



