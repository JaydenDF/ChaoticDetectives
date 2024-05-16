using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearcherCursor : Cursor
{

    [SerializeField] private float _searchRadius = 5f;
    [SerializeField] private float _timeToTravel = 0.5f;
    private bool _selectionMode = false;
    private ConeCollidersHandler _coneCollidersHandler;

    protected new void OnEnable()
    {
        base.OnEnable();
        _abstractInput.OnClick += OnClick;
        _abstractInput.OnDirectionclamped += DirectionSelected;
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _abstractInput.OnClick -= OnClick;
        _abstractInput.OnDirectionclamped -= DirectionSelected;
    }

    protected new void Awake()
    {
        base.Awake();
        _coneCollidersHandler = GetComponentInChildren<ConeCollidersHandler>();
    }

    protected new void FixedUpdate()
    {
        if (_selectionMode == false)
        {
            base.FixedUpdate();
        }
    }


    private void DirectionSelected(Vector2 vector)
    {
        if (_selectionMode == false) return;

        Dictionary<Direction, Collider2D> colliders = _coneCollidersHandler.GetNearestColliderPerDirectionOfComponent<Highlitable>(
            new GameObject[] { _targetCollider.gameObject, this.gameObject }
        );


        if (vector == Vector2.up)
        {
            HandleDirectionSelection(Direction.Up, colliders);
        }
        else if (vector == Vector2.down)
        {
            HandleDirectionSelection(Direction.Down, colliders);
        }
        else if (vector == Vector2.left)
        {
            HandleDirectionSelection(Direction.Right, colliders);
        }
        else if (vector == Vector2.right)
        {
            HandleDirectionSelection(Direction.Left, colliders);
        }
    }

    private void HandleDirectionSelection(Direction direction, Dictionary<Direction, Collider2D> colliders)
    {
        if (colliders == null) return;
        if (colliders.ContainsKey(direction))
        {
            Collider2D selectedCollider = colliders[direction];
            if (selectedCollider != null)
            {
                StartCoroutine(TravelToTarget(selectedCollider.transform.position, _timeToTravel));
            }
        }
    }


    protected new void OnClick()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _searchRadius);
        foreach (Collider2D collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            Highlitable highlitable = collider.GetComponent<Highlitable>();
            if (interactable != null && highlitable != null)
            {
                _selectionMode = true;
                StartCoroutine(TravelToTarget(collider.transform.position, _timeToTravel));
                return;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }

    private IEnumerator TravelToTarget(Vector2 target, float timeToTravel)
    {
        _selectionMode = false;
        float elapsedTime = 0;
        Vector2 startingPos = transform.position;
        while (elapsedTime < timeToTravel)
        {
            _rigidbody.MovePosition(Vector2.Lerp(startingPos, target, (elapsedTime / timeToTravel)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _rigidbody.velocity = Vector2.zero;
        _selectionMode = true;
    }
}