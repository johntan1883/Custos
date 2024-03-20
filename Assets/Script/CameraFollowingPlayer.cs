using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour
{
    private Transform Player;
    public Vector3 Offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
            return;

        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
}