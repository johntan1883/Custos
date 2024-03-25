using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected GameObject _interactableIndicatorIcon;

    protected bool _isInteractable;

    protected GameObject _player;

    protected CircleCollider2D _circleColl;

    private void Reset()
    {
        _circleColl = GetComponent<CircleCollider2D>();
        _circleColl.isTrigger = true;
        _circleColl.radius = 3.5f;
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject;
        _interactableIndicatorIcon.SetActive(false);
    }

    private void Update()
    {
        if (_isInteractable && UserInput.instance.controls.Interact.Interact.WasPressedThisFrame())
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _interactableIndicatorIcon.SetActive(true);
            _isInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            _interactableIndicatorIcon.SetActive(false);
            _isInteractable = false;
        }
    }

    public abstract void Interact();
}
