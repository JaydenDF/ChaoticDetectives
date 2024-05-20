using System;
using TMPro;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public static Action<int> OnClickIndex;
    public static Action<string> OnClickString;


    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI responseTextComponent;
    private int responseIndex;
    private string responseText;

    public void SetResponseText(string text)
    {
        responseText = text;
        responseTextComponent.text = text;
    }
    public void SetResponseIndex(int index)
    {
        responseIndex = index;
    }

    public void Clicked()
    {
        OnClickIndex?.Invoke(responseIndex);
        OnClickString?.Invoke(responseText);
    }
}