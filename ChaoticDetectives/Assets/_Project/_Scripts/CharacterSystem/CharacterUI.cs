using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public CharacterSO characterSO;
    public Image characterSpriteRenderer;
    public TMPro.TextMeshProUGUI characterNameText;
    public TMPro.TextMeshProUGUI characterDescriptionText;
    private void OnEnable()
    {
        StatSystem.OnCharacterChanged += NewCharacterSO;
    }
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        characterSpriteRenderer.sprite = characterSO.characterSprite;

        if (characterNameText != null)
        {
            characterNameText.text = characterSO.characterName;
        }

        if (characterDescriptionText != null)
        {
            characterDescriptionText.text = characterSO.characterDescription;
        }
    }

    public void NewCharacterSO(CharacterSO newCharacterSO)
    {
        characterSO = newCharacterSO;
        Setup();
    }
}