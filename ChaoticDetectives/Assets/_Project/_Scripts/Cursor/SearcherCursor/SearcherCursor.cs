using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CursorSearcher;
using Unity.VisualScripting;
public class SearcherCursor : Cursor
{
    public UnityEvent OnSearch;
    public UnityEvent OnSearchEnd;

    [SerializeField] private float _searchRadius = 5f;
    [SerializeField] private float _timeToTravel = 0.5f;
    [SerializeField][Range(0, 2)] private float _timeToHold = 0.8f;
    private Cursor _otherCursorScript;
    private bool _selectionMode = false;
    private ConeCollidersHandler _coneCollidersHandler;
    private List<Highlitable> _highlitables = new List<Highlitable>();

    private bool _canMove = true;
    private bool _searchingAnimationPlaying = false;
    private Coroutine _searchingCoroutine;

    protected new void OnEnable()
    {
        base.OnEnable();
        _abstractInput.OnClickDown += OnHoldStart;
        _abstractInput.OnClickUp += StopHolding;
        _abstractInput.OnDirectionclamped += DirectionSelected;
        OnClicked += InteractedWithObject;
    }

    protected new void OnDisable()
    {
        base.OnDisable();
        _abstractInput.OnClickDown -= OnHoldStart;
        _abstractInput.OnClickUp -= StopHolding;
        _abstractInput.OnDirectionclamped -= DirectionSelected;
        OnClicked -= InteractedWithObject;

    }
    protected new void Awake()
    {
        base.Awake();
        _otherCursorScript = GetComponent<Cursor>();
        _coneCollidersHandler = GetComponentInChildren<ConeCollidersHandler>();
    }

    protected new void FixedUpdate()
    {
        if (_canMove == false) { return; }
        if (_selectionMode == true)
        {
            if (_targetCollider == null && _highlitables.Count <= 0)
            {
                _selectionMode = false;
            }
            return;
        }

        base.FixedUpdate();
    }

    private void DirectionSelected(Vector2 vector)
    {
        if (_selectionMode == false) return;
        GameObject targetObject = _targetCollider?.gameObject;
        List<GameObject> exclude = new List<GameObject>();

        if (targetObject != null)
        {
            exclude.Add(_targetCollider.gameObject);
            exclude.Add(this.gameObject);
        }
        else
        {
            exclude.Add(this.gameObject);
        }

        foreach (Highlitable highlitable in FindObjectsOfType<Highlitable>())
        {
            if (_highlitables.Contains(highlitable) == false)
            {
                Debug.Log("Adding " + highlitable.gameObject.name + " to exclude list");
                exclude.Add(highlitable.gameObject);
            }
        }

        Dictionary<Direction, Collider2D> colliders = _coneCollidersHandler.GetNearestColliderPerDirectionOfComponent<Highlitable>(exclude.ToArray());


        if (vector == Vector2.up)
        {
            HandleDirectionSelection(Direction.Up, colliders);
        }
        else if (vector == Vector2.down)
        {
            HandleDirectionSelection(Direction.Down, colliders);
        }
        else if (vector == Vector2.right)
        {
            HandleDirectionSelection(Direction.Right, colliders);
        }
        else if (vector == Vector2.left)
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

    private void OnHoldStart()
    {
        _canMove = false;
        _searchingCoroutine = StartCoroutine(ClickIfHoldIsLongEnough());
        _rigidbody.velocity = Vector2.zero;
    }

    private IEnumerator ClickIfHoldIsLongEnough()
    {
        yield return new WaitForSeconds(_timeToHold);
        OnClick();
    }

    protected new void OnClick()
    {
        StartCoroutine(SearchingAnimation());
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _searchRadius);
        foreach (Collider2D collider in colliders)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            Highlitable highlitable = collider.GetComponent<Highlitable>();
            if (interactable != null && highlitable != null)
            {
                _selectionMode = true;
                _highlitables.Add(highlitable);
                if (_selectionMode == true)
                {
                    StartCoroutine(TravelToTarget(collider.transform.position, _timeToTravel, 1f));
                }
            }
        }

        if (_highlitables.Count > 0)
        {
            StartCoroutine(DelayedHighlite(true, 1.25f));
        }
    }

    private IEnumerator DelayedHighlite(bool highlite, float delay)
    {
        _selectionMode = false;
        yield return new WaitForSeconds(delay);
        Highlitable(highlite);
    }
    private void Highlitable(bool highlite)
    {
        foreach (Highlitable highlitable in _highlitables)
        {
            if (highlite)
            {
                highlitable.OnHoverEnter();
            }
            else
            {
                highlitable.OnHoverExit();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _searchRadius);
    }

    private IEnumerator TravelToTarget(Vector2 target, float timeToTravel, float delay = 0)
    {
        _abstractInput.enabled = false;
        yield return new WaitForSeconds(delay);
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
        _abstractInput.enabled = true;
    }
    private IEnumerator SearchingAnimation()
    {
        _searchingAnimationPlaying = true;
        OnSearch?.Invoke();
        yield return new WaitForSeconds(1.20f);
        _searchingAnimationPlaying = false;
        _canMove = true;
        OnSearchEnd?.Invoke();
    }

    private void StopHolding()
    {
        if (_searchingAnimationPlaying == false) { _canMove = true; }
        if (_searchingCoroutine != null)
        {
            StopCoroutine(_searchingCoroutine);
        }
    }

    private void InteractedWithObject()
    {
        if (_highlitables.Count > 0)
        {
            Highlitable(false);
            _highlitables.Clear();
            _selectionMode = false;
        }
    }

    public void OverrideCanMove(bool canMove)
    {
        _canMove = canMove;
    }
}