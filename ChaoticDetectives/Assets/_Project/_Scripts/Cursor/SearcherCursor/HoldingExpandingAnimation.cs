using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HoldingExpandingAnimation : MonoBehaviour
{
    [Header("Expanding Object References")]
    [SerializeField] private GameObject _expandingObject;
    [SerializeField] private SpriteRenderer _outlineSpriteRenderer;
    [SerializeField] private float _timeToHold;
    [SerializeField] private AbstractInput _abstractInput;


    [Space(10)]
    [Header("Animation Values")]
    [SerializeField][Range(0, 0.5f)] private float _startAnimationAfter = 0.05f;

    private Vector3 _targetScale;


    private void OnEnable()
    {
        _abstractInput.OnClickDown += OnHold;
        _abstractInput.OnClickUp += OnRelease;
    }

    private void OnDisable()
    {
        _abstractInput.OnClickDown -= OnHold;
        _abstractInput.OnClickUp -= OnRelease;
    }
    private void Start()
    {
        _targetScale = _expandingObject.transform.localScale;
        _expandingObject.transform.localScale = Vector3.zero;
        _outlineSpriteRenderer.enabled = false;
    }
    public void OnHold()
    {
        StartCoroutine(Expand());
    }
    public void OnRelease()
    {
        StopAllCoroutines();
        _expandingObject.transform.localScale = Vector3.zero;
        _outlineSpriteRenderer.enabled = false;
    }

    private IEnumerator Expand()
    {
        yield return new WaitForSeconds(_startAnimationAfter);
        _outlineSpriteRenderer.enabled = true;
        float elapsedTime = 0;
        while (elapsedTime < _timeToHold)
        {
            _expandingObject.transform.localScale = Vector3.Lerp(Vector3.zero, _targetScale, elapsedTime / _timeToHold);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _expandingObject.transform.localScale = _targetScale;
    }
}