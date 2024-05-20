using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Container")]
    [SerializeField] private DialogueContainer _dialogueContainer;
    private DialogueStep _currentDialogueStep;

    [Space(10)]
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Tooltip("The container for the dialogue buttons, basically the parent object for the buttons")]
    [SerializeField] private GameObject _dialogueButtonContainer;
    [SerializeField] private GameObject _dialogueButtonPrefab;

    private AbstractInput _input;
    private GameObject _selectedButton;

    private void OnEnable()
    {
        DialogueButton.OnClickIndex += OnResponseClicked;
        _input.OnDirectionclamped += ChangeSelectedButton;
        _input.OnClick += () => _selectedButton.GetComponent<IInteractable>().OnClick();
    }

    private void OnDisable()
    {
        DialogueButton.OnClickIndex -= OnResponseClicked;
        _input.OnDirectionclamped -= ChangeSelectedButton;
        _input.OnClick -= () => _selectedButton.GetComponent<IInteractable>().OnClick();
    }

    private void Awake()
    {
        _input = GetComponent<AbstractInput>();
    }

    private void Start()
    {
        NewDialogueStep(_dialogueContainer.GetStartingStep());
    }
    private void OnResponseClicked(Index index)
    {
        NewDialogueStep(_dialogueContainer.GetNextStepFromIndex(_currentDialogueStep, index.Value));
    }
    private void NewDialogueStep(DialogueStep dialogueStep)
    {
        _currentDialogueStep = dialogueStep;
        _dialogueText.text = dialogueStep.DialogueText;

        foreach (Transform child in _dialogueButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < dialogueStep.Responses.Length; i++)
        {
            GameObject button = Instantiate(_dialogueButtonPrefab, _dialogueButtonContainer.transform);
            button.GetComponent<DialogueButton>().SetResponseText(dialogueStep.Responses[i]);
            button.GetComponent<DialogueButton>().SetResponseIndex(i);
        }

        // Select the first button by default, using its Iinteractable interface
        _selectedButton = _dialogueButtonContainer.transform.GetChild(0).gameObject;
        _selectedButton.GetComponent<IInteractable>().OnHoverEnter();
    }

    private void ChangeSelectedButton(Vector2 vector)
    {
        int currentIndex = _selectedButton.transform.GetSiblingIndex();
        int newIndex = 0;

        if (vector == Vector2.up)
        {
            newIndex = currentIndex - 1;
        }
        else if (vector == Vector2.down)
        {
            newIndex = currentIndex + 1;
        }

        if (newIndex < 0)
        {
            newIndex = 0;
        }
        else if (newIndex >= _dialogueButtonContainer.transform.childCount)
        {
            newIndex = _dialogueButtonContainer.transform.childCount - 1;
        }

        _selectedButton.GetComponent<IInteractable>().OnHoverExit();
        _selectedButton = _dialogueButtonContainer.transform.GetChild(newIndex).gameObject;
        _selectedButton.GetComponent<IInteractable>().OnHoverEnter();
    }
}
