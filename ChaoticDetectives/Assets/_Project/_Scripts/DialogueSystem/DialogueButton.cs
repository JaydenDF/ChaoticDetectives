using System;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Image = UnityEngine.UI.Image;
public class DialogueButton : MonoBehaviour
{
    public static Action<int> OnClickIndex;
    public static Action<string> OnClickString;


    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI responseTextComponent;
    private int responseIndex;
    private string responseText;

    [Header("Sprite")]
    [SerializeField] Sprite[] _possibleSprites;
    private Image _image;

    private void Awake() {
        _image = GetComponent<Image>();
    }

    public void SetResponseText(string text)
    {
        responseText = text;
        responseTextComponent.text = text;
    }
    public void SetResponseIndex(int index)
    {
        responseIndex = index;
        if (index < _possibleSprites.Length)
            SetSpriteFromIndex(index);
        else
        {
            SetRandomSprite();
        }
    }

    public void Clicked()
    {
        OnClickIndex?.Invoke(responseIndex);
        OnClickString?.Invoke(responseText);
    }
    private void SetSpriteFromIndex(int index)
    {
        _image.sprite = _possibleSprites[index];
    }

    private void SetRandomSprite()
    {
        _image.sprite = _possibleSprites[UnityEngine.Random.Range(0, _possibleSprites.Length)];
    }
}