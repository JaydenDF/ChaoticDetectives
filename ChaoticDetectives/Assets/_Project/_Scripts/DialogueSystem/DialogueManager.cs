using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static Action OnDialogueEnd;

    private DialogueContainer _dialogueContainer;
    private DialogueStep _currentDialogueStep;

    [Space(10)]
    [Header("UI Elements")]
    [SerializeField] private GameObject _wholeDialogueParent;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Tooltip("The container for the dialogue buttons, basically the parent object for the buttons")]
    [SerializeField] private GameObject _dialogueButtonContainer;
    [SerializeField] private GameObject _dialogueButtonPrefab;

    private AbstractInput _input;
    private GameObject _selectedButton = null;

    private void OnEnable()
    {
        DialogueButton.OnClickIndex += OnResponseClicked;
        _input.OnDirectionclamped += ChangeSelectedButton;
        _input.OnClickDown += ClickSelected;
        DialogueStarterInteractable.OnDialogueStart += OnDialogueStart;
    }


    private void OnDisable()
    {
        DialogueButton.OnClickIndex -= OnResponseClicked;
        _input.OnDirectionclamped -= ChangeSelectedButton;
        _input.OnClickDown -= ClickSelected;
        DialogueStarterInteractable.OnDialogueStart -= OnDialogueStart;
    }

    private void Awake()
    {
        _input = GetComponent<AbstractInput>();
    }
    private void Start()
    {
        _wholeDialogueParent.SetActive(false);
    }

    private void OnDialogueStart(DialogueContainer dialogueContainer)
    {
        StartCoroutine(StartDialogueNextFrame(dialogueContainer));
    }
    private void StartDialogue(DialogueContainer newDialogue)
    {
        _dialogueContainer = newDialogue;
        _wholeDialogueParent.SetActive(true);
        NewDialogueStep(_dialogueContainer.GetStartingStep());
    }

    private IEnumerator StartDialogueNextFrame(DialogueContainer newDialogue)
    {
        yield return null;
        StartDialogue(newDialogue);
    }

    private void OnResponseClicked(int index)
    {
        if (index == -1)
        {
            StopDialogue();
            return;
        }

        NewDialogueStep(_dialogueContainer.GetNextStepFromIndex(_currentDialogueStep, index));
    }

    private void StopDialogue()
    {
        _dialogueContainer = null;
        _currentDialogueStep = null;
        _dialogueText.text = "";
        foreach (Transform child in _dialogueButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        _wholeDialogueParent.SetActive(false);
        OnDialogueEnd?.Invoke();
        return;
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

        if (dialogueStep.Responses.Length == 0)
        {
            GameObject button = Instantiate(_dialogueButtonPrefab, _dialogueButtonContainer.transform);
            button.GetComponent<DialogueButton>().SetResponseText("End");
            button.GetComponent<DialogueButton>().SetResponseIndex(-1);
        }

        StartCoroutine(HighliteFirstSelectionNextFrame());
    }

    private IEnumerator HighliteFirstSelectionNextFrame()
    {
        yield return null;
        HighliteFirstSelection();
    }

    private void HighliteFirstSelection()
    {
        _selectedButton = _dialogueButtonContainer.transform.GetChild(0).gameObject;
        _selectedButton.GetComponent<IInteractable>().OnHoverEnter();
    }

    private void ChangeSelectedButton(Vector2 vector)
    {
        if (_dialogueButtonContainer.transform.childCount <= 1 || _selectedButton == null)
        {
            return;
        }

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

    private void ClickSelected()
    {
        if (_selectedButton == null)
        {
            return;
        }

        _selectedButton.GetComponent<IInteractable>().OnClick();
        _selectedButton.GetComponent<IInteractable>().OnHoverExit();
        _selectedButton = null;
    }
}
