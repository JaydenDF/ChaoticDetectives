using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIChanceEvent : MonoBehaviour
{
    public static Action OnChanceEventStart;
    public static Action OnChanceEventEnd;

    [Header("UI Elements")]
    [SerializeField] private GameObject _objectToEnable;
    [SerializeField] private TextMeshProUGUI _outcome;
    [SerializeField] private TextMeshProUGUI _modifierText;

    [Space(10)]
    [Header("Animation")]
    [SerializeField] private float _waitTime = 1f;

    private ChanceEvent _chanceEvent;
    private ChanceEventStarter _chanceEventStarter;

    [SerializeField] private GameObject _firstPart;
    [SerializeField] private GameObject _secondPart;

    private void OnEnable()
    {
        ChanceEventStarter.OnChanceEvent += OnChanceEvent;
    }

    private void OnDisable()
    {
        ChanceEventStarter.OnChanceEvent -= OnChanceEvent;
    }

    private void Awake()
    {
        DisableUI();
    }

    private void OnChanceEvent(object sender, ChanceEvent chanceEvent)
    {
        MonologueSystem.Instance.ShowMonologue(chanceEvent.description);
        OnChanceEventStart?.Invoke();
        _objectToEnable.SetActive(true);

        _chanceEvent = chanceEvent;
        if (sender is ChanceEventStarter chanceEventStarter)
        {
            _chanceEventStarter = chanceEventStarter;
        }

        _outcome.color = Color.black;
        _outcome.text = "";
        _modifierText.text = "";
    }

    public void Roll()
    {
        int roll = UnityEngine.Random.Range((int)_chanceEvent.minRollPossible, (int)_chanceEvent.maxRollPossible);
        StartCoroutine(AnimateRoll(roll, _waitTime));
    }

    private IEnumerator AnimateRoll(int roll, float waitTime = 0.5f)
    {
        _outcome.text = roll.ToString();
        yield return new WaitForSeconds(waitTime);
        _modifierText.text = "+" + _chanceEvent.GetModifier().ToString();
        yield return new WaitForSeconds(waitTime / 2);
        _outcome.text = (roll + _chanceEvent.GetModifier()).ToString();
        _outcome.color = Color.blue;
        _modifierText.text = "";
        _chanceEventStarter.OnUIRollled((uint)roll + _chanceEvent.GetModifier());
        yield return new WaitForSeconds(waitTime/2);
        _outcome.color = (roll + _chanceEvent.GetModifier()) > 6 ? Color.green : Color.red;
        yield return new WaitForSeconds(waitTime/2);
        OnChanceEventEnd?.Invoke();
        DisableUI();
    }

    private void DisableUI()
    {
        _outcome.text = "0";
        _chanceEvent = null;
        _chanceEventStarter = null;
        _objectToEnable.SetActive(false);

        _firstPart.SetActive(true);
        _secondPart.SetActive(false);
    }
}