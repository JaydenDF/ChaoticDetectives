using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cursor : MonoBehaviour
{

    private AbstractInput _abstractInput;
    public float _speed = 10;
    public float _lerpSpeed = 10;
    private Collider2D _targetCollider = null;
    private Rigidbody2D _rigidbody;

    private void OnEnable() {
        _abstractInput.OnClick += OnClick;
    }

    private void OnDisable() {
        _abstractInput.OnClick -= OnClick;
    }
    private void Awake()
    {
        _abstractInput = GetComponent<AbstractInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnClick()
    {
        if (_targetCollider == null) return;

        IInteractable interactable = _targetCollider.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnClick();
        }
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(_abstractInput.GetHorizontalInput(), _abstractInput.GetVerticalInput());
        Vector2 targetVelocity = input * _speed;
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, targetVelocity, Time.fixedDeltaTime * _lerpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnHoverEnter();
            _targetCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnHoverExit();
            _targetCollider = null;
        }
    }
}
