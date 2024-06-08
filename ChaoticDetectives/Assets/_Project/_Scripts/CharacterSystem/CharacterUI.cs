using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public CharacterSO characterSO;
    public Image characterSpriteRenderer;
    public TMPro.TextMeshProUGUI characterNameText;
    public TMPro.TextMeshProUGUI[] statTexts;

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

        for (int i = 0; i < characterSO.stats.Length; i++)
        {
            statTexts[i].text = $"{characterSO.stats[i].statType}: {characterSO.stats[i].value}";
        }
    }

    public void NewCharacterSO(CharacterSO newCharacterSO)
    {
        characterSO = newCharacterSO;
        Setup();
    }
}