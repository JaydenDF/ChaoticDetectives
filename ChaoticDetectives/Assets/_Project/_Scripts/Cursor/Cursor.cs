using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Cursor : MonoBehaviour
{

    protected AbstractInput _abstractInput;
    public float _speed = 10;
    public float _lerpSpeed = 10;
    protected Collider2D _targetCollider = null;
    protected Rigidbody2D _rigidbody;

    protected void OnEnable()
    {
        _abstractInput.OnClick += OnClick;
    }

    protected void OnDisable()
    {
        _abstractInput.OnClick -= OnClick;
    }
    protected void Awake()
    {
        _abstractInput = GetComponent<AbstractInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    protected void OnClick()
    {
        if (_targetCollider == null) return;

        IInteractable interactable = _targetCollider.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnClick();
        }
    }

    protected void FixedUpdate()
    {
        Vector2 input = new Vector2(_abstractInput.GetHorizontalInput(), _abstractInput.GetVerticalInput());
        Vector2 targetVelocity = input * _speed;
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, targetVelocity, Time.fixedDeltaTime * _lerpSpeed);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnHoverEnter();
            _targetCollider = other;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.OnHoverExit();
            _targetCollider = null;
        }
    }
}

