using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AbstractInput))]
public class CharacterSelection : MonoBehaviour
{
    private List<GameObject> _characters = new List<GameObject>();
    private GameObject _selectedCharacter;
    private Image _selectedCharacterImage => _selectedCharacter.GetComponent<Image>();

    // light blue color
    private static Color _selectedColor = new Color(0.5f, 0.5f, 1f); 
    private AbstractInput _input;
    private void Awake() {
        _input = GetComponent<AbstractInput>();
    }

    private void OnEnable() {
        _input.OnDirectionclamped += ChangeSelectedCharacter;
        _input.OnClickDown += Click;
    }

    private void OnDisable() {
        _input.OnDirectionclamped -= ChangeSelectedCharacter;
        _input.OnClickDown -= Click;
    }

    private void Start() {
        foreach (Transform child in transform) {
            _characters.Add(child.gameObject);
        }

        _selectedCharacter = _characters[0];
        _selectedCharacterImage.color = _selectedColor;
    }

    private void ChangeSelectedCharacter(Vector2 direction) {
        if (direction.x > 0) {
            _selectedCharacterImage.color = Color.white;
            _selectedCharacter = _characters[(_characters.IndexOf(_selectedCharacter) + 1) % _characters.Count];
            _selectedCharacterImage.color = _selectedColor;
        } else if (direction.x < 0) {
            _selectedCharacterImage.color = Color.white;
            _selectedCharacter = _characters[(_characters.IndexOf(_selectedCharacter) - 1 + _characters.Count) % _characters.Count];
            _selectedCharacterImage.color = _selectedColor;
        }
    }

    private void Click() {
        _selectedCharacter.GetComponent<IInteractable>().OnClick();
    }
}