using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    public bool PickedUpEndKey;
    private GameObject endGameCanva;
    private GameObject boy;

    private void Awake()
    {
        boy = GameObject.FindGameObjectWithTag("BlindBoy");
        endGameCanva = GameObject.FindGameObjectWithTag("EndCan");
    }

    private void Update()
    {
        if (PickedUpEndKey == true)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {

        Destroy(boy);

        Time.timeScale = 0f;

      

    }
}
