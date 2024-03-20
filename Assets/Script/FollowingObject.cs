using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingObject : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody _rb;
    private GameObject BlindBoy;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        BlindBoy = GameObject.FindGameObjectWithTag("BlindBoy");

        transform.position = new Vector2 (transform.position.x, -4);
    }
    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.position = new Vector2 (transform.position.x, -4);
        }

        if (collision.gameObject.CompareTag("BlindBoy"))
        {
            Destroy(gameObject);
            _playerMovement.IsFollowing = !_playerMovement.IsFollowing;
        }
    }


}
