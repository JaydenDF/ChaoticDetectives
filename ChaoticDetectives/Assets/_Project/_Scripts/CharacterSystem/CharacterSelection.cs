using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AbstractInput))]
public class CharacterSelection : MonoBehaviour
{
    private List<GameObject> _characters = new List<GameObject>();
    private GameObject _selectedCharacter;
    private Image _selectedCharacterImage => _selectedCharacter.GetComponent<Image>();

    // a bit of darkness
    private static Color _unselectedColor = new Color(0.8f, 0.8f, 0.8f);
    private static Color _selectedColor = Color.white;
    private static Vector3 _selectedScale = new Vector3(1.2f, 1.2f, 1.2f);


    private bool _canClick = false;
    private AbstractInput _input;
    private void Awake()
    {
        _input = GetComponent<AbstractInput>();
    }

    private void OnEnable()
    {
        _input.OnDirectionclamped += ChangeSelectedCharacter;
        _input.OnClickDown += Click;

        UIChanceEvent.OnChanceEventEnd += Reset;

        StartCoroutine(EnableClick());
    }

    private void Reset()
    {
        _selectedCharacterImage.color = _unselectedColor;
        _selectedCharacter.transform.localScale = Vector3.one;
        _selectedCharacter = _characters[0];
        _selectedCharacterImage.color = _selectedColor;
        _selectedCharacter.transform.localScale = _selectedScale;
    }

    private IEnumerator EnableClick()
    {
        yield return new WaitForSeconds(3f);
        _canClick = true;
    }

    private void OnDisable()
    {
        _input.OnDirectionclamped -= ChangeSelectedCharacter;
        _input.OnClickDown -= Click;
        UIChanceEvent.OnChanceEventEnd -= Reset;
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _characters.Add(child.gameObject);
            _characters[_characters.Count - 1].GetComponent<Image>().color = _unselectedColor;
        }


        _selectedCharacter = _characters[0];
        _selectedCharacterImage.color = _selectedColor;
        _selectedCharacter.transform.localScale = _selectedScale;
    }
    private void ChangeSelectedCharacter(Vector2 direction)
    {
        _selectedCharacterImage.color = _unselectedColor;
        _selectedCharacter.transform.localScale = Vector3.one;
        _selectedCharacter.GetComponent<IInteractable>().OnHoverExit();

        int currentIndex = _characters.IndexOf(_selectedCharacter);
        int newIndex = (currentIndex + (int)Mathf.Sign(direction.x) + _characters.Count) % _characters.Count;

        _selectedCharacter = _characters[newIndex];
        _selectedCharacterImage.color = _selectedColor;
        _selectedCharacter.transform.localScale = _selectedScale;
        _selectedCharacter.GetComponent<IInteractable>().OnHoverEnter();
    }
    private void Click()
    {
        if (!_canClick) return;

        _selectedCharacter.GetComponent<IInteractable>().OnClick();
    }
}