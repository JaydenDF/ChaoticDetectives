using System;
using UnityEngine;

public class UIStats : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _perceptionText;
    [SerializeField] private TMPro.TextMeshProUGUI _creativityText;
    [SerializeField] private TMPro.TextMeshProUGUI _intelligenceText;

    private static string _perception = "Perception: ";
    private static string _creativity = "Creativity: ";
    private static string _intelligence = "Intelligence: ";
    private void OnEnable()
    {
        StatSystem.OnStatsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        StatSystem.OnStatsChanged -= UpdateUI;
    }

    private void UpdateUI(Stat[] obj)
    {
        foreach (var stat in obj)
        {
            switch (stat.statType)
            {
                case StatType.Perception:
                    _perceptionText.text = _perception + stat.value;
                    break;
                case StatType.Creativity:
                    _creativityText.text = _creativity + stat.value;
                    break;
                case StatType.Intelligence:
                    _intelligenceText.text = _intelligence + stat.value;
                    break;
            }
        }
    }
}