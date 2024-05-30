using System;
using System.Collections;
using UnityEngine;

public class DirtyCursorSFM : MonoBehaviour
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private SearcherCursor _searcherCursor;
    [SerializeField] private Cursor _normalCursor;
    private Map _map;

    private void OnEnable()
    {
        if (_map != null)
        {
            _map.OnMapClosed.AddListener(() => StartCoroutine(OnNextFrame(OnMapClosed)));
            _map.OnMapOpened.AddListener(() => StartCoroutine(OnNextFrame(OnMapOpened)));
        }
        DialogueStarterInteractable.OnDialogueStart += container => StartCoroutine(OnNextFrame(() => OnDialogueStart(container)));
        DialogueManager.OnDialogueEnd += () => StartCoroutine(OnNextFrame(OnDialogueEnd));
    }

    private void OnDisable()
    {
        if (_map != null)
        {
            _map.OnMapClosed.RemoveListener(() => StartCoroutine(OnNextFrame(OnMapClosed)));
            _map.OnMapOpened.RemoveListener(() => StartCoroutine(OnNextFrame(OnMapOpened)));
        }
        DialogueStarterInteractable.OnDialogueStart -= container => StartCoroutine(OnNextFrame(() => OnDialogueStart(container)));
        DialogueManager.OnDialogueEnd -= () => StartCoroutine(OnNextFrame(OnDialogueEnd));
    }
    private void Awake() {
        _map = FindObjectOfType<Map>();
    }

    private void Start()
    {
        _cursor.SetActive(true);
        _normalCursor.enabled = false;
        _searcherCursor.enabled = true;
    }

    private IEnumerator OnNextFrame(Action action)
    {
        yield return null;
        yield return null; 
        action();
    }

    private void OnDialogueEnd()
    {
        _cursor.SetActive(true);
    }

    private void OnDialogueStart(DialogueContainer container)
    {
        _cursor.SetActive(false);
    }

    private void OnMapOpened()
    {
        _searcherCursor.StopAllCoroutines();
        _searcherCursor.enabled = false;
        _normalCursor.enabled = true;
    }

    private void OnMapClosed()
    {
        _searcherCursor.enabled = true;
        _normalCursor.enabled = false;
    }
}
