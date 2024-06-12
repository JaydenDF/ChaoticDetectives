using System;
using TMPro;
using UnityEngine;

public class MonologueSystem : MonoBehaviour
{
    public static MonologueSystem Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static Action<String> MonologueText;
    private TextMeshProUGUI _monologueText;

    private void Start()
    {
        _monologueText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void ShowMonologue(string monologue)
    {
        _monologueText.text = monologue;
        MonologueText?.Invoke(monologue);
    }
}